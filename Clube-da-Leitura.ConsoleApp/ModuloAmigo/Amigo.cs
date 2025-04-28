using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloReserva;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

public class Amigo : EntidadeBase<Amigo>
{
    public string Nome { get; set; }
    public string NomeResponsavel { get; set; }
    public string Telefone { get; set; }
    public Reserva Reserva;
    public List<Multa> multas = new List<Multa>();
    public List<Emprestimo> Emprestimos = new List<Emprestimo>();

    public Amigo(string nome, string responsavel, string telefone) // id novo
    {
        Nome = nome;
        NomeResponsavel = responsavel;
        Telefone = telefone;
    }

    public Amigo(int id, string nome, string responsavel, string telefone)
    {
        Id = id;
        Nome = nome;
        NomeResponsavel = responsavel;
        Telefone = telefone;
    } // mantem o id

    public override void AtualizarRegistro(Amigo registroEditado)
    {
        Nome = registroEditado.Nome;
        NomeResponsavel = registroEditado.NomeResponsavel;
        Telefone = registroEditado.Telefone;
    }

    public override string Validar()
    {
        int tamanhoMaximoNome = 100;
        int tamanhoMinimoNome = 3;
        int tamanhoMaximoTelefone = 11;
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "> O Nome é obrigatório!\n";

        else if (Nome.Length < tamanhoMinimoNome || Nome.Length > tamanhoMaximoNome)
            erros += "> O Nome deve ter entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros += "> O Nome do Responsável é obrigatório!\n";

        else if (NomeResponsavel.Length < tamanhoMinimoNome || NomeResponsavel.Length > tamanhoMaximoNome)
            erros += "> O Nome do Responsável deve ter entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "> O Telefone é obrigatório!\n";

        else if (!Validador.VerificaSeEhNumero(Telefone))
            erros += "> O Telefone deve conter apenas números\n";

        else if (Telefone.Length < tamanhoMaximoTelefone || Telefone.Length > tamanhoMaximoTelefone) //(51) 99599-2244
            erros += "> O Telefone deve ter 11 caracteres no formato (XX)XXXXX-XXXX(SEM TRAÇO E ESPAÇO)\n";

        return erros;
    }

    public List<Emprestimo> ObterEmprestimos()
    {
        return Emprestimos;
    }

    public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        Emprestimos.Add(emprestimo);
    }

    public bool VerificaEmprestimoAtivo()
    {
        foreach (Emprestimo emprestimo in Emprestimos)
            if (emprestimo != null)
                if (emprestimo.StatusDeEmprestimo != "Concluído")
                    return true;

        return false;
    }

    public List<Multa> ObterMultas()
    {
        return multas;
    }

    public void AdicionarMulta(Multa multa)
    {
       multas.Add(multa);
    }

    public void AdicionarReserva(Reserva reserva)
    {
        Reserva = reserva;
    }

    public bool VerificaMultaAtiva()
    {
        foreach (Multa multa in multas)
            if (multa != null)
                if (multa.EstaPendente())
                    return true;
        return false;
    }

}
