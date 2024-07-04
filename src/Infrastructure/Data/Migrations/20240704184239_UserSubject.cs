using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wowtogo");

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    StartsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    FromDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    LeastAmountBuy = table.Column<int>(type: "integer", nullable: false),
                    MostAmountBuy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    TicketTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalSchema: "wowtogo",
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "wowtogo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationName = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizers_Users_Id",
                        column: x => x.Id,
                        principalSchema: "wowtogo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    BackgroundImageUrl = table.Column<string>(type: "text", nullable: false),
                    BannerImageUrl = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OrganizerId = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxTickets = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalSchema: "wowtogo",
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "wowtogo",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendees_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "wowtogo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventCategories",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "wowtogo",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCategories_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "wowtogo",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StartsAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndsAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shows_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "wowtogo",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "wowtogo",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staffs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "wowtogo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttendeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    UsedInFormat = table.Column<int>(type: "integer", nullable: true),
                    UsedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Attendees_AttendeeId",
                        column: x => x.AttendeeId,
                        principalSchema: "wowtogo",
                        principalTable: "Attendees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalSchema: "wowtogo",
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypeShows",
                schema: "wowtogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShowId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypeShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTypeShows_Shows_ShowId",
                        column: x => x.ShowId,
                        principalSchema: "wowtogo",
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketTypeShows_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalSchema: "wowtogo",
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Articles",
                columns: new[] { "Id", "CreatedAt", "Description", "EndsAt", "Location", "StartsAt", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0b0b1c3d-0b8d-8944-8d0b-71eff7e5f8a1"), new DateTimeOffset(new DateTime(2024, 1, 3, 11, 39, 27, 177, DateTimeKind.Unspecified).AddTicks(3136), new TimeSpan(0, 0, 0, 0, 0)), "Repellat dolores rerum. Deserunt voluptatem necessitatibus veniam molestias nostrum rerum. Aspernatur sint iure odit et. Delectus quia ut. Laudantium doloremque porro dolores aliquam quia. In voluptatem atque nihil et aut hic est amet.", new DateTime(2025, 2, 22, 8, 27, 35, 530, DateTimeKind.Utc).AddTicks(1523), "Port Jaymouth", new DateTime(2024, 7, 4, 16, 5, 22, 842, DateTimeKind.Utc).AddTicks(7380), 2, "Quis et et.", new DateTimeOffset(new DateTime(2023, 10, 10, 3, 32, 0, 835, DateTimeKind.Unspecified).AddTicks(7928), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("15919d40-a47f-a5ed-1411-ff876f4ff160"), new DateTimeOffset(new DateTime(2023, 7, 18, 1, 21, 8, 677, DateTimeKind.Unspecified).AddTicks(2934), new TimeSpan(0, 0, 0, 0, 0)), "Saepe quo quos. Enim qui et fugiat ut modi architecto. Aperiam adipisci consequatur autem non molestiae magnam nobis.", new DateTime(2024, 12, 19, 2, 21, 30, 148, DateTimeKind.Utc).AddTicks(5794), "North Jonathan", new DateTime(2023, 10, 2, 3, 48, 0, 754, DateTimeKind.Utc).AddTicks(3846), 1, "Explicabo maiores optio.", new DateTimeOffset(new DateTime(2023, 8, 27, 13, 4, 49, 705, DateTimeKind.Unspecified).AddTicks(5963), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("1de7f0c5-af98-25d1-a33a-f246ec74b73f"), new DateTimeOffset(new DateTime(2024, 2, 5, 14, 22, 14, 343, DateTimeKind.Unspecified).AddTicks(7732), new TimeSpan(0, 0, 0, 0, 0)), "Dolorem cumque enim at officiis. Nostrum quod et vel quod quibusdam. Beatae laboriosam iure saepe quae est sapiente.", new DateTime(2024, 9, 8, 17, 25, 10, 818, DateTimeKind.Utc).AddTicks(575), "Gutmannhaven", new DateTime(2023, 11, 27, 17, 54, 37, 407, DateTimeKind.Utc).AddTicks(9532), 2, "Nobis ipsam ut sed quia asperiores officiis.", new DateTimeOffset(new DateTime(2024, 1, 26, 8, 20, 59, 318, DateTimeKind.Unspecified).AddTicks(2195), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("2c8572cd-9b5b-3033-8739-b842af9ad2f4"), new DateTimeOffset(new DateTime(2024, 1, 29, 14, 21, 36, 111, DateTimeKind.Unspecified).AddTicks(697), new TimeSpan(0, 0, 0, 0, 0)), "Temporibus nesciunt hic ipsam tempora rem non. Ad sint necessitatibus nesciunt. Expedita dolorum aperiam asperiores eveniet sequi sint recusandae. Repellendus id maiores qui in ut est laudantium modi. Dolore excepturi adipisci omnis. Sequi molestias quibusdam debitis sit.", new DateTime(2024, 12, 17, 4, 44, 41, 320, DateTimeKind.Utc).AddTicks(6125), "South Pauline", new DateTime(2023, 12, 2, 16, 17, 47, 275, DateTimeKind.Utc).AddTicks(3531), 3, "Repudiandae sunt facere quia est provident eos rerum.", new DateTimeOffset(new DateTime(2024, 5, 31, 0, 49, 24, 303, DateTimeKind.Unspecified).AddTicks(7957), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("38ff3588-0426-08ab-5431-82312f09aa95"), new DateTimeOffset(new DateTime(2024, 1, 27, 10, 40, 21, 704, DateTimeKind.Unspecified).AddTicks(2072), new TimeSpan(0, 0, 0, 0, 0)), "Illum delectus exercitationem quia dignissimos ipsum. Quibusdam quasi dolores qui quibusdam temporibus unde minima consequatur nisi. Explicabo voluptas numquam est non enim rerum non. Vel qui eum vero molestiae. Sit maxime nihil dolorum libero perferendis est autem est. Voluptatum debitis odit eligendi quaerat nesciunt sunt suscipit nemo.", new DateTime(2025, 1, 11, 8, 27, 45, 946, DateTimeKind.Utc).AddTicks(6720), "Blockburgh", new DateTime(2023, 10, 17, 5, 3, 59, 791, DateTimeKind.Utc).AddTicks(2153), 0, "Ut praesentium et deleniti aut odio vel soluta delectus aut.", new DateTimeOffset(new DateTime(2024, 6, 26, 7, 40, 7, 535, DateTimeKind.Unspecified).AddTicks(1782), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("3a2278b9-3f59-f836-4f8f-26e1d5db09b3"), new DateTimeOffset(new DateTime(2023, 11, 9, 13, 2, 25, 946, DateTimeKind.Unspecified).AddTicks(2472), new TimeSpan(0, 0, 0, 0, 0)), "Earum aut facere animi minima quis minus quae. Dolores quo fuga in qui porro a nisi nemo perferendis. Laborum et iure dignissimos fuga quo quibusdam nihil.", new DateTime(2025, 6, 15, 22, 37, 39, 20, DateTimeKind.Utc).AddTicks(9415), "Brakusport", new DateTime(2023, 11, 14, 14, 45, 34, 790, DateTimeKind.Utc).AddTicks(9354), 0, "Quod reprehenderit et libero quia ut.", new DateTimeOffset(new DateTime(2023, 10, 1, 9, 0, 37, 827, DateTimeKind.Unspecified).AddTicks(3473), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("442dda20-dbdb-c888-041c-9ff0f96026ad"), new DateTimeOffset(new DateTime(2023, 10, 16, 18, 23, 40, 911, DateTimeKind.Unspecified).AddTicks(1103), new TimeSpan(0, 0, 0, 0, 0)), "Et odit et. Tempore adipisci quis odio et deleniti vero. Repudiandae et esse dolor est consequatur optio eius doloribus. Dignissimos quis aliquam veritatis deserunt quis accusamus. Quia molestiae asperiores enim iusto dolor dignissimos. Nemo earum sunt.", new DateTime(2025, 4, 22, 5, 38, 18, 76, DateTimeKind.Utc).AddTicks(1695), "Prosaccotown", new DateTime(2024, 1, 8, 8, 1, 3, 15, DateTimeKind.Utc).AddTicks(7156), 3, "Voluptatem quisquam sed doloribus nihil adipisci sed.", new DateTimeOffset(new DateTime(2024, 4, 9, 12, 48, 58, 84, DateTimeKind.Unspecified).AddTicks(425), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("56a9dd64-9975-081f-5060-08a286e8720e"), new DateTimeOffset(new DateTime(2023, 12, 21, 3, 28, 44, 496, DateTimeKind.Unspecified).AddTicks(9663), new TimeSpan(0, 0, 0, 0, 0)), "Incidunt quo corporis tenetur sed non quaerat vel temporibus. Ipsum ea voluptates dolorem nihil quisquam non delectus quas. Quasi voluptas minima harum maxime aut optio in iusto.", new DateTime(2025, 3, 24, 22, 53, 54, 490, DateTimeKind.Utc).AddTicks(2436), "Blockmouth", new DateTime(2023, 10, 23, 22, 46, 31, 462, DateTimeKind.Utc).AddTicks(8334), 0, "Voluptatem praesentium voluptas omnis quo iure ex quidem animi qui.", new DateTimeOffset(new DateTime(2024, 5, 30, 19, 34, 13, 301, DateTimeKind.Unspecified).AddTicks(9556), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5bda86a9-bd57-8e3f-e610-398f9334304d"), new DateTimeOffset(new DateTime(2024, 5, 21, 10, 5, 39, 237, DateTimeKind.Unspecified).AddTicks(7306), new TimeSpan(0, 0, 0, 0, 0)), "Sunt accusantium rerum tempora officiis voluptatum ullam quia consequuntur. Voluptatum consequatur iste nemo quasi. Voluptatem odio facere quia. Pariatur asperiores voluptatem maiores. At cumque earum ut est neque sit voluptatem. Architecto nostrum sed vitae.", new DateTime(2024, 12, 23, 3, 55, 9, 71, DateTimeKind.Utc).AddTicks(5145), "Krajcikfort", new DateTime(2024, 6, 11, 19, 52, 14, 746, DateTimeKind.Utc).AddTicks(1808), 3, "Iure quis consequatur et quo.", new DateTimeOffset(new DateTime(2023, 8, 30, 4, 6, 10, 925, DateTimeKind.Unspecified).AddTicks(2976), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5c04442e-6f36-3777-1792-5635f5d7b6f3"), new DateTimeOffset(new DateTime(2023, 11, 9, 2, 16, 58, 865, DateTimeKind.Unspecified).AddTicks(376), new TimeSpan(0, 0, 0, 0, 0)), "Consequatur enim incidunt inventore in. Assumenda consequatur quis perspiciatis fugiat a. Quidem quo aut earum facilis magnam officia. Molestias tempore beatae error corporis. Voluptatem cumque officiis.", new DateTime(2025, 6, 11, 6, 16, 28, 656, DateTimeKind.Utc).AddTicks(3837), "Agustinamouth", new DateTime(2024, 7, 4, 12, 59, 53, 587, DateTimeKind.Utc).AddTicks(4678), 1, "Ut omnis non sunt.", new DateTimeOffset(new DateTime(2024, 1, 13, 17, 1, 8, 848, DateTimeKind.Unspecified).AddTicks(2076), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6773f756-d8b1-22de-a543-ceb51d18f920"), new DateTimeOffset(new DateTime(2024, 3, 6, 14, 16, 22, 426, DateTimeKind.Unspecified).AddTicks(7923), new TimeSpan(0, 0, 0, 0, 0)), "Aspernatur dolores cupiditate eum. Dignissimos iusto earum rerum commodi et inventore velit. Nobis qui ea. Excepturi necessitatibus et. Vel atque et. Dolorum voluptatem exercitationem suscipit.", new DateTime(2024, 9, 23, 4, 35, 36, 364, DateTimeKind.Utc).AddTicks(26), "New Garretville", new DateTime(2023, 9, 8, 4, 4, 52, 499, DateTimeKind.Utc).AddTicks(2896), 2, "Temporibus quia ut aut voluptatem minima.", new DateTimeOffset(new DateTime(2023, 10, 10, 4, 55, 27, 358, DateTimeKind.Unspecified).AddTicks(7904), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2024, 6, 3, 5, 56, 27, 205, DateTimeKind.Unspecified).AddTicks(5673), new TimeSpan(0, 0, 0, 0, 0)), "Rerum iste porro cumque perferendis doloribus aut vero veritatis. Cupiditate enim asperiores eligendi iure iusto cum. Rem et ex non ipsum ut eligendi qui. Hic dicta aut corrupti.", new DateTime(2025, 2, 14, 9, 8, 43, 738, DateTimeKind.Utc).AddTicks(895), "Alycialand", new DateTime(2024, 3, 23, 7, 13, 2, 794, DateTimeKind.Utc).AddTicks(8853), 0, "Cumque hic explicabo neque eum quibusdam ipsum autem.", new DateTimeOffset(new DateTime(2024, 3, 12, 14, 57, 24, 134, DateTimeKind.Unspecified).AddTicks(1411), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8fe1edc5-b413-31df-9220-83cc956c7bf2"), new DateTimeOffset(new DateTime(2023, 11, 19, 6, 59, 10, 892, DateTimeKind.Unspecified).AddTicks(547), new TimeSpan(0, 0, 0, 0, 0)), "Quaerat nemo rerum ut eos et sapiente. Voluptatem accusamus facere id. Fugiat voluptatem minima temporibus sed quibusdam reiciendis illum illum. Dolorem qui omnis. Corporis eum error sint adipisci beatae.", new DateTime(2024, 12, 15, 5, 29, 42, 991, DateTimeKind.Utc).AddTicks(4958), "Burdettehaven", new DateTime(2024, 4, 30, 22, 34, 58, 384, DateTimeKind.Utc).AddTicks(2347), 1, "Similique quasi harum fugiat necessitatibus.", new DateTimeOffset(new DateTime(2024, 2, 7, 16, 43, 25, 466, DateTimeKind.Unspecified).AddTicks(6083), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ac46bc9f-d93f-53fb-22dc-84a2ca69546f"), new DateTimeOffset(new DateTime(2023, 11, 15, 5, 29, 27, 527, DateTimeKind.Unspecified).AddTicks(8098), new TimeSpan(0, 0, 0, 0, 0)), "Est vel vero. Quo repellat voluptate possimus. Est nihil debitis accusamus. Alias deserunt porro velit aspernatur cupiditate hic nihil soluta cumque. Illum neque repudiandae.", new DateTime(2024, 9, 16, 12, 32, 58, 787, DateTimeKind.Utc).AddTicks(1733), "Port Garthside", new DateTime(2024, 1, 25, 2, 35, 31, 500, DateTimeKind.Utc).AddTicks(4435), 0, "Occaecati officia harum expedita nulla possimus tenetur iste.", new DateTimeOffset(new DateTime(2023, 9, 30, 8, 43, 11, 705, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("b3ea500e-09c9-1840-f6ff-5f134b47a79f"), new DateTimeOffset(new DateTime(2023, 7, 23, 19, 26, 59, 382, DateTimeKind.Unspecified).AddTicks(5227), new TimeSpan(0, 0, 0, 0, 0)), "Similique sunt accusantium odit pariatur maiores optio aut rerum voluptas. Reprehenderit expedita voluptatem sint ea optio sed. Sint ullam optio repellendus accusantium totam. Laborum autem odit eligendi quaerat error quaerat impedit officia.", new DateTime(2025, 5, 28, 7, 47, 34, 694, DateTimeKind.Utc).AddTicks(8532), "Gaylordfort", new DateTime(2024, 3, 7, 14, 13, 39, 599, DateTimeKind.Utc).AddTicks(1500), 3, "Perspiciatis et qui qui.", new DateTimeOffset(new DateTime(2024, 4, 12, 22, 39, 58, 27, DateTimeKind.Unspecified).AddTicks(7514), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("be7e9c44-779a-e5fd-899f-ecf460cd2540"), new DateTimeOffset(new DateTime(2024, 4, 18, 1, 32, 3, 305, DateTimeKind.Unspecified).AddTicks(2859), new TimeSpan(0, 0, 0, 0, 0)), "Ipsum facilis laborum dolores quia soluta ut cupiditate ex. Non ea sunt. Rem alias quaerat qui. Quia earum omnis aut soluta qui.", new DateTime(2025, 1, 11, 5, 6, 46, 840, DateTimeKind.Utc).AddTicks(8827), "Haleyfurt", new DateTime(2024, 6, 12, 10, 51, 30, 687, DateTimeKind.Utc).AddTicks(4628), 1, "Fugit eaque dolor.", new DateTimeOffset(new DateTime(2023, 10, 24, 19, 2, 46, 298, DateTimeKind.Unspecified).AddTicks(4301), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e335c1c9-333c-8ae1-aa4f-bf104a9e54cc"), new DateTimeOffset(new DateTime(2023, 8, 20, 16, 9, 56, 984, DateTimeKind.Unspecified).AddTicks(9245), new TimeSpan(0, 0, 0, 0, 0)), "Ab laborum ut mollitia dolores dolor fugiat nobis. Dolorum quo eius dicta labore qui qui. Distinctio distinctio excepturi odit autem nemo.", new DateTime(2024, 12, 30, 15, 29, 5, 368, DateTimeKind.Utc).AddTicks(7702), "New Zoey", new DateTime(2023, 7, 14, 18, 59, 15, 110, DateTimeKind.Utc).AddTicks(1971), 2, "Illo qui sed occaecati voluptatem et perferendis quos omnis.", new DateTimeOffset(new DateTime(2024, 1, 20, 6, 26, 11, 810, DateTimeKind.Unspecified).AddTicks(6138), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e61e09de-4ae2-ccc3-689c-6ed22e1db28b"), new DateTimeOffset(new DateTime(2023, 7, 28, 7, 50, 49, 43, DateTimeKind.Unspecified).AddTicks(247), new TimeSpan(0, 0, 0, 0, 0)), "Deserunt autem quia incidunt in quam. Ex repellendus eligendi ducimus nulla debitis velit dicta maxime. Qui quam eius rem expedita fuga dicta cum. Nulla veritatis asperiores quam vel.", new DateTime(2024, 12, 7, 2, 49, 10, 725, DateTimeKind.Utc).AddTicks(7328), "Lake Elliottown", new DateTime(2024, 1, 11, 11, 32, 58, 218, DateTimeKind.Utc).AddTicks(5702), 1, "Mollitia consequatur natus qui et neque natus.", new DateTimeOffset(new DateTime(2024, 1, 30, 7, 15, 14, 805, DateTimeKind.Unspecified).AddTicks(9577), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e672b4df-1ce4-5bf3-4084-a82daa992812"), new DateTimeOffset(new DateTime(2024, 4, 18, 16, 58, 39, 44, DateTimeKind.Unspecified).AddTicks(3128), new TimeSpan(0, 0, 0, 0, 0)), "Veniam dolorum aut velit pariatur. Velit quia maiores consequatur commodi voluptatem et eaque rerum. Aut molestias est exercitationem sint quibusdam. In voluptatem magni optio quam esse temporibus fugiat mollitia.", new DateTime(2024, 8, 19, 15, 36, 16, 359, DateTimeKind.Utc).AddTicks(6333), "Port Toneyton", new DateTime(2024, 3, 9, 23, 15, 37, 446, DateTimeKind.Utc).AddTicks(8461), 3, "Eos rerum voluptatem.", new DateTimeOffset(new DateTime(2024, 3, 12, 3, 2, 18, 63, DateTimeKind.Unspecified).AddTicks(8887), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f5fa6e1e-3fd8-4f62-f92f-50433e33fc7b"), new DateTimeOffset(new DateTime(2023, 8, 10, 6, 7, 15, 299, DateTimeKind.Unspecified).AddTicks(9803), new TimeSpan(0, 0, 0, 0, 0)), "Qui omnis non doloribus quos consequatur culpa accusantium ratione reprehenderit. Numquam praesentium fuga sapiente. Enim consequatur consequatur autem temporibus facilis omnis. Praesentium harum nihil id.", new DateTime(2024, 7, 22, 18, 9, 17, 782, DateTimeKind.Utc).AddTicks(4180), "West Dashawn", new DateTime(2023, 8, 13, 11, 16, 5, 637, DateTimeKind.Utc).AddTicks(7413), 1, "Vero blanditiis est.", new DateTimeOffset(new DateTime(2024, 4, 10, 14, 42, 11, 928, DateTimeKind.Unspecified).AddTicks(1450), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("024e828e-be21-3b0f-5358-f0a328184be9"), new DateTimeOffset(new DateTime(2024, 5, 8, 22, 43, 49, 870, DateTimeKind.Unspecified).AddTicks(784), new TimeSpan(0, 0, 0, 0, 0)), "Tools", new DateTimeOffset(new DateTime(2023, 7, 23, 3, 20, 40, 460, DateTimeKind.Unspecified).AddTicks(8585), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("0dcfe258-02ef-152a-5e8d-7e518a270688"), new DateTimeOffset(new DateTime(2023, 12, 31, 9, 54, 4, 925, DateTimeKind.Unspecified).AddTicks(324), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2023, 9, 17, 3, 14, 10, 580, DateTimeKind.Unspecified).AddTicks(2130), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("0de87910-38c5-36b4-08b7-6a1fb627237c"), new DateTimeOffset(new DateTime(2024, 4, 18, 16, 16, 7, 6, DateTimeKind.Unspecified).AddTicks(3451), new TimeSpan(0, 0, 0, 0, 0)), "Clothing", new DateTimeOffset(new DateTime(2023, 7, 17, 19, 48, 30, 50, DateTimeKind.Unspecified).AddTicks(5695), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("116018c2-65b0-8b44-8b5b-f0b5b4e6e602"), new DateTimeOffset(new DateTime(2024, 4, 16, 11, 52, 38, 915, DateTimeKind.Unspecified).AddTicks(4573), new TimeSpan(0, 0, 0, 0, 0)), "Shoes", new DateTimeOffset(new DateTime(2023, 10, 27, 8, 31, 17, 318, DateTimeKind.Unspecified).AddTicks(1383), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("12457e46-2d20-6c89-c811-30e7bf3a4bce"), new DateTimeOffset(new DateTime(2023, 12, 11, 21, 39, 4, 520, DateTimeKind.Unspecified).AddTicks(4408), new TimeSpan(0, 0, 0, 0, 0)), "Tools", new DateTimeOffset(new DateTime(2023, 8, 4, 13, 32, 19, 159, DateTimeKind.Unspecified).AddTicks(1042), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("152872dc-4b51-d67e-8519-9aa660cee9d5"), new DateTimeOffset(new DateTime(2024, 3, 7, 3, 0, 7, 949, DateTimeKind.Unspecified).AddTicks(156), new TimeSpan(0, 0, 0, 0, 0)), "Baby", new DateTimeOffset(new DateTime(2023, 12, 10, 18, 52, 5, 34, DateTimeKind.Unspecified).AddTicks(4484), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22b19c6f-289a-4bc5-5e4d-fdad5164c4a0"), new DateTimeOffset(new DateTime(2023, 10, 29, 3, 9, 59, 636, DateTimeKind.Unspecified).AddTicks(2512), new TimeSpan(0, 0, 0, 0, 0)), "Outdoors", new DateTimeOffset(new DateTime(2024, 5, 13, 20, 59, 24, 872, DateTimeKind.Unspecified).AddTicks(1119), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25cd60f4-3b40-4fb6-99fa-252e84c9c137"), new DateTimeOffset(new DateTime(2024, 3, 8, 19, 34, 15, 470, DateTimeKind.Unspecified).AddTicks(102), new TimeSpan(0, 0, 0, 0, 0)), "Music", new DateTimeOffset(new DateTime(2023, 10, 29, 8, 59, 37, 661, DateTimeKind.Unspecified).AddTicks(7629), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31e7c326-3588-38ff-2604-ab0854318231"), new DateTimeOffset(new DateTime(2023, 8, 31, 13, 4, 48, 732, DateTimeKind.Unspecified).AddTicks(9317), new TimeSpan(0, 0, 0, 0, 0)), "Automotive", new DateTimeOffset(new DateTime(2024, 1, 19, 8, 49, 23, 159, DateTimeKind.Unspecified).AddTicks(2015), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("333ce335-8ae1-4faa-bf10-4a9e54ccecf2"), new DateTimeOffset(new DateTime(2024, 6, 15, 8, 12, 22, 941, DateTimeKind.Unspecified).AddTicks(2692), new TimeSpan(0, 0, 0, 0, 0)), "Sports", new DateTimeOffset(new DateTime(2024, 2, 10, 23, 36, 8, 116, DateTimeKind.Unspecified).AddTicks(3449), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("335a09f9-4324-4db3-0d39-5e7eabd934d4"), new DateTimeOffset(new DateTime(2023, 8, 20, 16, 9, 56, 993, DateTimeKind.Unspecified).AddTicks(4656), new TimeSpan(0, 0, 0, 0, 0)), "Clothing", new DateTimeOffset(new DateTime(2024, 1, 20, 6, 26, 11, 819, DateTimeKind.Unspecified).AddTicks(1549), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("34938f39-4d30-3c5a-52f6-55b351d69589"), new DateTimeOffset(new DateTime(2024, 6, 25, 3, 19, 21, 745, DateTimeKind.Unspecified).AddTicks(6950), new TimeSpan(0, 0, 0, 0, 0)), "Movies", new DateTimeOffset(new DateTime(2023, 8, 15, 9, 48, 34, 413, DateTimeKind.Unspecified).AddTicks(1254), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("3f593a22-f836-8f4f-26e1-d5db09b31f3e"), new DateTimeOffset(new DateTime(2023, 10, 7, 20, 37, 24, 764, DateTimeKind.Unspecified).AddTicks(2444), new TimeSpan(0, 0, 0, 0, 0)), "Grocery", new DateTimeOffset(new DateTime(2024, 2, 12, 7, 26, 21, 259, DateTimeKind.Unspecified).AddTicks(8483), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("4e4af774-4a5f-69d7-f094-e08d73e6e4f0"), new DateTimeOffset(new DateTime(2023, 7, 8, 3, 39, 21, 277, DateTimeKind.Unspecified).AddTicks(3505), new TimeSpan(0, 0, 0, 0, 0)), "Music", new DateTimeOffset(new DateTime(2024, 1, 11, 4, 48, 55, 684, DateTimeKind.Unspecified).AddTicks(8451), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("4e5b1334-6fa3-a584-4adf-7a0ea09ce3c1"), new DateTimeOffset(new DateTime(2023, 8, 25, 11, 30, 25, 827, DateTimeKind.Unspecified).AddTicks(7367), new TimeSpan(0, 0, 0, 0, 0)), "Jewelery", new DateTimeOffset(new DateTime(2024, 6, 11, 21, 40, 54, 275, DateTimeKind.Unspecified).AddTicks(6693), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("500e0755-b3ea-09c9-4018-f6ff5f134b47"), new DateTimeOffset(new DateTime(2024, 1, 13, 9, 43, 14, 557, DateTimeKind.Unspecified).AddTicks(9702), new TimeSpan(0, 0, 0, 0, 0)), "Baby", new DateTimeOffset(new DateTime(2024, 3, 7, 14, 13, 39, 607, DateTimeKind.Unspecified).AddTicks(6911), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5469caa2-0a6f-1e05-c36e-9841ddbe452a"), new DateTimeOffset(new DateTime(2024, 6, 18, 21, 32, 16, 362, DateTimeKind.Unspecified).AddTicks(2039), new TimeSpan(0, 0, 0, 0, 0)), "Beauty", new DateTimeOffset(new DateTime(2023, 10, 26, 23, 59, 29, 871, DateTimeKind.Unspecified).AddTicks(1644), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("57f812c7-2e2a-3174-6200-e10c30a46a47"), new DateTimeOffset(new DateTime(2023, 10, 12, 23, 29, 44, 575, DateTimeKind.Unspecified).AddTicks(6771), new TimeSpan(0, 0, 0, 0, 0)), "Books", new DateTimeOffset(new DateTime(2023, 9, 18, 18, 10, 59, 597, DateTimeKind.Unspecified).AddTicks(2064), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("643ec69c-3704-a727-64db-5dd894c312b3"), new DateTimeOffset(new DateTime(2023, 11, 19, 10, 47, 29, 519, DateTimeKind.Unspecified).AddTicks(3599), new TimeSpan(0, 0, 0, 0, 0)), "Clothing", new DateTimeOffset(new DateTime(2024, 2, 9, 15, 10, 3, 219, DateTimeKind.Unspecified).AddTicks(6355), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6898fc78-3940-c5de-4409-f8c4fa93da03"), new DateTimeOffset(new DateTime(2024, 3, 30, 2, 3, 37, 823, DateTimeKind.Unspecified).AddTicks(852), new TimeSpan(0, 0, 0, 0, 0)), "Kids", new DateTimeOffset(new DateTime(2024, 3, 18, 8, 30, 35, 967, DateTimeKind.Unspecified).AddTicks(450), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6ce2894b-cb29-178f-449c-7ebe9a77fde5"), new DateTimeOffset(new DateTime(2024, 6, 23, 19, 34, 44, 531, DateTimeKind.Unspecified).AddTicks(878), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2023, 10, 30, 13, 31, 43, 946, DateTimeKind.Unspecified).AddTicks(6216), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72e886a2-750e-af3e-baae-dc09f8589589"), new DateTimeOffset(new DateTime(2024, 1, 20, 22, 59, 12, 673, DateTimeKind.Unspecified).AddTicks(5627), new TimeSpan(0, 0, 0, 0, 0)), "Kids", new DateTimeOffset(new DateTime(2024, 4, 4, 8, 41, 1, 628, DateTimeKind.Unspecified).AddTicks(6285), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("73f756ca-b167-ded8-22a5-43ceb51d18f9"), new DateTimeOffset(new DateTime(2023, 9, 8, 4, 4, 52, 507, DateTimeKind.Unspecified).AddTicks(8307), new TimeSpan(0, 0, 0, 0, 0)), "Health", new DateTimeOffset(new DateTime(2024, 4, 15, 3, 32, 28, 0, DateTimeKind.Unspecified).AddTicks(8955), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("7c7231a1-1df6-6e1e-faf5-d83f624ff92f"), new DateTimeOffset(new DateTime(2024, 5, 17, 16, 13, 15, 292, DateTimeKind.Unspecified).AddTicks(8930), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2024, 3, 6, 21, 18, 1, 976, DateTimeKind.Unspecified).AddTicks(3328), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2023, 11, 22, 13, 30, 29, 154, DateTimeKind.Unspecified).AddTicks(5509), new TimeSpan(0, 0, 0, 0, 0)), "Home", new DateTimeOffset(new DateTime(2023, 10, 21, 0, 33, 55, 657, DateTimeKind.Unspecified).AddTicks(1844), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("873fa48b-cac7-f608-3404-c6ceec6dfd32"), new DateTimeOffset(new DateTime(2023, 7, 31, 20, 55, 58, 938, DateTimeKind.Unspecified).AddTicks(1252), new TimeSpan(0, 0, 0, 0, 0)), "Movies", new DateTimeOffset(new DateTime(2024, 1, 13, 8, 35, 34, 799, DateTimeKind.Unspecified).AddTicks(9042), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8891162f-8813-026d-bc01-44d5e3922476"), new DateTimeOffset(new DateTime(2023, 12, 28, 11, 11, 47, 714, DateTimeKind.Unspecified).AddTicks(3480), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2023, 7, 28, 16, 20, 27, 946, DateTimeKind.Unspecified).AddTicks(4306), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8fe1edc5-b413-31df-9220-83cc956c7bf2"), new DateTimeOffset(new DateTime(2024, 1, 22, 21, 10, 45, 355, DateTimeKind.Unspecified).AddTicks(2448), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2024, 3, 23, 20, 0, 3, 103, DateTimeKind.Unspecified).AddTicks(7408), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("93db5cc1-8204-54e4-fdec-62054b7150c5"), new DateTimeOffset(new DateTime(2023, 12, 22, 14, 37, 54, 765, DateTimeKind.Unspecified).AddTicks(1982), new TimeSpan(0, 0, 0, 0, 0)), "Books", new DateTimeOffset(new DateTime(2024, 4, 10, 14, 54, 28, 806, DateTimeKind.Unspecified).AddTicks(7995), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("96f5f854-2b4a-b310-706c-47f097c2da9f"), new DateTimeOffset(new DateTime(2024, 4, 9, 2, 31, 36, 634, DateTimeKind.Unspecified).AddTicks(3381), new TimeSpan(0, 0, 0, 0, 0)), "Garden", new DateTimeOffset(new DateTime(2024, 1, 26, 15, 12, 7, 33, DateTimeKind.Unspecified).AddTicks(4077), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("98daceee-2ba9-770f-9fbc-46ac3fd9fb53"), new DateTimeOffset(new DateTime(2024, 3, 15, 12, 31, 36, 311, DateTimeKind.Unspecified).AddTicks(9132), new TimeSpan(0, 0, 0, 0, 0)), "Grocery", new DateTimeOffset(new DateTime(2023, 9, 18, 8, 31, 15, 634, DateTimeKind.Unspecified).AddTicks(7437), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9a355437-eaac-4485-af13-938dff31365e"), new DateTimeOffset(new DateTime(2023, 7, 24, 17, 46, 20, 303, DateTimeKind.Unspecified).AddTicks(6805), new TimeSpan(0, 0, 0, 0, 0)), "Automotive", new DateTimeOffset(new DateTime(2024, 2, 23, 8, 50, 5, 523, DateTimeKind.Unspecified).AddTicks(2848), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9ba8323f-7998-7dce-7f6b-bfd1ae7f5f6c"), new DateTimeOffset(new DateTime(2023, 8, 19, 14, 45, 36, 84, DateTimeKind.Unspecified).AddTicks(8044), new TimeSpan(0, 0, 0, 0, 0)), "Sports", new DateTimeOffset(new DateTime(2024, 1, 13, 23, 12, 57, 462, DateTimeKind.Unspecified).AddTicks(8533), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ade0967a-4670-df63-2293-f92710bd1af2"), new DateTimeOffset(new DateTime(2023, 11, 11, 21, 8, 46, 12, DateTimeKind.Unspecified).AddTicks(5004), new TimeSpan(0, 0, 0, 0, 0)), "Outdoors", new DateTimeOffset(new DateTime(2024, 2, 5, 14, 22, 14, 352, DateTimeKind.Unspecified).AddTicks(3143), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("b7fcec49-f261-22db-d65c-e604d7befa1c"), new DateTimeOffset(new DateTime(2023, 12, 31, 2, 8, 39, 961, DateTimeKind.Unspecified).AddTicks(2172), new TimeSpan(0, 0, 0, 0, 0)), "Music", new DateTimeOffset(new DateTime(2024, 6, 29, 6, 44, 7, 931, DateTimeKind.Unspecified).AddTicks(1620), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ba7b04bb-55cb-0d85-9a5e-5834745d1d4a"), new DateTimeOffset(new DateTime(2023, 11, 16, 7, 22, 29, 423, DateTimeKind.Unspecified).AddTicks(4875), new TimeSpan(0, 0, 0, 0, 0)), "Health", new DateTimeOffset(new DateTime(2024, 2, 1, 4, 49, 13, 122, DateTimeKind.Unspecified).AddTicks(1675), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c1724c95-1f1f-de8f-d409-b2d248077225"), new DateTimeOffset(new DateTime(2023, 7, 17, 19, 33, 8, 83, DateTimeKind.Unspecified).AddTicks(895), new TimeSpan(0, 0, 0, 0, 0)), "Jewelery", new DateTimeOffset(new DateTime(2024, 3, 18, 12, 54, 36, 46, DateTimeKind.Unspecified).AddTicks(112), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("cf63a1e2-f0c5-1de7-98af-d125a33af246"), new DateTimeOffset(new DateTime(2024, 4, 7, 6, 31, 56, 548, DateTimeKind.Unspecified).AddTicks(4862), new TimeSpan(0, 0, 0, 0, 0)), "Toys", new DateTimeOffset(new DateTime(2024, 4, 14, 15, 32, 7, 839, DateTimeKind.Unspecified).AddTicks(4076), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("d15a7a1a-a34b-cd3a-7285-2c5b9b333087"), new DateTimeOffset(new DateTime(2024, 4, 20, 1, 30, 58, 679, DateTimeKind.Unspecified).AddTicks(6976), new TimeSpan(0, 0, 0, 0, 0)), "Automotive", new DateTimeOffset(new DateTime(2024, 1, 15, 17, 15, 58, 806, DateTimeKind.Unspecified).AddTicks(6997), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("da46d4f4-4577-fb00-969b-e0dfb77ed0ff"), new DateTimeOffset(new DateTime(2023, 12, 28, 17, 29, 15, 76, DateTimeKind.Unspecified).AddTicks(6116), new TimeSpan(0, 0, 0, 0, 0)), "Clothing", new DateTimeOffset(new DateTime(2023, 11, 20, 13, 26, 16, 693, DateTimeKind.Unspecified).AddTicks(4780), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("dc6c04bd-ef43-72a6-9530-763d1c0b0b8d"), new DateTimeOffset(new DateTime(2023, 10, 5, 7, 21, 40, 530, DateTimeKind.Unspecified).AddTicks(3308), new TimeSpan(0, 0, 0, 0, 0)), "Baby", new DateTimeOffset(new DateTime(2023, 10, 19, 10, 48, 7, 767, DateTimeKind.Unspecified).AddTicks(1106), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e4807585-464c-d387-b0fe-e00f0643594f"), new DateTimeOffset(new DateTime(2024, 2, 18, 13, 13, 38, 909, DateTimeKind.Unspecified).AddTicks(5372), new TimeSpan(0, 0, 0, 0, 0)), "Music", new DateTimeOffset(new DateTime(2023, 9, 26, 9, 28, 58, 70, DateTimeKind.Unspecified).AddTicks(6823), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e8980312-099b-5ce0-86f1-8c77537da207"), new DateTimeOffset(new DateTime(2024, 5, 26, 19, 56, 28, 376, DateTimeKind.Unspecified).AddTicks(9231), new TimeSpan(0, 0, 0, 0, 0)), "Jewelery", new DateTimeOffset(new DateTime(2023, 10, 25, 10, 42, 33, 871, DateTimeKind.Unspecified).AddTicks(337), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("eb946cbe-94be-f506-64dd-a95675991f08"), new DateTimeOffset(new DateTime(2024, 2, 28, 8, 51, 18, 247, DateTimeKind.Unspecified).AddTicks(3723), new TimeSpan(0, 0, 0, 0, 0)), "Tools", new DateTimeOffset(new DateTime(2024, 3, 6, 23, 40, 32, 414, DateTimeKind.Unspecified).AddTicks(4488), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ef710b8d-e5f7-a1f8-4728-bec785a83b7e"), new DateTimeOffset(new DateTime(2024, 5, 16, 17, 50, 31, 921, DateTimeKind.Unspecified).AddTicks(7485), new TimeSpan(0, 0, 0, 0, 0)), "Industrial", new DateTimeOffset(new DateTime(2023, 7, 24, 20, 17, 46, 596, DateTimeKind.Unspecified).AddTicks(4642), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ef7bfc33-0258-4c9b-d3ca-bf2865b935f4"), new DateTimeOffset(new DateTime(2023, 12, 6, 1, 32, 3, 199, DateTimeKind.Unspecified).AddTicks(4844), new TimeSpan(0, 0, 0, 0, 0)), "Tools", new DateTimeOffset(new DateTime(2024, 6, 24, 22, 10, 2, 443, DateTimeKind.Unspecified).AddTicks(5768), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f1bc12e3-c49a-c11a-5ea9-86da5b57bd3f"), new DateTimeOffset(new DateTime(2024, 1, 29, 18, 37, 30, 513, DateTimeKind.Unspecified).AddTicks(1001), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2023, 7, 20, 21, 21, 51, 153, DateTimeKind.Unspecified).AddTicks(2029), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f4d29aaf-ee01-957b-4bca-486fcecd1ce1"), new DateTimeOffset(new DateTime(2023, 9, 19, 10, 15, 4, 836, DateTimeKind.Unspecified).AddTicks(1633), new TimeSpan(0, 0, 0, 0, 0)), "Kids", new DateTimeOffset(new DateTime(2024, 5, 8, 9, 1, 19, 878, DateTimeKind.Unspecified).AddTicks(7100), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f7c39721-c1f2-1c38-3505-82a9e40f4fc0"), new DateTimeOffset(new DateTime(2023, 10, 3, 7, 50, 32, 925, DateTimeKind.Unspecified).AddTicks(6021), new TimeSpan(0, 0, 0, 0, 0)), "Outdoors", new DateTimeOffset(new DateTime(2023, 11, 26, 5, 40, 30, 231, DateTimeKind.Unspecified).AddTicks(4089), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("fd073eb4-92b1-7dcd-944b-6bd9bd0380b1"), new DateTimeOffset(new DateTime(2024, 5, 15, 11, 46, 4, 113, DateTimeKind.Unspecified).AddTicks(2935), new TimeSpan(0, 0, 0, 0, 0)), "Games", new DateTimeOffset(new DateTime(2023, 8, 28, 6, 31, 23, 914, DateTimeKind.Unspecified).AddTicks(7867), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "TicketTypes",
                columns: new[] { "Id", "Amount", "CreatedAt", "Description", "FromDate", "ImageUrl", "LeastAmountBuy", "MostAmountBuy", "Name", "Price", "ToDate", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("09dbd5e1-1fb3-813e-9094-9cc63e640437"), 96, new DateTimeOffset(new DateTime(2023, 11, 19, 10, 47, 29, 525, DateTimeKind.Unspecified).AddTicks(3940), new TimeSpan(0, 0, 0, 0, 0)), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new DateTimeOffset(new DateTime(2023, 10, 11, 4, 29, 10, 958, DateTimeKind.Unspecified).AddTicks(4594), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=303", 50, 7158, "Tasty Frozen Cheese", 43.377031811222920m, new DateTimeOffset(new DateTime(2024, 7, 22, 6, 32, 19, 978, DateTimeKind.Unspecified).AddTicks(9356), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 9, 15, 10, 3, 225, DateTimeKind.Unspecified).AddTicks(6696), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("09dcaeba-58f8-8995-19c4-f71079e80dc5"), 91, new DateTimeOffset(new DateTime(2023, 10, 14, 8, 51, 21, 672, DateTimeKind.Unspecified).AddTicks(7804), new TimeSpan(0, 0, 0, 0, 0)), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new DateTimeOffset(new DateTime(2024, 5, 2, 20, 0, 51, 168, DateTimeKind.Unspecified).AddTicks(2550), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=857", 83, 4286, "Ergonomic Frozen Ball", 84.802168316581390m, new DateTimeOffset(new DateTime(2024, 11, 29, 4, 27, 7, 50, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 18, 16, 16, 7, 12, DateTimeKind.Unspecified).AddTicks(3792), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20f9181d-855e-5437-359a-acea8544af13"), 37, new DateTimeOffset(new DateTime(2024, 2, 5, 3, 58, 26, 787, DateTimeKind.Unspecified).AddTicks(2906), new TimeSpan(0, 0, 0, 0, 0)), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new DateTimeOffset(new DateTime(2023, 7, 26, 18, 23, 0, 416, DateTimeKind.Unspecified).AddTicks(2291), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=482", 93, 657, "Sleek Granite Bacon", 49.278583908117640m, new DateTimeOffset(new DateTime(2025, 6, 14, 20, 57, 30, 612, DateTimeKind.Unspecified).AddTicks(1393), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 25, 2, 48, 56, 951, DateTimeKind.Unspecified).AddTicks(447), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("3e43502f-fc33-ef7b-5802-9b4cd3cabf28"), 40, new DateTimeOffset(new DateTime(2024, 1, 18, 11, 8, 23, 836, DateTimeKind.Unspecified).AddTicks(323), new TimeSpan(0, 0, 0, 0, 0)), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new DateTimeOffset(new DateTime(2024, 6, 24, 22, 10, 2, 449, DateTimeKind.Unspecified).AddTicks(6109), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=398", 24, 2216, "Handmade Frozen Chips", 62.061236953391350m, new DateTimeOffset(new DateTime(2024, 8, 26, 15, 26, 58, 430, DateTimeKind.Unspecified).AddTicks(3828), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 19, 22, 38, 30, 418, DateTimeKind.Unspecified).AddTicks(9435), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("4401bc02-e3d5-2492-76a7-488558e2cf0d"), 52, new DateTimeOffset(new DateTime(2024, 4, 2, 0, 1, 29, 446, DateTimeKind.Unspecified).AddTicks(2946), new TimeSpan(0, 0, 0, 0, 0)), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new DateTimeOffset(new DateTime(2024, 5, 3, 15, 2, 17, 385, DateTimeKind.Unspecified).AddTicks(3476), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=504", 20, 7796, "Rustic Cotton Hat", 30.106370407206160m, new DateTimeOffset(new DateTime(2024, 10, 2, 7, 47, 55, 462, DateTimeKind.Unspecified).AddTicks(1221), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 9, 10, 18, 37, 852, DateTimeKind.Unspecified).AddTicks(5489), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("46f23aa3-74ec-3fb7-32a8-9b9879ce7d7f"), 88, new DateTimeOffset(new DateTime(2023, 10, 8, 6, 35, 16, 993, DateTimeKind.Unspecified).AddTicks(3447), new TimeSpan(0, 0, 0, 0, 0)), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new DateTimeOffset(new DateTime(2024, 3, 12, 12, 22, 39, 211, DateTimeKind.Unspecified).AddTicks(2646), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=897", 48, 2972, "Tasty Fresh Car", 73.154406982080250m, new DateTimeOffset(new DateTime(2025, 5, 12, 22, 39, 3, 894, DateTimeKind.Unspecified).AddTicks(7627), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 15, 20, 5, 10, 752, DateTimeKind.Unspecified).AddTicks(9264), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5e4bc528-fd4d-51ad-64c4-a05b7d64a131"), 52, new DateTimeOffset(new DateTime(2023, 12, 6, 3, 56, 43, 565, DateTimeKind.Unspecified).AddTicks(1990), new TimeSpan(0, 0, 0, 0, 0)), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new DateTimeOffset(new DateTime(2024, 5, 24, 3, 55, 18, 203, DateTimeKind.Unspecified).AddTicks(3607), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=1056", 40, 9877, "Handcrafted Granite Bike", 27.265351860441910m, new DateTimeOffset(new DateTime(2025, 1, 20, 9, 55, 29, 560, DateTimeKind.Unspecified).AddTicks(6215), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 10, 4, 9, 40, 296, DateTimeKind.Unspecified).AddTicks(900), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5e9a0d85-3458-5d74-1d4a-6726411a7a5a"), 9, new DateTimeOffset(new DateTime(2024, 5, 19, 9, 9, 46, 447, DateTimeKind.Unspecified).AddTicks(1547), new TimeSpan(0, 0, 0, 0, 0)), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new DateTimeOffset(new DateTime(2024, 1, 16, 14, 41, 59, 484, DateTimeKind.Unspecified).AddTicks(6506), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=163", 12, 2252, "Gorgeous Plastic Salad", 77.506442110755640m, new DateTimeOffset(new DateTime(2025, 7, 3, 9, 0, 44, 755, DateTimeKind.Unspecified).AddTicks(5442), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 9, 16, 26, 7, 894, DateTimeKind.Unspecified).AddTicks(1736), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("634670ad-22df-f993-2710-bd1af25f3a06"), 83, new DateTimeOffset(new DateTime(2024, 3, 8, 3, 46, 17, 661, DateTimeKind.Unspecified).AddTicks(6534), new TimeSpan(0, 0, 0, 0, 0)), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new DateTimeOffset(new DateTime(2024, 3, 6, 20, 16, 17, 885, DateTimeKind.Unspecified).AddTicks(1359), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=954", 26, 9795, "Practical Granite Pants", 39.208276550848150m, new DateTimeOffset(new DateTime(2025, 3, 11, 20, 40, 34, 402, DateTimeKind.Unspecified).AddTicks(2012), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 15, 18, 56, 0, 290, DateTimeKind.Unspecified).AddTicks(5846), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6cbe111e-eb94-94be-06f5-64dda9567599"), 37, new DateTimeOffset(new DateTime(2023, 10, 14, 21, 13, 7, 694, DateTimeKind.Unspecified).AddTicks(414), new TimeSpan(0, 0, 0, 0, 0)), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new DateTimeOffset(new DateTime(2024, 6, 7, 13, 5, 28, 334, DateTimeKind.Unspecified).AddTicks(9288), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=355", 81, 6993, "Handcrafted Steel Pants", 17.7699586133332720m, new DateTimeOffset(new DateTime(2025, 3, 5, 18, 35, 0, 464, DateTimeKind.Unspecified).AddTicks(8686), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 5, 1, 13, 11, 19, DateTimeKind.Unspecified).AddTicks(1424), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("807585f9-4ce4-8746-d3b0-fee00f064359"), 35, new DateTimeOffset(new DateTime(2024, 3, 20, 19, 13, 11, 499, DateTimeKind.Unspecified).AddTicks(5562), new TimeSpan(0, 0, 0, 0, 0)), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new DateTimeOffset(new DateTime(2023, 8, 17, 9, 35, 3, 284, DateTimeKind.Unspecified).AddTicks(5526), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=776", 13, 3264, "Awesome Wooden Pants", 32.632803974036530m, new DateTimeOffset(new DateTime(2024, 12, 11, 15, 18, 50, 973, DateTimeKind.Unspecified).AddTicks(4007), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 26, 8, 20, 27, 820, DateTimeKind.Unspecified).AddTicks(5939), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), 80, new DateTimeOffset(new DateTime(2024, 3, 14, 8, 1, 5, 55, DateTimeKind.Unspecified).AddTicks(6269), new TimeSpan(0, 0, 0, 0, 0)), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new DateTimeOffset(new DateTime(2024, 5, 6, 21, 18, 8, 32, DateTimeKind.Unspecified).AddTicks(223), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=1030", 17, 7959, "Gorgeous Rubber Chicken", 18.404963257911130m, new DateTimeOffset(new DateTime(2024, 11, 21, 4, 46, 39, 327, DateTimeKind.Unspecified).AddTicks(9897), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 7, 13, 13, 45, 188, DateTimeKind.Unspecified).AddTicks(3999), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("95d651b3-d189-90d2-b43e-07fdb192cd7d"), 18, new DateTimeOffset(new DateTime(2023, 7, 6, 6, 8, 35, 921, DateTimeKind.Unspecified).AddTicks(6822), new TimeSpan(0, 0, 0, 0, 0)), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new DateTimeOffset(new DateTime(2024, 1, 26, 1, 7, 14, 525, DateTimeKind.Unspecified).AddTicks(2401), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=181", 14, 8526, "Fantastic Rubber Gloves", 33.645110267049220m, new DateTimeOffset(new DateTime(2025, 4, 5, 14, 57, 22, 874, DateTimeKind.Unspecified).AddTicks(3719), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 10, 8, 36, 28, 987, DateTimeKind.Unspecified).AddTicks(2980), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9af1bc12-1ac4-5ec1-a986-da5b57bd3f8e"), 7, new DateTimeOffset(new DateTime(2024, 2, 14, 4, 24, 2, 507, DateTimeKind.Unspecified).AddTicks(5070), new TimeSpan(0, 0, 0, 0, 0)), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new DateTimeOffset(new DateTime(2024, 1, 6, 16, 13, 55, 556, DateTimeKind.Unspecified).AddTicks(7655), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=1028", 47, 3666, "Practical Frozen Shirt", 21.028795042554280m, new DateTimeOffset(new DateTime(2024, 10, 29, 21, 52, 23, 755, DateTimeKind.Unspecified).AddTicks(8558), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 20, 7, 20, 48, 381, DateTimeKind.Unspecified).AddTicks(1861), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9ca00e7a-c1e3-6c17-e48e-824e0221be0f"), 70, new DateTimeOffset(new DateTime(2023, 7, 23, 3, 20, 40, 466, DateTimeKind.Unspecified).AddTicks(8926), new TimeSpan(0, 0, 0, 0, 0)), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new DateTimeOffset(new DateTime(2024, 5, 3, 18, 27, 41, 983, DateTimeKind.Unspecified).AddTicks(8028), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=383", 40, 1637, "Handcrafted Metal Shoes", 57.362738161982410m, new DateTimeOffset(new DateTime(2024, 11, 8, 6, 49, 12, 667, DateTimeKind.Unspecified).AddTicks(7501), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 3, 15, 43, 23, 105, DateTimeKind.Unspecified).AddTicks(9114), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9f89e5fd-f4ec-cd60-2540-3bb64f99fa25"), 24, new DateTimeOffset(new DateTime(2024, 6, 2, 19, 49, 34, 415, DateTimeKind.Unspecified).AddTicks(5786), new TimeSpan(0, 0, 0, 0, 0)), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new DateTimeOffset(new DateTime(2024, 3, 8, 19, 34, 15, 476, DateTimeKind.Unspecified).AddTicks(443), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=663", 53, 3636, "Practical Fresh Mouse", 21.991229556496840m, new DateTimeOffset(new DateTime(2025, 3, 10, 12, 4, 25, 268, DateTimeKind.Unspecified).AddTicks(6964), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 19, 5, 59, 6, 984, DateTimeKind.Unspecified).AddTicks(5144), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("af42b839-d29a-01f4-ee7b-954bca486fce"), 23, new DateTimeOffset(new DateTime(2024, 5, 13, 3, 39, 58, 698, DateTimeKind.Unspecified).AddTicks(5911), new TimeSpan(0, 0, 0, 0, 0)), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new DateTimeOffset(new DateTime(2023, 7, 22, 20, 12, 8, 660, DateTimeKind.Unspecified).AddTicks(8992), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=857", 48, 2179, "Refined Frozen Chips", 24.115663526633560m, new DateTimeOffset(new DateTime(2024, 10, 27, 4, 13, 59, 338, DateTimeKind.Unspecified).AddTicks(2175), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 26, 9, 54, 46, 432, DateTimeKind.Unspecified).AddTicks(5110), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("b7fcec49-f261-22db-d65c-e604d7befa1c"), 69, new DateTimeOffset(new DateTime(2024, 3, 23, 17, 30, 13, 842, DateTimeKind.Unspecified).AddTicks(1742), new TimeSpan(0, 0, 0, 0, 0)), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new DateTimeOffset(new DateTime(2024, 5, 10, 19, 24, 59, 646, DateTimeKind.Unspecified).AddTicks(7113), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=822", 16, 5025, "Ergonomic Granite Chair", 46.055659659232780m, new DateTimeOffset(new DateTime(2025, 1, 27, 22, 36, 6, 767, DateTimeKind.Unspecified).AddTicks(7813), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 8, 4, 11, 59, 49, 675, DateTimeKind.Unspecified).AddTicks(1398), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c5de3940-0944-c4f8-fa93-da03b195bc4b"), 66, new DateTimeOffset(new DateTime(2024, 5, 8, 15, 44, 5, 811, DateTimeKind.Unspecified).AddTicks(1314), new TimeSpan(0, 0, 0, 0, 0)), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new DateTimeOffset(new DateTime(2023, 10, 10, 4, 55, 27, 373, DateTimeKind.Unspecified).AddTicks(3656), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=636", 94, 7220, "Intelligent Rubber Pants", 39.553668545351230m, new DateTimeOffset(new DateTime(2024, 8, 28, 13, 53, 1, 458, DateTimeKind.Unspecified).AddTicks(7828), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 2, 12, 0, 21, 845, DateTimeKind.Unspecified).AddTicks(9973), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("cac7873f-f608-0434-c6ce-ec6dfd32d231"), 32, new DateTimeOffset(new DateTime(2024, 4, 30, 3, 38, 57, 390, DateTimeKind.Unspecified).AddTicks(5752), new TimeSpan(0, 0, 0, 0, 0)), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new DateTimeOffset(new DateTime(2023, 8, 5, 21, 54, 23, 935, DateTimeKind.Unspecified).AddTicks(9322), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=917", 21, 7475, "Sleek Fresh Chips", 72.461103141522520m, new DateTimeOffset(new DateTime(2024, 7, 24, 13, 24, 59, 904, DateTimeKind.Unspecified).AddTicks(4347), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 4, 4, 22, 8, 605, DateTimeKind.Unspecified).AddTicks(7711), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Subject", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9"), new DateTimeOffset(new DateTime(2024, 6, 15, 0, 54, 3, 81, DateTimeKind.Unspecified).AddTicks(5093), new TimeSpan(0, 0, 0, 0, 0)), "Emie.Conn@gmail.com", "Beaulah", "MacGyver", "google-oauth2|101146750969927268793", new DateTimeOffset(new DateTime(2024, 1, 1, 5, 29, 56, 516, DateTimeKind.Unspecified).AddTicks(3251), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c555c709-25db-20da-efac-4b1318c28344"), new DateTimeOffset(new DateTime(2024, 5, 29, 20, 21, 23, 67, DateTimeKind.Unspecified).AddTicks(8804), new TimeSpan(0, 0, 0, 0, 0)), "Rosalyn_Gislason@hotmail.com", "Meggie", "Pfeffer", "auth0|668514d53722eb41601e3d3f", new DateTimeOffset(new DateTime(2024, 5, 20, 1, 11, 37, 587, DateTimeKind.Unspecified).AddTicks(785), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "Currency", "Status", "TicketTypeId", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("58533b0f-a3f0-1828-4be9-91eb81e312bc"), new DateTimeOffset(new DateTime(2024, 6, 3, 5, 56, 27, 224, DateTimeKind.Unspecified).AddTicks(5418), new TimeSpan(0, 0, 0, 0, 0)), "DKK", 0, new Guid("5e9a0d85-3458-5d74-1d4a-6726411a7a5a"), 77.506442110755640m, new DateTimeOffset(new DateTime(2024, 3, 12, 14, 57, 24, 153, DateTimeKind.Unspecified).AddTicks(1156), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c555c709-25db-20da-efac-4b1318c28344") },
                    { new Guid("5bda86a9-bd57-8e3f-e610-398f9334304d"), new DateTimeOffset(new DateTime(2024, 2, 20, 7, 20, 48, 385, DateTimeKind.Unspecified).AddTicks(5854), new TimeSpan(0, 0, 0, 0, 0)), "ISK", 1, new Guid("9ca00e7a-c1e3-6c17-e48e-824e0221be0f"), 57.362738161982410m, new DateTimeOffset(new DateTime(2024, 7, 1, 23, 56, 41, 748, DateTimeKind.Unspecified).AddTicks(4119), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c555c709-25db-20da-efac-4b1318c28344") },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2023, 7, 23, 3, 51, 25, 466, DateTimeKind.Unspecified).AddTicks(4755), new TimeSpan(0, 0, 0, 0, 0)), "SCR", 2, new Guid("46f23aa3-74ec-3fb7-32a8-9b9879ce7d7f"), 73.154406982080250m, new DateTimeOffset(new DateTime(2024, 5, 31, 14, 23, 11, 535, DateTimeKind.Unspecified).AddTicks(5617), new TimeSpan(0, 0, 0, 0, 0)), new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9") },
                    { new Guid("846fa34e-4aa5-7adf-0ea0-9ce3c1176ce4"), new DateTimeOffset(new DateTime(2023, 10, 25, 20, 2, 58, 769, DateTimeKind.Unspecified).AddTicks(2264), new TimeSpan(0, 0, 0, 0, 0)), "ZMK", 1, new Guid("5e4bc528-fd4d-51ad-64c4-a05b7d64a131"), 27.265351860441910m, new DateTimeOffset(new DateTime(2024, 2, 14, 6, 40, 49, 938, DateTimeKind.Unspecified).AddTicks(2436), new TimeSpan(0, 0, 0, 0, 0)), new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9") },
                    { new Guid("8995d651-d2d1-b490-3e07-fdb192cd7d94"), new DateTimeOffset(new DateTime(2024, 3, 30, 14, 56, 53, 482, DateTimeKind.Unspecified).AddTicks(4310), new TimeSpan(0, 0, 0, 0, 0)), "CNY", 0, new Guid("3e43502f-fc33-ef7b-5802-9b4cd3cabf28"), 62.061236953391350m, new DateTimeOffset(new DateTime(2024, 1, 26, 1, 7, 14, 529, DateTimeKind.Unspecified).AddTicks(6394), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c555c709-25db-20da-efac-4b1318c28344") }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Organizers",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "OrganizationName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9"), new DateTimeOffset(new DateTime(2024, 4, 5, 0, 1, 22, 560, DateTimeKind.Unspecified).AddTicks(3798), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=347", "Nitzsche Inc", new DateTimeOffset(new DateTime(2023, 7, 8, 12, 35, 46, 760, DateTimeKind.Unspecified).AddTicks(8320), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("c555c709-25db-20da-efac-4b1318c28344"), new DateTimeOffset(new DateTime(2023, 11, 7, 3, 3, 53, 945, DateTimeKind.Unspecified).AddTicks(396), new TimeSpan(0, 0, 0, 0, 0)), "https://picsum.photos/640/480/?image=469", "Konopelski Group", new DateTimeOffset(new DateTime(2024, 2, 26, 4, 26, 21, 5, DateTimeKind.Unspecified).AddTicks(8370), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Events",
                columns: new[] { "Id", "BackgroundImageUrl", "BannerImageUrl", "CreatedAt", "Description", "Location", "MaxTickets", "OrganizerId", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "https://picsum.photos/640/480/?image=570", "https://picsum.photos/640/480/?image=183", new DateTimeOffset(new DateTime(2024, 2, 11, 3, 46, 26, 255, DateTimeKind.Unspecified).AddTicks(21), new TimeSpan(0, 0, 0, 0, 0)), "Quibusdam ipsum autem nemo qui rerum. Porro cumque perferendis doloribus aut vero veritatis. Cupiditate enim asperiores eligendi iure iusto cum.", "Jaquanberg", 723, new Guid("c555c709-25db-20da-efac-4b1318c28344"), 1, "Et nihil cumque hic explicabo.", new DateTimeOffset(new DateTime(2024, 5, 8, 22, 43, 49, 868, DateTimeKind.Unspecified).AddTicks(997), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), "https://picsum.photos/640/480/?image=149", "https://picsum.photos/640/480/?image=923", new DateTimeOffset(new DateTime(2023, 7, 10, 13, 58, 20, 33, DateTimeKind.Unspecified).AddTicks(9144), new TimeSpan(0, 0, 0, 0, 0)), "Rerum quia quos voluptatem veritatis totam ea iure quis. Et quo vero. Sunt accusantium rerum tempora officiis voluptatum ullam quia consequuntur. Voluptatum consequatur iste nemo quasi.", "East Iciemouth", 386, new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9"), 0, "Dolor molestiae a.", new DateTimeOffset(new DateTime(2023, 11, 9, 9, 47, 1, 499, DateTimeKind.Unspecified).AddTicks(8019), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("d8b16773-22de-43a5-ceb5-1d18f9205e85"), "https://picsum.photos/640/480/?image=356", "https://picsum.photos/640/480/?image=796", new DateTimeOffset(new DateTime(2023, 7, 27, 15, 3, 32, 800, DateTimeKind.Unspecified).AddTicks(9048), new TimeSpan(0, 0, 0, 0, 0)), "Aspernatur dolores cupiditate eum. Dignissimos iusto earum rerum commodi et inventore velit. Nobis qui ea. Excepturi necessitatibus et. Vel atque et. Dolorum voluptatem exercitationem suscipit.", "New Garretville", 691, new Guid("c555c709-25db-20da-efac-4b1318c28344"), 2, "Temporibus quia ut aut voluptatem minima.", new DateTimeOffset(new DateTime(2023, 10, 15, 13, 25, 51, 816, DateTimeKind.Unspecified).AddTicks(760), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e5fd779a-9f89-f4ec-60cd-25403bb64f99"), "https://picsum.photos/640/480/?image=153", "https://picsum.photos/640/480/?image=899", new DateTimeOffset(new DateTime(2024, 1, 4, 16, 9, 50, 862, DateTimeKind.Unspecified).AddTicks(6527), new TimeSpan(0, 0, 0, 0, 0)), "Laborum dolores quia soluta ut cupiditate ex dicta. Ea sunt ipsum rem. Quaerat qui corrupti.", "Port Orahaven", 690, new Guid("c555c709-25db-20da-efac-4b1318c28344"), 2, "Dolor laudantium dolorem.", new DateTimeOffset(new DateTime(2024, 4, 18, 1, 32, 3, 311, DateTimeKind.Unspecified).AddTicks(8483), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), "https://picsum.photos/640/480/?image=946", "https://picsum.photos/640/480/?image=699", new DateTimeOffset(new DateTime(2023, 12, 22, 23, 32, 36, 466, DateTimeKind.Unspecified).AddTicks(1076), new TimeSpan(0, 0, 0, 0, 0)), "Labore doloremque similique et modi dolorum animi dolor. Nobis ipsam ut sed quia asperiores officiis. Ex dolorem cumque. At officiis rem nostrum quod. Vel quod quibusdam molestias beatae laboriosam iure saepe quae.", "Victoriahaven", 494, new Guid("c555c709-25db-20da-efac-4b1318c28344"), 1, "Quia nulla eligendi repudiandae illo voluptatem sed maxime dolor vitae.", new DateTimeOffset(new DateTime(2024, 2, 29, 15, 6, 52, 345, DateTimeKind.Unspecified).AddTicks(4673), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Attendees",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "Email", "EventId", "FirstName", "LastName", "PhoneNumber", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("51b355f6-95d6-d189-d290-b43e07fdb192"), new DateTimeOffset(new DateTime(2023, 8, 26, 18, 14, 41, 841, DateTimeKind.Unspecified).AddTicks(9443), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1997, 8, 21, 9, 34, 2, 475, DateTimeKind.Unspecified).AddTicks(6264), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), "Fernando", "Hills", "(996) 486-9561 x00102", new DateTimeOffset(new DateTime(2023, 8, 19, 14, 45, 36, 92, DateTimeKind.Unspecified).AddTicks(7674), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2024, 1, 14, 22, 14, 1, 129, DateTimeKind.Unspecified).AddTicks(5453), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1963, 5, 10, 19, 28, 25, 56, DateTimeKind.Unspecified).AddTicks(1362), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "Nick", "Purdy", "(660) 798-8055 x29634", new DateTimeOffset(new DateTime(2024, 2, 23, 22, 33, 56, 703, DateTimeKind.Unspecified).AddTicks(3467), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c555c709-25db-20da-efac-4b1318c28344") },
                    { new Guid("e0967ad4-70ad-6346-df22-93f92710bd1a"), new DateTimeOffset(new DateTime(2023, 11, 7, 12, 32, 24, 470, DateTimeKind.Unspecified).AddTicks(8522), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1970, 4, 1, 18, 40, 43, 332, DateTimeKind.Unspecified).AddTicks(3288), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "Josh", "Nolan", "433-910-9482 x47120", new DateTimeOffset(new DateTime(2023, 7, 27, 15, 3, 32, 810, DateTimeKind.Unspecified).AddTicks(8465), new TimeSpan(0, 0, 0, 0, 0)), new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9") },
                    { new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), new DateTimeOffset(new DateTime(2024, 5, 12, 18, 29, 46, 828, DateTimeKind.Unspecified).AddTicks(1445), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1992, 10, 20, 1, 51, 41, 776, DateTimeKind.Unspecified).AddTicks(2716), new TimeSpan(0, 0, 0, 0, 0)), "", new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "Brian", "Dare", "1-424-251-4025 x419", new DateTimeOffset(new DateTime(2024, 2, 11, 19, 29, 27, 525, DateTimeKind.Unspecified).AddTicks(9643), new TimeSpan(0, 0, 0, 0, 0)), new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9") }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "EventCategories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "EventId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1ac49af1-5ec1-86a9-da5b-57bd3f8ee610"), new Guid("c1724c95-1f1f-de8f-d409-b2d248077225"), new DateTimeOffset(new DateTime(2023, 7, 23, 22, 39, 58, 403, DateTimeKind.Unspecified).AddTicks(5467), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), new DateTimeOffset(new DateTime(2024, 5, 20, 22, 18, 1, 990, DateTimeKind.Unspecified).AddTicks(8111), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("3c5a4d30-f652-b355-51d6-9589d1d290b4"), new Guid("eb946cbe-94be-f506-64dd-a95675991f08"), new DateTimeOffset(new DateTime(2024, 3, 16, 19, 27, 2, 130, DateTimeKind.Unspecified).AddTicks(6648), new TimeSpan(0, 0, 0, 0, 0)), new Guid("e5fd779a-9f89-f4ec-60cd-25403bb64f99"), new DateTimeOffset(new DateTime(2024, 5, 20, 23, 16, 33, 495, DateTimeKind.Unspecified).AddTicks(4837), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new Guid("500e0755-b3ea-09c9-4018-f6ff5f134b47"), new DateTimeOffset(new DateTime(2023, 10, 21, 0, 33, 55, 658, DateTimeKind.Unspecified).AddTicks(7194), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), new DateTimeOffset(new DateTime(2023, 10, 21, 22, 7, 44, 227, DateTimeKind.Unspecified).AddTicks(9174), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a34e5b13-846f-4aa5-df7a-0ea09ce3c117"), new Guid("f1bc12e3-c49a-c11a-5ea9-86da5b57bd3f"), new DateTimeOffset(new DateTime(2023, 12, 29, 12, 16, 34, 382, DateTimeKind.Unspecified).AddTicks(7534), new TimeSpan(0, 0, 0, 0, 0)), new Guid("e5fd779a-9f89-f4ec-60cd-25403bb64f99"), new DateTimeOffset(new DateTime(2023, 12, 25, 12, 50, 33, 113, DateTimeKind.Unspecified).AddTicks(6797), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("be21024e-3b0f-5853-f0a3-28184be991eb"), new Guid("34938f39-4d30-3c5a-52f6-55b351d69589"), new DateTimeOffset(new DateTime(2023, 7, 14, 10, 6, 46, 490, DateTimeKind.Unspecified).AddTicks(1698), new TimeSpan(0, 0, 0, 0, 0)), new Guid("e5fd779a-9f89-f4ec-60cd-25403bb64f99"), new DateTimeOffset(new DateTime(2024, 1, 8, 3, 39, 52, 52, DateTimeKind.Unspecified).AddTicks(4865), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Shows",
                columns: new[] { "Id", "CreatedAt", "EndsAt", "EventId", "StartsAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0dcfe258-02ef-152a-5e8d-7e518a270688"), new DateTimeOffset(new DateTime(2024, 3, 4, 19, 11, 41, 931, DateTimeKind.Unspecified).AddTicks(5038), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 21, 15, 58, 11, 800, DateTimeKind.Unspecified).AddTicks(245), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), new DateTimeOffset(new DateTime(2023, 9, 17, 3, 14, 10, 584, DateTimeKind.Unspecified).AddTicks(4761), new TimeSpan(0, 0, 0, 0, 0)), "sequi", new DateTimeOffset(new DateTime(2024, 3, 12, 1, 5, 40, 277, DateTimeKind.Unspecified).AddTicks(1084), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22df6346-f993-1027-bd1a-f25f3a06ca56"), new DateTimeOffset(new DateTime(2024, 3, 6, 20, 16, 17, 883, DateTimeKind.Unspecified).AddTicks(3649), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 31, 5, 38, 59, 523, DateTimeKind.Unspecified).AddTicks(1083), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), new DateTimeOffset(new DateTime(2023, 8, 17, 20, 48, 37, 857, DateTimeKind.Unspecified).AddTicks(718), new TimeSpan(0, 0, 0, 0, 0)), "nisi", new DateTimeOffset(new DateTime(2023, 10, 28, 0, 18, 6, 973, DateTimeKind.Unspecified).AddTicks(2514), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("2f3360a5-9116-1388-886d-02bc0144d5e3"), new DateTimeOffset(new DateTime(2023, 12, 28, 11, 11, 47, 718, DateTimeKind.Unspecified).AddTicks(6111), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 29, 17, 32, 56, 515, DateTimeKind.Unspecified).AddTicks(9240), new TimeSpan(0, 0, 0, 0, 0)), new Guid("e5fd779a-9f89-f4ec-60cd-25403bb64f99"), new DateTimeOffset(new DateTime(2023, 8, 11, 19, 44, 52, 24, DateTimeKind.Unspecified).AddTicks(8342), new TimeSpan(0, 0, 0, 0, 0)), "aperiam", new DateTimeOffset(new DateTime(2023, 7, 28, 16, 20, 27, 950, DateTimeKind.Unspecified).AddTicks(6937), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("3631ff8d-7d5e-2382-78fc-98684039dec5"), new DateTimeOffset(new DateTime(2024, 2, 17, 3, 41, 41, 371, DateTimeKind.Unspecified).AddTicks(570), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 8, 5, 49, 46, 720, DateTimeKind.Unspecified).AddTicks(5638), new TimeSpan(0, 0, 0, 0, 0)), new Guid("e5fd779a-9f89-f4ec-60cd-25403bb64f99"), new DateTimeOffset(new DateTime(2023, 11, 17, 17, 26, 14, 150, DateTimeKind.Unspecified).AddTicks(7355), new TimeSpan(0, 0, 0, 0, 0)), "excepturi", new DateTimeOffset(new DateTime(2024, 1, 10, 0, 49, 1, 426, DateTimeKind.Unspecified).AddTicks(1185), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("39873033-42b8-9aaf-d2f4-01ee7b954bca"), new DateTimeOffset(new DateTime(2023, 7, 24, 20, 13, 4, 21, DateTimeKind.Unspecified).AddTicks(6925), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 15, 2, 34, 0, 46, DateTimeKind.Unspecified).AddTicks(3127), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d8b16773-22de-43a5-ceb5-1d18f9205e85"), new DateTimeOffset(new DateTime(2023, 12, 23, 12, 43, 46, 333, DateTimeKind.Unspecified).AddTicks(9027), new TimeSpan(0, 0, 0, 0, 0)), "quia", new DateTimeOffset(new DateTime(2023, 7, 15, 0, 13, 35, 13, DateTimeKind.Unspecified).AddTicks(2297), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("58533b0f-a3f0-1828-4be9-91eb81e312bc"), new DateTimeOffset(new DateTime(2024, 6, 3, 5, 56, 27, 218, DateTimeKind.Unspecified).AddTicks(3715), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 4, 19, 54, 866, DateTimeKind.Unspecified).AddTicks(6848), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2024, 5, 21, 13, 18, 23, 212, DateTimeKind.Unspecified).AddTicks(1980), new TimeSpan(0, 0, 0, 0, 0)), "rerum", new DateTimeOffset(new DateTime(2024, 3, 12, 14, 57, 24, 146, DateTimeKind.Unspecified).AddTicks(9453), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5bda86a9-bd57-8e3f-e610-398f9334304d"), new DateTimeOffset(new DateTime(2024, 2, 20, 7, 20, 48, 379, DateTimeKind.Unspecified).AddTicks(4151), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 22, 23, 44, 10, 331, DateTimeKind.Unspecified).AddTicks(1490), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), new DateTimeOffset(new DateTime(2024, 2, 23, 22, 33, 56, 699, DateTimeKind.Unspecified).AddTicks(6468), new TimeSpan(0, 0, 0, 0, 0)), "veritatis", new DateTimeOffset(new DateTime(2024, 7, 1, 23, 56, 41, 742, DateTimeKind.Unspecified).AddTicks(2416), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("5d743458-4a1d-2667-411a-7a5ad14ba33a"), new DateTimeOffset(new DateTime(2024, 6, 3, 19, 48, 19, 334, DateTimeKind.Unspecified).AddTicks(3918), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 3, 9, 0, 44, 753, DateTimeKind.Unspecified).AddTicks(7732), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d8b16773-22de-43a5-ceb5-1d18f9205e85"), new DateTimeOffset(new DateTime(2024, 1, 16, 14, 41, 59, 482, DateTimeKind.Unspecified).AddTicks(8796), new TimeSpan(0, 0, 0, 0, 0)), "voluptatem", new DateTimeOffset(new DateTime(2024, 5, 23, 16, 32, 41, 985, DateTimeKind.Unspecified).AddTicks(5780), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("60f4ec9f-25cd-3b40-b64f-99fa252e84c9"), new DateTimeOffset(new DateTime(2023, 10, 29, 8, 59, 37, 666, DateTimeKind.Unspecified).AddTicks(260), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 30, 10, 6, 56, 86, DateTimeKind.Unspecified).AddTicks(5695), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d8b16773-22de-43a5-ceb5-1d18f9205e85"), new DateTimeOffset(new DateTime(2024, 5, 17, 0, 22, 1, 2, DateTimeKind.Unspecified).AddTicks(7204), new TimeSpan(0, 0, 0, 0, 0)), "facilis", new DateTimeOffset(new DateTime(2024, 4, 10, 2, 12, 57, 680, DateTimeKind.Unspecified).AddTicks(7191), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("63a1e292-c5cf-e7f0-1d98-afd125a33af2"), new DateTimeOffset(new DateTime(2023, 11, 20, 5, 27, 23, 843, DateTimeKind.Unspecified).AddTicks(5581), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 23, 16, 33, 58, 450, DateTimeKind.Unspecified).AddTicks(4218), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), new DateTimeOffset(new DateTime(2024, 4, 7, 6, 31, 56, 552, DateTimeKind.Unspecified).AddTicks(7493), new TimeSpan(0, 0, 0, 0, 0)), "doloremque", new DateTimeOffset(new DateTime(2023, 11, 27, 17, 54, 37, 420, DateTimeKind.Unspecified).AddTicks(7574), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("6e1e1df6-f5fa-3fd8-624f-f92f50433e33"), new DateTimeOffset(new DateTime(2024, 6, 3, 9, 5, 40, 548, DateTimeKind.Unspecified).AddTicks(9502), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 22, 18, 9, 17, 795, DateTimeKind.Unspecified).AddTicks(2222), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2023, 8, 13, 11, 16, 5, 650, DateTimeKind.Unspecified).AddTicks(5455), new TimeSpan(0, 0, 0, 0, 0)), "soluta", new DateTimeOffset(new DateTime(2023, 8, 25, 17, 17, 21, 334, DateTimeKind.Unspecified).AddTicks(719), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("79989ba8-7dce-6b7f-bfd1-ae7f5f6c3c28"), new DateTimeOffset(new DateTime(2024, 2, 16, 9, 4, 48, 835, DateTimeKind.Unspecified).AddTicks(2899), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 22, 22, 7, 34, 403, DateTimeKind.Unspecified).AddTicks(3401), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), new DateTimeOffset(new DateTime(2023, 10, 8, 6, 35, 16, 991, DateTimeKind.Unspecified).AddTicks(5737), new TimeSpan(0, 0, 0, 0, 0)), "rem", new DateTimeOffset(new DateTime(2023, 10, 8, 5, 32, 5, 249, DateTimeKind.Unspecified).AddTicks(1934), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2023, 7, 23, 3, 51, 25, 460, DateTimeKind.Unspecified).AddTicks(3052), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 17, 22, 26, 59, 375, DateTimeKind.Unspecified).AddTicks(9797), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d8b16773-22de-43a5-ceb5-1d18f9205e85"), new DateTimeOffset(new DateTime(2023, 10, 21, 0, 33, 55, 661, DateTimeKind.Unspecified).AddTicks(4475), new TimeSpan(0, 0, 0, 0, 0)), "veniam", new DateTimeOffset(new DateTime(2024, 5, 31, 14, 23, 11, 529, DateTimeKind.Unspecified).AddTicks(3914), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("846fa34e-4aa5-7adf-0ea0-9ce3c1176ce4"), new DateTimeOffset(new DateTime(2023, 10, 25, 20, 2, 58, 763, DateTimeKind.Unspecified).AddTicks(561), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 2, 12, 22, 1, 112, DateTimeKind.Unspecified).AddTicks(2149), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), new DateTimeOffset(new DateTime(2024, 3, 26, 22, 12, 53, 132, DateTimeKind.Unspecified).AddTicks(9734), new TimeSpan(0, 0, 0, 0, 0)), "sint", new DateTimeOffset(new DateTime(2024, 2, 14, 6, 40, 49, 932, DateTimeKind.Unspecified).AddTicks(733), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("8995d651-d2d1-b490-3e07-fdb192cd7d94"), new DateTimeOffset(new DateTime(2024, 3, 30, 14, 56, 53, 476, DateTimeKind.Unspecified).AddTicks(2607), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 3, 22, 50, 20, 231, DateTimeKind.Unspecified).AddTicks(3875), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), new DateTimeOffset(new DateTime(2024, 6, 9, 17, 13, 47, 208, DateTimeKind.Unspecified).AddTicks(6993), new TimeSpan(0, 0, 0, 0, 0)), "iste", new DateTimeOffset(new DateTime(2024, 1, 26, 1, 7, 14, 523, DateTimeKind.Unspecified).AddTicks(4691), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("95b103da-4bbc-e289-6c29-cb8f17449c7e"), new DateTimeOffset(new DateTime(2023, 8, 10, 4, 48, 19, 618, DateTimeKind.Unspecified).AddTicks(103), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 17, 20, 59, 16, 874, DateTimeKind.Unspecified).AddTicks(2055), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2024, 7, 2, 12, 0, 21, 844, DateTimeKind.Unspecified).AddTicks(2263), new TimeSpan(0, 0, 0, 0, 0)), "quisquam", new DateTimeOffset(new DateTime(2024, 5, 5, 18, 26, 38, 540, DateTimeKind.Unspecified).AddTicks(6539), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9a22b19c-c528-5e4b-4dfd-ad5164c4a05b"), new DateTimeOffset(new DateTime(2023, 11, 7, 19, 23, 33, 896, DateTimeKind.Unspecified).AddTicks(6627), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 27, 8, 30, 38, 170, DateTimeKind.Unspecified).AddTicks(4512), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2023, 9, 5, 11, 25, 51, 245, DateTimeKind.Unspecified).AddTicks(2371), new TimeSpan(0, 0, 0, 0, 0)), "soluta", new DateTimeOffset(new DateTime(2024, 1, 4, 16, 9, 50, 868, DateTimeKind.Unspecified).AddTicks(8945), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("bfcad34c-6528-35b9-f45e-fc20bb047bba"), new DateTimeOffset(new DateTime(2024, 3, 27, 19, 42, 13, 465, DateTimeKind.Unspecified).AddTicks(6057), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 11, 23, 9, 26, 88, DateTimeKind.Unspecified).AddTicks(5002), new TimeSpan(0, 0, 0, 0, 0)), new Guid("d8b16773-22de-43a5-ceb5-1d18f9205e85"), new DateTimeOffset(new DateTime(2023, 7, 19, 9, 15, 45, 814, DateTimeKind.Unspecified).AddTicks(5470), new TimeSpan(0, 0, 0, 0, 0)), "praesentium", new DateTimeOffset(new DateTime(2024, 7, 3, 5, 29, 17, 84, DateTimeKind.Unspecified).AddTicks(624), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("ce43a522-1db5-f918-205e-853754359aac"), new DateTimeOffset(new DateTime(2024, 5, 29, 17, 6, 23, 921, DateTimeKind.Unspecified).AddTicks(211), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 28, 15, 18, 4, 512, DateTimeKind.Unspecified).AddTicks(6180), new TimeSpan(0, 0, 0, 0, 0)), new Guid("bc12e381-9af1-1ac4-c15e-a986da5b57bd"), new DateTimeOffset(new DateTime(2023, 7, 23, 5, 10, 8, 152, DateTimeKind.Unspecified).AddTicks(6152), new TimeSpan(0, 0, 0, 0, 0)), "voluptatem", new DateTimeOffset(new DateTime(2024, 1, 5, 16, 26, 0, 492, DateTimeKind.Unspecified).AddTicks(5634), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("de4ff2b1-a48b-873f-c7ca-08f63404c6ce"), new DateTimeOffset(new DateTime(2024, 6, 4, 9, 28, 0, 647, DateTimeKind.Unspecified).AddTicks(1337), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 21, 0, 26, 713, DateTimeKind.Unspecified).AddTicks(945), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2024, 3, 19, 15, 20, 6, 967, DateTimeKind.Unspecified).AddTicks(333), new TimeSpan(0, 0, 0, 0, 0)), "magni", new DateTimeOffset(new DateTime(2023, 7, 31, 20, 55, 58, 942, DateTimeKind.Unspecified).AddTicks(3883), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Staffs",
                columns: new[] { "Id", "CreatedAt", "EventId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("41c85008-7cd1-4eb4-a73b-bbaf0f7569f5"), new DateTimeOffset(new DateTime(2024, 1, 15, 20, 29, 20, 895, DateTimeKind.Unspecified).AddTicks(1570), new TimeSpan(0, 0, 0, 0, 0)), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2023, 9, 26, 8, 56, 25, 230, DateTimeKind.Unspecified).AddTicks(1212), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c555c709-25db-20da-efac-4b1318c28344") },
                    { new Guid("54db441f-bc1b-42a6-acde-ca5a65c31f4e"), new DateTimeOffset(new DateTime(2024, 2, 26, 4, 26, 21, 12, DateTimeKind.Unspecified).AddTicks(2729), new TimeSpan(0, 0, 0, 0, 0)), new Guid("f608cac7-0434-cec6-ec6d-fd32d23192e2"), new DateTimeOffset(new DateTime(2023, 7, 25, 7, 49, 28, 977, DateTimeKind.Unspecified).AddTicks(7938), new TimeSpan(0, 0, 0, 0, 0)), new Guid("183051e7-e11c-d755-9d52-2bb36eb98bf9") }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "TicketTypeShows",
                columns: new[] { "Id", "CreatedAt", "ShowId", "TicketTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0434f608-cec6-6dec-fd32-d23192e2a163"), new DateTimeOffset(new DateTime(2023, 8, 30, 11, 6, 10, 947, DateTimeKind.Unspecified).AddTicks(8304), new TimeSpan(0, 7, 0, 0, 0)), new Guid("2f3360a5-9116-1388-886d-02bc0144d5e3"), new Guid("9ca00e7a-c1e3-6c17-e48e-824e0221be0f"), new DateTimeOffset(new DateTime(2024, 7, 4, 9, 3, 16, 770, DateTimeKind.Unspecified).AddTicks(4038), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("1027f993-1abd-5ff2-3a06-ca56f77367b1"), new DateTimeOffset(new DateTime(2024, 3, 7, 3, 16, 17, 893, DateTimeKind.Unspecified).AddTicks(935), new TimeSpan(0, 7, 0, 0, 0)), new Guid("5d743458-4a1d-2667-411a-7a5ad14ba33a"), new Guid("46f23aa3-74ec-3fb7-32a8-9b9879ce7d7f"), new DateTimeOffset(new DateTime(2024, 7, 4, 9, 16, 1, 963, DateTimeKind.Unspecified).AddTicks(7190), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("1ac49af1-5ec1-86a9-da5b-57bd3f8ee610"), new DateTimeOffset(new DateTime(2024, 2, 8, 20, 2, 52, 195, DateTimeKind.Unspecified).AddTicks(7551), new TimeSpan(0, 7, 0, 0, 0)), new Guid("2f3360a5-9116-1388-886d-02bc0144d5e3"), new Guid("9af1bc12-1ac4-5ec1-a986-da5b57bd3f8e"), new DateTimeOffset(new DateTime(2024, 7, 4, 7, 7, 40, 626, DateTimeKind.Unspecified).AddTicks(4093), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("3c5a4d30-f652-b355-51d6-9589d1d290b4"), new DateTimeOffset(new DateTime(2023, 8, 20, 13, 36, 29, 939, DateTimeKind.Unspecified).AddTicks(7520), new TimeSpan(0, 7, 0, 0, 0)), new Guid("63a1e292-c5cf-e7f0-1d98-afd125a33af2"), new Guid("9af1bc12-1ac4-5ec1-a986-da5b57bd3f8e"), new DateTimeOffset(new DateTime(2024, 7, 4, 14, 16, 46, 460, DateTimeKind.Unspecified).AddTicks(1400), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new DateTimeOffset(new DateTime(2024, 3, 23, 14, 13, 2, 817, DateTimeKind.Unspecified).AddTicks(4181), new TimeSpan(0, 7, 0, 0, 0)), new Guid("6e1e1df6-f5fa-3fd8-624f-f92f50433e33"), new Guid("4401bc02-e3d5-2492-76a7-488558e2cf0d"), new DateTimeOffset(new DateTime(2024, 7, 4, 10, 56, 33, 499, DateTimeKind.Unspecified).AddTicks(7452), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("947dcd92-6b4b-bdd9-0380-b1f24fde8ba4"), new DateTimeOffset(new DateTime(2023, 7, 10, 20, 58, 20, 49, DateTimeKind.Unspecified).AddTicks(8848), new TimeSpan(0, 7, 0, 0, 0)), new Guid("39873033-42b8-9aaf-d2f4-01ee7b954bca"), new Guid("af42b839-d29a-01f4-ee7b-954bca486fce"), new DateTimeOffset(new DateTime(2024, 7, 4, 10, 4, 48, 14, DateTimeKind.Unspecified).AddTicks(3551), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("a34e5b13-846f-4aa5-df7a-0ea09ce3c117"), new DateTimeOffset(new DateTime(2023, 8, 25, 18, 30, 25, 841, DateTimeKind.Unspecified).AddTicks(7284), new TimeSpan(0, 7, 0, 0, 0)), new Guid("3631ff8d-7d5e-2382-78fc-98684039dec5"), new Guid("5e4bc528-fd4d-51ad-64c4-a05b7d64a131"), new DateTimeOffset(new DateTime(2024, 7, 5, 0, 12, 39, 142, DateTimeKind.Unspecified).AddTicks(1526), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("be21024e-3b0f-5853-f0a3-28184be991eb"), new DateTimeOffset(new DateTime(2023, 7, 23, 10, 20, 40, 474, DateTimeKind.Unspecified).AddTicks(8502), new TimeSpan(0, 7, 0, 0, 0)), new Guid("0dcfe258-02ef-152a-5e8d-7e518a270688"), new Guid("9f89e5fd-f4ec-cd60-2540-3bb64f99fa25"), new DateTimeOffset(new DateTime(2024, 7, 4, 23, 40, 12, 7, DateTimeKind.Unspecified).AddTicks(7925), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("d1af981d-a325-f23a-46ec-74b73f32a89b"), new DateTimeOffset(new DateTime(2023, 10, 29, 3, 27, 19, 348, DateTimeKind.Unspecified).AddTicks(9307), new TimeSpan(0, 7, 0, 0, 0)), new Guid("63a1e292-c5cf-e7f0-1d98-afd125a33af2"), new Guid("5e4bc528-fd4d-51ad-64c4-a05b7d64a131"), new DateTimeOffset(new DateTime(2024, 7, 4, 18, 10, 56, 736, DateTimeKind.Unspecified).AddTicks(7032), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("d1bf6b7f-7fae-6c5f-3c28-d47a96e0ad70"), new DateTimeOffset(new DateTime(2023, 9, 17, 15, 24, 24, 512, DateTimeKind.Unspecified).AddTicks(5625), new TimeSpan(0, 7, 0, 0, 0)), new Guid("846fa34e-4aa5-7adf-0ea0-9ce3c1176ce4"), new Guid("634670ad-22df-f993-2710-bd1af25f3a06"), new DateTimeOffset(new DateTime(2024, 7, 4, 13, 30, 6, 528, DateTimeKind.Unspecified).AddTicks(9165), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "wowtogo",
                table: "Tickets",
                columns: new[] { "Id", "AttendeeId", "Code", "CreatedAt", "TicketTypeId", "UpdatedAt", "UsedAt", "UsedInFormat" },
                values: new object[,]
                {
                    { new Guid("0836b438-6ab7-b61f-2723-7c8bd6f98575"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "AMQVPKFT2K", new DateTimeOffset(new DateTime(2024, 5, 30, 19, 34, 13, 322, DateTimeKind.Unspecified).AddTicks(6582), new TimeSpan(0, 0, 0, 0, 0)), new Guid("09dcaeba-58f8-8995-19c4-f71079e80dc5"), new DateTimeOffset(new DateTime(2024, 2, 18, 13, 13, 38, 921, DateTimeKind.Unspecified).AddTicks(6987), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 16, 21, 2, 55, 662, DateTimeKind.Unspecified).AddTicks(8928), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("17c1e39c-e46c-828e-4e02-21be0f3b5358"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "I6COE5Y3ZH", new DateTimeOffset(new DateTime(2024, 5, 21, 13, 18, 23, 220, DateTimeKind.Unspecified).AddTicks(964), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20f9181d-855e-5437-359a-acea8544af13"), new DateTimeOffset(new DateTime(2024, 4, 17, 3, 56, 5, 527, DateTimeKind.Unspecified).AddTicks(3163), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 21, 21, 9, 55, 150, DateTimeKind.Unspecified).AddTicks(7806), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("184009c9-fff6-135f-4b47-a79f0fdc7228"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "LBZKK03UZP", new DateTimeOffset(new DateTime(2024, 3, 7, 3, 0, 7, 961, DateTimeKind.Unspecified).AddTicks(1771), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9af1bc12-1ac4-5ec1-a986-da5b57bd3f8e"), new DateTimeOffset(new DateTime(2023, 12, 10, 18, 52, 5, 46, DateTimeKind.Unspecified).AddTicks(6099), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 23, 21, 9, 39, 195, DateTimeKind.Unspecified).AddTicks(1641), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("1f997556-5008-0860-a286-e8720e753eaf"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "URUDCMLG8T", new DateTimeOffset(new DateTime(2024, 3, 15, 22, 19, 9, 685, DateTimeKind.Unspecified).AddTicks(8020), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9f89e5fd-f4ec-cd60-2540-3bb64f99fa25"), new DateTimeOffset(new DateTime(2023, 7, 22, 3, 13, 39, 883, DateTimeKind.Unspecified).AddTicks(1868), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 21, 10, 30, 7, 339, DateTimeKind.Unspecified).AddTicks(2257), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("23827d5e-fc78-6898-4039-dec54409f8c4"), new Guid("51b355f6-95d6-d189-d290-b43e07fdb192"), "N7M9AC8LD6", new DateTimeOffset(new DateTime(2023, 10, 10, 4, 55, 27, 379, DateTimeKind.Unspecified).AddTicks(4930), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9f89e5fd-f4ec-cd60-2540-3bb64f99fa25"), new DateTimeOffset(new DateTime(2024, 5, 10, 19, 56, 5, 823, DateTimeKind.Unspecified).AddTicks(433), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 1, 15, 16, 4, 885, DateTimeKind.Unspecified).AddTicks(8806), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("31d232fd-e292-63a1-cfc5-f0e71d98afd1"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "M81K87ML6L", new DateTimeOffset(new DateTime(2024, 2, 27, 22, 6, 13, 295, DateTimeKind.Unspecified).AddTicks(7662), new TimeSpan(0, 0, 0, 0, 0)), new Guid("af42b839-d29a-01f4-ee7b-954bca486fce"), new DateTimeOffset(new DateTime(2023, 12, 22, 0, 9, 12, 478, DateTimeKind.Unspecified).AddTicks(338), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 27, 6, 37, 22, 269, DateTimeKind.Unspecified).AddTicks(1205), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("333e4350-7bfc-58ef-029b-4cd3cabf2865"), new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), "HDK05E87GM", new DateTimeOffset(new DateTime(2024, 3, 27, 19, 42, 13, 473, DateTimeKind.Unspecified).AddTicks(5041), new TimeSpan(0, 0, 0, 0, 0)), new Guid("b7fcec49-f261-22db-d65c-e604d7befa1c"), new DateTimeOffset(new DateTime(2024, 7, 3, 5, 29, 17, 91, DateTimeKind.Unspecified).AddTicks(9608), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 11, 23, 9, 26, 96, DateTimeKind.Unspecified).AddTicks(3986), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("593a2278-363f-4ff8-8f26-e1d5db09b31f"), new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), "QEFO788OXZ", new DateTimeOffset(new DateTime(2024, 3, 24, 6, 19, 6, 812, DateTimeKind.Unspecified).AddTicks(966), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c5de3940-0944-c4f8-fa93-da03b195bc4b"), new DateTimeOffset(new DateTime(2024, 2, 20, 1, 6, 45, 530, DateTimeKind.Unspecified).AddTicks(3982), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 9, 3, 35, 18, 169, DateTimeKind.Unspecified).AddTicks(6478), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("5d743458-4a1d-2667-411a-7a5ad14ba33a"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "GZ3474ZX7G", new DateTimeOffset(new DateTime(2024, 6, 2, 3, 31, 59, 319, DateTimeKind.Unspecified).AddTicks(1873), new TimeSpan(0, 0, 0, 0, 0)), new Guid("6cbe111e-eb94-94be-06f5-64dda9567599"), new DateTimeOffset(new DateTime(2024, 1, 12, 4, 9, 1, 216, DateTimeKind.Unspecified).AddTicks(5311), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 23, 37, 37, 897, DateTimeKind.Unspecified).AddTicks(399), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), new Guid("51b355f6-95d6-d189-d290-b43e07fdb192"), "PPY35DS6SB", new DateTimeOffset(new DateTime(2023, 12, 14, 4, 9, 5, 75, DateTimeKind.Unspecified).AddTicks(9751), new TimeSpan(0, 0, 0, 0, 0)), new Guid("5e9a0d85-3458-5d74-1d4a-6726411a7a5a"), new DateTimeOffset(new DateTime(2023, 10, 16, 10, 55, 9, 954, DateTimeKind.Unspecified).AddTicks(494), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 24, 8, 11, 2, 859, DateTimeKind.Unspecified).AddTicks(9202), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("86a95ec1-5bda-bd57-3f8e-e610398f9334"), new Guid("51b355f6-95d6-d189-d290-b43e07fdb192"), "2GCDD0MUUR", new DateTimeOffset(new DateTime(2023, 8, 15, 9, 48, 34, 425, DateTimeKind.Unspecified).AddTicks(2869), new TimeSpan(0, 0, 0, 0, 0)), new Guid("634670ad-22df-f993-2710-bd1af25f3a06"), new DateTimeOffset(new DateTime(2024, 4, 13, 1, 47, 37, 181, DateTimeKind.Unspecified).AddTicks(1127), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 14, 9, 28, 1, 147, DateTimeKind.Unspecified).AddTicks(4052), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("957bee01-ca4b-6f48-cecd-1ce1a560332f"), new Guid("51b355f6-95d6-d189-d290-b43e07fdb192"), "G759XW5NNM", new DateTimeOffset(new DateTime(2023, 8, 11, 19, 44, 52, 32, DateTimeKind.Unspecified).AddTicks(7326), new TimeSpan(0, 0, 0, 0, 0)), new Guid("cac7873f-f608-0434-c6ce-ec6dfd32d231"), new DateTimeOffset(new DateTime(2024, 5, 9, 16, 11, 37, 900, DateTimeKind.Unspecified).AddTicks(9560), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 2, 8, 4, 7, 994, DateTimeKind.Unspecified).AddTicks(2161), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("9abe7e9c-fd77-89e5-9fec-f460cd25403b"), new Guid("e0967ad4-70ad-6346-df22-93f92710bd1a"), "416GT6NM4B", new DateTimeOffset(new DateTime(2023, 12, 25, 19, 37, 13, 904, DateTimeKind.Unspecified).AddTicks(9417), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9ca00e7a-c1e3-6c17-e48e-824e0221be0f"), new DateTimeOffset(new DateTime(2024, 2, 25, 1, 19, 10, 730, DateTimeKind.Unspecified).AddTicks(7420), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 28, 5, 35, 13, 347, DateTimeKind.Unspecified).AddTicks(6026), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("b16773f7-ded8-a522-43ce-b51d18f9205e"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "S6805AY53H", new DateTimeOffset(new DateTime(2023, 11, 3, 9, 56, 32, 620, DateTimeKind.Unspecified).AddTicks(3197), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c5de3940-0944-c4f8-fa93-da03b195bc4b"), new DateTimeOffset(new DateTime(2024, 1, 23, 22, 35, 6, 909, DateTimeKind.Unspecified).AddTicks(2565), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 3, 9, 21, 57, 582, DateTimeKind.Unspecified).AddTicks(465), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("b1fd073e-cd92-947d-4b6b-d9bd0380b1f2"), new Guid("8286d046-9740-a3e4-95cf-ff46699c73c4"), "ZBZNUPXJO5", new DateTimeOffset(new DateTime(2024, 5, 17, 16, 40, 40, 168, DateTimeKind.Unspecified).AddTicks(3121), new TimeSpan(0, 0, 0, 0, 0)), new Guid("807585f9-4ce4-8746-d3b0-fee00f064359"), new DateTimeOffset(new DateTime(2024, 6, 7, 11, 19, 51, 870, DateTimeKind.Unspecified).AddTicks(6550), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 12, 23, 17, 16, 445, DateTimeKind.Unspecified).AddTicks(2055), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("b312c394-2bbf-4936-ecfc-b761f2db22d6"), new Guid("51b355f6-95d6-d189-d290-b43e07fdb192"), "MPSPX3I0NR", new DateTimeOffset(new DateTime(2023, 12, 10, 1, 11, 54, 252, DateTimeKind.Unspecified).AddTicks(9242), new TimeSpan(0, 0, 0, 0, 0)), new Guid("c5de3940-0944-c4f8-fa93-da03b195bc4b"), new DateTimeOffset(new DateTime(2023, 10, 26, 18, 31, 17, 805, DateTimeKind.Unspecified).AddTicks(9393), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 28, 14, 24, 2, 536, DateTimeKind.Unspecified).AddTicks(2890), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("c5289a22-5e4b-fd4d-ad51-64c4a05b7d64"), new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), "NH7OZ64JIE", new DateTimeOffset(new DateTime(2024, 1, 10, 4, 9, 40, 302, DateTimeKind.Unspecified).AddTicks(2174), new TimeSpan(0, 0, 0, 0, 0)), new Guid("634670ad-22df-f993-2710-bd1af25f3a06"), new DateTimeOffset(new DateTime(2023, 9, 17, 12, 4, 26, 282, DateTimeKind.Unspecified).AddTicks(2213), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 31, 19, 36, 0, 213, DateTimeKind.Unspecified).AddTicks(2070), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("d1bf6b7f-7fae-6c5f-3c28-d47a96e0ad70"), new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), "2CDW1QYVAV", new DateTimeOffset(new DateTime(2024, 1, 26, 8, 20, 59, 338, DateTimeKind.Unspecified).AddTicks(9221), new TimeSpan(0, 0, 0, 0, 0)), new Guid("5e4bc528-fd4d-51ad-64c4-a05b7d64a131"), new DateTimeOffset(new DateTime(2023, 12, 22, 23, 32, 36, 480, DateTimeKind.Unspecified).AddTicks(2478), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 1, 13, 12, 11, 963, DateTimeKind.Unspecified).AddTicks(2083), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { new Guid("e2588548-0dcf-02ef-2a15-5e8d7e518a27"), new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), "5ISVBBJSRF", new DateTimeOffset(new DateTime(2023, 11, 7, 3, 55, 52, 947, DateTimeKind.Unspecified).AddTicks(587), new TimeSpan(0, 0, 0, 0, 0)), new Guid("46f23aa3-74ec-3fb7-32a8-9b9879ce7d7f"), new DateTimeOffset(new DateTime(2023, 12, 21, 1, 55, 36, 520, DateTimeKind.Unspecified).AddTicks(9391), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 4, 17, 59, 46, 293, DateTimeKind.Unspecified).AddTicks(840), new TimeSpan(0, 0, 0, 0, 0)), 0 },
                    { new Guid("e8980312-099b-5ce0-86f1-8c77537da207"), new Guid("fd779abe-89e5-ec9f-f460-cd25403bb64f"), "O9K9PLX9TU", new DateTimeOffset(new DateTime(2024, 2, 27, 2, 12, 5, 50, DateTimeKind.Unspecified).AddTicks(1985), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9af1bc12-1ac4-5ec1-a986-da5b57bd3f8e"), new DateTimeOffset(new DateTime(2024, 2, 19, 16, 43, 40, 670, DateTimeKind.Unspecified).AddTicks(5719), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 25, 9, 19, 26, 267, DateTimeKind.Unspecified).AddTicks(4377), new TimeSpan(0, 0, 0, 0, 0)), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                schema: "wowtogo",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_UserId",
                schema: "wowtogo",
                table: "Attendees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_CategoryId",
                schema: "wowtogo",
                table: "EventCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_EventId",
                schema: "wowtogo",
                table: "EventCategories",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                schema: "wowtogo",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TicketTypeId",
                schema: "wowtogo",
                table: "Orders",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "wowtogo",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_EventId",
                schema: "wowtogo",
                table: "Shows",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_EventId",
                schema: "wowtogo",
                table: "Staffs",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UserId",
                schema: "wowtogo",
                table: "Staffs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AttendeeId",
                schema: "wowtogo",
                table: "Tickets",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                schema: "wowtogo",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypeShows_ShowId",
                schema: "wowtogo",
                table: "TicketTypeShows",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypeShows_TicketTypeId",
                schema: "wowtogo",
                table: "TicketTypeShows",
                column: "TicketTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "EventCategories",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Staffs",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "TicketTypeShows",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Attendees",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Shows",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "TicketTypes",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Organizers",
                schema: "wowtogo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "wowtogo");
        }
    }
}
