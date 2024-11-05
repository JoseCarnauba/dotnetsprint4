using global::Sprint4.Models; // Importando o namespace para os modelos
using Microsoft.EntityFrameworkCore; // Importando o namespace do Entity Framework Core
using System.Collections.Generic; // Importando para o uso de listas e coleções

namespace Sprint4.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet para a entidade User
        public DbSet<User> Users { get; set; }

        // DbSet para outra entidade, exemplo Product (adicional)
        public DbSet<Product> Products { get; set; }

        // Método para configurar o modelo (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações para a entidade User
            modelBuilder.Entity<User>()
                .ToTable("Users") // Nome da tabela
                .HasKey(u => u.Id); // Definindo a chave primária

            // Adicionando configurações para a entidade Product (opcional)
            modelBuilder.Entity<Product>()
                .ToTable("Products") // Nome da tabela
                .HasKey(p => p.Id); // Definindo a chave primária
        }
    }
}
