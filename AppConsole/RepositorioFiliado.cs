using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsole
{
    public class RepositorioFiliado : IRepositorioFiliado
    {
        public IEnumerable<Filiado> ObterPessoas()
        {
            /*
             * Lógica para ir no banco de dados e obter as pessoas
             * 
             */
            return new List<Filiado> { };
        }
    }
}
