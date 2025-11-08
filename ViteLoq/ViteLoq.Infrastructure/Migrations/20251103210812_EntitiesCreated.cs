using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViteLoq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScientificName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLiquid = table.Column<bool>(type: "bit", nullable: false),
                    IsComposite = table.Column<bool>(type: "bit", nullable: false),
                    IsDemo = table.Column<bool>(type: "bit", nullable: false),
                    DefaultWeightInGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JsonMetadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IngredientsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergens = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPerishable = table.Column<bool>(type: "bit", nullable: false),
                    TypicalShelfLife = table.Column<TimeSpan>(type: "time", nullable: true),
                    EnergyKcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyKj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sugar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Starch = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fiber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaturatedFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonounsaturatedFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolyunsaturatedFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CholesterolMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlcoholGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaffeineMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaltGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SodiumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotassiumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminA_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminD_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminE_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminK_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminC_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Thiamin_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Riboflavin_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Niacin_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminB6_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Folate_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VitaminB12_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PantothenicAcid_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Biotin_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalciumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IronMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MagnesiumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhosphorusMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ZincMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeleniumUg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CopperMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ManganeseMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FluorideMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChlorideMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GlycemicIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GlycemicLoad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCarbs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fructose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lactose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Omega3Grams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Omega6Grams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatineMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LysineMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MethionineMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TryptophanMg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlavorProfile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypicalPricePer100Grams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThermicEffectPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BmrImpactKcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BmrMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatietyScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsulinResponseIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HormonalImpactNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeightCm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGymTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGymTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNutritionTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetCalories = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetProteinGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetCarbsGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetFatsGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetFiberGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetSugarGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetSodiumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetCholesterolMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetAlcoholGrams = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetWaterLiters = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WaterReminderMl = table.Column<int>(type: "int", nullable: true),
                    TargetVitaminA_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminD_IU = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminC_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminE_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminK_Ug = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB1_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB2_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB3_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB5_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB6_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB7_Mcg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetFolate_Mcg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetVitaminB12_Mcg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetCalciumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetIronMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetMagnesiumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetPhosphorusMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetPotassiumMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetZincMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetSeleniumMcg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetIodineMcg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetCopperMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetManganeseMg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetChromiumMcg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetOmega3_Mg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetSteps = table.Column<int>(type: "int", nullable: true),
                    TargetActiveMinutes = table.Column<int>(type: "int", nullable: true),
                    TargetExerciseSessionsPerWeek = table.Column<int>(type: "int", nullable: true),
                    TargetStressLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetSleepQuality = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetMoodScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetSleepHours = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetWeightDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BMR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TDEE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GoalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalorieAdjustmentPerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeeklyWeightChangeKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UseAutoCalculatedTargets = table.Column<bool>(type: "bit", nullable: true),
                    ReceiveReminders = table.Column<bool>(type: "bit", nullable: true),
                    AdditionalTargetsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNutritionTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTotalHealth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heart = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kidney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Liver = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lungs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eyes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brain = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pancreas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stomach = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTotalHealth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTotalMental",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stress = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sleep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTotalMental", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTotalMuscle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Back = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Biceps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Triceps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quadriceps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hamstrings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Calves = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shoulders = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Abs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Glutes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTotalMuscle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTotalSkin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkinPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HairPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NailsPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTotalSkin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTotalHealthId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTotalMentalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTotalMuscleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTotalSkinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTotalHealth_UserTotalHealthId",
                        column: x => x.UserTotalHealthId,
                        principalTable: "UserTotalHealth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTotalMental_UserTotalMentalId",
                        column: x => x.UserTotalMentalId,
                        principalTable: "UserTotalMental",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTotalMuscle_UserTotalMuscleId",
                        column: x => x.UserTotalMuscleId,
                        principalTable: "UserTotalMuscle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTotalSkin_UserTotalSkinId",
                        column: x => x.UserTotalSkinId,
                        principalTable: "UserTotalSkin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "UserNutritionEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NutritionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grams = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNutritionEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNutritionEntries_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserWorkoutEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkoutEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkoutEntries_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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
                name: "IX_AspNetUsers_UserTotalHealthId",
                table: "AspNetUsers",
                column: "UserTotalHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalMentalId",
                table: "AspNetUsers",
                column: "UserTotalMentalId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalMuscleId",
                table: "AspNetUsers",
                column: "UserTotalMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalSkinId",
                table: "AspNetUsers",
                column: "UserTotalSkinId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserNutritionEntries_AppUserId",
                table: "UserNutritionEntries",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutEntries_AppUserId",
                table: "UserWorkoutEntries",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "NutritionItems");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "UserGymTargets");

            migrationBuilder.DropTable(
                name: "UserNutritionEntries");

            migrationBuilder.DropTable(
                name: "UserNutritionTargets");

            migrationBuilder.DropTable(
                name: "UserWorkoutEntries");

            migrationBuilder.DropTable(
                name: "WorkoutTemplates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserTotalHealth");

            migrationBuilder.DropTable(
                name: "UserTotalMental");

            migrationBuilder.DropTable(
                name: "UserTotalMuscle");

            migrationBuilder.DropTable(
                name: "UserTotalSkin");
        }
    }
}
