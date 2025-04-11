namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class RepositorioAmigo
{
    public Amigo[] amigos = new Amigo[100];
    public int contadoramigo = 0;

    public void Inserir(Amigo amigo)
    {
        amigos[contadoramigo] = amigo;
        contadoramigo++;
    }

    public void Editar(int Id, Amigo novoamigo)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo.Id == Id)
            {
               amigo.Nome = novoamigo.Nome;
               amigo.NomeResponsavel = novoamigo.NomeResponsavel;
               amigo.Telefone = novoamigo.Telefone;
            }
        }
    }

    public void Excluir(int Id)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo.Id == Id)
            {
                amigo.Nome = null;
                amigo.NomeResponsavel = null;
                amigo.Telefone = null;
            }
        }
    }

    public Amigo[] SelecionarTodos()
    {
        Amigo[] amigosSelecionados = new Amigo[contadoramigo];

        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                amigosSelecionados[contadoramigo] = amigo;
                contadoramigo++;
            }
        }
        return amigosSelecionados;
    }

    public Amigo SelecionarPorId(int Id)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo.Id == Id)
                return amigo;
        }
        return null;
    }

    public void VisualizarEmprestimo(int Id)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo.Id == Id)
            {
                Console.WriteLine($"Nome: {amigo.Nome}");
                Console.WriteLine($"Nome Responsável: {amigo.NomeResponsavel}");
                Console.WriteLine($"Telefone: {amigo.Telefone}");
                Console.WriteLine($"Emprestimo: {amigo.Emprestimo}"); // adicionar qual revista e titulo
            }
        }
    }

    //● Não pode haver amigos com o mesmo nome e telefone.
    public bool VerificaAmigoExistente(string nome, string telefone)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                if (amigo.Nome == nome && amigo.Telefone == telefone)
                    return true;
            }
        }
        return false;
    }

    //● Não permitir excluir um amigo caso tenha empréstimos vinculados
    public bool VerificaEmprestimoExistente(int Id)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                if (amigo.Id == Id && amigo.Emprestimo != null)
                    return true;
            }
        }
        return false;
    }

}
