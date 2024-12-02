﻿// <auto-generated />
using Elsa.EntityFrameworkCore.Modules.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elsa.EntityFrameworkCore.MySql.Migrations.Tenants
{
    [DbContext(typeof(TenantsElsaDbContext))]
    [Migration("20241202095623_V3_3")]
    partial class V3_3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Elsa")
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Elsa.Common.Multitenancy.Tenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Configuration")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Tenant_Name");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_Tenant_TenantId");

                    b.ToTable("Tenants", "Elsa");
                });
#pragma warning restore 612, 618
        }
    }
}
