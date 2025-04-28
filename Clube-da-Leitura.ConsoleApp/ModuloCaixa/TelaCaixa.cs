using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloCaixa;

public class TelaCaixa : TelaBase<Caixa>, ITelaCrud
{
    public RepositorioCaixa repositorioCaixa;
    string opcaoMenu;

    public TelaCaixa(RepositorioCaixa repositorioCaixa) : base("Caixa", repositorioCaixa)
    {
        this.repositorioCaixa = repositorioCaixa;
    }


    public string ApresentarMenu()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔════════════════════════════════╗", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║    Gerenciamento de Caixas     ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║════════════════════════════════║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 1- Cadastrar Caixa.            ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 2- Editar Caixa.               ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 3- Excluir Caixa.              ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 4- Visualizar todas as Caixas. ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 5- Voltar ao Menu Principal.   ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("╚════════════════════════════════╝", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
        return opcaoMenu = Console.ReadLine().Trim();
    }

    public void ExecutarOpcao(string opcao)
    {
        switch (opcao)
        {
            case "1": CadastrarRegistro(1); break;

            case "2": EditarRegistro(2); break;

            case "3": ExcluirRegistro(3); break;

            case "4": VisualizarRegistros(true, true); break;

            case "5": return;

            default: Notificador.ApresentarOpcaoInvalida(); break;
        }
    }
    public override void VisualizarRegistros(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║       Visualizar todas as Caixas        ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
            Console.WriteLine();
        }

        if (!ExisteRegistros())
            return;

        Console.WriteLine();
        Console.WriteLine("╔═════╦══════════════════════╦════════════╦════════════════════╦═════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-20} ║ {2,-10} ║ {3, -18} ║ {4, -15} ║",
                             "Id", "Etiqueta", "cor", "Dias de empréstimo", "Qts de Revistas");
        Console.WriteLine("║═════╬══════════════════════╬════════════╬════════════════════╬═════════════════║");

        int contador = 0;
        List<Caixa> caixas = repositorioCaixa.SelecionarTodosRegistros();

        foreach (Caixa caixa in caixas)
        {
            if (caixa != null)
            {
                int quantidadeDeRevistasNaCaixa = repositorioCaixa.ContarRevistasNaCaixa(caixa);

                Console.WriteLine("║{0, -4} ║ {1, -20} ║ {2, -10} ║ {3, -18} ║ {4, -15} ║",
                caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.DiasDeEmprestimoMaximo, quantidadeDeRevistasNaCaixa);

                if (contador < caixas.Count - 1)
                    Console.WriteLine("║═════╬══════════════════════╬════════════╬════════════════════╬═════════════════║");
            }
            contador++;
        }

        Console.WriteLine("╚═════╩══════════════════════╩════════════╩════════════════════╩═════════════════╝");
        Console.WriteLine();

        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public override Caixa ObterDadosDoRegistro(bool criarIdNovo, int idExistente = 0)
    {
        ColorirTexto.ExibirMensagemSemLinha("> Digite a Etiqueta da Caixa: ", ConsoleColor.Yellow);
        string etiqueta = Console.ReadLine()!.Trim();
        etiqueta = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(etiqueta);

        ColorirTexto.ExibirMensagemSemLinha("> Digite a Cor da Caixa: ", ConsoleColor.Yellow);
        string cor = Console.ReadLine()!.Trim();
        cor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cor);

        ColorirTexto.ExibirMensagemSemLinha("> Digite os Dias de Empréstimo: ", ConsoleColor.Yellow);
        int diasDeEmprestimo = Validador.DigitouUmNumero();

        if (criarIdNovo)
            return new Caixa(etiqueta, cor, diasDeEmprestimo);

        return new Caixa(idExistente, etiqueta, cor, diasDeEmprestimo);
    }

    public override bool NaoConseguiuValidar(Caixa novaCaixa)
    {
        if (novaCaixa.Validar() != "")
        {
            ApresentarDadosInvalidos(novaCaixa);
            return true;
        }
        return false;
    }

    public void ApresentarDadosInvalidos(Caixa novaCaixa)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Caixa!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagem(novaCaixa.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }



    public override void ApresentarTitulo(int numeroDoTitulo)
    {
        switch (numeroDoTitulo)
        {
            case 1:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Cadastrar Caixa             ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            case 2:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║              Editar Caixa               ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            case 3:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Excluir Caixa               ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            default: ColorirTexto.ExibirMensagem(">> Nenhum título encontrado!", ConsoleColor.Red); break;
        }
    }
}
