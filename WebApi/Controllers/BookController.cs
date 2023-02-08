using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController] // This is a controller that will be used to handle API requests, controller actions will return an Http response
[Route("[controller]s")] // Which controller will meet the requests coming to the WebApi is determined by these route attributes.
//Resource name: Book
public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>(){

        new Book{
            Id =1,
            Title="The Fountainhead",
            GenreId=1,
            PageCount=500,
            PublishDate=DateTime.Now.AddYears(-10),
        },
        new Book{
            Id =2,
            Title="Herland",
            GenreId=2,
            PageCount=250,
            PublishDate=DateTime.Now.AddYears(-10),
        },
        new Book{
            Id =3,
            Title="Dune",
            GenreId=2,
            PageCount=600,
            PublishDate=DateTime.Now.AddYears(-10),
        }

    };

    private readonly ILogger<BookController> _logger;

    // The constructor is used to inject dependencies( like ILogger<BookController>) into the controller
    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<Book> GetBooks()
    {
        var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        return bookList;
    }

    //      //Book?id=3
    //    [HttpGet]
    //     public Book GetById2([FromQuery] string id)
    //     {
    //         var book= BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
    //         return book;
    //     }


    // //Book/3 
    // [HttpGet("{id}")]
    // public Book GetById(int id)
    // {
    //     var book = BookList.Where(book => book.Id == id).SingleOrDefault();
    //     return book;
    // }


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }



    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if (book != null)
        {
            return BadRequest("A book with the same title already exists.");
        }

        BookList.Add(newBook);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        BookList.Remove(book);
        return Ok();
    }


}
