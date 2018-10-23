using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lunchers.Models;
using Lunchers.Models.Repositories;

namespace Lunchers.Data.Repositories
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

        public void Register(UserModel user)
        {
            _users.Add(user);
            saveChanges();
        }

        private void saveChanges(){
            _context.SaveChanges();
        }
    }
}
