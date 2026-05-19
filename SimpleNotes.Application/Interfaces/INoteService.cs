using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface INoteService
{
    Task<Result<IEnumerable<NoteDomain>>> ListAsync();

    Task<Result<NoteDomain?>> GetAsync(int id);

    Task<Result<NoteDomain>> CreateAsync(NoteDomain note);

    Task<Result<bool>> UpdateAsync(NoteDomain note);

    Task<Result<bool>> DeleteAsync(int id);
}

