﻿using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloReserva;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class Amigo
{
    public int Id;
    public string Nome;
    public string NomeResponsavel;
    public string Telefone;
    public Reserva Reserva;
    public Multa[] multas = new Multa[100];
    public Emprestimo[] Emprestimos = new Emprestimo[100];

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
            erros += "> O Nome é obrigatório!\n";

        else if (Nome.Length < tamanhoMinimoNome || Nome.Length > tamanhoMaximoNome)
            erros += "> O Nome deve ter entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros += "> O Nome do Responsável é obrigatório!\n";

        else if (NomeResponsavel.Length < tamanhoMinimoNome || NomeResponsavel.Length > tamanhoMaximoNome)
            erros += "> O Nome do Responsável deve ter entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "> O Telefone é obrigatório!\n";

        else if (!Validador.VerificaSeEhNumero(Telefone))
            erros += "> O Telefone deve conter apenas números\n";

        else if (Telefone.Length < tamanhoMaximoTelefone || Telefone.Length > tamanhoMaximoTelefone) //(51) 99599-2244
            erros += "> O Telefone deve ter 11 caracteres no formato (XX)XXXXX-XXXX(SEM TRAÇO E ESPAÇO)\n";

        return erros;
    }

    public Emprestimo[] ObterEmprestimos()
    {
        int contadorEmprestimosPreenchidos = 0;

        foreach (Emprestimo emprestimo in Emprestimos)
            if (emprestimo != null)
                contadorEmprestimosPreenchidos++;

        Emprestimo[] emprestimosSelecionados = new Emprestimo[contadorEmprestimosPreenchidos];
        int contador = 0;

        foreach (Emprestimo emprestimo in Emprestimos)
            if (emprestimo != null)
                emprestimosSelecionados[contador++] = emprestimo;

        return emprestimosSelecionados;
    }

    public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        for (int i = 0; i < Emprestimos.Length; i++)
            if (Emprestimos[i] == null)
            {
                Emprestimos[i] = emprestimo;
                break;
            }
    }

    public bool VerificaEmprestimoAtivo()
    {
        foreach (Emprestimo emprestimo in Emprestimos)
            if (emprestimo != null)
                if (emprestimo.StatusDeEmprestimo != "Concluído")
                    return true;

        return false;
    }

    public Multa[] ObterMultas()
    {
        int contadorMultasPreenchidas = 0;

        foreach (Multa multa in multas)
            if (multa != null)
                contadorMultasPreenchidas++;
        Multa[] multasSelecionadas = new Multa[contadorMultasPreenchidas];
        int contador = 0;

        foreach(Multa multa in multas)
            if (multa != null)
                multasSelecionadas[contador++] = multa;
        return multasSelecionadas;
    }

    public void AdicionarMulta(Multa multa)
    {
        for(int i = 0; i < multas.Length; i++)
            if (multas[i] == null)
            {
                multas[i] = multa;
                break;
            }
    }
    public void AdicionarReserva(Reserva reserva)
    {
        Reserva = reserva;
    }

    public bool VerificaMultaAtiva()
    {
        foreach (Multa multa in multas)
            if (multa != null)
                if (multa.EstaPendente())
                    return true;
        return false;
    }
}
