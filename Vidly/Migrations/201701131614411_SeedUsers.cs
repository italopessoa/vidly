namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b22a1acc-22da-4a90-91fa-972fa35bf506', N'admin@vidly.com', 0, N'AIrUM8Gj08e7v2Yp9Dq+UMx8VErlPuo7unKW7uSEyAvpWQVuRbF+GWBfe6IQ91P/DA==', N'e019887e-34af-457c-b9df-648cd9b4d97c', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ddaeb325-9564-44c8-bb63-aa9f1f7b25e5', N'guest@vidly.com', 0, N'AB0eeq8xDK8SI8gpNQpUCtBGx4g55sbMAvQohYlPjuohlXYDyCSS9rk1vbv32d8tvA==', N'58b6285b-9b3a-4910-8c1d-1fb333c0c78f', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'23330570-324b-46fd-87fd-b824e73aab0d', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b22a1acc-22da-4a90-91fa-972fa35bf506', N'23330570-324b-46fd-87fd-b824e73aab0d')

");
        }
        
        public override void Down()
        {
        }
    }
}
