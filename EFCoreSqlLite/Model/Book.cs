using System;
using System.Collections.Generic;

namespace EFCoreSqlLite.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublishingId { get; set; }
        public int Copies { get; set; }
        public Publishing Publishing { get; set; }
        public List<Author> Authors { get; set; }
        public List<AuthorBook> AuthorBook { get; set; }


        public Book()
        {
            Random random = new Random();
            Copies = random.Next(0, 2000);
        }
    }
}
