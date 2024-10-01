using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metas.shared.Dados.Migrations
{
    /// <inheritdoc />
    public partial class SeedsBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserindo algumas metas iniciais
            migrationBuilder.InsertData(
                table: "Metas",
                columns: new[] { "Id", "Nome", "Status", "Tempo", "Continuo", "Descricao" },
                values: new object[,]
                {
                    { 1, "Aprender Go", false, 100, false, "Aprender a programar em Go" },
                    { 2, "Correr uma maratona", false, 200, true, "Treinar para correr 42km" },
                    { 3, "Estudar Matemática Avançada", false, 300, false, "Aprofundar os estudos em álgebra e cálculo" }
                }
            );

            // Inserindo passos relacionados às metas
            migrationBuilder.InsertData(
                table: "Passos",
                columns: new[] { "Id", "Nome", "MetaID", "Continuo", "Tempo", "Status", "Descricao" },
                values: new object[,]
                {
                    { 1, "Estudar a sintaxe básica", 1, false, 10, false, "Começar pelos conceitos fundamentais de Go" },
                    { 2, "Treinar diariamente", 2, true, 50, false, "Manter uma rotina de treinos" },
                    { 3, "Resolver exercícios", 1, false, 20, false, "Focar em problemas práticos de programação em Go" },
                    { 4, "Correr 10km", 2, false, 20, false, "Alcançar a meta de 10km como parte do treinamento" },
                    { 5, "Estudar álgebra linear", 3, false, 60, false, "Estudar os fundamentos de matrizes e vetores" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Removendo os passos
            migrationBuilder.DeleteData(table: "Passos", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "Passos", keyColumn: "Id", keyValue: 2);
            migrationBuilder.DeleteData(table: "Passos", keyColumn: "Id", keyValue: 3);
            migrationBuilder.DeleteData(table: "Passos", keyColumn: "Id", keyValue: 4);
            migrationBuilder.DeleteData(table: "Passos", keyColumn: "Id", keyValue: 5);

            // Removendo as metas
            migrationBuilder.DeleteData(table: "Metas", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "Metas", keyColumn: "Id", keyValue: 2);
            migrationBuilder.DeleteData(table: "Metas", keyColumn: "Id", keyValue: 3);
        }
    }
}
