using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetAll();
        Ingredient GetById(int id);
        Ingredient GetByName(string name);
        void Add(Ingredient ingredient);
        void Delete(Ingredient ingredient);
        void SaveChanges();
    }
}
