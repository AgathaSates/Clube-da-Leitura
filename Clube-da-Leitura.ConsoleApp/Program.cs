using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;
using Clube_da_Leitura.ConsoleApp.Utilitarios;
namespace Clube_da_Leitura.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        RepositorioRevista repositorioRevista = new RepositorioRevista();
        RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);
        TelaRevista telaRevista = new TelaRevista(repositorioRevista);
        TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioCaixa);
        telaPincipal telaPincipal = new telaPincipal();

        while (true) 
        {
            Console.Title = "Clube da Leitura";
            telaPincipal.ApresentarMenuPrincipal();
            string opcaomenu = Console.ReadLine();
            switch (opcaomenu)
            {
                case "1": telaAmigo.ApresentarMenu(); break;

                case "2": telaRevista.ApresentarMenu(); break;

                case "3": telaCaixa.ApresentarMenu(); break;

                case "4": telaEmprestimo.ApresentarMenu(); break;

                case "5": telaPincipal.ApresentarSairDoClube(); return;

                default: telaPincipal.ApresentarOpçaoInvalida(); break;
            }
        }      
    }
}
