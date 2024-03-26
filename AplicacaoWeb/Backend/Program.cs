using Backend.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


List<Produto> produtos =
[
    new Produto("a12", "Iphone", "15-Pro", 15000.00m),
    new Produto("a13", "Notebook", "Vaio", 2500.00m),
    new Produto("a14", "Notebook", "Dell", 4000.00m),
    new Produto("x97", "PlayStation", "5", 5000.00m)
];

//end points - funcionalidades - retorna dados no formato JSON
app.MapGet("/api/produto/listar", () => produtos);

app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id) =>
{
    //verifica o id, caso positivo vai retornar 200 e o dado, caso contrário retorna 404
    for (int i = 0; i < produtos.Count; i++)
    {
        if (produtos[i].Id == id)
        {
            return Results.Ok(produtos[i]);
        }
    }
    return Results.NotFound();
});
//Fazer o cadastro de um produto na lista
//a) atraves das informações na URL
//b) atraves do corpo da requisição

app.MapPost("/api/produto/cadastro", () => "Minha primeira aplicação");

//Realizar as operações de alterações e remoção da lista

app.Run();

