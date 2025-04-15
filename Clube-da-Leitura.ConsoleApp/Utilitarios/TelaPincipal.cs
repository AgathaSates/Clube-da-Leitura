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


    public TelaPincipal()
    {
        repositorioReserva = new RepositorioReserva();
        repositorioAmigo = new RepositorioAmigo();
        repositorioCaixa = new RepositorioCaixa();
        repositorioRevista = new RepositorioRevista();
        repositorioEmprestimo = new RepositorioEmprestimo();

        telaReserva = new TelaReserva(repositorioReserva, repositorioRevista, repositorioCaixa);
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
            ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
            string opcaomenu = Console.ReadLine().Trim();

            switch (opcaomenu)
            {
                case "1": telaAmigo.ApresentarMenu(); break;

                case "2": telaRevista.ApresentarMenu(); break;

                case "3": telaCaixa.ApresentarMenu(); break;

                case "4": telaEmprestimo.ApresentarMenu(); break;

                case "5": telaReserva.ApresentarMenu(); break;

                case "6": ApresentarSairDoClube(); return;


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
