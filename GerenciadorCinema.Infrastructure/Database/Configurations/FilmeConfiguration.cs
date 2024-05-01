using GerenciadorCinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCinema.Infrastructure.Database.Configurations;

public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
{
    public void Configure(EntityTypeBuilder<Filme> builder)
    {
        builder.ToTable("Filmes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Diretor)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Duracao)
               .IsRequired();

        builder.HasOne(x => x.Sala)
               .WithMany(x => x.Filmes)
               .HasForeignKey(x => x.SalaId);

        builder.HasData(
            new Filme(Guid.Parse("23844d9c-0d76-477d-8fe6-dc10781ce0c7"))
            {   
                Nome = "O Poderoso Chefão",
                Diretor = "Francis Ford Coppola",
                Duracao = new TimeSpan(2, 55, 0),
                SalaId = Guid.Parse("99ef33b3-ac0f-4e96-8b61-a1faae89971b")
            },
            new Filme(Guid.Parse("d6e38674-8057-48a2-9e73-224e7a0d4d16"))
            {   
                Nome = "Forrest Gump",
                Diretor = "Robert Zemeckis",
                Duracao = new TimeSpan(2, 22, 0),
                SalaId = null
            },
            new Filme(Guid.Parse("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9"))
            {   
                Nome = "Matrix",
                Diretor = "Lana e Lilly Wachowski",
                Duracao = new TimeSpan(2, 16, 0),
                SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
            },
            new Filme(Guid.Parse("67f4cc13-8e14-4c13-8f58-404178b62fb1"))
            {   
                Nome = "Harry Potter e a Pedra Filosofal",
                Diretor = "Chris Columbus",
                Duracao = new TimeSpan(2, 32, 0),
                SalaId = Guid.Parse("48d76a83-1453-4fef-ba32-a56110e12b7e")
            }
        );

    }
}
