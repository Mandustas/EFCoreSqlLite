using System.Collections.Generic;

namespace EFCoreSqlLite.Model
{
    public class Publishing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
