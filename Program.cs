var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Ok($"Hello World"));
app.MapGet("/name/{nome}", (string nome) => Results.Ok($"Hello {nome}"));

app.MapPost("/", (User user) => { return Results.Ok(user); });
app.Run();


public class User
{
    public int ID { get; set; }
    public string UserName { get; set; }

    public User(string userName)
    {
        UserName = userName;
    }
}