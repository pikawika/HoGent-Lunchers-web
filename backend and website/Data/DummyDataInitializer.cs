using System;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class DummyDataInitializer
    {

        private readonly ApplicationDbContext _dbContext;

        public DummyDataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                UserModel user = new UserModel{Email="jodi@jodideloof.be",Name="jodi", Password="test123",Rol="admin"};

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

            }
        }
    }
}
