using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase<Amigo>
{
    private int contadorIds = 0;

    public override string CadastrarRegistro(Amigo novoAmigo)
    {
        if (VerificaAmigoJaExiste(novoAmigo))
            return ">> (X)Amigo já cadastrado.";

        registros.Add(novoAmigo);
        novoAmigo.Id = ++contadorIds;
        return ">> (V) Registro cadastrado com sucesso!";
    }

    public override bool ExcluirRegistro(int id)        
    {
        Amigo amigo = SelecionarRegistroPorId(id);
        if (amigo == null)
            return false;

        if (amigo.VerificaEmprestimoAtivo())
            return false;

        registros.Remove(amigo);
        return true;
    }

    public List<Emprestimo> VisualizarEmprestimos(int id)
    {
        Amigo amigo = SelecionarRegistroPorId(id);
        return amigo.ObterEmprestimos();
    }

    public bool VerificaAmigoJaExiste(Amigo novoAmigo)
    {
        bool jaExiste = false;
        foreach (Amigo amigo in registros)
            if (amigo != null)
                if (amigo.Nome == novoAmigo.Nome && amigo.Telefone == novoAmigo.Telefone)
                    jaExiste = true;
        

        return jaExiste;
    }
}
