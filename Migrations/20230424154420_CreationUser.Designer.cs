﻿// <auto-generated />
using System;
using Api_Ayanet_2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api_Ayanet_2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230424154420_CreationUser")]
    partial class CreationUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api_Ayanet_2.Entities.Clientes", b =>
                {
                    b.Property<string>("cod_cliente")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("bloqueado")
                        .HasColumnType("bit");

                    b.Property<string>("cod_postal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contacto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha_export")
                        .HasColumnType("datetime2");

                    b.Property<string>("localidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pass_cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("provincia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cod_cliente");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Api_Ayanet_2.Entities.Direcciones", b =>
                {
                    b.Property<string>("cod_direccion")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("clientecod_cliente")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("cod_cliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha_export")
                        .HasColumnType("datetime2");

                    b.HasKey("cod_direccion");

                    b.HasIndex("clientecod_cliente");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("Api_Ayanet_2.Entities.Productos", b =>
                {
                    b.Property<string>("cod_producto")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("bloqueado")
                        .HasColumnType("bit");

                    b.Property<string>("calculo_caducidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cod_seguimiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha_export")
                        .HasColumnType("datetime2");

                    b.Property<string>("formato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("naturaleza")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipo_formato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipo_materia_fabrica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cod_producto");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Api_Ayanet_2.Entities.Users", b =>
                {
                    b.Property<string>("cod_user")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cod_user");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Api_Ayanet_2.Entities.Direcciones", b =>
                {
                    b.HasOne("Api_Ayanet_2.Entities.Clientes", "cliente")
                        .WithMany("direcciones")
                        .HasForeignKey("clientecod_cliente");

                    b.Navigation("cliente");
                });

            modelBuilder.Entity("Api_Ayanet_2.Entities.Clientes", b =>
                {
                    b.Navigation("direcciones");
                });
#pragma warning restore 612, 618
        }
    }
}