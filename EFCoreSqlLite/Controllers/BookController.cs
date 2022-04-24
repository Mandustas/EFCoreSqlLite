using AutoMapper;
using EFCoreSqlLite.Dto;
using EFCoreSqlLite.Infrastructure;
using EFCoreSqlLite.Model;
using EFCoreSqlLite.Model.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EFCoreSqlLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        IMapper _mapper { get; }

        public BookController(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        [HttpPost]
        [Route("createRand")]
        public IActionResult GenerateRandomData()
        {
            var books = GenerateDataHelper.GenerateBooks();

            _bookRepository.CreateBooks(books);
            _bookRepository.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("createbook")]
        public IActionResult CreateBook([FromBody] BookCreateDto bookCreateDto)
        {
            var newBook = new Model.Book();
            _mapper.Map(bookCreateDto, newBook);

            if (bookCreateDto.AuthorsId.Count != 0 && bookCreateDto.AuthorsId != null)
            {
                newBook.Authors = new List<Author>();
                foreach (var id in bookCreateDto.AuthorsId)
                {
                    newBook.Authors.Add(_authorRepository.GetAuthorFromId(id));
                }
            }

            _bookRepository.CreateBook(newBook);
            _bookRepository.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("createauthor")]
        public IActionResult CreateAuthor([FromBody] AuthorCreateDto authorCreateDto)
        {
            var newAuthor = new Model.Author();
            _mapper.Map(authorCreateDto, newAuthor);

            _authorRepository.CreateAuthor(newAuthor);
            _authorRepository.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("bookpublishing")]
        public BookPublishingView GeBookPublishing(int bookId)
        {
            var book = _bookRepository.GetBookFromId(bookId);
            BookPublishingView result = new BookPublishingView();
            if (book != null)
            {
                result = _bookRepository.GetBookPublishing(book);
            }
            return result;
        }

        [HttpGet]
        [Route("topauthors")]
        public List<TopAuthorsByPublishingView> GetTopAuthors(int publishingId)
        {
            List<TopAuthorsByPublishingView> result = new List<TopAuthorsByPublishingView>();
            result = _bookRepository.GetTopAuthorsByPublishing(publishingId);
            return result;
        }
    }
}
