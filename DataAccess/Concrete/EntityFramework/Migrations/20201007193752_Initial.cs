using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Concrete.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.CreateTable(
                name: "Proposal",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalNo = table.Column<decimal>(nullable: false),
                    EndorsNo = table.Column<int>(nullable: false),
                    RenewalNo = table.Column<int>(nullable: false),
                    ProductNo = table.Column<string>(maxLength: 20, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SerializedResponse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposal",
                schema: "product");
        }
    }
}
