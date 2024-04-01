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

        public DateTime CriadoEm { get; set; }

        public Produto()
        {

        }
        public Produto(string nome, string descricao, decimal preco)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            CriadoEm = DateTime.Now;
        }
    }
}
