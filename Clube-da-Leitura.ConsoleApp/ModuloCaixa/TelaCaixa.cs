using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;

namespace Clube_da_Leitura.ConsoleApp.ModuloCaixa;

class TelaCaixa
{
    public RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    public void ApresentarMenu()
    {
        while (true)
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
            string opcaoMenu = Console.ReadLine().Trim();

            switch (opcaoMenu)
            {
                case "1": InserirCaixa(); break;

                case "2": EditarCaixa(); break;

                case "3": ExcluirCaixa(); break;

                case "4": VisualizarTodasAsCaixas(true, true); break;

                case "5": return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void InserirCaixa()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Cadastrar Caixa             ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        Caixa novaCaixa = ObterDadosCaixa(true);

        if (NaoConseguiuValidarCaixa(novaCaixa))
        {
            InserirCaixa();
            return;
        }
        string mensagemResultado = repositorioCaixa.Inserir(novaCaixa);

        if (mensagemResultado == ">> (V) Caixa cadastrada com sucesso!")
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);
        else
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);

        Notificador.ApresentarMensagemParaSair();
    }

    public void EditarCaixa()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║              Editar Caixa               ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteCaixas())
            return;

        VisualizarTodasAsCaixas(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID da caixa que deseja editar: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouCaixa(id))
            return;

        Caixa caixaEditada = ObterDadosCaixa(false, id);

        if (NaoConseguiuValidarCaixa(caixaEditada))
        {
            EditarCaixa();
            return;
        }
        bool editou = repositorioCaixa.Editar(id, caixaEditada);

        if (!editou)
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível editar a caixa!", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem(">> (V) Caixa editada com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public void ExcluirCaixa()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Excluir Caixa               ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteCaixas())
            return;

        VisualizarTodasAsCaixas(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID da caixa que deseja excluir: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouCaixa(id))
            return;


        bool excluiu = repositorioCaixa.Excluir(id);
        if (!excluiu)
            ColorirTexto.ExibirMensagem(">> (X) Não é possível excluir a caixa, pois ela possui revistas vinculadas.", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem(">> (V) Caixa excluída com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();

    }

    public void VisualizarTodasAsCaixas(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║       Visualizar todas as Caixas        ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
            Console.WriteLine();
        }

        if (!ExisteCaixas())
            return;

        Console.WriteLine();
        Console.WriteLine("╔═════╦══════════════════════╦════════════╦════════════════════╦═════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-20} ║ {2,-10} ║ {3, -18} ║ {4, -15} ║",
                             "Id", "Etiqueta", "cor", "Dias de empréstimo", "Qts de Revistas");
        Console.WriteLine("║═════╬══════════════════════╬════════════╬════════════════════╬═════════════════║");

        int contador = 0;
        Caixa[] caixas = repositorioCaixa.SelecionarTodos();

        foreach (Caixa caixa in caixas)
        {
            if (caixa != null)
            {
                int quantidadeDeRevistasNaCaixa = repositorioCaixa.ContarRevistasNaCaixa(caixa);

                Console.WriteLine("║{0, -4} ║ {1, -20} ║ {2, -10} ║ {3, -18} ║ {4, -15} ║",
                caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.DiasDeEmprestimoMaximo, quantidadeDeRevistasNaCaixa);

                if (contador < caixas.Length - 1)
                    Console.WriteLine("║═════╬══════════════════════╬════════════╬════════════════════╬═════════════════║");
            }
            contador++;
        }

        Console.WriteLine("╚═════╩══════════════════════╩════════════╩════════════════════╩═════════════════╝");
        Console.WriteLine();

        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public Caixa ObterDadosCaixa(bool criarIdNovo, int idExistente = 0)
    {
        ColorirTexto.ExibirMensagemSemLinha("> Digite a Etiqueta da Caixa: ", ConsoleColor.Yellow);
        string etiqueta = Console.ReadLine().Trim();
        etiqueta = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(etiqueta);

        ColorirTexto.ExibirMensagemSemLinha("> Digite a Cor da Caixa: ", ConsoleColor.Yellow);
        string cor = Console.ReadLine().Trim();
        cor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cor);

        ColorirTexto.ExibirMensagemSemLinha("> Digite os Dias de Empréstimo: ", ConsoleColor.Yellow);
        int diasDeEmprestimo = Validador.DigitouUmNumero();

        if (criarIdNovo)
            return new Caixa(etiqueta, cor, diasDeEmprestimo);

        return new Caixa(idExistente, etiqueta, cor, diasDeEmprestimo);
    }

    public bool ExisteCaixas()
    {
        if (repositorioCaixa.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhuma caixa cadastrada!", ConsoleColor.Red);
            Console.WriteLine();
            Notificador.ApresentarMensagemParaSair();
            return false;
        }
        return true;
    }

    public bool NaoEncontrouCaixa(int id)
    {

        if (repositorioCaixa.SelecionarPorId(id) == null)
        {
            ColorirTexto.ExibirMensagem("(X) Caixa não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
            return true;
        }
        return false;
    }

    public bool NaoConseguiuValidarCaixa(Caixa novaCaixa)
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
}
