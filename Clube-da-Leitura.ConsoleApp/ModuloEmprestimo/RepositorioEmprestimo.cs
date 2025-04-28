using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
{
    private int contadorIds = 0;

    public override string CadastrarRegistro(Emprestimo novoEmprestimo)
    {

        if (VerificaAmigoTemEmprestimoAtivo(novoEmprestimo.amigo))
            return ">> Amigo já possui um empréstimo ativo.";

        if (VerificaRevistaJaEmprestada(novoEmprestimo.revista))
            return ">> Revista já emprestada.";

        if (VerificaAmigoMultaAtiva(novoEmprestimo.amigo))
            return ">> O Amigo possuí uma multa ativa.";

        if (!VerificaRevistaReservada(novoEmprestimo))
            return $">> A Revista está reservada pelo amigo {novoEmprestimo.revista.Reserva.Amigo.Nome}";

        registros.Add(novoEmprestimo);
        novoEmprestimo.Id = ++contadorIds;
        novoEmprestimo.RegistrarEmprestimo();
        novoEmprestimo.amigo.AdicionarEmprestimo(novoEmprestimo);
        novoEmprestimo.amigo.Reserva.ConcluirReserva();
        return ">> (V) Registro cadastrado com sucesso!";
    }

    public override bool EditarRegistro(int id, Emprestimo novoEmprestimo)
    {
        foreach (Emprestimo emprestimo in registros)
            if (emprestimo != null)
                if (emprestimo.Id == id)
                {
                    if (emprestimo.amigo.VerificaMultaAtiva())
                        return false;
                    if (emprestimo.amigo.VerificaEmprestimoAtivo())
                        return false;
                    emprestimo.AtualizarRegistro(novoEmprestimo);
                    return true;
                }
        return false;
    }

    public bool Excluir(int id)
    {
        Emprestimo emprestimo = SelecionarPorId(id);

        if (emprestimo == null)
            return false;

        if (emprestimo.StatusDeEmprestimo != "Concluído")
            return false;

        registros.Remove(emprestimo);
        return true;

    }

    public List<Emprestimo> SelecionarTodos()
    {
        return registros;
    }

    public List<Multa> SelecionarTodasAsMultas()
    {
        List<Emprestimo> emprestimos = SelecionarTodos();
        List<Multa> multas = new List<Multa>();

        foreach (Emprestimo emprestimo in emprestimos)
        {
            if (emprestimo != null)
            {
                if (emprestimo.Multa != null)
                {
                    multas.Add(emprestimo.Multa);
                }
            }
        }

        return multas;
    }

    public Emprestimo SelecionarPorId(int id)
    {
        foreach (Emprestimo emprestimo in registros)
            if (emprestimo != null)
                if (emprestimo.Id == id)
                    return emprestimo;
        return null;
    }

    public Multa SelecionarMultaPorId(int id)
    {
        List<Multa> multas = SelecionarTodasAsMultas();
        foreach (Multa multa in multas)
            if (multas != null)
                if (multa.Id == id)
                    return multa;
        return null;
    }

    public bool VerificaAmigoTemEmprestimoAtivo(Amigo amigo)
    {
        return amigo.VerificaEmprestimoAtivo();
    }

    public bool VerificaRevistaJaEmprestada(Revista revista)
    {

        return revista.EstaEmprestada();
    }

    public bool VerificaAmigoMultaAtiva(Amigo amigo)
    {
        return amigo.VerificaMultaAtiva();
    }

    public bool VerificaRevistaReservada(Emprestimo novoEmprestimo)
    {
        return novoEmprestimo.revista.Reserva.EstaReservada(novoEmprestimo);
    }
}
