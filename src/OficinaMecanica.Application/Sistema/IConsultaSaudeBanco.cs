namespace OficinaMecanica.Application.Sistema;

public interface IConsultaSaudeBanco
{
    Task<bool> PodeConectarAsync(CancellationToken cancelamento);
}
