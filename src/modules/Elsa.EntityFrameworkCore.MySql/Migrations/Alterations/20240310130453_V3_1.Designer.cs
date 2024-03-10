﻿// <auto-generated />
using System;
using Elsa.EntityFrameworkCore.Modules.Alterations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elsa.EntityFrameworkCore.MySql.Migrations.Alterations
{
    [DbContext(typeof(AlterationsElsaDbContext))]
    [Migration("20240310130453_V3_1")]
    partial class V3_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Elsa")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Elsa.Alterations.Core.Entities.AlterationJob", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PlanId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SerializedLog")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WorkflowInstanceId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

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

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_AlterationJob_TenantId");

                    b.HasIndex("WorkflowInstanceId")
                        .HasDatabaseName("IX_AlterationJob_WorkflowInstanceId");

                    b.ToTable("AlterationJobs", "Elsa");
                });

            modelBuilder.Entity("Elsa.Alterations.Core.Entities.AlterationPlan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SerializedAlterations")
                        .HasColumnType("longtext");

                    b.Property<string>("SerializedWorkflowInstanceIds")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("StartedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenantId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CompletedAt")
                        .HasDatabaseName("IX_AlterationPlan_CompletedAt");

                    b.HasIndex("CreatedAt")
                        .HasDatabaseName("IX_AlterationPlan_CreatedAt");

                    b.HasIndex("StartedAt")
                        .HasDatabaseName("IX_AlterationPlan_StartedAt");

                    b.HasIndex("Status")
                        .HasDatabaseName("IX_AlterationPlan_Status");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_AlterationPlan_TenantId");

                    b.ToTable("AlterationPlans", "Elsa");
                });
#pragma warning restore 612, 618
        }
    }
}