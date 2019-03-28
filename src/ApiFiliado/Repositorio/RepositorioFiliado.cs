using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFiliado.Models;

namespace ApiFiliado.Repositorio
{
    /// <summary>
    /// Implementação fake do repositório
    /// Simulando consultas em um banco de dados
    /// </summary>
    public class RepositorioFiliado : IRepositorioFiliado
    {
        private static readonly List<Filiado> _filiados = new List<Filiado>
        {
            new Filiado
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Nome = "Filiado A"
            }
        };

        public IQueryable<Filiado> Filiados => _filiados.AsQueryable();

        public Filiado Add(Filiado filiado)
        {
            var add = filiado;
            add.Id = Guid.NewGuid();

            _filiados.Add(add);
            return add;
        }

        public void Delete(Filiado filiado)
        {
            _filiados.Remove(filiado);
        }

        public Filiado Update(Filiado filiado)
        {
            var filiadoUpdate = _filiados.FirstOrDefault(p => p.Id == filiado.Id);

            filiadoUpdate.Nome = filiado.Nome;
            filiadoUpdate.Ativo = filiado.Ativo;

            return filiadoUpdate;
        }
    }
}
