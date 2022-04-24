
using EFCoreSqlLite.Model;
using System;
using System.Collections.Generic;

namespace EFCoreSqlLite.Infrastructure
{
    public static class GenerateDataHelper
    {
        private static readonly Random random = new Random();
        private static string nameStr = "Fidel Joseph Cullen Nehemiah Eduardo Griffin Uri August Pierce Cullen Xanthus Matias Nelson Ismael Milo Adriel Rodrigo Anthony Otis Xeno";
        private static string[] Names = nameStr.Split(" ");
        private static string SecondNamesStr = "Gonzales Thomas Smith Scott Sanders Lee Thomas Wood Simmons Lopez Barnes Griffin Coleman Sanders Thomas Carter Ross Brooks Anderson White";
        private static string[] SecondNames = SecondNamesStr.Split(" ");
        private static string bookStr = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque euismod lobortis dolor, at imperdiet felis gravida id. Aenean sit amet lacinia justo. Etiam leo tortor, pretium sed augue et, imperdiet mattis est. Praesent urna metus, molestie eu justo sed, aliquam luctus lectus. Nunc iaculis congue erat, a congue ex egestas non. Integer lobortis porta enim eget gravida. Ut vitae sem vitae ante blandit efficitur sed id orci. Donec feugiat, est at elementum maximus, urna tortor iaculis nunc, sed gravida erat orci eget tortor. Cras pretium nisl vel ligula molestie ornare. Integer volutpat pharetra massa. Quisque sed eleifend ipsum, ut sodales lacus. Vestibulum condimentum lacus augue, ac fringilla turpis ultricies nec. Vestibulum dapibus porttitor velit. Proin eu posuere tellus.";
        private static string[] Books = bookStr.Split(" ");

        public static List<Author> GenerateAuthors()
        {
            List<Author> authors = new List<Author>();
            for (int i = 0; i < 500; i++)
            {
                authors.Add(new Author { Name = GetAuthorName() });
            }
            return authors;
        }

        public static List<Book> GenerateBooks()
        {
            List<Publishing> publishingList = new List<Publishing> {
                new Publishing { Name = "Dialectica"},
                new Publishing { Name = "Piter"},
                new Publishing { Name = "Eksmo"}
            };

            List<Author> authors = GenerateAuthors();
            List<Book> books = new List<Book>();
            for (int i = 0; i < 1000; i++)
            {
                List<Author> authorsOfBook = new List<Author>();
                int authorCount = random.Next(1, 5);
                List<int> blockedAuthorsNums = new List<int>();
                for (int j = 0; j < authorCount; j++)
                {
                    int authorPos = random.Next(0, authors.Count);
                    while (true)
                    {
                        if (blockedAuthorsNums.Contains(authorPos))
                        {
                            authorPos = random.Next(0, authors.Count);
                        }
                        else
                        {
                            break;
                        }
                    }
                    authorsOfBook.Add(authors[authorPos]);
                }


                books.Add(new Book
                {
                    Name = GetBookName(),
                    Authors = authorsOfBook,
                    Publishing = publishingList[random.Next(0, publishingList.Count)],
                });
            }
            
            return books;
        }

        public static string GetAuthorName()
        {
            int NamePosition = random.Next(0, Names.Length);
            int SecondNamePosition = random.Next(0, SecondNames.Length);

            return Names[NamePosition] + " " + SecondNames[SecondNamePosition];
        }

        public static string GetBookName()
        {

            string result = "";
            Random random = new Random();
            int wordsCount = random.Next(2, 5);
            for (int i = 0; i < wordsCount; i++)
            {
                int NamePosition = random.Next(0, Books.Length);
                result += Books[NamePosition];
                if (i + 1 != wordsCount)
                {
                    result += " ";
                }
            }

            return FirstUpper(result);
        }

        public static string FirstUpper(string str)
        {
            return str.Substring(0, 1).ToUpper() + (str.Length > 1 ? str.Substring(1) : "");
        }
    }
}
