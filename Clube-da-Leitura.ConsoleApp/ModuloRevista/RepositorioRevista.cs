using Clube_da_Leitura.ConsoleApp.Compartilhado;

namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;
public class RepositorioRevista : RepositorioBase<Revista>
{
    private int contadorIds = 0;

    public override string CadastrarRegistro(Revista novaRevista)
    {
        if (VerificaRevistaJaExiste(novaRevista))
            return ">> (X) Revista já cadastrada.";

        registros.Add(novaRevista);
        novaRevista.Id = ++contadorIds;
        novaRevista.Caixa.AdicionarRevista(novaRevista);
        return ">> (V) Registro cadastrado com sucesso!";
    }

    public override bool EditarRegistro(int id, Revista registroEditado)
    {
        Revista revista = SelecionarRegistroPorId(id);
        if (revista != null)
            if (revista.Id == id)
            {
                revista.Caixa.RemoverRevista(revista);

                revista.AtualizarRegistro(registroEditado);

                revista.Caixa.AdicionarRevista(revista);
                return true;
            }

        return false;
    }

    public override bool ExcluirRegistro(int id)
    {
        Revista revista = SelecionarRegistroPorId(id);
        if (revista != null)
        {
            if (revista.StatusDeEmprestimo == "Emprestada")
                return false;

            revista.Caixa.RemoverRevista(revista);
            registros.Remove(revista);
            return true;
        }
        return false;
    }

    public bool VerificaRevistaJaExiste(Revista novaRevista)
    {
        bool jaExiste = false;

        foreach (Revista revista in registros)
            if (revista != null)
                if (revista.Titulo == novaRevista.Titulo && revista.NumeroDaEdicao == novaRevista.NumeroDaEdicao)
                    jaExiste = true;
        return jaExiste;
    }

}
