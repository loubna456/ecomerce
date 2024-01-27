using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomfinal.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    ProduitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PanierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit", x => x.id);
                    table.ForeignKey(
                        name: "FK_Produit_Panier_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Panier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Panier_ProduitId",
                table: "Panier",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_PanierId",
                table: "Produit",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Panier_Produit_ProduitId",
                table: "Panier",
                column: "ProduitId",
                principalTable: "Produit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Panier_Produit_ProduitId",
                table: "Panier");

            migrationBuilder.DropTable(
                name: "Produit");

            migrationBuilder.DropTable(
                name: "Panier");
        }
    }
}
