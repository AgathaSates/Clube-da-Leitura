namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

public class Multa
{
    public const decimal ValorPorDiaAtraso = 2;

    public int Id { get; set; }
    public Emprestimo Emprestimo { get; set; }
    public string Status { get; set; } // Pendente / Quitada

    private int contadorIds = 0;

    public Multa(Emprestimo emprestimo)
    {
        Id = ++contadorIds;
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
