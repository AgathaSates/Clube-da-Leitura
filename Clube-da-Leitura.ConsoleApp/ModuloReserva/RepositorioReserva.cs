using Clube_da_Leitura.ConsoleApp.Compartilhado;

namespace Clube_da_Leitura.ConsoleApp.ModuloReserva;

public class RepositorioReserva : RepositorioBase<Reserva>
{
    private int contadorIds = 0;

    public override string CadastrarRegistro(Reserva novoRegistro)
    {
        if (VerificaDataJaOcupada(novoRegistro))
            return ">> Esta data já está ocupada.";

        registros.Add(novoRegistro);
        novoRegistro.Id = ++contadorIds;
        novoRegistro.AtivarReserva();
        novoRegistro.Revista.Reservar(novoRegistro);
        novoRegistro.Amigo.AdicionarReserva(novoRegistro);
        return ">> (V) Registro cadastrado com sucesso!";
    }

    public bool VerificaDataJaOcupada(Reserva reserva)
    {
        bool jaOcupada = false;
        foreach (Reserva dataOcupada in registros)
            if (dataOcupada != null)
                if (dataOcupada.DataReserva == reserva.DataReserva)
                    jaOcupada = true;

        return jaOcupada;
    }

}
