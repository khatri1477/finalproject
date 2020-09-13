namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationForms",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false, identity: true),
                        applicantName = c.String(nullable: false),
                        FatherName = c.String(nullable: false),
                        coApplicantName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        aadhar = c.String(nullable: false),
                        panNumber = c.String(nullable: false),
                        mobileNo = c.String(nullable: false),
                        EmployeementTypeId = c.Int(nullable: false),
                        requestedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        homeLoan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        autoMobile = c.Decimal(nullable: false, precision: 18, scale: 2),
                        bussinessLoan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        applyDate = c.DateTime(nullable: false),
                        status = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationId)
                .ForeignKey("dbo.EmployeementTypes", t => t.EmployeementTypeId, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.userId, cascadeDelete: true)
                .Index(t => t.EmployeementTypeId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.EmployeementTypes",
                c => new
                    {
                        EmployeementTypeId = c.Int(nullable: false, identity: true),
                        employeeType = c.String(),
                    })
                .PrimaryKey(t => t.EmployeementTypeId);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.loanRequestStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.Int(nullable: false),
                        eligibleAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        modificationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ApplicationForms", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.loanRequestStatus", "ApplicationId", "dbo.ApplicationForms");
            DropForeignKey("dbo.ApplicationForms", "userId", "dbo.users");
            DropForeignKey("dbo.ApplicationForms", "EmployeementTypeId", "dbo.EmployeementTypes");
            DropIndex("dbo.loanRequestStatus", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationForms", new[] { "userId" });
            DropIndex("dbo.ApplicationForms", new[] { "EmployeementTypeId" });
            DropTable("dbo.loanRequestStatus");
            DropTable("dbo.users");
            DropTable("dbo.EmployeementTypes");
            DropTable("dbo.ApplicationForms");
        }
    }
}
