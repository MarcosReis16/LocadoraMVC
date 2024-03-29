﻿using Locadora.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Contexto
{
    public class LocadoraContext : DbContext
    {

        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
        {

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>()
                .ToTable("Filme");

            modelBuilder.Entity<Filme>()
                .HasKey(m => m.IdFilme)
                .HasName("IdFilme");

            modelBuilder.Entity<Filme>().Property(m => m.NomeFilme)
                .HasColumnName("NomeFilme")
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<Filme>().Property(m => m.GeneroFilme)
                .HasColumnName("GeneroFilme")
                .HasColumnType("integer")
                .IsRequired();


            modelBuilder.Entity<Filme>().Property(m => m.FaixaEtariaFilme)
                .HasColumnName("FaixaEtaria")
                .HasColumnType("integer")
                .IsRequired();

            modelBuilder.Entity<Filme>().Property(m => m.ValorEmprestimo)
                .HasColumnName("ValorEmprestimo")
                .HasColumnType("money")
                .IsRequired();

            modelBuilder.Entity<Filme>().Property(m => m.QtdEstoque)
                .HasColumnName("QtdEstoque")
                .HasColumnType("integer")
                .IsRequired();

            //--------------------------------------------------------------------------------------------

            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente");

            modelBuilder.Entity<Cliente>()
                .HasKey(m => m.IdCliente)
                .HasName("IdCliente");

            modelBuilder.Entity<Cliente>().Property(m => m.NomeCliente)
                .HasColumnName("NomeCliente")
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<Cliente>().Property(m => m.CPFCliente)
                .HasColumnName("CPFCliente")
                .HasColumnType("varchar")
                .IsRequired();

            modelBuilder.Entity<Cliente>().Property(m => m.CEP)
                .HasColumnName("CEP")
                .HasColumnType("varchar");

            modelBuilder.Entity<Cliente>().Property(m => m.Endereco)
                .HasColumnName("Endereco")
                .HasColumnType("varchar");

            modelBuilder.Entity<Cliente>().Property(m => m.Bairro)
                .HasColumnName("Bairro")
                .HasColumnType("varchar");

            modelBuilder.Entity<Cliente>().Property(m => m.Cidade)
                .HasColumnName("Cidade")
                .HasColumnType("varchar");

            modelBuilder.Entity<Cliente>().Property(m => m.Estado)
                .HasColumnName("Estado")
                .HasColumnType("varchar");
            //------------------------------------------------------------------------------------------------

            modelBuilder.Entity<Aluguel>()
                .ToTable("Aluguel");

            modelBuilder.Entity<Aluguel>()
                .HasKey(m => m.IdAluguel)
                .HasName("IdAluguel");

            modelBuilder.Entity<Aluguel>()
                .HasOne(m => m.Cliente)
                .WithMany(m => m.Alugueis)
                .HasForeignKey(m => m.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Aluguel>().Property(m => m.ValorTotal)
                .HasColumnName("ValorTotal")
                .HasColumnType("money")
                .IsRequired();

            modelBuilder.Entity<Aluguel>().Property(m => m.DataEmprestimo)
                .HasColumnName("DataEmprestimo")
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Aluguel>().Property(m => m.DataDevolucao)
                .HasColumnName("DataDevolucao")
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Aluguel>().Property(m => m.StatusEmprestimo)
                .HasColumnName("StatusEmprestimo")
                .HasColumnType("boolean")
                .IsRequired();

            //------------------------------------------------------------------------------------------------

            modelBuilder.Entity<AluguelFilme>()
                .ToTable("AluguelFilme");

            modelBuilder.Entity<AluguelFilme>()
                .HasKey(m => new { m.IdAluguel, m.IdFilme });

            modelBuilder.Entity<AluguelFilme>()
                .HasOne(m => m.Aluguel)
                .WithMany(m => m.AluguelFilmes)
                .HasForeignKey(m => m.IdAluguel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AluguelFilme>()
                .HasOne(m => m.Filme)
                .WithMany(m => m.AluguelFilmes)
                .HasForeignKey(m => m.IdFilme)
                .OnDelete(DeleteBehavior.Cascade);


        }


    }
}
