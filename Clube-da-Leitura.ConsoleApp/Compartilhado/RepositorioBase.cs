namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

public abstract class RepositorioBase<T> where T : EntidadeBase<T>
{
    private List<T> registros = new List<T>();
    private int contadorIds = 0;

    public abstract string CadastrarRegistro(T novoRegistro);
    

    public virtual bool EditarRegistro(int idRegistro, T registroEditado)
    {
        T registro = SelecionarRegistroPorId(idRegistro);
        if (registro != null)
        {
            registro.AtualizarRegistro(registroEditado);
            return true;
        }
        return false;
    }

    public virtual bool ExcluirRegistro(int idRegistro)
    {
        T registro = SelecionarRegistroPorId(idRegistro);
        if (registro != null)
        {
            registros.Remove(registro);
            return true;
        }
        return false;
    }

    public T SelecionarRegistroPorId(int idRegistro)
    {
        foreach (T registro in registros)
            if (registro.Id == idRegistro)
                return registro;

        return null;
    }

    public List<T> SelecionarTodosRegistros()
    {
        return registros;
    }

    public bool NaoExisteRegistros()
    {
        return registros.Count == 0;
    }

    public bool NaoEncontrouRegistro(int id)
    {
        return SelecionarRegistroPorId(id) == null;
    }
}
