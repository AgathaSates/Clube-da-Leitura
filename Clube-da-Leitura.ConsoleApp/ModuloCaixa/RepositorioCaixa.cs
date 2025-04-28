
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloCaixa;

public class RepositorioCaixa : RepositorioBase<Caixa>
{
    private int contadorIds = 0;

    public override string CadastrarRegistro(Caixa caixa)
    {

        if (VerificaCaixaJaExiste(caixa))
            return ">> (X) Caixa já cadastrada, não pode registrar etiquetas duplicadas.";

        registros.Add(caixa);
        caixa.Id = ++contadorIds;
        return ">> (V) Registro cadastrado com sucesso!";

    }

    public override bool ExcluirRegistro(int id)
    {
        Caixa caixa = SelecionarRegistroPorId(id);
        if (caixa == null)
            return false;
        if (!caixa.TemRevistas())
            return false;
        registros.Remove(caixa);
        return true;
    }
    
    public int ContarRevistasNaCaixa(Caixa caixa)
    {
        int contador = 0;
        foreach (Revista revista in caixa.revistas)
            if (revista != null)
                contador++;
        return contador;
    }

    public bool VerificaCaixaJaExiste(Caixa caixa)
    {
        bool jaExiste = false;
        foreach (Caixa caixaExistente in registros)
            if (caixaExistente != null)
                if (caixa.Etiqueta == caixaExistente.Etiqueta)
                    jaExiste = true;

        return jaExiste;
    }

}
