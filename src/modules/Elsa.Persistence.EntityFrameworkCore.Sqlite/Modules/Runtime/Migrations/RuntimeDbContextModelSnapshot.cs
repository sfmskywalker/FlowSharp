﻿// <auto-generated />
using System;
using Elsa.Persistence.EntityFrameworkCore.Modules.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elsa.Persistence.EntityFrameworkCore.Sqlite.Modules.Runtime.Migrations
{
    [DbContext(typeof(RuntimeDbContext))]
    partial class RuntimeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Elsa.Workflows.Core.State.WorkflowState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CorrelationId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("DefinitionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SubStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CorrelationId")
                        .HasDatabaseName("IX_WorkflowState_CorrelationId");

                    b.HasIndex("CreatedAt")
                        .HasDatabaseName("IX_WorkflowInstance_CreatedAt");

                    b.HasIndex("DefinitionId")
                        .HasDatabaseName("IX_WorkflowState_DefinitionId");

                    b.HasIndex("UpdatedAt")
                        .HasDatabaseName("IX_WorkflowInstance_UpdatedAt");

                    b.HasIndex("Status", "DefinitionId")
                        .HasDatabaseName("IX_WorkflowInstance_Status_DefinitionId");

                    b.HasIndex("Status", "SubStatus")
                        .HasDatabaseName("IX_WorkflowInstance_Status_SubStatus");

                    b.HasIndex("Status", "SubStatus", "DefinitionId", "Version")
                        .HasDatabaseName("IX_WorkflowInstance_Status_SubStatus_DefinitionId_Version");

                    b.ToTable("WorkflowStates");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Entities.WorkflowExecutionLogRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityInstanceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EventName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentActivityInstanceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PayloadData")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowDefinitionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowInstanceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WorkflowVersion")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityId");

                    b.HasIndex("ActivityInstanceId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityInstanceId");

                    b.HasIndex("ActivityType")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityType");

                    b.HasIndex("EventName")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_EventName");

                    b.HasIndex("ParentActivityInstanceId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ParentActivityInstanceId");

                    b.HasIndex("Timestamp")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_Timestamp");

                    b.HasIndex("WorkflowDefinitionId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowDefinitionId");

                    b.HasIndex("WorkflowInstanceId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowInstanceId");

                    b.HasIndex("WorkflowVersion")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowVersion");

                    b.ToTable("WorkflowExecutionLogRecords");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Entities.WorkflowTrigger", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowDefinitionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Hash")
                        .HasDatabaseName("IX_WorkflowTrigger_Hash");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_WorkflowTrigger_Name");

                    b.HasIndex("WorkflowDefinitionId")
                        .HasDatabaseName("IX_WorkflowTrigger_WorkflowDefinitionId");

                    b.ToTable("WorkflowTriggers");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Models.StoredBookmark", b =>
                {
                    b.Property<string>("BookmarkId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityTypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowInstanceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BookmarkId");

                    b.HasIndex(new[] { "ActivityTypeName" }, "IX_StoredBookmark_ActivityTypeName");

                    b.HasIndex(new[] { "ActivityTypeName", "Hash" }, "IX_StoredBookmark_ActivityTypeName_Hash");

                    b.HasIndex(new[] { "ActivityTypeName", "Hash", "WorkflowInstanceId" }, "IX_StoredBookmark_ActivityTypeName_Hash_WorkflowInstanceId");

                    b.HasIndex(new[] { "Hash" }, "IX_StoredBookmark_Hash");

                    b.HasIndex(new[] { "WorkflowInstanceId" }, "IX_StoredBookmark_WorkflowInstanceId");

                    b.ToTable("Bookmarks");
                });
#pragma warning restore 612, 618
        }
    }
}
