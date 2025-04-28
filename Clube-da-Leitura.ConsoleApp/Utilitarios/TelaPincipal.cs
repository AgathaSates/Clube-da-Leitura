using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloReserva;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.Utilitarios;

class TelaPincipal
{
    public readonly TelaAmigo telaAmigo;
    public readonly TelaCaixa telaCaixa;
    public readonly TelaRevista telaRevista;
    public readonly TelaEmprestimo telaEmprestimo;
    public readonly TelaReserva telaReserva;

    RepositorioReserva repositorioReserva;
    RepositorioAmigo repositorioAmigo;
    RepositorioCaixa repositorioCaixa;
    RepositorioRevista repositorioRevista;
    RepositorioEmprestimo repositorioEmprestimo;
    string opcaoMenu;

    public TelaPincipal()
    {
        repositorioReserva = new RepositorioReserva();
        repositorioAmigo = new RepositorioAmigo();
        repositorioCaixa = new RepositorioCaixa();
        repositorioRevista = new RepositorioRevista();
        repositorioEmprestimo = new RepositorioEmprestimo();

        telaAmigo = new TelaAmigo(repositorioAmigo);
        telaCaixa = new TelaCaixa(repositorioCaixa);
        telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa, telaCaixa);
        telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, telaAmigo, telaRevista);
        telaReserva = new TelaReserva(repositorioAmigo, repositorioReserva, repositorioRevista, repositorioCaixa, telaAmigo, telaRevista);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Title = "Clube da Leitura";


        Console.Clear();
        ColorirTexto.ExibirMensagem("╔════════════════════════════════╗", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║        Clube da Leitura        ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║════════════════════════════════║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ Bem-vindo ao Clube da Leitura! ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║════════════════════════════════║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 1. Gerenciar Amigos.           ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 2. Gerenciar Revistas.         ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 3. Gerenciar Caixas.           ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 4. Gerenciar Empréstimos.      ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 5. Gerenciar Reservas.         ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 6. Sair do Clube.              ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("╚════════════════════════════════╝", ConsoleColor.DarkCyan);
    }

    public ITela ObterTela()
    {
        while (true)
        {
            Console.WriteLine();
            ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
            opcaoMenu = Console.ReadLine()!.Trim();
            switch (opcaoMenu)
            {
                case "1": return telaAmigo;
                case "2": return telaRevista;
                case "3": return telaCaixa;
                case "4": return telaEmprestimo;
                case "5": return telaReserva;
                case "6": ApresentarSairDoClube(); return null;
                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void ApresentarSairDoClube()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║  Obrigado por usar o Clube da Leitura!  ║", ConsoleColor.DarkCyan);
         ColorirTexto.ExibirMensagem("║              Até a próxima!             ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.DarkCyan);
    }
}
