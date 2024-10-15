namespace Metas.Shared.Modelos.Validador;

public abstract class Valida : IValidavel
{
    private readonly Erros erros = new();
    public bool EhValido { get; private set; } = true;  
    public Erros Erros => erros;

    // Método para disparar a validação
    public void ValidarEntidade()
    {
        erros.LimparErros(); 
        Validar();  
        EhValido = !erros.Any();  
    }

    protected abstract void Validar(); 
}
