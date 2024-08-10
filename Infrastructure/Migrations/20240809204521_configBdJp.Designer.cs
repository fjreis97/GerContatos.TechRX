﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240809204521_configBdJp")]
    partial class configBdJp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("RegiaoId")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("TipoTelefoneId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegiaoId");

                    b.HasIndex("TipoTelefoneId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Contato", (string)null);
                });

            modelBuilder.Entity("Core.Entities.DDD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoDDD")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("RegiaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegiaoId");

                    b.ToTable("DDD", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Regiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Regiao", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Papel", (string)null);
                });

            modelBuilder.Entity("Core.Entities.TipoTelefone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.ToTable("TipoTelefone", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("RoleId")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Contato", b =>
                {
                    b.HasOne("Core.Entities.Regiao", "Regiao")
                        .WithMany("Contatos")
                        .HasForeignKey("RegiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.TipoTelefone", "TipoTelefone")
                        .WithMany("Contatos")
                        .HasForeignKey("TipoTelefoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Usuario", "Usuario")
                        .WithMany("Contatos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Regiao");

                    b.Navigation("TipoTelefone");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Core.Entities.DDD", b =>
                {
                    b.HasOne("Core.Entities.Regiao", "Regiao")
                        .WithMany("DDDs")
                        .HasForeignKey("RegiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.HasOne("Core.Entities.Role", "Role")
                        .WithMany("Usuarios")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Core.Entities.Regiao", b =>
                {
                    b.Navigation("Contatos");

                    b.Navigation("DDDs");
                });

            modelBuilder.Entity("Core.Entities.Role", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Core.Entities.TipoTelefone", b =>
                {
                    b.Navigation("Contatos");
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.Navigation("Contatos");
                });
#pragma warning restore 612, 618
        }
    }
}
