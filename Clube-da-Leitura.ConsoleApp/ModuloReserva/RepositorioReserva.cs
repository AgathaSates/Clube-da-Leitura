using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

namespace Clube_da_Leitura.ConsoleApp.ModuloReserva;

class RepositorioReserva
{
    public Reserva[] reservas = new Reserva[100];
    public int contadorReservas = 0;

    public string Inserir(Reserva novaReserva)
    {
        if (VerificarLimiteReservas())
            return ">> Limite de reservas atingido.";

        if (VerificaDataJaOcupada(novaReserva))
            return ">> Esta data já está ocupada.";

        reservas[++contadorReservas] = novaReserva;
        return ">> (V) Reserva cadastrada com sucesso!";
    }

    public bool Excluir(int idReserva)
    {
        foreach (Reserva reserva in reservas)
            if (reserva != null)
                if (reserva.Id == idReserva)
                {
                    reservas[contadorReservas] = null;
                    contadorReservas--;
                    return true;
                }
        return false;
    }

    public Reserva[] SelecionarTodos()
    {
        int contadorReservasPreenchidas = 0;

        foreach (Reserva reserva in reservas)
            if (reserva != null)
                contadorReservasPreenchidas++;

        Reserva[] reservasSelecionadas = new Reserva[contadorReservasPreenchidas];

        int contador = 0;

        foreach (Reserva reserva in reservas)
            if (reserva != null)
                reservasSelecionadas[contador++] = reserva;


        return reservasSelecionadas;
    }

    public Reserva SelecionarPorId(int id)
    {
        foreach (Reserva reserva in reservas)
            if (reserva != null)
                if (reserva.Id == id)
                    return reserva;
        return null;
    }

    public bool VerificarLimiteReservas()
    {
        if (contadorReservas == reservas.Length)
            return true;
        return false;
    }

    public bool VerificaDataJaOcupada(Reserva reserva)
    {
        bool jaOcupada = false;
        foreach (Reserva dataOcupada in reservas)
            if (dataOcupada != null)
                if (reserva.DataReserva == reserva.DataReserva)
                    jaOcupada = true;

        return jaOcupada;
    }
}
