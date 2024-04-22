using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Registrar o serviço de banco de dados na app
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapGet("/api/produtos", async ([FromServices] AppDataContext contextProdutos) =>
{
    var produtos = await contextProdutos.Produtos.ToListAsync();
    if (produtos.Any())
    {
        return Results.Ok(produtos);
    }
    return Results.NotFound("Nenhum produto registrado");
});

app.MapGet("/api/produto/{id}", async ([FromRoute] string id, [FromServices] AppDataContext context) =>
{
    var produto = await context.Produtos.FindAsync(id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado.");
    }

    return Results.Ok(produto);
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

app.MapPut("/api/produto/{id}", async ([FromRoute] string id, [FromBody] Produto produtoAlterado, [FromServices] AppDataContext contextProdutos) =>
{
    var existingProduto = await contextProdutos.Produtos.FindAsync(id);
    if (existingProduto == null)
    {
        return Results.NotFound("Produto não encontrado.");
    }

    existingProduto.Nome = produtoAlterado.Nome;
    existingProduto.Descricao = produtoAlterado.Descricao;
    existingProduto.Preco = produtoAlterado.Preco;
    existingProduto.Quantidade = produtoAlterado.Quantidade;

    await contextProdutos.SaveChangesAsync();

    return Results.Ok(existingProduto);
});

app.MapDelete("/api/produto/{id}", async ([FromRoute] string id, [FromServices] AppDataContext contextProdutos) =>
{
    var produto = await contextProdutos.Produtos.FindAsync(id);

    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }

    contextProdutos.Produtos.Remove(produto);
    await contextProdutos.SaveChangesAsync();

    return Results.Ok(produto);
});

app.Run();
