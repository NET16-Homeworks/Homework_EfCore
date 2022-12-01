using Homework_EfCore.Interfaces;
using Homework_EfCore.Entities;
using Homework_EfCore.Dtos;
using Homework_EfCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Homework_EfCore.Extentions;

namespace Homework_EfCore.Services
{
    public class UserServices : IUserServices
    {
        private readonly MyDbContext _dbContext;

        public UserServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Create(User model)
        {
            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return new UserDto(model);
        }

        public async Task<UserDto> Delete(User model)
        {
            var user = await GetUser(model);
            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();

            return new UserDto(model);
        }

        public async Task<UserDto> Get(User model)
        {
            var user = await GetUser(model);

            return new UserDto(user);
        }

        public async Task<List<UserDto>> GetList()
        {
            return await _dbContext.Users.Select(user => new UserDto(user)).ToListAsync();
        }

        public async Task<UserDto> Update(User model)
        {
            var user = await GetUser(model);

            user.Update(model);

            await _dbContext.SaveChangesAsync();

            return new UserDto(model);
        }

        private async Task<User> GetUser(User model)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == model.Email);

            if (user is null)
            {
                throw new Exception();
            }

            return user;
        }
    }
}