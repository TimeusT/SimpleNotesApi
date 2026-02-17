using SimpleNotes.Api.Services.Domain;

namespace SimpleNotes.Api.Services
{
    public interface INoteService
    {
        IEnumerable<NoteDomain> List();

        NoteDomain? Get(int id);

        NoteDomain Create(NoteDomain note);

        bool Update(NoteDomain note);

        bool Delete(int id);
    }
}
