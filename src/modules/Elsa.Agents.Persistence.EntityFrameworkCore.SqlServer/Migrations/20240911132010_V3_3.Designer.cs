﻿// <auto-generated />
using Elsa.Agents.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elsa.Agents.Persistence.EntityFrameworkCore.SqlServer.Migrations
{
    [DbContext(typeof(AgentsDbContext))]
    [Migration("20240911132010_V3_3")]
    partial class V3_3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Elsa")
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Elsa.Agents.Persistence.Entities.AgentDefinition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgentConfig")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_AgentDefinition_Name");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_AgentDefinition_TenantId");

                    b.ToTable("AgentDefinitions", "Elsa");
                });

            modelBuilder.Entity("Elsa.Agents.Persistence.Entities.ApiKeyDefinition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_ApiKeyDefinition_Name");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_ApiKeyDefinition_TenantId");

                    b.ToTable("ApiKeysDefinitions", "Elsa");
                });

            modelBuilder.Entity("Elsa.Agents.Persistence.Entities.ServiceDefinition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Settings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_ServiceDefinition_Name");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_ServiceDefinition_TenantId");

                    b.ToTable("ServicesDefinitions", "Elsa");
                });
#pragma warning restore 612, 618
        }
    }
}
