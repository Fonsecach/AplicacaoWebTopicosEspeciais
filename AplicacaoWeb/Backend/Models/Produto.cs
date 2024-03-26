using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Produto
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }

        public Produto(string id, string nome, string descricao, decimal preco)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }
    }
}
