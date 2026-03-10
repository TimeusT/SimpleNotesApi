using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface INoteService
{
    Result<IEnumerable<NoteDomain>> List();

    Result<NoteDomain?> Get(int id);

    Result<NoteDomain> Create(NoteDomain note);

    Result<bool> Update(NoteDomain note);

    Result<bool> Delete(int id);
}

