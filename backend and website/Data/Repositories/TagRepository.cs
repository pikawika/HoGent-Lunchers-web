using Lunchers.Models;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DbSet<Tag> _tags;
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
            _tags = context.Tags;
        }

        public void Add(Tag tag)
        {
            _tags.Add(tag);
        }

        public void Delete(Tag tag)
        {
            _tags.Remove(tag);
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _tags.SingleOrDefault(t => t.TagId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
