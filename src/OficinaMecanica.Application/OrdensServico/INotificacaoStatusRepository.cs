using OficinaMecanica.Domain.OrdensServico;

namespace OficinaMecanica.Application.OrdensServico;

public interface INotificacaoStatusRepository
{
    Task<IReadOnlyCollection<NotificacaoStatusOrdemServico>> ListarParaProcessamentoAsync(
        DateTimeOffset agora, int quantidadeMaxima, CancellationToken cancelamento);
    Task<bool> TentarAssumirAsync(Guid notificacaoId, Guid processamentoId, DateTimeOffset agora,
        DateTimeOffset processandoAteUtc, CancellationToken cancelamento);
    Task<bool> MarcarEnviadaAsync(Guid notificacaoId, Guid processamentoId,
        DateTimeOffset enviadaEmUtc, CancellationToken cancelamento);
    Task<bool> ReagendarAsync(Guid notificacaoId, Guid processamentoId, DateTimeOffset proximaTentativaEmUtc,
        string erro, CancellationToken cancelamento);
}
