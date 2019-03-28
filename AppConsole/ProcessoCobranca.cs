using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsole
{
    public class ProcessoCobranca
    {
        public string[] Filiados { get; private set; }

        public void Executar()
        {
            LerArquivoCobranca();
            EnviarEmailCobranca();
        }

        public void LerArquivoCobranca()
        {
            Console.WriteLine("Lendo filiados do arquivo");
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = "../../arquivo.txt";

            var path = Path.Combine(dir, filePath);

            Filiados = File.ReadAllLines(path);

            Console.WriteLine("Filiados carregados");
        }

        public void CarregarFiliadosDoBanco()
        {
            Console.WriteLine("Carregando filiados do banco");

            IRepositorioFiliado repositorio = _obterRepositorio();

            var resultado = repositorio.ObterPessoas();

            if (resultado == null)
            {
                Filiados = new string[] { };
            }
            else
            {
                var nomes = resultado.Select(p => p.Nome);
                Filiados = nomes.ToArray();
            }
            
            Console.WriteLine("Filiados carregados");
        }

        private IRepositorioFiliado _obterRepositorio()
        {
            return new RepositorioFiliado();
        }

        public void EnviarEmailCobranca()
        {
            Console.WriteLine("Enviando email para filiados");
            var email = new ServicoEmail();
            foreach (var filiado in Filiados)
            {
                email.EnviarEmail(filiado);
                Console.WriteLine($"Email enviado para {filiado}");

            }
            Console.WriteLine("Email enviados");
        }
    }
}
