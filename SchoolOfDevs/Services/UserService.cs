using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Helpers;
using SchoolOfDevs.Model;

namespace SchoolOfDevs.Services
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        public Task<User> GetById(int id);
        public Task Update(User userIn, int id);
        public Task Delete(int id);
        public Task<List<User>> GetAll();
    }
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == user.UserName);
            if (userDb is not null)
                throw new Exception($"User name {user.UserName} already exist.");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            User userDb = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == id);
            if (userDb is null)
                throw new Exception($"User {id} not found.");
            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();


        public async Task<User> GetById(int id)
        {
            User userDb = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"User {id} not found.");

            return userDb;
        }

        public async Task Update(User userIn, int id)
        {
            if (userIn.Id != id)
                throw new Exception("Router id differs User id.");

            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"User {id} not found.");

            _context.Entry(userIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
