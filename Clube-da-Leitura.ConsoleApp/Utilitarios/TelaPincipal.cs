using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.Utilitarios;

class TelaPincipal
{
    public readonly TelaAmigo telaAmigo;
    public readonly TelaCaixa telaCaixa;
    public readonly TelaRevista telaRevista;
    public readonly TelaEmprestimo telaEmprestimo;

    RepositorioAmigo repositorioAmigo;
    RepositorioCaixa repositorioCaixa;
    RepositorioRevista repositorioRevista;
    RepositorioEmprestimo repositorioEmprestimo;


    public TelaPincipal()
    {
        repositorioAmigo = new RepositorioAmigo();
        repositorioCaixa = new RepositorioCaixa();
        repositorioRevista = new RepositorioRevista();
        repositorioEmprestimo = new RepositorioEmprestimo();


        telaAmigo = new TelaAmigo(repositorioAmigo);
        telaCaixa = new TelaCaixa(repositorioCaixa);
        telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa, telaCaixa);
        telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista,telaAmigo,telaRevista);
    }

    public void ApresentarMenuPrincipal()
    {
        while (true)
        {
            Console.Title = "Clube da Leitura";


            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║        Clube da Leitura        ║");
            Console.WriteLine("║════════════════════════════════║");
            Console.WriteLine("║ Bem-vindo ao Clube da Leitura! ║");
            Console.WriteLine("║════════════════════════════════║");
            Console.WriteLine("║ 1. Gerenciar Amigos.           ║");
            Console.WriteLine("║ 2. Gerenciar Revistas.         ║");
            Console.WriteLine("║ 3. Gerenciar Caixas.           ║");
            Console.WriteLine("║ 4. Gerenciar Empréstimos.      ║");
            Console.WriteLine("║ 5. Sair do Clube.              ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.Write("> Digite uma opção: ");
            string opcaomenu = Console.ReadLine().Trim();

            switch (opcaomenu)
            {
                case "1": telaAmigo.ApresentarMenu(); break;

                case "2": telaRevista.ApresentarMenu(); break;

                case "3": telaCaixa.ApresentarMenu(); break;

                case "4": telaEmprestimo.ApresentarMenu(); break;

                case "5": ApresentarSairDoClube(); return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void ApresentarSairDoClube()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║  Obrigado por usar o Clube da Leitura!  ║");
        Console.WriteLine("║              Até a próxima!             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
    }
}
