using EFCoreSqlLite.Model;
using EFCoreSqlLite.Model.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreSqlLite.Infrastructure
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }

    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public void CreateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _bookContext.Add(book);
        }

        public void CreateBooks(List<Book> books)
        {
            if (books == null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            _bookContext.AddRange(books);
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _bookContext.Remove(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookContext.Books.ToList();
        }

        public Book GetBookFromId(int id)
        {
            return _bookContext.Books.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _bookContext.SaveChanges() >= 0;
        }

        public void UpdateBook(Book book)
        {

        }

        public async Task<BookPublishingView> GetBookPublishing(Book book)
        {
            var bookPublishingView = await _bookContext.Books.Select(p => new BookPublishingView
            {
                BookId = p.Id,
                BookName = p.Name,
                PublishingId = p.PublishingId,
                PublishingName = p.Publishing.Name
            }).FirstOrDefaultAsync(b => b.BookId == book.Id);

            return bookPublishingView;
        }

        public async Task<List<TopAuthorsByPublishingView>> GetTopAuthorsByPublishingAsync(int publishingId)
        {
            var topAuthorsByPublishingView = await _bookContext.AuthorBook
                .Join(
                _bookContext.Authors,
                ab => ab.AuthorId,
                a => a.Id,
                (ab, a) => new
                {
                    AuthorId = a.Id,
                    BookId = ab.BookId,
                    Name = a.Name,
                })
                .Join(
                _bookContext.Books,
                ab => ab.BookId,
                b => b.Id,
                (ab, b) => new
                {
                    ab.AuthorId,
                    ab.Name,
                    b.PublishingId
                })
                .Where(b => b.PublishingId == publishingId)
                .GroupBy(t => new
                {
                    t.AuthorId,
                    t.Name,
                })
                .Select(q => new
                {
                    Result = q.Key,
                    Count = q.Count()
                })
                .OrderByDescending(q => q.Count)
                .Take(10)
                .Select(q => new TopAuthorsByPublishingView
                {
                    Name = q.Result.Name,
                    BookCount = q.Count
                }).ToListAsync();

            for (int i = 0; i < topAuthorsByPublishingView.Count; i++)
            {
                topAuthorsByPublishingView[i].Position = i + 1;
            }

            return topAuthorsByPublishingView;
        }

        public async Task<List<CoAuthorsView>> GetCoAuthors()
        {
            var CoAuthors = new List<CoAuthorsView>();

            var query = await _bookContext.AuthorBook
                .Join(
                _bookContext.Authors,
                ab => ab.AuthorId,
                a => a.Id,
                (ab, a) => new
                {
                    AuthorId = a.Id,
                    BookId = ab.BookId,
                    AuthorName = a.Name,
                })
                .ToListAsync();

            var booksIds = query.Select(q => q.BookId).Distinct().ToList();

            List<AuthorDto> authorsDto = new List<AuthorDto>();
            foreach (var id in booksIds)
            {
                authorsDto = await _bookContext.AuthorBook
                .Where(q => q.BookId == id)
                .Join(
                _bookContext.Authors,
                ab => ab.AuthorId,
                a => a.Id,
                (ab, a) => new AuthorDto
                {
                    AuthorId = a.Id,
                    AuthorName = a.Name,
                })
                .ToListAsync();
            }

            //if (authorsDto.Count > 1)
            //{
            //    for (int i = 0; i < authorsDto.Count; i++)
            //    {
            //        for (int j = 1; j < authorsDto.Count; j++)
            //        {
            //            CoAuthors.Add(new CoAuthorsView
            //            {
                            
            //            });
            //        }
            //    }
            //}

            return CoAuthors;
        }

    }
}
