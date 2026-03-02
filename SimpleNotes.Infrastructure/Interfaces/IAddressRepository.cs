using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface IAddressRepository
{
    AddressEntity? GetAddress(int id);

    UserEntity CreateAddress(UserEntity user);

    bool UpdateAddress(UserEntity user);

    bool DeleteAddress(UserEntity id);
}
