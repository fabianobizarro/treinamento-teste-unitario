using ApiFiliado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFiliado.Repositorio
{
    public interface IRepositorioFiliado
    {
        IQueryable<Filiado> Filiados { get; }
        Filiado Add(Filiado filiado);
        Filiado Update(Filiado filiado);
        void Delete(Filiado filiado);
    }
}
