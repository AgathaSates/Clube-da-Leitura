﻿using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class Amigo
{
    public int Id;
    public string Nome;
    public string NomeResponsavel;
    public string Telefone;
    public Emprestimo Emprestimo;

    public Amigo(string nome, string responsavel, string telefone)
    {
        Id = GeradorDeIDs.GerarIdAmigo();
        Nome = nome;
        NomeResponsavel = responsavel;
        Telefone = telefone;
    }

    public Amigo(int id, string nome, string responsavel, string telefone)
    {
        Id = id;
        Nome = nome;
        NomeResponsavel = responsavel;
        Telefone = telefone;
    }

    public string Validar()
    {
        int tamanhoMaximoNome = 100;
        int tamanhoMinimoNome = 3;
        int tamanhoMaximoTelefone = 11;
        string erros = "";
        if (string.IsNullOrWhiteSpace(Nome))
            erros+= "> O Nome é obrigatório!\n";

        else if (Nome.Length < tamanhoMinimoNome || Nome.Length > tamanhoMaximoNome)
            erros+= "> O Nome deve ter entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros+= "> O Nome Responsável é obrigatório!\n";

        else if (NomeResponsavel.Length < tamanhoMinimoNome || NomeResponsavel.Length > tamanhoMaximoNome)
            erros += "> O Nome Responsável deve ter entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "> O campo Telefone é obrigatório!\n";

        else if (Telefone.Length < tamanhoMaximoTelefone || Telefone.Length > tamanhoMaximoTelefone) //(51) 98596-2346
            erros += "> O Telefone deve ter 11 caracteres no formato (XX)XXXXX-XXXX(SEM TRAÇO E ESPAÇO)\n";

        return erros;
    }

    public Emprestimo ObterEmprestimo() 
    {
        return Emprestimo;
    }
}
