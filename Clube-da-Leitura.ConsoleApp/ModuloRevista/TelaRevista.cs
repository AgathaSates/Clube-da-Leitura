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
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║    Gerenciamento de Revistas   ║");
            Console.WriteLine("║════════════════════════════════║");
            Console.WriteLine("║ 1- Cadastrar Revista.          ║");
            Console.WriteLine("║ 2- Editar Revista.             ║");
            Console.WriteLine("║ 3- Excluir Revista.            ║");
            Console.WriteLine("║ 4- Visualizar todas as Revistas║");
            Console.WriteLine("║ 5- Voltar ao Menu Principal.   ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.Write("> Digite uma opção: ");
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

        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Cadastrar Revista           ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();

        if (ExisteCaixas())
            return;

        Caixa caixaSelecionada = null;

        Revista novaRevista = ObterDadosRevista(ref caixaSelecionada, true);

        if (novaRevista.Validar() != "")
        {
            ApresentarDadosInvalidos(novaRevista);
            InserirRevista();
            return;
        }

        string mensagemResultado = repositorioRevista.Inserir(novaRevista);
        caixaSelecionada.AdicionarRevista(novaRevista);

        if (mensagemResultado == "(V) Revista cadastrada com sucesso!")
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);

        else
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);


        Notificador.ApresentarMensagemParaSair();
    }

    public void EditarRevista()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Editar Revista              ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodasAsRevistas(false, false);

        Console.Write("Digite o Id da Revista que deseja editar: ");

        int id = Validador.DigitouUmNumero();

        Revista revistaExiste = repositorioRevista.SelecionarPorId(id);

        if (revistaExiste == null)
        {
            ColorirTexto.ExibirMensagem("(X) Revista não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();

            EditarRevista();

            return;
        }

        Revista revistaEditada = ObterDadosRevista(ref revistaExiste.Caixa, false, id);

        if (revistaEditada.Validar() != "")
        {
            ApresentarDadosInvalidos(revistaEditada);

            EditarRevista();

            return;
        }

        bool editou = repositorioRevista.Editar(id, revistaEditada);

        if (!editou)
        {
            ColorirTexto.ExibirMensagem("(X) Não foi possível editar a revista!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();

            return;
        }

        ColorirTexto.ExibirMensagem("(V) Revista editada com sucesso!", ConsoleColor.Green);
        Notificador.ApresentarMensagemParaSair();
    }

    public void ExcluirRevista()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Excluir Revista             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodasAsRevistas(false, false);

        Console.Write("Digite o Id da Revista que deseja excluir: ");
        int id = Validador.DigitouUmNumero();

        Revista revista = repositorioRevista.SelecionarPorId(id);
        if (revista == null)
        {
            ColorirTexto.ExibirMensagem("(X) Revista não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
            ExcluirRevista();
            return;
        }

        repositorioRevista.Excluir(revista.Id);
        ColorirTexto.ExibirMensagem("(V) Revista excluída com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public void VisualizarTodasAsRevistas(bool exibirTitulo, bool exibirSair)
    {
        
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║       Visualizar todas as Revistas      ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            Console.WriteLine();
        }

        Revista[] revistas = repositorioRevista.SelecionarTodos();

        if (revistas.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhuma revista cadastrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("╔═════╦═══════════════════════╦═════════════════╦══════════════════════╦══════════════════════╦═════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -15} ║",
                          "Id", "Título", "N° da Edição", "Ano de Publicação", "Status de Emprestimo", "Caixa");
        Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬═════════════════╣");

        int contador = 0;

        foreach (var revista in revistas)
        {
            
            Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -15} ║",
                revista.Id, revista.Titulo, revista.NumeroDaEdicao, revista.AnoDaPublicacao, revista.StatusDeEmprestimo, revista.Caixa.Etiqueta);
            if (contador < revistas.Length - 1)
                Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════╬══════════════════╣");

            contador++;
        }
        Console.WriteLine("╚═════╩═══════════════════════╩═════════════════╩══════════════════════╩══════════════════════╩═════════════════╝");
        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public Revista ObterDadosRevista(ref Caixa caixaSelecionada, bool criarIdNovo, int idExistente = 0)
    {
        Console.Write("Digite o título da revista: ");
        string titulo = Console.ReadLine().Trim();
        titulo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(titulo);

        Console.Write("Digite o número da edição: ");
        int numeroDaEdicao = Validador.DigitouUmNumero();

        Console.Write("Digite o ano de publicação: ");
        int anoDaPublicacao = Validador.DigitouUmNumero();

        Console.WriteLine("> Selecione a caixa que deseja inserir a revista:");
        VisualizarCaixas();
        Console.Write("Digite o Id da Caixa: ");
        int idCaixa = Validador.DigitouUmNumero();

        caixaSelecionada = repositorioCaixa.SelecionarPorId(idCaixa);

        if (criarIdNovo)
            return new Revista(titulo, numeroDaEdicao, anoDaPublicacao, caixaSelecionada);

        return new Revista(idExistente, titulo, numeroDaEdicao, anoDaPublicacao, caixaSelecionada);
    }

    public bool ExisteCaixas()
    {
        if (repositorioCaixa.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("Não há caixas cadastradas. Cadastre uma caixa antes de cadastrar uma revista.", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return true;
        }
        return false;
    }

    public void VisualizarCaixas()
    {
        telaCaixa.VisualizarTodasAsCaixas(false, false);
    }

    public void ApresentarDadosInvalidos(Revista novaRevista)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Revista!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagem(novaRevista.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }
}
