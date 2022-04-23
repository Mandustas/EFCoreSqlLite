using EFCoreSqlLite.Model;
using EFCoreSqlLite.Model.Views;
using System.Collections.Generic;

namespace EFCoreSqlLite.Infrastructure
{
    public interface IBookRepository
    {
        bool SaveChanges();
        IEnumerable<Book> GetAllBooks();
        Book GetBookFromId(int id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void CreateBooks(List<Book> books);
        BookPublishingView GetBookPublishing(Book book);
    }
}
