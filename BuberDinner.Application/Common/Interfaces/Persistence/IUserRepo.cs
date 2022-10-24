using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepo
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
