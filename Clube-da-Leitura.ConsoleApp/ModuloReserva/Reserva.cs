using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
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

        if (Revista.StatusDeEmprestimo != "Disponível")
            erros += "> A Revista não está disponível para reserva!\n";

        return erros;
    }

    public void ConcluirReserva()
    {
        Status = "Concluída";
    }
}
