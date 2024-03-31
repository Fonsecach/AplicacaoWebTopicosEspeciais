using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<Produto> produtos = new List<Produto>
{
    new Produto("a12", "Iphone", "15-Pro", 15000.00m),
    new Produto("a13", "Notebook", "Vaio", 2500.00m),
    new Produto("a14", "Notebook", "Dell", 4000.00m),
    new Produto("x97", "PlayStation", "5", 5000.00m)
};

app.MapGet("/api/produtos", () => produtos);

app.MapGet("/api/produto/{id}", ([FromRoute] string id) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);
    if (produto != null)
    {
        return Results.Ok(produto);
    }
    else
    {
        return Results.NotFound("Produto não encontrado.");
    }
});

app.MapPost("/api/produto", ([FromBody] Produto novoProduto) => 
{
    if (novoProduto == null)
    {
        return Results.BadRequest("O produto enviado é inválido.");
    }

    produtos.Add(novoProduto);
    return Results.Ok(novoProduto);
});

app.MapPut("/api/produto/{id}", ([FromRoute] string id, [FromBody] Produto produtoAlterado) =>
{
    var index = produtos.FindIndex(p => p.Id == id);
    if (index != -1)
    {
        produtos[index] = produtoAlterado;
        return Results.Ok(produtoAlterado);
    }
    else
    {
        return Results.NotFound("Produto não encontrado.");
    }
});

app.MapDelete("/api/produto/{id}", ([FromRoute] string id) =>
{
    var produtoARemover = produtos.FirstOrDefault(p => p.Id == id);
    if (produtoARemover != null)
    {
        produtos.Remove(produtoARemover);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound("Produto não encontrado.");
    }
});

app.Run();
