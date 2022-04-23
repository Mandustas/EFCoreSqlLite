using System.Collections.Generic;

namespace EFCoreSqlLite.Dto
{
    public class BookCreateDto
    {
        public string Name { get; set; }
        public int PublishingId { get; set; }
        public List<int> AuthorsId { get; set; }
    }
}
