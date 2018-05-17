namespace MyNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4a8ef11a-07d0-436b-bb29-9598890e2de6', N'admin@admin.com', 0, N'AOm6UbnUHWKCU3LJ0tVarycSFdqxsyWMKGYiVEz66DP5/VPC39/jTHcshCGZVkiZ/w==', N'297f3ac0-dab4-4b26-b96f-ef67243e3677', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7e3fbd27-db7e-4b93-9c05-aca9dec9fe59', N'Admin')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4a8ef11a-07d0-436b-bb29-9598890e2de6', N'7e3fbd27-db7e-4b93-9c05-aca9dec9fe59')
");
        }

        public override void Down()
        {
        }
    }
}
