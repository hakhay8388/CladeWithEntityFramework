﻿// <auto-generated />
using System;
using Data.Domain.nDatabaseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Domain.Migrations
{
    [DbContext(typeof(cDatabaseContext))]
    [Migration("20230203081756_V4")]
    partial class V4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Domain.nDatabaseService.nEntities.cPaymentEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cBatchJobEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<bool>("AutoAddExecution")
                        .HasColumnType("boolean");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("ExecuteFirstWithoutWait")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxRetryCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<bool>("StopAfterFirstExecution")
                        .HasColumnType("boolean");

                    b.Property<int>("TimePeriodMilisecond")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("BatchJobs");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cBatchJobExecutionEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<long>("BatchJobID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CurrentTryCount")
                        .HasColumnType("integer");

                    b.Property<int>("ElapsedTimeMilisecond")
                        .HasColumnType("integer");

                    b.Property<string>("Exception")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ParameterObjects")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.HasIndex("BatchJobID");

                    b.ToTable("BatchJobExecutions");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cDataSourceColumnEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("ColumnName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DataSourceCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DataSourceID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("DataSourceColumns");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cDataSourcePermissionEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<bool>("CanCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanRead")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdate")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DataSourceCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DataSourceID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("DataSourcePermissions");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cDefaultDataChecksumEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("CheckSum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("DefaultDataChecksums");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cGlobalParamEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer");

                    b.Property<string>("TypeFullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("GlobalParams");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cLanguageEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IconCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cLanguageWordEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("CheckSum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("LanguageID")
                        .HasColumnType("bigint");

                    b.Property<int>("ParamCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("LanguageID");

                    b.ToTable("LanguageWords");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cMenuEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MenuTypeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("PageID")
                        .HasColumnType("bigint");

                    b.Property<long?>("RootMenuID")
                        .HasColumnType("bigint");

                    b.Property<int>("SortValue")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.HasIndex("PageID");

                    b.HasIndex("RootMenuID");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cNotificationEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<int>("ChannelID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("NotificationBroadcasted")
                        .HasColumnType("boolean");

                    b.Property<string>("ParameterObjects")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ValidUntilDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cNotificationUserDetailEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("NotificationID")
                        .HasColumnType("bigint");

                    b.Property<bool>("Readed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.HasIndex("NotificationID");

                    b.ToTable("NotificationUserDetails");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cPageEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cRoleMenuMapEntity", b =>
                {
                    b.Property<long>("cRoleEntityID")
                        .HasColumnType("bigint");

                    b.Property<long>("cMenuEntityID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("ID")
                        .HasColumnType("bigint");

                    b.Property<int>("SortValue")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("cRoleEntityID", "cMenuEntityID");

                    b.HasIndex("cMenuEntityID");

                    b.ToTable("RoleMenuMaps");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cUserDetailEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cUserEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MainPageID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UserDetailID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("MainPageID");

                    b.HasIndex("UserDetailID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cUserSessionEntity", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SessionHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("cDataSourceColumnEntitycRoleEntity", b =>
                {
                    b.Property<long>("DataSourceColumnsID")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesID")
                        .HasColumnType("bigint");

                    b.HasKey("DataSourceColumnsID", "RolesID");

                    b.HasIndex("RolesID");

                    b.ToTable("cDataSourceColumnEntitycRoleEntity");
                });

            modelBuilder.Entity("cDataSourcePermissionEntitycRoleEntity", b =>
                {
                    b.Property<long>("DataSourcePermissionsID")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesID")
                        .HasColumnType("bigint");

                    b.HasKey("DataSourcePermissionsID", "RolesID");

                    b.HasIndex("RolesID");

                    b.ToTable("cDataSourcePermissionEntitycRoleEntity");
                });

            modelBuilder.Entity("cNotificationUserDetailEntitycUserEntity", b =>
                {
                    b.Property<long>("NotificationDetailID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("NotificationDetailID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("cNotificationUserDetailEntitycUserEntity");
                });

            modelBuilder.Entity("cPageEntitycRoleEntity", b =>
                {
                    b.Property<long>("PagesID")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesID")
                        .HasColumnType("bigint");

                    b.HasKey("PagesID", "RolesID");

                    b.HasIndex("RolesID");

                    b.ToTable("cPageEntitycRoleEntity");
                });

            modelBuilder.Entity("cRoleEntitycUserEntity", b =>
                {
                    b.Property<long>("RolesID")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersID")
                        .HasColumnType("bigint");

                    b.HasKey("RolesID", "UsersID");

                    b.HasIndex("UsersID");

                    b.ToTable("cRoleEntitycUserEntity");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cBatchJobExecutionEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cBatchJobEntity", "BatchJob")
                        .WithMany("JobExecutions")
                        .HasForeignKey("BatchJobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BatchJob");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cLanguageWordEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cLanguageEntity", "Language")
                        .WithMany("Words")
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cMenuEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cPageEntity", "Page")
                        .WithMany("Menus")
                        .HasForeignKey("PageID");

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cMenuEntity", "RootMenu")
                        .WithMany()
                        .HasForeignKey("RootMenuID");

                    b.Navigation("Page");

                    b.Navigation("RootMenu");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cNotificationUserDetailEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cNotificationEntity", "Notification")
                        .WithMany("UserDetails")
                        .HasForeignKey("NotificationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cRoleMenuMapEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cMenuEntity", "Menu")
                        .WithMany("Roles")
                        .HasForeignKey("cMenuEntityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", "Role")
                        .WithMany("Menus")
                        .HasForeignKey("cRoleEntityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cUserEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cPageEntity", "MainPage")
                        .WithMany()
                        .HasForeignKey("MainPageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cUserDetailEntity", "UserDetail")
                        .WithMany()
                        .HasForeignKey("UserDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainPage");

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cUserSessionEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cUserEntity", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("cDataSourceColumnEntitycRoleEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cDataSourceColumnEntity", null)
                        .WithMany()
                        .HasForeignKey("DataSourceColumnsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cDataSourcePermissionEntitycRoleEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cDataSourcePermissionEntity", null)
                        .WithMany()
                        .HasForeignKey("DataSourcePermissionsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cNotificationUserDetailEntitycUserEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cNotificationUserDetailEntity", null)
                        .WithMany()
                        .HasForeignKey("NotificationDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cUserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cPageEntitycRoleEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cPageEntity", null)
                        .WithMany()
                        .HasForeignKey("PagesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cRoleEntitycUserEntity", b =>
                {
                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Domain.nDatabaseService.nSystemEntities.cUserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cBatchJobEntity", b =>
                {
                    b.Navigation("JobExecutions");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cLanguageEntity", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cMenuEntity", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cNotificationEntity", b =>
                {
                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cPageEntity", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cRoleEntity", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("Data.Domain.nDatabaseService.nSystemEntities.cUserEntity", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
