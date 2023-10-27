﻿// <auto-generated />
using System;
using Elsa.EntityFrameworkCore.Modules.Alterations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Elsa.EntityFrameworkCore.PostgreSql.Migrations.Alterations
{
    [DbContext(typeof(AlterationsElsaDbContext))]
    partial class AlterationsElsaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Elsa")
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Elsa.Alterations.Core.Entities.AlterationJob", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PlanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SerializedLog")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .HasColumnType("text");

                    b.Property<string>("WorkflowInstanceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompletedAt")
                        .HasDatabaseName("IX_AlterationJob_CompletedAt");

                    b.HasIndex("CreatedAt")
                        .HasDatabaseName("IX_AlterationJob_CreatedAt");

                    b.HasIndex("PlanId")
                        .HasDatabaseName("IX_AlterationJob_PlanId");

                    b.HasIndex("StartedAt")
                        .HasDatabaseName("IX_AlterationJob_StartedAt");

                    b.HasIndex("Status")
                        .HasDatabaseName("IX_AlterationJob_Status");

                    b.HasIndex("WorkflowInstanceId")
                        .HasDatabaseName("IX_AlterationJob_WorkflowInstanceId");

                    b.ToTable("AlterationJobs", "Elsa");
                });

            modelBuilder.Entity("Elsa.Alterations.Core.Entities.AlterationPlan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SerializedAlterations")
                        .HasColumnType("text");

                    b.Property<string>("SerializedWorkflowInstanceIds")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompletedAt")
                        .HasDatabaseName("IX_AlterationPlan_CompletedAt");

                    b.HasIndex("CreatedAt")
                        .HasDatabaseName("IX_AlterationPlan_CreatedAt");

                    b.HasIndex("StartedAt")
                        .HasDatabaseName("IX_AlterationPlan_StartedAt");

                    b.HasIndex("Status")
                        .HasDatabaseName("IX_AlterationPlan_Status");

                    b.ToTable("AlterationPlans", "Elsa");
                });
#pragma warning restore 612, 618
        }
    }
}
