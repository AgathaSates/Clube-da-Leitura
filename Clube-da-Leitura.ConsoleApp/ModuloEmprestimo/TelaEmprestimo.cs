using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
public class TelaEmprestimo : TelaBase<Emprestimo>, ITelaCrud
{
    public RepositorioEmprestimo repositorioEmprestimo;
    public RepositorioAmigo repositorioAmigo;
    public RepositorioRevista repositorioRevista;
    public TelaAmigo telaAmigo;
    public TelaRevista telaRevista;
    string opcaoMenu;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, TelaAmigo telaAmigo, TelaRevista telaRevista) : base("Empréstimo", repositorioEmprestimo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
    }

    public string ApresentarMenu()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║     Gerenciamento de Empréstimos     ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║══════════════════════════════════════║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 1- Cadastrar Empréstimo.             ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 2- Editar Empréstimo.                ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 3- Excluir Empréstimo.               ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 4- Registrar Devolução.              ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 5- Visualizar todos os Empréstimos.  ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 6- Visualizar Multas.                ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 7- Quitar Multas.                    ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("║ 8- Voltar ao Menu Principal.         ║", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.DarkCyan);
        ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
        return opcaoMenu = Console.ReadLine()!.Trim();
    }

    public void ExecutarOpcao(string opcao)
    {
        switch (opcao)
        {
            case "1": CadastrarRegistro(1); break;

            case "2": EditarRegistro(2); break;

            case "3": ExcluirRegistro(3); break;

            case "4": RegistrarDevolucao(); break;

            case "5": VisualizarRegistros(true, true); break;

            case "6": VisualizarMultas(true); break;

            case "7": QuitarMultas(); break;

            case "8": return;

            default: Notificador.ApresentarOpcaoInvalida(); break;
        }
    }

    public override void CadastrarRegistro(int numeroDoTitulo)
    {
        ApresentarTitulo(numeroDoTitulo);

        if (!telaAmigo.ExisteRegistros())
            return;

        if (!telaRevista.ExisteRegistros())
            return;

        Emprestimo novoEmprestimo = ObterDadosDoRegistro(true);

        if (NaoConseguiuValidar(novoEmprestimo))
        {
            CadastrarRegistro(numeroDoTitulo);
            return;
        }

        string mensagemrResultado = repositorioEmprestimo.CadastrarRegistro(novoEmprestimo);

        if (mensagemrResultado == ">> (V) Registro cadastrado com sucesso!")
            ColorirTexto.ExibirMensagem(mensagemrResultado, ConsoleColor.Green);

        else
            ColorirTexto.ExibirMensagem(mensagemrResultado, ConsoleColor.Red);

        Notificador.ApresentarMensagemParaSair();
    }

    public override void EditarRegistro(int numeroDoTitulo)
    {
        ApresentarTitulo(numeroDoTitulo);

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);
        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID do Empréstimo que deseja editar: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouRegistro(id))
            return;

        Emprestimo emprestimoEditado = ObterDadosDoRegistro(false, id);

        if (NaoConseguiuValidar(emprestimoEditado))
        {
            EditarRegistro(numeroDoTitulo);
            return;
        }
        bool editou = repositorioEmprestimo.EditarRegistro(id, emprestimoEditado);

        if (!editou)
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível editar o Empréstimo, pois o amigo possui um empréstimo ativo ", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem(">> (V) Empréstimo editado com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public override void ExcluirRegistro(int numeroDoTitulo)
    {
       ApresentarTitulo(numeroDoTitulo);  

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);
        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id do empréstimo que deseja excluir: ", ConsoleColor.Yellow);
        int idEmprestimo = Validador.DigitouUmNumero();

        if (NaoEncontrouRegistro(idEmprestimo))
            return;

        bool excluiu = repositorioEmprestimo.Excluir(idEmprestimo);

        if (!excluiu)
            ColorirTexto.ExibirMensagem(">> (X) Não é possível excluir o Empréstimo, pois ele não está concluído.", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem("(V) Empréstimo excluído com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public override void VisualizarRegistros(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║   Visualizando todos os Empréstimos  ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
        }

        if (!ExisteRegistros())
            return;

        Console.WriteLine("╔═════╦═══════════════════════╦═════════════════╦══════════════════════╦══════════════════════╦═══════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -15} ║",
                          "Id", "Amigo", "Revista", "Data do Empréstimo", "Status de Emprestimo", "Data de Devolução");
        Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬═══════════════════╣");
        int contador = 0;
        List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();

        foreach (Emprestimo emprestimo in emprestimos)
        {
            if (emprestimo != null)
            {
                emprestimo.EmprestimoEstaAtrasado(emprestimo);

                string linhaCompleta = string.Format(
                    "║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4,-20} ║ {5,-15}   ║",
                    emprestimo.Id,
                    emprestimo.amigo.Nome,
                    emprestimo.revista.Titulo,
                    emprestimo.DataIniciodoEmprestimo.ToString("dd/MM/yyyy"),
                    emprestimo.StatusDeEmprestimo,
                    emprestimo.DataDevolucao.ToString("dd/MM/yyyy")
                );

                if (emprestimo.StatusDeEmprestimo == "Atrasado")
                {
                    ColorirTexto.ExibirMensagem(linhaCompleta, ConsoleColor.Red);
                    if (contador < emprestimos.Count - 1)
                        Console.WriteLine("║═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬═══════════════════║");
                    contador++;
                }
                else
                {
                    Console.WriteLine(linhaCompleta);
                    if (contador < emprestimos.Count - 1)
                        Console.WriteLine("║═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬═══════════════════║");
                    contador++;
                }

                contador++;
            }
        }

        Console.WriteLine("╚═════╩═══════════════════════╩═════════════════╩══════════════════════╩══════════════════════╩═══════════════════╝");

        if (exibirSair)
        {
            Notificador.ApresentarMensagemParaSair();
        }

    }

    public void RegistrarDevolucao()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║         Registrar Devolução          ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id do Empréstimo que deseja registar a devolução: ", ConsoleColor.Yellow);
        int idEmprestimo = Validador.DigitouUmNumero();

        if (NaoEncontrouRegistro(idEmprestimo))
            return;

        Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idEmprestimo);
        if (emprestimoSelecionado.EmprestimoEstaAtrasado(emprestimoSelecionado))
        {
            ColorirTexto.ExibirMensagem("! Você está devolvendo uma revista após o prazo de devolução !", ConsoleColor.Red);
            ColorirTexto.ExibirMensagem($"> Sua multa por atraso é de: ${emprestimoSelecionado.Multa.ValorDaMulta()}.", ConsoleColor.Red);
            ColorirTexto.ExibirMensagem("> Por favor direcione-se ao setor de Multas para quitar o valor pendente após a devolução.", ConsoleColor.Red);
            Thread.Sleep(1000);
        }
        emprestimoSelecionado.RegistrarDevolucao(emprestimoSelecionado.revista);

        ColorirTexto.ExibirMensagem("(V) Devolução registrada com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public void VisualizarMultas(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║          Visualizar Multas           ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
        }

        if (!ExisteMultas())
            return;

        int contador = 0;
        List<Multa> multas = repositorioEmprestimo.SelecionarTodasAsMultas();

        Console.WriteLine("╔═════╦═══════════════════════╦═════════════════╦══════════════════════╦════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -10} ║",
                              "Id", "Revista", "Status", "Data de Devolução", "Valor");
        Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬════════════╣");



        foreach (Multa multa in multas)
        {
            if (multa != null)
                if (multa.EstaPendente())
                {
                    {
                        Console.WriteLine(
                       "║{0, -4} ║ {1, -21} ║ {2, -15} ║ {3, -20} ║ {4, -10} ║",
                       multa.Id, multa.Emprestimo.revista.Titulo, multa.Status, multa.Emprestimo.DataDevolucao, multa.ValorDaMulta());
                        if (contador < multas.Count - 1)
                            Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬════════════╣");
                        contador++;
                    }
                }
        }
        Console.WriteLine("╚═════╩═══════════════════════╩═════════════════╩══════════════════════╩════════════╝");
        Notificador.ApresentarMensagemParaSair();
    }

    public void QuitarMultas()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║          Quitar Multas               ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

        if (!ExisteMultas())
            return;

        VisualizarMultas(false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id da Multa que irá fazer o pagamento: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouMulta(id))
            return;

        Multa multa = repositorioEmprestimo.SelecionarMultaPorId(id);

        multa.Quitar();

        ColorirTexto.ExibirMensagem("> Multa quitada com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public override Emprestimo ObterDadosDoRegistro(bool criarIdNovo, int idExistente = 0)
    {
        telaAmigo.VisualizarRegistros(true, false);
        ColorirTexto.ExibirMensagemSemLinha("> Selecione o amigo: ", ConsoleColor.Yellow);
        int idAmigo = Validador.DigitouUmNumero();

        Amigo amigoSelecionado = repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        telaRevista.VisualizarRegistros(true, false);
        ColorirTexto.ExibirMensagemSemLinha("Selecione a revista: ", ConsoleColor.Yellow);
        int idRevista = Validador.DigitouUmNumero();

        Revista revistaSelecionada = repositorioRevista.SelecionarRegistroPorId(idRevista);

        if (criarIdNovo)
            return new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);

        return new Emprestimo(idExistente, amigoSelecionado, revistaSelecionada, DateTime.Now);
    }

    public bool ExisteMultas()
    {
        if (repositorioEmprestimo.SelecionarTodasAsMultas().Count == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhuma Multa cadastrada !", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return false;
        }
        return true;
    }

    public bool NaoEncontrouMulta(int id)
    {
        if (repositorioEmprestimo.SelecionarMultaPorId(id) == null)
        {
            ColorirTexto.ExibirMensagem("Multa não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return true;
        }
        return false;
    }

    public override bool NaoConseguiuValidar(Emprestimo novoEmprestimo)
    {
        if (novoEmprestimo.Validar() != "")
        {
            ApresentarDadosInvalidos(novoEmprestimo);
            return true;
        }
        return false;
    }

    public void ApresentarDadosInvalidos(Emprestimo novoEmprestimo)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Empréstimo!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagem(novoEmprestimo.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }

    public override void ApresentarTitulo(int numeroDoTitulo)
    {
        switch (numeroDoTitulo)
        {
            case 1:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║         Cadastrar Empréstimo         ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
                break;
            case 2:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║         Editar Empréstimo            ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
                break;
            case 3:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║         Excluir Empréstimo           ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
                break;
            default: ColorirTexto.ExibirMensagem(">> Nenhum título encontrado!", ConsoleColor.Red); break;
        }
    }
}
