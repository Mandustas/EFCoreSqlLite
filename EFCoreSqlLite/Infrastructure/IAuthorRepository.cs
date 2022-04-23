using EFCoreSqlLite.Model;
using System.Collections.Generic;

namespace EFCoreSqlLite.Infrastructure
{
    public interface IAuthorRepository
    {
        bool SaveChanges();
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorFromId(int id);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        void CreateAuthors(List<Author> authors);
        void GetTopAuthors(Publishing publishing);

    }
}
