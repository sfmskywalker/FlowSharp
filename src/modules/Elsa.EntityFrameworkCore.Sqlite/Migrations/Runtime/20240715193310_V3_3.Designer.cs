﻿// <auto-generated />
using System;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elsa.EntityFrameworkCore.Sqlite.Migrations.Runtime
{
    [DbContext(typeof(RuntimeElsaDbContext))]
    [Migration("20240715193310_V3_3")]
    partial class V3_3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("Elsa.KeyValues.Entities.SerializedKeyValuePair", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TenantId" }, "IX_SerializedKeyValuePair_TenantId");

                    b.ToTable("KeyValuePairs");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Entities.ActivityExecutionRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityNodeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActivityTypeVersion")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasBookmarks")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerializedActivityState")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedActivityStateCompressionAlgorithm")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedException")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedOutputs")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedPayload")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedProperties")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("StartedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowInstanceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId")
                        .HasDatabaseName("IX_ActivityExecutionRecord_ActivityId");

                    b.HasIndex("ActivityName")
                        .HasDatabaseName("IX_ActivityExecutionRecord_ActivityName");

                    b.HasIndex("ActivityNodeId")
                        .HasDatabaseName("IX_ActivityExecutionRecord_ActivityNodeId");

                    b.HasIndex("ActivityType")
                        .HasDatabaseName("IX_ActivityExecutionRecord_ActivityType");

                    b.HasIndex("ActivityTypeVersion")
                        .HasDatabaseName("IX_ActivityExecutionRecord_ActivityTypeVersion");

                    b.HasIndex("CompletedAt")
                        .HasDatabaseName("IX_ActivityExecutionRecord_CompletedAt");

                    b.HasIndex("HasBookmarks")
                        .HasDatabaseName("IX_ActivityExecutionRecord_HasBookmarks");

                    b.HasIndex("StartedAt")
                        .HasDatabaseName("IX_ActivityExecutionRecord_StartedAt");

                    b.HasIndex("Status")
                        .HasDatabaseName("IX_ActivityExecutionRecord_Status");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_ActivityExecutionRecord_TenantId");

                    b.HasIndex("WorkflowInstanceId")
                        .HasDatabaseName("IX_ActivityExecutionRecord_WorkflowInstanceId");

                    b.HasIndex("ActivityType", "ActivityTypeVersion")
                        .HasDatabaseName("IX_ActivityExecutionRecord_ActivityType_ActivityTypeVersion");

                    b.ToTable("ActivityExecutionRecords");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Entities.BookmarkQueueItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityInstanceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityTypeName")
                        .HasColumnType("TEXT");

                    b.Property<string>("BookmarkId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CorrelationId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedOptions")
                        .HasColumnType("TEXT");

                    b.Property<string>("StimulusHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowInstanceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ActivityInstanceId" }, "IX_BookmarkQueueItem_ActivityInstanceId");

                    b.HasIndex(new[] { "ActivityTypeName" }, "IX_BookmarkQueueItem_ActivityTypeName");

                    b.HasIndex(new[] { "BookmarkId" }, "IX_BookmarkQueueItem_BookmarkId");

                    b.HasIndex(new[] { "CorrelationId" }, "IX_BookmarkQueueItem_CorrelationId");

                    b.HasIndex(new[] { "CreatedAt" }, "IX_BookmarkQueueItem_CreatedAt");

                    b.HasIndex(new[] { "StimulusHash" }, "IX_BookmarkQueueItem_StimulusHash");

                    b.HasIndex(new[] { "TenantId" }, "IX_BookmarkQueueItem_TenantId");

                    b.HasIndex(new[] { "WorkflowInstanceId" }, "IX_BookmarkQueueItem_WorkflowInstanceId");

                    b.ToTable("BookmarkQueueItems");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Entities.StoredBookmark", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityInstanceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityTypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CorrelationId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedMetadata")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedPayload")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowInstanceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ActivityInstanceId" }, "IX_StoredBookmark_ActivityInstanceId");

                    b.HasIndex(new[] { "ActivityTypeName" }, "IX_StoredBookmark_ActivityTypeName");

                    b.HasIndex(new[] { "ActivityTypeName", "Hash" }, "IX_StoredBookmark_ActivityTypeName_Hash");

                    b.HasIndex(new[] { "ActivityTypeName", "Hash", "WorkflowInstanceId" }, "IX_StoredBookmark_ActivityTypeName_Hash_WorkflowInstanceId");

                    b.HasIndex(new[] { "CreatedAt" }, "IX_StoredBookmark_CreatedAt");

                    b.HasIndex(new[] { "Hash" }, "IX_StoredBookmark_Hash");

                    b.HasIndex(new[] { "TenantId" }, "IX_StoredBookmark_TenantId");

                    b.HasIndex(new[] { "WorkflowInstanceId" }, "IX_StoredBookmark_WorkflowInstanceId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("Elsa.Workflows.Runtime.Entities.StoredTrigger", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedPayload")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowDefinitionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowDefinitionVersionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Hash")
                        .HasDatabaseName("IX_StoredTrigger_Hash");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_StoredTrigger_Name");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_StoredTrigger_TenantId");

                    b.HasIndex("WorkflowDefinitionId")
                        .HasDatabaseName("IX_StoredTrigger_WorkflowDefinitionId");

                    b.HasIndex("WorkflowDefinitionVersionId")
                        .HasDatabaseName("IX_StoredTrigger_WorkflowDefinitionVersionId");

                    b.ToTable("Triggers");
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

                    b.Property<string>("ActivityName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityNodeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActivityTypeVersion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EventName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentActivityInstanceId")
                        .HasColumnType("TEXT");

                    b.Property<long>("Sequence")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerializedActivityState")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerializedPayload")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowDefinitionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowDefinitionVersionId")
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

                    b.HasIndex("ActivityName")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityName");

                    b.HasIndex("ActivityNodeId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityNodeId");

                    b.HasIndex("ActivityType")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityType");

                    b.HasIndex("ActivityTypeVersion")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityTypeVersion");

                    b.HasIndex("EventName")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_EventName");

                    b.HasIndex("ParentActivityInstanceId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ParentActivityInstanceId");

                    b.HasIndex("Sequence")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_Sequence");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_TenantId");

                    b.HasIndex("Timestamp")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_Timestamp");

                    b.HasIndex("WorkflowDefinitionId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowDefinitionId");

                    b.HasIndex("WorkflowDefinitionVersionId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowDefinitionVersionId");

                    b.HasIndex("WorkflowInstanceId")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowInstanceId");

                    b.HasIndex("WorkflowVersion")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_WorkflowVersion");

                    b.HasIndex("ActivityType", "ActivityTypeVersion")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_ActivityType_ActivityTypeVersion");

                    b.HasIndex("Timestamp", "Sequence")
                        .HasDatabaseName("IX_WorkflowExecutionLogRecord_Timestamp_Sequence");

                    b.ToTable("WorkflowExecutionLogRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
