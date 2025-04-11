using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class Amigo
{
    public int Id;
    public string Nome;
    public string NomeResponsavel;
    public string Telefone;
    public Emprestimo Emprestimo;

    public Amigo(string nome, string responsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = responsavel;
        Telefone = telefone;
    }

    public string Validar()
    {
        int tamanhoMaximoNome = 100;
        int tamanhoMinimoNome = 3;
        int tamanhoMaximoTelefone = 11;
        string erros = "";
        if (string.IsNullOrWhiteSpace(Nome))
            erros+= "O Nome é obrigatório";

        else if (Nome.Length < tamanhoMinimoNome && Nome.Length > tamanhoMaximoNome)
            erros+= "O Nome deve ter entre 3 e 100 caracteres";

        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros+= "O Nome Responsável é obrigatório";

        else if (NomeResponsavel.Length < tamanhoMinimoNome && NomeResponsavel.Length > tamanhoMaximoNome)
            erros += "O Nome Responsável deve ter entre 3 e 100 caracteres";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo Telefone é obrigatório";
        else if (Telefone.Length < tamanhoMaximoTelefone && Telefone.Length > tamanhoMaximoTelefone) //(51) 98596-2346
            erros += "O Telefone deve ter 11 caracteres no formato (XX)XXXXX-XXXX(SEM TRAÇO)";

        return erros;
    }

    public Emprestimo ObterEmprestimo() 
    {
        return Emprestimo;
    }
}
