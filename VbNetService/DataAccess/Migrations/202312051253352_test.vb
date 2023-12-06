Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class test
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Categories",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Name = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Categories")
        End Sub
    End Class
End Namespace
