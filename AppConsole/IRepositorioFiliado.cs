﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsole
{
    public interface IRepositorioFiliado
    {
        IEnumerable<Filiado> ObterPessoas();
    }
}
