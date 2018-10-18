using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using WebApplication5.Models.Repositories;

namespace WebApplication5.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<UserModel> _users;
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public UserModel Authenticate(string username, string password)
        {
            return _users.Where(user => user.Name == username && user.Password == password).FirstOrDefault();
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _users.ToList();
        }
    }
}
