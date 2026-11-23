namespace OficinaMecanica.Application.OrdensServico;

public interface ITentativaReservaEstoque
{
    Task<ResultadoTentativaReserva> TentarReservarAsync(long numero, CancellationToken cancelamento);
}
