using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lunchers.Models;
using Lunchers.Models.Domain;

namespace Lunchers.Data.Mappers
{
    public class UserMapper : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
        }
    }
}
