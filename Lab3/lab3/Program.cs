using lab3.Database;
using lab3.Models;
using lab3.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<DbConnection>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<ServiceConnection>();
builder.Services.AddSingleton(typeof(GetCollection<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Headers.ContentType = new Microsoft.Extensions.Primitives.StringValues("text/html; charset=UTF-8");
    var mainString = new StringBuilder();
    mainString.Append("<div><h1>Welcome</h1><h3><a href=\"/\">Home</a></h3><br/><h3><a href=\"/readers\">About readers</a></h3><br/><h3><a href=\"/books\">About books</a></h3><br/><img src=\"/images/logo.png\"/></div>");
   
    await context.Response.WriteAsync(text: "<html><body>" + mainString.ToString() + "</body></html>");

});

app.MapGet("/readers", async (HttpContext context) =>
{
    context.Response.Headers.ContentType = new Microsoft.Extensions.Primitives.StringValues("text/html; charset=UTF-8");
    var readerString = new StringBuilder();
    
    var readersService = app.Services.GetRequiredService<GetCollection<Reader>>();
    var readers = (await readersService.GetAll()).OrderBy(r => r.Surname);
    readerString.Append("<h3><a href=\"/\">Home</a></h3><br/><h3><a href=\"/readers\">About readers</a></h3><br/><h3><a href=\"/books\">About books</a></h3><br/><h1>All readers </h1><table style = \"width:100%\"><thead><tr><th>ID</th><th>Surname</th><th>Name</th><th>Patronymic name</th><th>Address</th><th>Phone</th><th>Date birth</th><th>ID ticket</th><th>Date start</th><th>Date end</th></tr></thead><tbody>");
     
       foreach(var reader in readers)
       {
        foreach (var ticket in reader.Tickets) {
            readerString.Append($"<tr><td>{reader.Id}</td><td>{reader.Surname}</td><td>{reader.Name}</td><td>{reader.PatronymicName}</td><td>{reader.Address}</td><td>{reader.Phone}</td><td>{reader.DateBirth.ToString("d")}</td><td>{ticket.Id}</td><td>{ticket.DateStart.ToString("d")}</td><td>{ticket.DateEnd.ToString("d")}</td></tr>");
        }
                
       }
    
    await context.Response.WriteAsync(text: "<html><body>" + readerString.ToString() + "</tbody></table></body></html>");

});

app.MapGet("/books", async (HttpContext context) =>
{
    context.Response.Headers.ContentType = new Microsoft.Extensions.Primitives.StringValues("text/html; charset=UTF-8");
    var bookString = new StringBuilder();

    var booksService = app.Services.GetRequiredService<GetCollection<Book>>();
    var books = (await booksService.GetAll()).OrderBy(r => r.Name);
    bookString.Append("<h3><a href=\"/\">Home</a></h3><br/><h3><a href=\"/readers\">About readers</a></h3><br/><h3><a href=\"/books\">About books</a></h3><br/><h1>All books </h1><table style = \"width:100%\"><thead><tr><th>Surname reader</th><th>Name reader</th><th>Patronymic name reader</th><th>Address</th><th>Phone</th><th>Date birth</th><th>ID ticket</th><th>Name book</th><th>Author book</th><th>Price book</th><th>Date take book</th><th>Date return book</th></tr></thead><tbody>");

    foreach (var book in books)
    {
        foreach (var reader in book.Readers)
        {
            foreach (var infoBook in reader.InfoBooks)
            {
                bookString.Append($"<tr><td>{reader.Surname}</td><td>{reader.Name}</td><td>{reader.PatronymicName}</td><td>{reader.Address}</td><td>{reader.Phone}</td><td>{reader.DateBirth.ToString("d")}</td><td>{reader.TicketID}</td><td>{book.Name}</td><td>{book.Author}</td><td>{book.Price}</td><td>{infoBook.DateTakeBook.ToString("d")}</td>");
                if(infoBook.DateReturnBook != null)
                {
                    bookString.Append($"<td>{infoBook.DateReturnBook}</td></tr>");
                }
                else
                {
                    bookString.Append("<td>Project isn't ender</td></tr>");
                }
            }
        }

    }

    await context.Response.WriteAsync(text: "<html><body>" + bookString.ToString() + "</tbody></table></body></html>");

});

app.UseStaticFiles();

app.Run();
