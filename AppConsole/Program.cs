using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var processo = new ProcessoCobranca();

                processo.Executar();

                Console.WriteLine("Processo Finalizado");
                Console.ReadKey();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();

            }
        }
    }
}
