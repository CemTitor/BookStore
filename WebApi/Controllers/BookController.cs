using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
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

    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    // [HttpGet]
    // public List<Book> GetBooks()
    // {
    //     var bookList= BookList.OrderBy(x => x.Id).ToList<Book>();
    //     return bookList;
    // }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book= BookList.Where(book => book.Id == id).SingleOrDefault();
        return book;
    }
   
   [HttpGet]
    public Book Get([FromQuery] string id)
    {
        var book= BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        return book;
    }
    

}
