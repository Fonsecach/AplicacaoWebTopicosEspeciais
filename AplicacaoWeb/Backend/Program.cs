using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//registrar o serviço de banco de dados na app
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

List<Produto> produtos = new List<Produto>
{
    new Produto("Iphone", "15-Pro", 15000.00m, 1),
    new Produto("Notebook", "Vaio", 2500.00m, 5),
    new Produto("Notebook", "Dell", 4000.00m, 0),
    new Produto("PlayStation", "5", 5000.00m, 2)
};

app.MapGet("/api/produtos", async ([FromServices] AppDataContext contextProdutos) =>
{
    var produtos = await contextProdutos.Produtos.ToListAsync();
    return produtos;
});


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

app.MapPost("/api/produto", async ([FromBody] Produto produto, [FromServices] AppDataContext contextProdutos) =>
{
    if (produto == null)
    {
        return Results.BadRequest("O produto enviado é inválido.");
    }

    // Verificar se o ID já existe no banco de dados
    var existingProduto = await contextProdutos.Produtos.FindAsync(produto.Id);
    if (existingProduto != null)
    {
        return Results.BadRequest("Já existe um produto com o mesmo ID.");
    }

    contextProdutos.Produtos.Add(produto);
    await contextProdutos.SaveChangesAsync();

    return Results.Created("", produto);
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
