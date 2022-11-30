using Homework_EfCore.Entities;
using Homework_EfCore.Dtos;

namespace Homework_EfCore.Interfaces
{
    public interface IUserServices : ICRUDAsync<User, UserDto>
    {
    }
}
