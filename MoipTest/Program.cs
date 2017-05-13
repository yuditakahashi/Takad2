using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MoipTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader rd = new StreamReader(@"D:\Projetos\MoipTest\MoipTest\log.txt");
            var contadorLinha = 0;
            List<Valores> lista = new List<Valores>();
            while (!rd.EndOfStream)
            {
                string linha = rd.ReadLine();
                contadorLinha = contadorLinha + 1;

                if (contadorLinha > 3 && contadorLinha < 10002)
                {
                    //40 = posição inicial da string de URL, tamanho fixo
                    string inicioUrl = linha.Substring(40);
                    var fimUrl = inicioUrl.IndexOf(" response_headers");
                    var url = inicioUrl.Substring(0, (fimUrl - 1));

                    var fimStatus = inicioUrl.IndexOf(" response_status=");

                    //18 = tamanho da string fixo "response_status="
                    int status = Convert.ToInt32(inicioUrl.Substring((fimStatus + 18), 3));

                    lista.Add(new Valores() { url = url, status = status });
                }
            }

            var filtroUrl = lista.GroupBy(x => x.url).Select(y => new {
                urlNome = y.Key, Qtde = y.Count()}).OrderByDescending(w=>w.Qtde).ToList();

            var filtroStatus = lista.GroupBy(x => x.status).Select(y => new {
                StatusNome = y.Key,
                Qtde = y.Count()
            }).OrderByDescending(w => w.Qtde).ToList();


            Console.WriteLine("RESULTADO DO CALCULO DA URL");
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(string.Concat(filtroUrl[i].urlNome, " - ", filtroUrl[i].Qtde));
            }
            Console.WriteLine();
            Console.WriteLine("RESULTADO DO CALCULO DO STATUS");
            Console.WriteLine();
            for (int i = 0; i < filtroStatus.Count(); i++)
            {
                Console.WriteLine(string.Concat(filtroStatus[i].StatusNome, " - ", filtroStatus[i].Qtde));
            }
        }
    }
}
