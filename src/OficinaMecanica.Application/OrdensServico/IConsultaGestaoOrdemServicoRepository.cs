using OficinaMecanica.Domain.OrdensServico;

namespace OficinaMecanica.Application.OrdensServico;

public interface IConsultaGestaoOrdemServicoRepository
{
    Task<PaginaOrdensServico> ListarAsync(StatusOrdemServico? status, int pagina,
        int tamanhoPagina, CancellationToken cancelamento);

    Task<IReadOnlyList<TempoServicoConcluido>> ObterTemposConcluidosAsync(
        CancellationToken cancelamento);
}
