using GerenciadorCinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCinema.Infrastructure.Database.Configurations;

public class SalaConfiguration : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.ToTable("Salas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NumeroSala)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Descricao)
               .IsRequired()
               .HasMaxLength(250);

        builder.HasMany(x => x.Filmes)
               .WithOne(x => x.Sala)
               .HasForeignKey(x => x.SalaId);

        builder.HasData(
            new Sala(Guid.Parse("99ef33b3-ac0f-4e96-8b61-a1faae89971b"))
            {   
                NumeroSala = "Sala 1",
                Descricao = "Sala principal com capacidade para 150 pessoas"
            },
            new Sala(Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336"))
            {   
                NumeroSala = "Sala 2",
                Descricao = "Sala VIP com assentos reclináveis e serviço de bar"
            },
            new Sala(Guid.Parse("48d76a83-1453-4fef-ba32-a56110e12b7e"))
            {   
                NumeroSala = "Sala 3",
                Descricao = "Sala com projeção 3D"
            }
        );
    }
}

