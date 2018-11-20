using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
        Tag GetByName(string name);
        void Add(Tag tag);
        void Delete(Tag tag);
        void SaveChanges();
    }
}
