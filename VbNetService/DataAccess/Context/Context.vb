Imports System.Configuration
Imports System.Data.Entity
Imports Models

Public Class Context
    Inherits DbContext

    Public Sub New()
        MyBase.New(ConfigurationManager.ConnectionStrings("Context").ConnectionString)
        Database.SetInitializer(Of Context)(Nothing)
    End Sub

    Public Property Categories As DbSet(Of Category)
End Class
