using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DbSet<Ingredient> _ingredienten;
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
            _ingredienten = context.Ingredienten;
        }

        public void Add(Ingredient ingredient)
        {
            _ingredienten.Add(ingredient);
        }

        public void Delete(Ingredient ingredient)
        {
            _ingredienten.Remove(ingredient);
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _ingredienten.ToList();
        }

        public Ingredient GetById(int id)
        {
            return _ingredienten.SingleOrDefault(i => i.IngredientId == id);
        }

        public Ingredient GetByName(string name)
        {
            return _ingredienten.SingleOrDefault(i => i.Naam.ToLower() == name.ToLower());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
