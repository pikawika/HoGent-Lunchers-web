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
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Administrator> Adminstratoren { get; set; }
        public DbSet<Handelaar> Handelaars { get; set; }
        public DbSet<Ingredient> Ingredienten { get; set; }
        public DbSet<Lunch> Lunches { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<Rol> Rollen { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Favoriet> Favorieten { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Gebruiker>()
                .HasOne(g => g.Login)
                .WithOne(l => l.gebruiker)
                .HasForeignKey<Login>(b => b.gebruikerLoginId);

            //BEGIN INGREDIENT LUNCH
            modelBuilder.Entity<LunchIngredient>()
                .HasKey(li => new { li.LunchId, li.IngredientId });

            modelBuilder.Entity<LunchIngredient>()
                .HasOne(li => li.Lunch)
                .WithMany(l => l.LunchIngredienten)
                .HasForeignKey(li => li.LunchId);

            modelBuilder.Entity<LunchIngredient>()
                .HasOne(bc => bc.Ingredient)
                .WithMany(i => i.LunchIngredienten)
                .HasForeignKey(bc => bc.IngredientId);
            //EINDE INGREDIENT LUNCH

            //BEGIN TAG LUNCH
            modelBuilder.Entity<LunchTag>()
                .HasKey(lt => new { lt.LunchId, lt.TagId });

            modelBuilder.Entity<LunchTag>()
                .HasOne(lt => lt.Lunch)
                .WithMany(l => l.LunchTags)
                .HasForeignKey(lt => lt.LunchId);

            modelBuilder.Entity<LunchTag>()
                .HasOne(lt => lt.Tag)
                .WithMany(t => t.LunchTag)
                .HasForeignKey(lt => lt.TagId);
            //EINDE TAG LUNCH

            //BEGIN LUNCH HANDELAAR
            modelBuilder.Entity<Lunch>()
                .HasOne(l => l.Handelaar)
                .WithMany(h => h.Lunches);
            //EINDE LUNCH HANDELAAR

            //BEGIN RESERVATIE KLANT
            modelBuilder.Entity<Reservatie>()
                .HasOne(r => r.Klant)
                .WithMany(k => k.Reservaties);
            //EINDE RESERVATIE KLANT

            //BEGIN FAVORIET KLANT
            modelBuilder.Entity<Favoriet>()
                .HasOne(f => f.Klant)
                .WithMany(k => k.Favorieten);
            //EINDE FAVORIET KLANT
        }
    }
}