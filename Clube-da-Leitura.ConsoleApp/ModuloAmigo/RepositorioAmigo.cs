namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class RepositorioAmigo
{
    public Amigo[] amigos = new Amigo[5];
    public int contadorAmigo = 0;

    public string Inserir(Amigo novoAmigo)
    {
        if (VerificarLimiteAmigos())
            return "Limite de amigos atingido.";
        

        else if (VerificaAmigoJaExiste(novoAmigo)) 
            return"Amigo já cadastrado.";

        else
        {           
            amigos[contadorAmigo++] = novoAmigo;
            return "(V) Amigo cadastrado com sucesso!";
        }          
    }


    public bool Editar(int Id, Amigo novoamigo)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                if (amigo.Id == Id)
                {
                    amigo.Nome = novoamigo.Nome;
                    amigo.NomeResponsavel = novoamigo.NomeResponsavel;
                    amigo.Telefone = novoamigo.Telefone;
                    
                    return true;
                }
            }
        }
        return false;
    }

    public void Excluir(int Id)           //● Não permitir excluir um amigo caso tenha empréstimos vinculados(status pendente)
    {
        for (int i = 0; i < amigos.Length; i++)
        {
            if (amigos[i] != null)
            {
                if (amigos[i].Id == Id)
                {
                    amigos[i] = null;
                    contadorAmigo--;
                    break;
                }
            }
        }
    }

    public Amigo[] SelecionarTodos()
    {
        int contadoramigosPreenchidos = 0;

        foreach (Amigo amigo in amigos) 
        {
            if (amigo != null)
                contadoramigosPreenchidos++;
        }

        Amigo[] amigosSelecionados = new Amigo[contadoramigosPreenchidos];

        int contador = 0;

        foreach (Amigo amigo in amigos)
        {  
            if (amigo != null)
            {
                amigosSelecionados[contador] = amigo;
                contador++;
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

    //public Emprestimo[] VisualizarEmprestimos(int Id) // terminar para exibir todos os emprstimos(.status))
    //{
    //    foreach (Amigo amigo in amigos)
    //    {
    //        if (amigo.Id == Id)
    //        {
    //            Console.WriteLine($"Nome: {amigo.Nome}");
    //            Console.WriteLine($"Nome Responsável: {amigo.NomeResponsavel}");
    //            Console.WriteLine($"Telefone: {amigo.Telefone}");
    //            Console.WriteLine($"Emprestimo: {amigo.Emprestimo.status}"); // adicionar qual revista e titulo
    //        }
    //    }
    //}

    public bool VerificaAmigoJaExiste(Amigo novoAmigo)
    {
        bool jaExiste = false;
        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                if (amigo.Nome == novoAmigo.Nome && amigo.Telefone == novoAmigo.Telefone)
                {
                    jaExiste = true;
                } 
            }
        }
        return jaExiste;
    }

    //● Não permitir excluir um amigo caso tenha empréstimos vinculados em aberto
    public bool VerificaEmprestimoExistente(int Id)
    {
        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                if (amigo.Id == Id && amigo.Emprestimo != null) // finalizas apos o modulo emprestimo
                    return true;
            }
        }
        return false;
    }

    public bool VerificarLimiteAmigos()
    {
        if (contadorAmigo == amigos.Length)          
            return true;      
        return false;
    }
}
