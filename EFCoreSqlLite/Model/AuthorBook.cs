using System.ComponentModel.DataAnnotations;

namespace EFCoreSqlLite.Model
{
    public class AuthorBook
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
