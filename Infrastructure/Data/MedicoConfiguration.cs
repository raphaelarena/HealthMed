using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Data
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome).IsRequired().HasMaxLength(100);
            builder.Property(m => m.CPF).IsRequired().HasMaxLength(11);
            builder.Property(m => m.CRM).IsRequired().HasMaxLength(20);
        }
    }
}
