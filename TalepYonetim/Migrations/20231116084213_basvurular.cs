using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalepYonetim.Migrations
{
    /// <inheritdoc />
    public partial class basvurular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basvurular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiraNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCIdentity = table.Column<int>(type: "int", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasvuruDurum = table.Column<int>(type: "int", nullable: false),
                    LiseMezuniyet = table.Column<int>(type: "int", nullable: false),
                    UniversiteMezuniyet = table.Column<int>(type: "int", nullable: false),
                    YabanciDil = table.Column<int>(type: "int", nullable: false),
                    OnKontrolDurumu = table.Column<int>(type: "int", nullable: false),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false),
                    OnKontrolIptalAciklamasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IptalAciklamasi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basvurular", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basvurular");
        }
    }
}
