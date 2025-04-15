using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;

namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;

class TelaRevista
{
    public RepositorioRevista repositorioRevista;
    public RepositorioCaixa repositorioCaixa;
    public TelaCaixa telaCaixa;

    public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaCaixa telaCaixa)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
        this.telaCaixa = telaCaixa;
    }

    public void ApresentarMenu()
    {
        while (true)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔════════════════════════════════╗", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║    Gerenciamento de Revistas   ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║════════════════════════════════║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 1- Cadastrar Revista.          ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 2- Editar Revista.             ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 3- Excluir Revista.            ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 4- Visualizar todas as Revistas║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 5- Voltar ao Menu Principal.   ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("╚════════════════════════════════╝", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
            string opcaoMenu = Console.ReadLine().Trim();

            switch (opcaoMenu)
            {
                case "1": InserirRevista(); break;

                case "2": EditarRevista(); break;

                case "3": ExcluirRevista(); break;

                case "4": VisualizarTodasAsRevistas(true, true); break;

                case "5": return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void InserirRevista()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Cadastrar Revista           ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!telaCaixa.ExisteCaixas())
            return;

        Revista novaRevista = ObterDadosRevista(true);

        if (NaoConseguiuValidarRevista(novaRevista))
            InserirRevista();

        string mensagemResultado = repositorioRevista.Inserir(novaRevista);

        if (mensagemResultado == ">> (V) Revista cadastrada com sucesso!")
        {
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }

        else
        {
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }
 
    }

    public void EditarRevista()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Editar Revista              ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteRevistas())
            return;

        VisualizarTodasAsRevistas(false, false);

        ColorirTexto.ExibirMensagem("> Digite o Id da Revista que deseja editar: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouRevista(id, true, false))
            return;

        Revista revistaEditada = ObterDadosRevista(false, id);

        if (NaoConseguiuValidarRevista(revistaEditada))
            EditarRevista();


        bool editou = repositorioRevista.Editar(id, revistaEditada);

        if (!editou)
        {
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível editar a revista!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }

        else
        {
            ColorirTexto.ExibirMensagem(">> (V) Revista editada com sucesso!", ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }
    }

    public void ExcluirRevista()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Excluir Revista             ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteRevistas())
            return;

        VisualizarTodasAsRevistas(false, false);

        ColorirTexto.ExibirMensagem("Digite o Id da Revista que deseja excluir: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouRevista(id, false, true))
            return;


        bool excluiu = repositorioRevista.Excluir(id);

        if (!excluiu)
        {
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível excluir a revista, pois ela está emprestada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }

        else
        {
            ColorirTexto.ExibirMensagem("(V) Revista excluída com sucesso!", ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }
    }

    public void VisualizarTodasAsRevistas(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║       Visualizar todas as Revistas      ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
            Console.WriteLine();
        }

        if (!ExisteRevistas())
            return;

        Console.WriteLine("╔═════╦═══════════════════════════╦═════════════════╦══════════════════════╦══════════════════════╦════════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-25} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -18} ║",
                          "Id", "Título", "N° da Edição", "Ano de Publicação", "Status de Emprestimo", "Caixa");
        Console.WriteLine("╠═════╬═══════════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬════════════════════╣");

        int contador = 0;

        Revista[] revistas = repositorioRevista.SelecionarTodos();

        foreach (var revista in revistas)
        {

            Console.WriteLine("║{0, -4} ║ {1,-25} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -18} ║",
                revista.Id, revista.Titulo, revista.NumeroDaEdicao, revista.AnoDaPublicacao, revista.StatusDeEmprestimo, revista.Caixa.Etiqueta);
            if (contador < revistas.Length - 1)
                Console.WriteLine("╠═════╬═══════════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬════════════════════╣");

            contador++;
        }
        Console.WriteLine("╚═════╩═══════════════════════════╩═════════════════╩══════════════════════╩══════════════════════╩════════════════════╝");
        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public Revista ObterDadosRevista(bool criarIdNovo, int idExistente = 0)
    {
        ColorirTexto.ExibirMensagemSemLinha("> Digite o título da revista: ", ConsoleColor.Yellow);
        string titulo = Console.ReadLine().Trim();
        titulo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(titulo);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o número da edição: ", ConsoleColor.Yellow);
        int numeroDaEdicao = Validador.DigitouUmNumero();

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ano de publicação: ", ConsoleColor.Yellow);
        int anoDaPublicacao = Validador.DigitouUmNumero();

        Console.WriteLine();
        ColorirTexto.ExibirMensagem("═════════════════════════════════════════════════════════════════════════════════);", ConsoleColor.DarkMagenta);
        ColorirTexto.ExibirMensagem("            >> Selecione a caixa que deseja inserir a revista:", ConsoleColor.DarkMagenta);
        ColorirTexto.ExibirMensagem("═════════════════════════════════════════════════════════════════════════════════);", ConsoleColor.DarkMagenta);

        VisualizarCaixas();

        ColorirTexto.ExibirMensagemSemLinha(">> Digite o Id da Caixa: ", ConsoleColor.Yellow);
        int idCaixa = Validador.DigitouUmNumero();

        Caixa caixaSelecionada = repositorioCaixa.SelecionarPorId(idCaixa);

        if (criarIdNovo)
            return new Revista(titulo, numeroDaEdicao, anoDaPublicacao, caixaSelecionada);

        return new Revista(idExistente, titulo, numeroDaEdicao, anoDaPublicacao, caixaSelecionada);
    }

    public void VisualizarCaixas()
    {
        telaCaixa.VisualizarTodasAsCaixas(false, false);
    }

    public bool NaoEncontrouRevista(int id, bool ehEditar, bool ehExcluir)
    {
        if (repositorioRevista.SelecionarPorId(id) == null)
        {
            ColorirTexto.ExibirMensagem("(X) Revista não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();

            if (ehEditar)
                EditarRevista();

            if (ehExcluir)
                ExcluirRevista();
        }
        return false;
    }

    public bool ExisteRevistas()
    {
        if (repositorioRevista.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Não há revistas cadastradas!", ConsoleColor.Red);
            Console.WriteLine();
            Notificador.ApresentarMensagemParaSair();
            return false;
        }
        return true;
    }

    public bool NaoConseguiuValidarRevista(Revista novaRevista)
    {
        if (novaRevista.Validar() != "")
        {
            ApresentarDadosInvalidos(novaRevista);
            return true;
        }
        return false;
    }

    public void ApresentarDadosInvalidos(Revista novaRevista)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Revista!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagem(novaRevista.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }
}
