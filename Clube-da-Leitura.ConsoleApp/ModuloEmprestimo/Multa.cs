using Clube_da_Leitura.ConsoleApp.Compartilhado;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

class Multa
{
    public const decimal ValorPorDiaAtraso = 2;

    public int Id;
    public Emprestimo Emprestimo;
    public string Status;

    public Multa(Emprestimo emprestimo)
    {
        Id = GeradorDeIDs.IdMulta;
        Emprestimo = emprestimo;
    }

    public void GerarMulta(Emprestimo emprestimo)
    {
        Emprestimo = emprestimo;
        emprestimo.amigo.AdicionarMulta(emprestimo.Multa);
        Status = "Pendente";
    }

    public decimal ValorDaMulta()
    {
        int diasAtraso = Emprestimo.ObterDiasAtraso();
        return diasAtraso * ValorPorDiaAtraso;
    }

    public void Quitar()
    {
        Status = "Quitada";
    }

    public bool EstaPendente()
    {
        if (Status == "Pendente")
            return true;

        return false;
    }

}
