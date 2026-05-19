using FluentResults;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Mapping;
using SimpleNotes.Infrastructure;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Application.Services;
public class NoteService : INoteService
{

    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;

    public NoteService(INoteRepository noteRepository, IUserRepository userRepository)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<NoteDomain>>> ListAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var notes = await _noteRepository.ListAsync(cancellationToken);
            var notesDomain = notes.Select(x => x.ToDomain());

            if (notesDomain == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "No note exists."));
            }

            return Result.Ok(notesDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<NoteDomain?>> GetAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var isNoteExist = await _noteRepository.GetAsync(id, cancellationToken);
            var noteDomain = isNoteExist?.ToDomain();

            if (noteDomain == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "Note ID does not exist."));
            }

            return noteDomain;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<NoteDomain>> CreateAsync(NoteDomain note, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var isUserExist =  await _userRepository.GetUserAsync(note.UserId, cancellationToken);

            if (isUserExist == null)
            {
                return Result.Fail(new ValidationError().WithError("UserId", "User ID does not exist."));
            }

            var noteEntity = note.ToEntity();
            var noteCreate = await _noteRepository.CreateAsync(noteEntity, cancellationToken);
            var noteDomain = noteCreate.ToDomain();

            return Result.Ok(noteDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateAsync(NoteDomain note, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var isNoteExist = await _noteRepository.GetAsync(note.Id, cancellationToken);

            if (isNoteExist == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "Note ID does not exist."));
            }

            var noteEntity = note.ToEntity();

            var noteUpdate = await _noteRepository.UpdateAsync(noteEntity, cancellationToken);

            return Result.Ok(noteUpdate);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var isNoteExist = await _noteRepository.GetAsync(id, cancellationToken);

            if (isNoteExist == null)
            {
                return Result.Fail(new ValidationError(404).WithError("Id", "Note ID does not exist."));
            }

            var noteDelete = await _noteRepository.DeleteAsync(id, cancellationToken);

            return Result.Ok(noteDelete);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
