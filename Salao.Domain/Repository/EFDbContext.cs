﻿using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Models.Endereco;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Salao.Domain.Repository
{
    public class EFDbContext: DbContext
    {
        public EFDbContext()
            : base("SalaoConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<CliGrupoPermissao>().HasKey(x => new { x.IdGrupo, x.IdPermissao });
            modelBuilder.Entity<CliUsuarioGrupo>().HasKey(x => new { x.IdUsuario, x.IdGrupo });
            modelBuilder.Entity<GrupoPermissao>().HasKey(x => new { x.IdGrupo, x.IdPermissao });
            modelBuilder.Entity<UsuarioGrupo>().HasKey(x => new { x.IdUsuario, x.IdGrupo });
            modelBuilder.Entity<SalaoFormaPgto>().HasKey(x => new { x.IdFormaPgto, x.IdSalao });
            modelBuilder.Entity<ProfissionalServico>().HasKey(x => new { x.IdProfissional, x.IdServico });
        }

        // DbSets - admin - endereco
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<EnderecoBairro> EnderecoBairro { get; set; }
        public DbSet<EnderecoCidade> EnderecoCidade { get; set; }
        public DbSet<EnderecoEmail> EnderecoEmail { get; set; }
        public DbSet<EnderecoEstado> EnderecoEstado { get; set; }
        public DbSet<EnderecoTelefone> EnderecoTelefone { get; set; }
        public DbSet<EnderecoTipoEndereco> EnderecoTipoEndereco { get; set; }

        // DbSets - admin - outros
        public DbSet<Area> Area { get; set; }
        public DbSet<FormaPgto> FormaPgto { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<GrupoPermissao> GrupoPermissao { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<PreContato> PreContato { get; set; }
        public DbSet<SistemaParametro> SistemaParametro { get; set; }
        public DbSet<SubArea> SubArea { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioGrupo> UsuarioGrupo { get; set; }

        // DbSets - cliente
        public DbSet<CliGrupo> CliGrupo { get; set; }
        public DbSet<CliGrupoPermissao> CliGrupoPermissao { get; set; }
        public DbSet<CliPermissao> CliPermissao { get; set; }
        public DbSet<CliUsuario> CliUsuario { get; set; }
        public DbSet<CliUsuarioGrupo> CliUsuarioGrupo { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Profissional> Profissional { get; set; }
        public DbSet<ProfissionalServico> ProfissionalServico { get; set; }
        public DbSet<Models.Cliente.Salao> Salao { get; set; }
        public DbSet<SalaoFormaPgto> SalaoFormaPgto { get; set; }
        public DbSet<Servico> Servico { get; set; }
    }
}
