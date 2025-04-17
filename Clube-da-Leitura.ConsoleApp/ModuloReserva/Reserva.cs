using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloReserva;

class Reserva
{
    public int Id;
    public Amigo Amigo;
    public Revista Revista;
    public DateTime DataReserva;
    public string Status;

    public Reserva(Amigo amigo, Revista revista)
    {
        Id = GeradorDeIDs.GerarIdReserva();
        Amigo = amigo;
        Revista = revista;
        DataReserva = DateTime.Now.AddDays(7);
        Status = "Ativa";
    }

    public string Validar()
    {
        string erros = "";

        if (Amigo == null)
            erros += "> O Amigo é obrigatório!\n";

        if (Revista == null)
            erros += "> A Revista é obrigatória!\n";

        if (Revista.StatusDeEmprestimo != "Disponível" )
            erros += "> A Revista não está disponível para reserva!\n";

        if (Amigo.VerificaMultaAtiva())
            erros += "> O Amigo tem uma Multa pendente e não pode fazer uma reserva!\n";

        return erros;
    }

    public bool EstaReservada(Emprestimo emprestimo)
    {
        if (emprestimo.amigo.Id == Amigo.Id)
            return true;

        return false;
    }

    public void AtivarReserva()
    {
        Status = "Ativa";
    }

    public void ConcluirReserva()
    {
        Status = "Concluída";
    }
}
