using Books.Model;
using Books.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() => _bookService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;

        }

        [HttpPost]
        public ActionResult<Book> Add(Book book)
        {
            _bookService.Add(book);
            // Return the created book with a 201 status code and a location header
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Book updatedBook)
        {
            var existingBook = _bookService.GetById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Ensure the ID in the URL matches the ID in the updated book object
            if (id != updatedBook.Id)
            {
                return BadRequest();
            }

            _bookService.Update(updatedBook);
            return NoContent(); // 204 No Content
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookService.Delete(id);
            return NoContent(); // 204 No Content
        }
    }
}
