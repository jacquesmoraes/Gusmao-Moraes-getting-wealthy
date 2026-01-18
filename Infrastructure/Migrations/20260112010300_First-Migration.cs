using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    BoxId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoxName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BoxGoal = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    BudgetTarget = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.BoxId);
                    table.CheckConstraint("CK_Box_BudgetTarget_Positive", "\"BudgetTarget\" > 0");
                    table.CheckConstraint("CK_Box_CurrentBalance_NonNegative", "\"CurrentBalance\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    VisualType = table.Column<string>(type: "text", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    RealExpense = table.Column<decimal>(type: "numeric", nullable: false),
                    BoxContribute = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentExpenses",
                columns: table => new
                {
                    InstallmentExpenseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumberOfInstallments = table.Column<int>(type: "integer", nullable: false),
                    InstallmentAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FirstInstallmentDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentExpenses", x => x.InstallmentExpenseId);
                    table.CheckConstraint("CK_InstallmentExpense_InstallmentAmount_Positive", "\"InstallmentAmount\" > 0");
                    table.CheckConstraint("CK_InstallmentExpense_NumberOfInstallments_Positive", "\"NumberOfInstallments\" > 0");
                });

            migrationBuilder.CreateTable(
                name: "RecurringExpenses",
                columns: table => new
                {
                    RecurringExpenseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecurringFrequency = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    NextDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringExpenses", x => x.RecurringExpenseId);
                    table.CheckConstraint("CK_RecurringExpense_Day_Range", "\"Day\" >= 1 AND \"Day\" <= 31");
                });

            migrationBuilder.CreateTable(
                name: "RecurringIncomes",
                columns: table => new
                {
                    RecurringIncomeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecurringFrequency = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    NextDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringIncomes", x => x.RecurringIncomeId);
                    table.CheckConstraint("CK_RecurringIncome_Day_Range", "\"Day\" >= 1 AND \"Day\" <= 31");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportType = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalExpense = table.Column<decimal>(type: "numeric", nullable: false),
                    NetBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    EconomyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    GenerationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "BoxContributes",
                columns: table => new
                {
                    BoxContributeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ReferenceMonth = table.Column<string>(type: "text", nullable: false),
                    Observation = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BoxId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxContributes", x => x.BoxContributeId);
                    table.ForeignKey(
                        name: "FK_BoxContributes_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "BoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyBudgets",
                columns: table => new
                {
                    WeeklyBudgetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Week = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalSpent = table.Column<decimal>(type: "numeric", nullable: false),
                    WeekBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BudgetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyBudgets", x => x.WeeklyBudgetId);
                    table.ForeignKey(
                        name: "FK_WeeklyBudgets_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId");
                });

            migrationBuilder.CreateTable(
                name: "BudgetCategories",
                columns: table => new
                {
                    BudgetCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BudgetedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SpentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    AvaliableBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentUsage = table.Column<decimal>(type: "numeric", nullable: false),
                    BudgetStatus = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BudgetId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCategories", x => x.BudgetCategoryId);
                    table.ForeignKey(
                        name: "FK_BudgetCategories_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId");
                    table.ForeignKey(
                        name: "FK_BudgetCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentExpenseItems",
                columns: table => new
                {
                    InstallmentExpenseItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstallmentNumber = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    MonthReference = table.Column<string>(type: "text", nullable: false),
                    InstallmentExpenseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentExpenseItems", x => x.InstallmentExpenseItemId);
                    table.ForeignKey(
                        name: "FK_InstallmentExpenseItems_InstallmentExpenses_InstallmentExpe~",
                        column: x => x.InstallmentExpenseId,
                        principalTable: "InstallmentExpenses",
                        principalColumn: "InstallmentExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpensePaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    Observations = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    InstallmentExpenseId = table.Column<long>(type: "bigint", nullable: true),
                    RecurringExpenseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.CheckConstraint("CK_Expense_Amount_NonNegative", "\"Amount\" > 0");
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_InstallmentExpenses_InstallmentExpenseId",
                        column: x => x.InstallmentExpenseId,
                        principalTable: "InstallmentExpenses",
                        principalColumn: "InstallmentExpenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_RecurringExpenses_RecurringExpenseId",
                        column: x => x.RecurringExpenseId,
                        principalTable: "RecurringExpenses",
                        principalColumn: "RecurringExpenseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    IncomeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IncomeMethod = table.Column<int>(type: "integer", nullable: false),
                    AlreadyReceived = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Notes = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RecurringIncomeId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                    table.CheckConstraint("CK_Income_Amount_Positive", "\"Amount\" > 0");
                    table.ForeignKey(
                        name: "FK_Incomes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incomes_RecurringIncomes_RecurringIncomeId",
                        column: x => x.RecurringIncomeId,
                        principalTable: "RecurringIncomes",
                        principalColumn: "RecurringIncomeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportItems",
                columns: table => new
                {
                    ReportCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Percent = table.Column<decimal>(type: "numeric", nullable: false),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportItems", x => x.ReportCategoryId);
                    table.ForeignKey(
                        name: "FK_ReportItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportItems_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxWithdraws",
                columns: table => new
                {
                    BoxWithdrawId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BoxId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxWithdraws", x => x.BoxWithdrawId);
                    table.ForeignKey(
                        name: "FK_BoxWithdraws_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "BoxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxWithdraws_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxContributes_BoxId",
                table: "BoxContributes",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_CreationDate",
                table: "Boxes",
                column: "CreationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_EndDate",
                table: "Boxes",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_Status",
                table: "Boxes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_Status_EndDate",
                table: "Boxes",
                columns: new[] { "Status", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_BoxWithdraws_BoxId",
                table: "BoxWithdraws",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxWithdraws_Date",
                table: "BoxWithdraws",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_BoxWithdraws_ExpenseId",
                table: "BoxWithdraws",
                column: "ExpenseId",
                unique: true,
                filter: "\"ExpenseId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategories_BudgetId",
                table: "BudgetCategories",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategories_CategoryId",
                table: "BudgetCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActive",
                table: "Categories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsFavorite",
                table: "Categories",
                column: "IsFavorite");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Type",
                table: "Categories",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Date",
                table: "Expenses",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Date_CategoryId",
                table: "Expenses",
                columns: new[] { "Date", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpensePaymentMethod",
                table: "Expenses",
                column: "ExpensePaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_InstallmentExpenseId",
                table: "Expenses",
                column: "InstallmentExpenseId",
                unique: true,
                filter: "\"InstallmentExpenseId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_RecurringExpenseId",
                table: "Expenses",
                column: "RecurringExpenseId",
                unique: true,
                filter: "\"RecurringExpenseId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_AlreadyReceived",
                table: "Incomes",
                column: "AlreadyReceived");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_CategoryId",
                table: "Incomes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_Date",
                table: "Incomes",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_Date_CategoryId",
                table: "Incomes",
                columns: new[] { "Date", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeMethod",
                table: "Incomes",
                column: "IncomeMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_RecurringIncomeId",
                table: "Incomes",
                column: "RecurringIncomeId",
                unique: true,
                filter: "\"RecurringIncomeId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentExpenseItems_InstallmentExpenseId",
                table: "InstallmentExpenseItems",
                column: "InstallmentExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentExpenses_FirstInstallmentDate",
                table: "InstallmentExpenses",
                column: "FirstInstallmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_IsActive",
                table: "RecurringExpenses",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_IsActive_NextDate",
                table: "RecurringExpenses",
                columns: new[] { "IsActive", "NextDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_NextDate",
                table: "RecurringExpenses",
                column: "NextDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_RecurringFrequency",
                table: "RecurringExpenses",
                column: "RecurringFrequency");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringIncomes_IsActive",
                table: "RecurringIncomes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringIncomes_IsActive_NextDate",
                table: "RecurringIncomes",
                columns: new[] { "IsActive", "NextDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringIncomes_NextDate",
                table: "RecurringIncomes",
                column: "NextDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringIncomes_RecurringFrequency",
                table: "RecurringIncomes",
                column: "RecurringFrequency");

            migrationBuilder.CreateIndex(
                name: "IX_ReportItems_CategoryId",
                table: "ReportItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportItems_ReportId",
                table: "ReportItems",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyBudgets_BudgetId",
                table: "WeeklyBudgets",
                column: "BudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxContributes");

            migrationBuilder.DropTable(
                name: "BoxWithdraws");

            migrationBuilder.DropTable(
                name: "BudgetCategories");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "InstallmentExpenseItems");

            migrationBuilder.DropTable(
                name: "ReportItems");

            migrationBuilder.DropTable(
                name: "WeeklyBudgets");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "RecurringIncomes");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "InstallmentExpenses");

            migrationBuilder.DropTable(
                name: "RecurringExpenses");
        }
    }
}
