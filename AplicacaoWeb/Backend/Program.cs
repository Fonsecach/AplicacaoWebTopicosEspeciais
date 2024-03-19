var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

List<Produto> produtos = new List<Produto>()
{
    new Produto("Celular", "Android"),
    new Produto("COmputador", "Vaio"),
    new Produto("Celular", "Iphone"),
    new Produto("Carro", "Corsa")
};

//end points - funcionalidades - retorna dados no formato JSON
app.MapPost("/api/produto", () => "Minha primeira aplicação");
app.MapGet("/api/produto", () => produtos);

app.Run();

public record Produto(string Nome, string Descrição);
