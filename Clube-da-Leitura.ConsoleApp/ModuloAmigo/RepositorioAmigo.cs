using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class RepositorioAmigo
{
    public Amigo[] Amigos = new Amigo[100];
    public int contadorAmigo = 0;

    public string Inserir(Amigo novoAmigo)
    {
        if (VerificarLimiteAmigos())
            return ">> (X) Limite de amigos atingido.";

        if (VerificaAmigoJaExiste(novoAmigo))
            return ">> (X)Amigo já cadastrado.";

        Amigos[contadorAmigo++] = novoAmigo;
        return ">> (V) Amigo cadastrado com sucesso!";
    }

    public bool Editar(int id, Amigo novoAmigo)
    {
        foreach (Amigo amigo in Amigos)
            if (amigo != null)
                if (amigo.Id == id)
                {
                    amigo.Nome = novoAmigo.Nome;
                    amigo.NomeResponsavel = novoAmigo.NomeResponsavel;
                    amigo.Telefone = novoAmigo.Telefone;

                    return true;
                }

        return false;
    }

    public bool Excluir(int id)        
    {
        for (int i = 0; i < Amigos.Length; i++)
            if (Amigos[i] != null)
                if (Amigos[i].Id == id)
                {
                    if (Amigos[i].VerificaEmprestimoAtivo())
                        return false;

                    Amigos[i] = null;
                    contadorAmigo--;
                    return true;
                }
        return false;
    }

    public Amigo[] SelecionarTodos()
    {
        int contadorAmigosPreenchidos = 0;

        foreach (Amigo amigo in Amigos)
            if (amigo != null)
                contadorAmigosPreenchidos++;

        Amigo[] amigosSelecionados = new Amigo[contadorAmigosPreenchidos];

        int contador = 0;

        foreach (Amigo amigo in Amigos)
            if (amigo != null)
                amigosSelecionados[contador++] = amigo;
                

        return amigosSelecionados;
    }

    public Amigo SelecionarPorId(int id)
    {
        foreach (Amigo amigo in Amigos)
            if (amigo != null)
                if (amigo.Id == id)
                    return amigo;

        return null;
    }

    public Emprestimo[] VisualizarEmprestimos(int id)
    {
        Amigo amigo = SelecionarPorId(id);
        int contadorEmprestimosPreenchidos = 0;

        foreach (Emprestimo emprestimo in amigo.Emprestimos)
            if (emprestimo != null)
                contadorEmprestimosPreenchidos++;

        Emprestimo[] emprestimosSelecionados = new Emprestimo[contadorEmprestimosPreenchidos];

        int contador = 0;
        foreach (Emprestimo emprestimo in amigo.Emprestimos)
            if (emprestimo != null)
                emprestimosSelecionados[contador++] = emprestimo;

        return emprestimosSelecionados;
    }

    public bool VerificarLimiteAmigos()
    {
        if (contadorAmigo == Amigos.Length)
            return true;
        return false;
    }

    public bool VerificaAmigoJaExiste(Amigo novoAmigo)
    {
        bool jaExiste = false;
        foreach (Amigo amigo in Amigos)
            if (amigo != null)
                if (amigo.Nome == novoAmigo.Nome && amigo.Telefone == novoAmigo.Telefone)
                    jaExiste = true;
        

        return jaExiste;
    }
}
