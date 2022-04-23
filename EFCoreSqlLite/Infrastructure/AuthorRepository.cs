using EFCoreSqlLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreSqlLite.Infrastructure
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookContext _bookContext;

        public AuthorRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public void CreateAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _bookContext.Add(author);
        }

        public void CreateAuthors(List<Author> author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _bookContext.AddRange(author);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            _bookContext.Remove(author);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _bookContext.Authors.ToList();
        }

        public Author GetAuthorFromId(int id)
        {
            return _bookContext.Authors.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _bookContext.SaveChanges() >= 0;
        }

        public void UpdateAuthor(Author author)
        {

        }

        public void GetTopAuthors(Publishing publishing)
        {
            List<Author> authors = new List<Author>();
        }
    }
}
