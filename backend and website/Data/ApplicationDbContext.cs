using System;
using Microsoft.EntityFrameworkCore;
using Lunchers.Models;
using Lunchers.Models.Domain;

namespace Lunchers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Afbeelding> Afbeeldingen { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Handelaar> Handelaars { get; set; }
        public DbSet<Ingredient> Ingredienten { get; set; }
        public DbSet<IngredientInLunch> IngredientInLunch { get; set; }
        public DbSet<Lunch> Lunches { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<Rol> Rollen { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagInLunch> TagsInLunch { get; set; }
        public DbSet<Favoriet> Favorieten { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}