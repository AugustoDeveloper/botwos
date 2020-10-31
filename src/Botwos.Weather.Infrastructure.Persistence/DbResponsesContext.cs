using Botwos.Weather.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Botwos.Weather.Infrastructure.Persistence
{
    public class DbResponsesContext : DbContext
    {
        public DbSet<Greeting> Greetings { get; set; }
        public DbSet<InitialPhrase> InitialPhrases { get; set; }
        public DbSet<FinalPhrase> FinalPhrases { get; set; }
        public DbResponsesContext(DbContextOptions<DbResponsesContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Greeting>(m =>
                {
                    m.ToTable("greetings");
                    m.HasKey(p => p.Id);
                    m
                        .Property(x => x.Id)
                        .UseIdentityAlwaysColumn()
                        .HasColumnName("id");
                    m
                        .Property(x => x.Kind)
                        .IsRequired()
                        .HasDefaultValue(GreetingKind.Success)
                        .HasColumnName("kind");
                    m
                        .Property(x => x.ShortCode)
                        .HasMaxLength(15)
                        .IsRequired()
                        .HasColumnName("short_code");
                    m
                        .Property(x => x.Description)
                        .HasMaxLength(500)
                        .HasColumnName("description");
                    m
                        .Property(x => x.TextFormat)
                        .HasMaxLength(500)
                        .IsRequired()
                        .HasColumnName("text_format");
                    m
                        .Property(x => x.Language)
                        .HasMaxLength(10)
                        .IsRequired()
                        .HasColumnName("language");
                    m
                        .Property(x => x.Name)
                        .HasMaxLength(50)
                        .IsRequired()
                        .HasColumnName("name");
                    m
                        .HasIndex(x => new { x.ShortCode, x.Kind, x.Language })
                        .HasName("ix_uq_greetings_short_code")
                        .IsUnique();
                })
                .Entity<InitialPhrase>(m =>
                {
                    m.ToTable("initial_phrases");
                    m.HasKey(p => p.Id);
                    m
                        .Property(x => x.Id)
                        .UseIdentityAlwaysColumn()
                        .HasColumnName("id");
                    m
                        .Property(x => x.ShortCode)
                        .HasMaxLength(15)
                        .IsRequired()
                        .HasColumnName("short_code");
                    m
                        .Property(x => x.Description)
                        .HasMaxLength(500)
                        .HasColumnName("description");
                    m
                        .Property(x => x.TextFormat)
                        .HasMaxLength(500)
                        .IsRequired()
                        .HasColumnName("text_format");
                    m
                        .Property(x => x.Language)
                        .HasMaxLength(10)
                        .IsRequired()
                        .HasColumnName("language");
                    m
                        .Property(x => x.Name)
                        .HasMaxLength(50)
                        .IsRequired()
                        .HasColumnName("name");
                    m
                        .Property(x => x.BeginFeelsLikeCelsiusRange)
                        .IsRequired()
                        .HasColumnName("begin_feels_like_celsius_range");
                    m
                        .Property(x => x.EndFeelsLikeCelsiusRange)
                        .IsRequired()
                        .HasColumnName("end_feels_like_celsius_range");
                })
                .Entity<FinalPhrase>(m =>
                {
                    m.ToTable("final_phrases");
                    m.HasKey(p => p.Id);
                    m
                        .Property(x => x.Id)
                        .UseIdentityAlwaysColumn()
                        .HasColumnName("id");
                    m
                        .Property(x => x.ShortCode)
                        .HasMaxLength(15)
                        .IsRequired()
                        .HasColumnName("short_code");
                    m
                        .Property(x => x.Description)
                        .HasMaxLength(500)
                        .HasColumnName("description");
                    m
                        .Property(x => x.TextFormat)
                        .HasMaxLength(500)
                        .IsRequired()
                        .HasColumnName("text_format");
                    m
                        .Property(x => x.Language)
                        .HasMaxLength(10)
                        .IsRequired()
                        .HasColumnName("language");
                    m
                        .Property(x => x.Name)
                        .HasMaxLength(50)
                        .IsRequired()
                        .HasColumnName("name");
                    m
                        .Property(x => x.BeginCloudPercentageRange)
                        .HasColumnName("begin_cloud_percentage_range")
                        .IsRequired();
                    m
                        .Property(x => x.EndCloudPercentageRange)
                        .HasColumnName("end_cloud_percentage_range")
                        .IsRequired();
                    m
                        .Property(x => x.BeginPrecipitationMMRange)
                        .HasColumnName("begin_precipitation_mm_range")
                        .IsRequired();
                    m
                        .Property(x => x.EndPrecipitationMMRange)
                        .HasColumnName("end_precipitation_mm_range")
                        .IsRequired();
                });
        }
    }
}

// ex.: Se liga só, MATIAS, o clima hoje tá uma uva com 20ºC. Vem chuva por aí no Rio de janeiro, não esquece o guarda-chuva!
// Success Initial Greeting
// - Se liga só {name},
// - Cóé cria {name},
// - Salve, Salve {name},
// - E aí XSCHOFEM {name}!?
// - Firmeza, {name}!?
// - Te dá o papo {name},
// - Fala parelha {name},
// - Se achegue {name},
// - Benção {name},
// - Fala fiel {name},

// Success first part phrase for tempC > 30 - QUENTE
/// - Só uma gelada nesse calor de {1}°C!
/// - Com {1}°C até na sombra ta quente para cacete!
/// - Já dá pra fritar um ovo na calçada com {1}°C!
/// - Que isso fera! Humano nenhum aguenta {1}°C!
/// - Piscininha amor, piscininha amor! Nesse calor de {1}°C, só uma piscininha.
/// - I A Í, RAPAZIADAAAA! Já liga para rapaziada e pede uma cerva, que tá {1}°C
/// - Tá Calor, Tá quente, {1}°C, Tá Calor, Tá quente!
/// - {1}°C, partiu para uma praiana!?
/// - {1}°C é bom para um churrasco na laje, fala tu!?
// Success first part phrase for tempC > 10 - GOSTOSIN
/// - O clima hoje tá uma uva com {1}°C.
/// - {1}°C tá gostosin!!!
/// - Tá bom, tá bom! Vamo que vamos nesse clima de {1}°C!
/// - Hoje tá {1}°C, tempinho bão sô!
/// - Tempo tá bom ein!? {1}°C, Cê acredita!?
// Success first part phrase for tempC < 10 - FRIO PRA CARAI
/// - Bota um casaco, porque {1}°C não rola sair brusinha!
/// - Méldélzis, {1}°C! Que frio é esse!?
/// - Avisa ao Geime ofi Tronos que o Tal Winder ou Winderson ta chegando com {1}°C!
/// - Vai sair!? Tá {1}°C! Pensa direito nisso aí, já é!?
/// - Ixi, {1}°C FRIO DA PORRA!
/// - POURRA BICHO! Frio surreal de {1}ºC!
/// - {1}°C!?!?!?! Tá mais frio lá fora do que na minha geladeira!
/// - {1}°C é frio de frigorifico!
/// - Um frio de {1}°C tá cngelndo me- tclad-!
/// - Corre Bino, que um frio {1}°C é cilada!