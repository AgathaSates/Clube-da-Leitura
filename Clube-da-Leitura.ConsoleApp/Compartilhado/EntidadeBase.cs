namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

public abstract class EntidadeBase<T>
{
    public int Id { get; set; }

    public abstract void AtualizarRegistro(T registroEditado);
    public abstract string Validar();
}
