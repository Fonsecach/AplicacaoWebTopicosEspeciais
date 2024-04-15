using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class AppDataContext : DbContext
    {
        //EF Code First
        //Informa qual classe vai representar as tabelas do banco de dados
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.sqlite"); // Caminho para o arquivo SQLite - String de conexão
        }
    }
}
