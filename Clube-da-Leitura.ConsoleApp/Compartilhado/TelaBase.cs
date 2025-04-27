using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>, ITelaCrud
{
    protected string nomeEntidade;
    private readonly RepositorioBase<T> repositorio;

    protected TelaBase(string nomeEntidade, int quantidadeLetras, RepositorioBase<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public abstract string ApresentarMenu();

    public virtual void CadastrarRegistro(int numeroDoTitulo) 
    {
        ApresentarTitulo(numeroDoTitulo);

        T novoRegistro = ObterDadosDoRegistro(true);

        if (NãoConseguiuValidar(novoRegistro))
        { 
            CadastrarRegistro(numeroDoTitulo);
            return;
        }

        string mensagemResultado = repositorio.CadastrarRegistro(novoRegistro);
        ConseguiuCadastrar(mensagemResultado);

        Notificador.ApresentarMensagemParaSair();
    }

    public virtual void EditarRegistro(int numeroDoTitulo) 
    {
        ApresentarTitulo(numeroDoTitulo);

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);

        ColorirTexto.ExibirMensagemSemLinha($">> Digite o Id do {nomeEntidade} que deseja editar: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if(NãoEncontrouRegistro(id))
            return;

        T registroEditado = ObterDadosDoRegistro(false, id);

        if (NãoConseguiuValidar(registroEditado))
        {
            EditarRegistro(numeroDoTitulo);
            return;
        }

        bool editou = repositorio.EditarRegistro(id, registroEditado);
        ConseguiuEditar(editou);
        
        Notificador.ApresentarMensagemParaSair();

    }

    public virtual void ExcluirRegistro(int numeroDoTitulo)
    {
        ApresentarTitulo(numeroDoTitulo);

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);

        ColorirTexto.ExibirMensagemSemLinha($">> Digite o Id do {nomeEntidade} que deseja excluir: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NãoEncontrouRegistro(id))
            return;

        bool excluiu = repositorio.ExcluirRegistro(id);
        ConseguiuExcluir(excluiu);

        Notificador.ApresentarMensagemParaSair();
    }

 
    public abstract void VisualizarRegistros(bool v1, bool v2);

    public abstract T ObterDadosDoRegistro(bool v, int idExistente = 0);

    public virtual void ConseguiuCadastrar(string mensagemResulado)
    {

        if (mensagemResulado == ">> (V) Registro cadastrado com sucesso!" )
            ColorirTexto.ExibirMensagem(mensagemResulado, ConsoleColor.Green);
        else
            ColorirTexto.ExibirMensagem(mensagemResulado, ConsoleColor.Red);
    }

    public virtual void ConseguiuExcluir(bool excluiu)
    {
        if (excluiu)
            ColorirTexto.ExibirMensagem($">> (V) {nomeEntidade} excluído com sucesso!", ConsoleColor.Green);
        else
            ColorirTexto.ExibirMensagem($">> (X) Não foi possível excluir o {nomeEntidade}!", ConsoleColor.Red);
    }

    public virtual void ConseguiuEditar(bool editou)
    {
        if (editou)
            ColorirTexto.ExibirMensagem($">> (V) {nomeEntidade} editado com sucesso!", ConsoleColor.Green);
        else
            ColorirTexto.ExibirMensagem($">> (X) Não foi possível editar o {nomeEntidade}!", ConsoleColor.Red);
    }

    public virtual bool ExisteRegistros()
    {
        if (repositorio.NaoExisteRegistros())
        {
            ColorirTexto.ExibirMensagem($">> (X) Nenhum {nomeEntidade} cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return false;
        }
        return true;
    }

    public abstract bool NãoConseguiuValidar(T Registro);

    public virtual bool NãoEncontrouRegistro(int id)
    {
        if (repositorio.NaoEncontrouRegistro(id))
        {
            ColorirTexto.ExibirMensagem($">> (X) {nomeEntidade} não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return true;
        }
        return false;
    }

    public abstract void ApresentarTitulo(int numeroDoTitulo);
  
}
