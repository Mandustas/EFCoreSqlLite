using EFCoreSqlLite.Model;
using EFCoreSqlLite.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreSqlLite.Infrastructure
{
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

        public BookPublishingView GetBookPublishing(Book book)
        {
            var bookPublishingView = new BookPublishingView();

            bookPublishingView = _bookContext.Books.Select(p => new BookPublishingView
            {
                BookId = p.Id,
                BookName = p.Name,
                PublishingId = p.PublishingId,
                PublishingName = p.Publishing.Name
            }).FirstOrDefault(b => b.BookId == book.Id);

            return bookPublishingView;
        }

    }
}
