
using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

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
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║    Gerenciamento de Caixas     ║");
            Console.WriteLine("║════════════════════════════════║");
            Console.WriteLine("║ 1- Cadastrar Caixa.            ║");
            Console.WriteLine("║ 2- Editar Caixa.               ║");
            Console.WriteLine("║ 3- Excluir Caixa.              ║");
            Console.WriteLine("║ 4- Visualizar todas as Caixas. ║");
            Console.WriteLine("║ 5- Voltar ao Menu Principal.   ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.Write("> Digite uma opção: ");
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
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Cadastrar Caixa             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        Caixa novaCaixa = ObterDadosCaixa(true);

        if (novaCaixa.Validar() != "")
        {
            ApresentarDadosInvalidos(novaCaixa);

            InserirCaixa();

            return;
        }

        string mensagemResultado = repositorioCaixa.Inserir(novaCaixa);
        if (mensagemResultado == "(V) Caixa cadastrada com sucesso!")
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);

        else
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);

        Notificador.ApresentarMensagemParaSair();
    }

    public void EditarCaixa()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║              Editar Caixa               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodasAsCaixas(false, false);
        Console.WriteLine();

        Console.Write("> Digite o ID da caixa que deseja editar: ");
        int id = Validador.DigitouUmNumero();

        Caixa caixaExiste = repositorioCaixa.SelecionarPorId(id);

        if (caixaExiste == null)
        {
            ColorirTexto.ExibirMensagem("(X) Caixa não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();

            EditarCaixa();

            return;
        }

        Caixa caixaEditada = ObterDadosCaixa(false, id);

        if (caixaEditada.Validar() != "")
        {
            ApresentarDadosInvalidos(caixaEditada);

            EditarCaixa();

            return;
        }

        bool editou = repositorioCaixa.Editar(id, caixaEditada);

        if (!editou)
        {
            ColorirTexto.ExibirMensagem("(X) Não foi possível editar a caixa!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        ColorirTexto.ExibirMensagem("(V) Caixa editada com sucesso!", ConsoleColor.Green);
        Notificador.ApresentarMensagemParaSair();
    }

    public void ExcluirCaixa()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Excluir Caixa               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodasAsCaixas(false, false);

        Console.Write("> Digite o ID da caixa que deseja excluir: ");
        int id = Validador.DigitouUmNumero();

        Caixa caixa = repositorioCaixa.SelecionarPorId(id);

        if (caixa == null)
        {
            ColorirTexto.ExibirMensagem("(X) Caixa não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
            ExcluirCaixa();
            return;
        }


        if (repositorioCaixa.Excluir(caixa.Id))
        {
            ColorirTexto.ExibirMensagem("(X) Não é possível excluir a caixa, pois ela possui revistas vinculadas.", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }    

        ColorirTexto.ExibirMensagem("(V) Caixa excluída com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();

    }

    public void VisualizarTodasAsCaixas(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║       Visualizar todas as Caixas        ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            Console.WriteLine();
        }

        Caixa[] caixas = repositorioCaixa.SelecionarTodos();

        if (caixas.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhuma caixa cadastrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("╔═════╦══════════════╦════════════╦════════════════════╦═════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-12} ║ {2,-10} ║ {3, -17} ║ {4, -14} ║",
                             "Id", "Etiqueta", "cor", "Dias de empréstimo", "Qts de Revistas");
        Console.WriteLine("║═════╬══════════════╬════════════╬════════════════════╬═════════════════║");

        int contador = 0;

        foreach (Caixa caixa in caixas)
        {
            int quantidadeDeRevistasNaCaixa = repositorioCaixa.ContarRevistasNaCaixa(caixa);


            Console.WriteLine("║{0, -4} ║ {1, -12} ║ {2, -10} ║ {3, -18} ║ {4, -15} ║",
            caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.DiasDeEmprestimoMaximo, quantidadeDeRevistasNaCaixa);

            if (contador < caixas.Length -1)
            {
                Console.WriteLine("║═════╬══════════════╬════════════╬════════════════════╬═════════════════║");
            }

            contador++;
        }
        Console.WriteLine("╚═════╩══════════════╩════════════╩════════════════════╩═════════════════╝");

        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public Caixa ObterDadosCaixa(bool criarIdNovo, int idExistente = 0)
    {
        Console.Write("> Digite a Etiqueta da Caixa: ");
        string etiqueta = Console.ReadLine().Trim();
        etiqueta = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(etiqueta);

        Console.Write("> Digite a Cor da Caixa: ");
        string cor = Console.ReadLine().Trim();
        cor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cor);

        Console.Write("> Digite os Dias de Empréstimo: ");
        int diasDeEmprestimo = Validador.DigitouUmNumero();

        if (criarIdNovo)
            return new Caixa(etiqueta, cor, diasDeEmprestimo);

        return new Caixa(idExistente, etiqueta, cor, diasDeEmprestimo);
    }

    public void ApresentarDadosInvalidos(Caixa novaCaixa)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Caixa!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagem(novaCaixa.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }

}
