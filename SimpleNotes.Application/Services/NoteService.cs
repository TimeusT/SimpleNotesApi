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

    public Result<IEnumerable<NoteDomain>> List()
    {
        try
        {
            var notes = _noteRepository.List().Select(x => x.ToDomain());

            if (notes == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "No note exists."));
            }

            return Result.Ok(notes);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<NoteDomain?> Get(string uniqueId)
    {
        try
        {
            var isNoteExist = _noteRepository.Get(uniqueId)?.ToDomain();

            if (isNoteExist == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "Note ID does not exist."));
            }

            return isNoteExist;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<NoteDomain> Create(NoteDomain note)
    {
        try
        {
            // Validate email exist in db, if not return Result.Fail
            var isUserExist = _userRepository.GetByEmail(note.Email);

            if (isUserExist == null)
            {
                return Result.Fail(new ValidationError().WithError("Email", "Email does not exist."));
            }

            // convert domain to entity
            var noteEntity = note.ToEntity();

            // call create method in repo
            var noteCreate = _noteRepository.Create(noteEntity);

            // convert entity to domain
            var noteDomain = noteCreate.ToDomain();

            // returns domain
            return Result.Ok(noteDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<bool> Update(NoteDomain note)
    {
        try
        {
            var isNoteExist = _noteRepository.Get(note.UniqueId);

            if (isNoteExist == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "Note ID does not exist."));
            }

            // convert domain to entity
            var noteEntity = note.ToEntity();

            // call update method
            var noteUpdate = _noteRepository.Update(noteEntity);

            return Result.Ok(noteUpdate);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<bool> Delete(string uniqueId)
    {
        try
        {
            var isNoteExist = _noteRepository.Get(uniqueId);

            if (isNoteExist == null)
            {
                return Result.Fail(new ValidationError(404).WithError("Id", "Note ID does not exist."));
            }

            // call delete on repo
            var noteDelete = _noteRepository.Delete(uniqueId);

            return Result.Ok(noteDelete);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
