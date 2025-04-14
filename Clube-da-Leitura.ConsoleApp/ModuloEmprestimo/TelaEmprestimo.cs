using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
class TelaEmprestimo
{
    public RepositorioEmprestimo repositorioEmprestimo;
    public RepositorioAmigo repositorioAmigo;
    public RepositorioRevista repositorioRevista;   
    public TelaAmigo telaAmigo;
    public TelaRevista telaRevista;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, TelaAmigo telaAmigo, TelaRevista telaRevista)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
    }

    internal void ApresentarMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║     Gerenciamento de Empréstimos     ║");
            Console.WriteLine("║══════════════════════════════════════║");
            Console.WriteLine("║ 1- Cadastrar Empréstimo.             ║");
            Console.WriteLine("║ 2- Editar Empréstimo.                ║");
            Console.WriteLine("║ 3- Excluir Empréstimo.               ║");
            Console.WriteLine("║ 4- Visualizar todos os Empréstimos.  ║");
            Console.WriteLine("║ 5- Visualizar Revistas.              ║");
            Console.WriteLine("║ 6- Visualizar Amigos.                ║");
            Console.WriteLine("║ 7- Registrar Devolução.              ║");
            Console.WriteLine("║ 8- Voltar ao Menu Principal.         ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("> Digite uma opção: ");
            string opcaoMenu = Console.ReadLine().Trim();

            switch (opcaoMenu)
            {
               case "1": InserirEmprestimo(); break;

                case "2": EditarEmprestimo(); break;

                case "3": ExcluirEmprestimo(); break;

                case "4": VisualizarTodosOsEmprestimos(true, true); break;

                case "5": VisualizarRevistas(); break;

                case "6": VisualizarAmigos(); break;

                case "7": RegistrarDevolucao(); break;

                case "8": return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void InserirEmprestimo()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         Cadastrar Empréstimo         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        if (repositorioAmigo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum amigo cadastrado! Registre um Amigo primeiro!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        telaAmigo.VisualizarTodosOsAmigos(false, false);
        Console.Write("Selecione o amigo:");
        int idAmigo = Validador.DigitouUmNumero();

        Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigoSelecionado == null)
        {
            ColorirTexto.ExibirMensagem("Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();

            return;
        }

        if (repositorioRevista.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhuma revista cadastrada! Registre uma Revista primeiro!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }
        
        telaRevista.VisualizarTodasAsRevistas(false, false);
        Console.Write("Selecione a revista:");
        int idRevista = Validador.DigitouUmNumero();
        Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

        if (revistaSelecionada == null)
        {
            ColorirTexto.ExibirMensagem("Revista não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);
        string resultadoValidacao = novoEmprestimo.Validar();

        if (resultadoValidacao != "")
        {
            ColorirTexto.ExibirMensagem(resultadoValidacao, ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        string resultado = repositorioEmprestimo.Inserir(novoEmprestimo);

        if (resultado == "(V) Empréstimo cadastrado com sucesso!")
        {
            ColorirTexto.ExibirMensagem(resultado, ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }

        else
        {
            ColorirTexto.ExibirMensagem(resultado, ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }

       
    }

    public void EditarEmprestimo()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         Editar Empréstimo            ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        if (repositorioEmprestimo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum Empréstimo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("Selecione o Empréstimo:");
        VisualizarTodosOsEmprestimos(false, false);
        int idEmprestimo = Validador.DigitouUmNumero();

        Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            ColorirTexto.ExibirMensagem("Empréstimo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("Selecione o amigo:");
        telaAmigo.VisualizarTodosOsAmigos(false, false);
        int idAmigo = Validador.DigitouUmNumero();

        Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigoSelecionado == null)
        {
            ColorirTexto.ExibirMensagem("Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("Selecione a revista:");
        telaRevista.VisualizarTodasAsRevistas(false, false);
        int idRevista = Validador.DigitouUmNumero();

        Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

        if (revistaSelecionada == null)
        {
            ColorirTexto.ExibirMensagem("Revista não encontrada!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Emprestimo emprestimoEditado = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);

    }

    public void ExcluirEmprestimo()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         Excluir Empréstimo           ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        if (repositorioEmprestimo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum Empréstimo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("Selecione o Empréstimo:");
        VisualizarTodosOsEmprestimos(false, false);
        int idEmprestimo = Validador.DigitouUmNumero();

        Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            ColorirTexto.ExibirMensagem("Empréstimo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        repositorioEmprestimo.Excluir(emprestimoSelecionado.Id);

        ColorirTexto.ExibirMensagem("(V) Empréstimo excluído com sucesso!", ConsoleColor.Green);
        Notificador.ApresentarMensagemParaSair();

    }

    public void VisualizarTodosOsEmprestimos(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   Visualizando todos os Empréstimos  ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        }

        if (repositorioEmprestimo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum Empréstimo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        if (emprestimos.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum Empréstimo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("╔═════╦═══════════════════════╦═════════════════╦══════════════════════╦══════════════════════╦═════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -15} ║",
                          "Id", "Amigo", "Revista", "Data do Empréstimo", "Status de Emprestimo", "Data de Devolução");

        int contador = 0;

        foreach (Emprestimo emprestimo in emprestimos)
        {
            if (emprestimo != null)
            { 
                if (emprestimo.EmprestimoEstaAtrasado(emprestimo))
                    emprestimo.RegistrarAtraso();
            
                Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬═════════════════╣");
                Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -15} ║",
                    emprestimo.Id,
                    emprestimo.amigo.Nome,
                    emprestimo.revista.Titulo,
                    emprestimo.DataIniciodoEmprestimo.ToString("dd/MM/yyyy"),
                    emprestimo.StatusDeEmprestimo,
                    emprestimo.DataDevolucao.ToString("dd/MM/yyyy"));
                contador++;
            }
        }

        Console.WriteLine("╚═════╩═══════════════════════╩═════════════════╩══════════════════════╩══════════════════════╩═════════════════╝");
        
        if (exibirSair)
        {
            Notificador.ApresentarMensagemParaSair();
        }

    }

    public void VisualizarRevistas()
    {
        telaRevista.VisualizarTodasAsRevistas(true, true);
    }

    public void VisualizarAmigos()
    {
        telaAmigo.VisualizarTodosOsAmigos(true, true);
    }

    public void RegistrarDevolucao()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         Registrar Devolução          ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        if (repositorioEmprestimo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum Empréstimo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        VisualizarTodosOsEmprestimos(false, false);

        Console.Write("Selecione o Empréstimo:");
        int idEmprestimo = Validador.DigitouUmNumero();

        Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            ColorirTexto.ExibirMensagem("Empréstimo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        if (emprestimoSelecionado.StatusDeEmprestimo == "Concluído")
        {
            ColorirTexto.ExibirMensagem("Empréstimo já concluído!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        emprestimoSelecionado.RegistrarDevolucao();
        ColorirTexto.ExibirMensagem("(V) Devolução registrada com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }
 
}
