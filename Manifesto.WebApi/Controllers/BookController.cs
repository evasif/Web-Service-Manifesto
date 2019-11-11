using Microsoft.AspNetCore.Mvc;
using Manifesto.Models.InputModels;
using Manifesto.Services;
using Manifesto.Models;

namespace Manifesto.WebApi.Controllers
{
    [Route("api/books")]
    public class BookController : Controller
    {
        private readonly BookService _bookService = new BookService();

        [HttpGet] 
        [Route("")]
        public IActionResult GetAllBooks([FromQuery] string category)
        {
            return Ok(_bookService.GetAllBooks(category));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetBookById")]
        public IActionResult GetBookById(int id)
        {
            return Ok(_bookService.GetBookById(id));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateBook([FromBody] BookInputModel book)
        {
            if (!ModelState.IsValid) { return StatusCode(412, book); }
            var id = _bookService.CreateBook(book);
            return CreatedAtRoute("GetBookById", new { id }, null);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookInputModel book)
        {
            if (!ModelState.IsValid) { return StatusCode(412, book); }
            
            _bookService.UpdateBookById(book, id);

            return NoContent();
        }

        public IActionResult DeleteBookById(int id)
        {
            _bookService.DeleteBookById(id);
            return NoContent();
        }
    }
}