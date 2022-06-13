using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevInSales.Migrations
{
    public partial class incluidopermissoes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Profile_profileid",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_User_userid",
                table: "UserClaim");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Profile_profileid",
                table: "UserClaim",
                column: "profileid",
                principalTable: "Profile",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_User_userid",
                table: "UserClaim",
                column: "userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Profile_profileid",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_User_userid",
                table: "UserClaim");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Profile_profileid",
                table: "UserClaim",
                column: "profileid",
                principalTable: "Profile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_User_userid",
                table: "UserClaim",
                column: "userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
