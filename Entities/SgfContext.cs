using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SGFBackend.Entities
{
    public partial class SgfContext : DbContext
    {
        public SgfContext()
        {
        }

        public SgfContext(DbContextOptions<SgfContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Exercicio> Exercicios { get; set; }
        public virtual DbSet<ExerciciosAluno> ExerciciosAluno { get; set; }
        public virtual DbSet<Professor> Professores { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;userid=root;pwd=abc@123;database=sgf");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.Idaluno)
                    .HasName("PRIMARY");

                entity.ToTable("aluno");

                entity.HasIndex(e => e.Idaluno)
                    .HasName("fk_aluno_user1_idx");

                entity.Property(e => e.Idaluno)
                    .HasColumnName("idaluno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Altura).HasColumnName("altura");

                entity.Property(e => e.Doenca)
                    .HasColumnName("doenca")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Idade)
                    .HasColumnName("idade")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdalunoNavigation)
                    .WithOne(p => p.Aluno)
                    .HasForeignKey<Aluno>(d => d.Idaluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_aluno_user1");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PRIMARY");

                entity.ToTable("categoria");

                entity.Property(e => e.Idcategoria)
                    .HasColumnName("idcategoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Exercicio>(entity =>
            {
                entity.HasKey(e => e.Idexercicio)
                    .HasName("PRIMARY");

                entity.ToTable("exercicio");

                entity.HasIndex(e => e.Idcategoria)
                    .HasName("fk_exercicio_categoria_exerc_idx");

                entity.Property(e => e.Idexercicio)
                    .HasColumnName("idexercicio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idcategoria)
                    .HasColumnName("idcategoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Exercicio)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_exercicio_categoria_exerc");
            });

            modelBuilder.Entity<ExerciciosAluno>(entity =>
            {
                entity.HasKey(e => e.IdexerciciosAluno)
                    .HasName("PRIMARY");

                entity.ToTable("exercicios_aluno");

                entity.HasIndex(e => e.Idaluno)
                    .HasName("fk_exercicio_ficha_aluno1_idx");

                entity.HasIndex(e => e.Idexercicio)
                    .HasName("fk_exercicio_ficha_exercicio1_idx");

                entity.Property(e => e.IdexerciciosAluno)
                    .HasColumnName("idexercicios_aluno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idaluno)
                    .HasColumnName("idaluno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idexercicio)
                    .HasColumnName("idexercicio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Repeticoes)
                    .IsRequired()
                    .HasColumnName("repeticoes")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdalunoNavigation)
                    .WithMany(p => p.ExerciciosAluno)
                    .HasForeignKey(d => d.Idaluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_exercicio_ficha_aluno1");

                entity.HasOne(d => d.IdexercicioNavigation)
                    .WithMany(p => p.ExerciciosAluno)
                    .HasForeignKey(d => d.Idexercicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_exercicio_ficha_exercicio1");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.Idprofessor)
                    .HasName("PRIMARY");

                entity.ToTable("professor");

                entity.HasIndex(e => e.Idprofessor)
                    .HasName("fk_professor_user1_idx");

                entity.Property(e => e.Idprofessor)
                    .HasColumnName("idprofessor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cref)
                    .HasColumnName("cref")
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdprofessorNavigation)
                    .WithOne(p => p.Professor)
                    .HasForeignKey<Professor>(d => d.Idprofessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_professor_user1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Iduser)
                    .HasColumnName("iduser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasColumnType("enum('Professor','Aluno','Admin')")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
