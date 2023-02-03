namespace WebApi;

public class WeatherForecast
{
    public int Id { get; set; }

    public string? Summary { get; set; }

    public int GenreId { get; set; }   

    public int PageCount {get; set;}

    public DateTime PublishDate {get; set;}

}
