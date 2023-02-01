using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Domain.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParameterObjects = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ChannelID = table.Column<int>(type: "integer", nullable: false),
                    NotificationBroadcasted = table.Column<bool>(type: "boolean", nullable: false),
                    ValidUntilDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUserDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Readed = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationID = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotificationUserDetails_Notifications_NotificationID",
                        column: x => x.NotificationID,
                        principalTable: "Notifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cNotificationUserDetailEntitycUserEntity",
                columns: table => new
                {
                    NotificationDetailID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cNotificationUserDetailEntitycUserEntity", x => new { x.NotificationDetailID, x.UserID });
                    table.ForeignKey(
                        name: "FK_cNotificationUserDetailEntitycUserEntity_NotificationUserDe~",
                        column: x => x.NotificationDetailID,
                        principalTable: "NotificationUserDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cNotificationUserDetailEntitycUserEntity_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cNotificationUserDetailEntitycUserEntity_UserID",
                table: "cNotificationUserDetailEntitycUserEntity",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUserDetails_NotificationID",
                table: "NotificationUserDetails",
                column: "NotificationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cNotificationUserDetailEntitycUserEntity");

            migrationBuilder.DropTable(
                name: "NotificationUserDetails");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
