using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;

public class TelaRevista : TelaBase<Revista>, ITelaCrud
{
    public RepositorioRevista repositorioRevista;
    public RepositorioCaixa repositorioCaixa;
    public TelaCaixa telaCaixa;
    string opcaoMenu;

    public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaCaixa telaCaixa) : base("Revista", repositorioRevista)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
        this.telaCaixa = telaCaixa;
    }

    public string ApresentarMenu()
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
        return opcaoMenu = Console.ReadLine()!.Trim();
    }
    public void ExecutarOpcao(string opcao)
    {
        switch (opcaoMenu)
        {
            case "1": CadastrarRegistro(1); break;

            case "2": EditarRegistro(2); break;

            case "3": ExcluirRegistro(3); break;

            case "4": VisualizarRegistros(true, true); break;

            case "5": return;

            default: Notificador.ApresentarOpcaoInvalida(); break;
        }
    }
    public void CadastrarRegistro(int numeroDoTitulo)
    {
        ApresentarTitulo(numeroDoTitulo);

        if (!telaCaixa.ExisteRegistros())
            return;

        Revista novaRevista = ObterDadosDoRegistro(true);

        if (NaoConseguiuValidar(novaRevista))
        {
            CadastrarRegistro(numeroDoTitulo);
            return;
        }

        string mensagemResultado = repositorioRevista.CadastrarRegistro(novaRevista);

        if (mensagemResultado == ">> (V) Registro cadastrado com sucesso!")
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);

        else
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);

        Notificador.ApresentarMensagemParaSair();
    }

    public override void ExcluirRegistro(int numeroDoTitulo)
    {
        ApresentarTitulo(numeroDoTitulo);

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);

        ColorirTexto.ExibirMensagem("Digite o Id da Revista que deseja excluir: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouRegistro(id))
            return;


        bool excluiu = repositorioRevista.ExcluirRegistro(id);

        if (!excluiu)
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível excluir a revista, pois ela está emprestada!", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem("(V) Revista excluída com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public override void VisualizarRegistros(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║       Visualizar todas as Revistas      ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
            Console.WriteLine();
        }

        if (!ExisteRegistros())
            return;

        Console.WriteLine("╔═════╦═══════════════════════════╦═════════════════╦══════════════════════╦══════════════════════╦════════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-25} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -18} ║",
                          "Id", "Título", "N° da Edição", "Ano de Publicação", "Status de Emprestimo", "Caixa");
        Console.WriteLine("╠═════╬═══════════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬════════════════════╣");

        int contador = 0;

        List<Revista> revistas = repositorioRevista.SelecionarTodosRegistros();

        foreach (var revista in revistas)
        {

            Console.WriteLine("║{0, -4} ║ {1,-25} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -18} ║",
                revista.Id, revista.Titulo, revista.NumeroDaEdicao, revista.AnoDaPublicacao, revista.StatusDeEmprestimo, revista.Caixa.Etiqueta);
            if (contador < revistas.Count - 1)
                Console.WriteLine("╠═════╬═══════════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬════════════════════╣");

            contador++;
        }
        Console.WriteLine("╚═════╩═══════════════════════════╩═════════════════╩══════════════════════╩══════════════════════╩════════════════════╝");
        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public override Revista ObterDadosDoRegistro(bool criarIdNovo, int idExistente = 0)
    {
        ColorirTexto.ExibirMensagemSemLinha("> Digite o título da revista: ", ConsoleColor.Yellow);
        string titulo = Console.ReadLine().Trim();
        titulo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(titulo);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o número da edição: ", ConsoleColor.Yellow);
        int numeroDaEdicao = Validador.DigitouUmNumero();

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ano de publicação: ", ConsoleColor.Yellow);
        int anoDaPublicacao = Validador.DigitouUmNumero();

        Console.WriteLine();
        ColorirTexto.ExibirMensagem("═════════════════════════════════════════════════════════════════════════════════", ConsoleColor.DarkMagenta);
        ColorirTexto.ExibirMensagem("            >> Selecione a caixa que deseja inserir a revista:", ConsoleColor.DarkMagenta);
        ColorirTexto.ExibirMensagem("═════════════════════════════════════════════════════════════════════════════════", ConsoleColor.DarkMagenta);

        telaCaixa.VisualizarRegistros(false, false);

        ColorirTexto.ExibirMensagemSemLinha(">> Digite o Id da Caixa: ", ConsoleColor.Yellow);
        int idCaixa = Validador.DigitouUmNumero();

        Caixa caixaSelecionada = repositorioCaixa.SelecionarRegistroPorId(idCaixa);

        if (criarIdNovo)
            return new Revista(titulo, numeroDaEdicao, anoDaPublicacao, caixaSelecionada);

        return new Revista(idExistente, titulo, numeroDaEdicao, anoDaPublicacao, caixaSelecionada);
    }

    public override bool NaoEncontrouRegistro(int id)
    {
        if (repositorioRevista.SelecionarRegistroPorId(id) == null)
        {
            ColorirTexto.ExibirMensagem("(X) Revista não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return true;
        }
        return false;
    }

    public override bool NaoConseguiuValidar(Revista novaRevista)
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

    public override void ApresentarTitulo(int numeroDoTitulo)
    {
        switch (numeroDoTitulo)
        {
            case 1:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Cadastrar Revista           ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            case 2:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Editar Revista              ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            case 3:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Excluir Revista             ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            default: ColorirTexto.ExibirMensagem(">> Nenhum título encontrado!", ConsoleColor.Red); break;
        }
    }
}
