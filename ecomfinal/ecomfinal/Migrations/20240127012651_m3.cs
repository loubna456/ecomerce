using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomfinal.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProduitId",
                table: "Panier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Panier_ProduitId",
                table: "Panier",
                column: "ProduitId");

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

            migrationBuilder.DropIndex(
                name: "IX_Panier_ProduitId",
                table: "Panier");

            migrationBuilder.DropColumn(
                name: "ProduitId",
                table: "Panier");
        }
    }
}
