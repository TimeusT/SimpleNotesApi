using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface INoteService
{
    Task<Result<IEnumerable<NoteDomain>>> ListAsync(CancellationToken cancellationToken);

    Task<Result<NoteDomain?>> GetAsync(int id, CancellationToken cancellationToken);

    Task<Result<NoteDomain>> CreateAsync(NoteDomain note, CancellationToken cancellationToken);

    Task<Result<bool>> UpdateAsync(NoteDomain note, CancellationToken cancellationToken);

    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken);
}

