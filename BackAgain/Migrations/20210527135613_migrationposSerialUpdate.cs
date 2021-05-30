using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackAgain.Migrations
{
    public partial class migrationposSerialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_OrderStateEnums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderStateEnums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_POSState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__POSState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_TerminalModes",
                columns: table => new
                {
                    ModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TerminalModes", x => x.ModeId);
                });

            migrationBuilder.CreateTable(
                name: "_TerminalState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TerminalState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Theme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DefaultPrimary = table.Column<string>(nullable: false),
                    DefaultSecondary = table.Column<string>(nullable: false),
                    DefaultAccent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Theme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_TransactionState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TransactionState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_TransactionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_UpdateType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdateAble = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UpdateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    PairingId = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    SubDate = table.Column<DateTime>(nullable: false),
                    SubType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Menu",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Menu_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_POSs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: false),
                    state = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__POSs", x => x.Id);
                    table.UniqueConstraint("AK__POSs_Serial", x => x.Serial);
                    table.ForeignKey(
                        name: "FK__POSs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__POSs__POSState_state",
                        column: x => x.state,
                        principalTable: "_POSState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Transaction",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    MyPropertyId = table.Column<int>(nullable: true),
                    FailedTries = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TransactionStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Transaction__TransactionType_MyPropertyId",
                        column: x => x.MyPropertyId,
                        principalTable: "_TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Transaction__TransactionState_TransactionStateId",
                        column: x => x.TransactionStateId,
                        principalTable: "_TransactionState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Transaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PrimaryColor = table.Column<string>(nullable: true),
                    SecondaryColor = table.Column<string>(nullable: true),
                    AccentColor = table.Column<string>(nullable: true),
                    ThemeId = table.Column<int>(nullable: false),
                    BrandName = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    TerminalModeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserSettings__TerminalModes_TerminalModeId",
                        column: x => x.TerminalModeId,
                        principalTable: "_TerminalModes",
                        principalColumn: "ModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UserSettings__Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "_Theme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UserSettings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_UserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    SubscriptionId = table.Column<int>(nullable: false),
                    SubscriptionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK__UserSubscriptions__subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "_subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__UserSubscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_VersionUpdateLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    UpdateIn = table.Column<int>(nullable: false),
                    SettingsVersion = table.Column<float>(nullable: false),
                    MenuVersion = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VersionUpdateLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK__VersionUpdateLog__UpdateType_UpdateIn",
                        column: x => x.UpdateIn,
                        principalTable: "_UpdateType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__VersionUpdateLog_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Category",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MenuId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Display = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Category__Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Category_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Order",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    POSSerial = table.Column<string>(nullable: true),
                    Table = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AdditionalInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Order__POSs_POSSerial",
                        column: x => x.POSSerial,
                        principalTable: "_POSs",
                        principalColumn: "Serial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Terminals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: false),
                    state = table.Column<int>(nullable: false),
                    Table = table.Column<int>(nullable: false),
                    PosSerial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Terminals", x => x.Id);
                    table.UniqueConstraint("AK__Terminals_Serial", x => x.Serial);
                    table.ForeignKey(
                        name: "FK__Terminals__POSs_PosSerial",
                        column: x => x.PosSerial,
                        principalTable: "_POSs",
                        principalColumn: "Serial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Terminals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Terminals__TerminalState_state",
                        column: x => x.state,
                        principalTable: "_TerminalState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_MenuItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoryId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Display = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    HasOptions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MenuItem__Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__MenuItem_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Orderstatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    MyProperty = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orderstatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Orderstatus__Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Orderstatus__OrderStateEnums_State",
                        column: x => x.State,
                        principalTable: "_OrderStateEnums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_OrderTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderTransactions__Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderTransactions__Transaction_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "_Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderComment__Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_SocketConnection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionID = table.Column<string>(nullable: true),
                    PosID = table.Column<int>(nullable: true),
                    TerminalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SocketConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SocketConnection__POSs_PosID",
                        column: x => x.PosID,
                        principalTable: "_POSs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__SocketConnection__Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "_Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_TransactionAffiliate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<string>(nullable: true),
                    PosSerial = table.Column<string>(nullable: true),
                    TerminalSerial = table.Column<string>(nullable: true),
                    Affiliation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TransactionAffiliate", x => x.Id);
                    table.ForeignKey(
                        name: "FK__TransactionAffiliate__POSs_PosSerial",
                        column: x => x.PosSerial,
                        principalTable: "_POSs",
                        principalColumn: "Serial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TransactionAffiliate__Terminals_TerminalSerial",
                        column: x => x.TerminalSerial,
                        principalTable: "_Terminals",
                        principalColumn: "Serial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TransactionAffiliate__Transaction_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "_Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_ItemExtra",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    MenuItemId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Display = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ItemExtra", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ItemExtra__MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "_MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ItemExtra_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_ItemOption",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    MenuItemId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Display = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ItemOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ItemOption__MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "_MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ItemOption_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_OrderItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderId = table.Column<string>(nullable: false),
                    ItemCode = table.Column<string>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    ItemOptionId = table.Column<int>(nullable: false),
                    MyPropertyId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderItem__MenuItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "_MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderItem__ItemOption_MyPropertyId",
                        column: x => x.MyPropertyId,
                        principalTable: "_ItemOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderItem__Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_OrderItemExtras",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderItemId = table.Column<string>(nullable: false),
                    ItemExtraId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderItemExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderItemExtras__ItemExtra_ItemExtraId",
                        column: x => x.ItemExtraId,
                        principalTable: "_ItemExtra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderItemExtras__OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "_OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Category_MenuId",
                table: "_Category",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX__Category_UserId",
                table: "_Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__ItemExtra_MenuItemId",
                table: "_ItemExtra",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX__ItemExtra_UserId",
                table: "_ItemExtra",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__ItemOption_MenuItemId",
                table: "_ItemOption",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX__ItemOption_UserId",
                table: "_ItemOption",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Menu_UserId",
                table: "_Menu",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__MenuItem_CategoryId",
                table: "_MenuItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX__MenuItem_UserId",
                table: "_MenuItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Order_POSSerial",
                table: "_Order",
                column: "POSSerial");

            migrationBuilder.CreateIndex(
                name: "IX__Order_UserId",
                table: "_Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_ItemId",
                table: "_OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_MyPropertyId",
                table: "_OrderItem",
                column: "MyPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_OrderId",
                table: "_OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItemExtras_ItemExtraId",
                table: "_OrderItemExtras",
                column: "ItemExtraId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItemExtras_OrderItemId",
                table: "_OrderItemExtras",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX__Orderstatus_OrderId",
                table: "_Orderstatus",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX__Orderstatus_State",
                table: "_Orderstatus",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX__OrderTransactions_OrderId",
                table: "_OrderTransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderTransactions_TransactionID",
                table: "_OrderTransactions",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX__POSs_UserId",
                table: "_POSs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__POSs_state",
                table: "_POSs",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "IX__SocketConnection_PosID",
                table: "_SocketConnection",
                column: "PosID",
                unique: true,
                filter: "[PosID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX__SocketConnection_TerminalId",
                table: "_SocketConnection",
                column: "TerminalId",
                unique: true,
                filter: "[TerminalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX__Terminals_PosSerial",
                table: "_Terminals",
                column: "PosSerial");

            migrationBuilder.CreateIndex(
                name: "IX__Terminals_UserId",
                table: "_Terminals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Terminals_state",
                table: "_Terminals",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "IX__Transaction_MyPropertyId",
                table: "_Transaction",
                column: "MyPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX__Transaction_TransactionStateId",
                table: "_Transaction",
                column: "TransactionStateId");

            migrationBuilder.CreateIndex(
                name: "IX__Transaction_UserId",
                table: "_Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__TransactionAffiliate_PosSerial",
                table: "_TransactionAffiliate",
                column: "PosSerial");

            migrationBuilder.CreateIndex(
                name: "IX__TransactionAffiliate_TerminalSerial",
                table: "_TransactionAffiliate",
                column: "TerminalSerial");

            migrationBuilder.CreateIndex(
                name: "IX__TransactionAffiliate_TransactionID",
                table: "_TransactionAffiliate",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX__UserSettings_TerminalModeId",
                table: "_UserSettings",
                column: "TerminalModeId");

            migrationBuilder.CreateIndex(
                name: "IX__UserSettings_ThemeId",
                table: "_UserSettings",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX__UserSettings_UserId",
                table: "_UserSettings",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX__UserSubscriptions_SubscriptionId",
                table: "_UserSubscriptions",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX__UserSubscriptions_UserId",
                table: "_UserSubscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__VersionUpdateLog_UpdateIn",
                table: "_VersionUpdateLog",
                column: "UpdateIn");

            migrationBuilder.CreateIndex(
                name: "IX__VersionUpdateLog_UserId",
                table: "_VersionUpdateLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderComment_OrderId",
                table: "OrderComment",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_OrderItemExtras");

            migrationBuilder.DropTable(
                name: "_Orderstatus");

            migrationBuilder.DropTable(
                name: "_OrderTransactions");

            migrationBuilder.DropTable(
                name: "_SocketConnection");

            migrationBuilder.DropTable(
                name: "_TransactionAffiliate");

            migrationBuilder.DropTable(
                name: "_UserSettings");

            migrationBuilder.DropTable(
                name: "_UserSubscriptions");

            migrationBuilder.DropTable(
                name: "_VersionUpdateLog");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderComment");

            migrationBuilder.DropTable(
                name: "_ItemExtra");

            migrationBuilder.DropTable(
                name: "_OrderItem");

            migrationBuilder.DropTable(
                name: "_OrderStateEnums");

            migrationBuilder.DropTable(
                name: "_Terminals");

            migrationBuilder.DropTable(
                name: "_Transaction");

            migrationBuilder.DropTable(
                name: "_TerminalModes");

            migrationBuilder.DropTable(
                name: "_Theme");

            migrationBuilder.DropTable(
                name: "_subscriptions");

            migrationBuilder.DropTable(
                name: "_UpdateType");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "_ItemOption");

            migrationBuilder.DropTable(
                name: "_Order");

            migrationBuilder.DropTable(
                name: "_TerminalState");

            migrationBuilder.DropTable(
                name: "_TransactionType");

            migrationBuilder.DropTable(
                name: "_TransactionState");

            migrationBuilder.DropTable(
                name: "_MenuItem");

            migrationBuilder.DropTable(
                name: "_POSs");

            migrationBuilder.DropTable(
                name: "_Category");

            migrationBuilder.DropTable(
                name: "_POSState");

            migrationBuilder.DropTable(
                name: "_Menu");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
