namespace EFCoreSqlLite.Model.Views
{
    public class CoAuthorsView
    {
        public int IdOfFirstCoAuthor { get; set; }
        public string NameOfFirstCoAuthor { get; set; }
        public int IdOfSecondCoAuthor { get; set; }
        public string NameOfSecondCoAuthor { get; set; }
        public int CommonBooks { get; set; }
    }
}
