Imports AutoMapper
Imports Models

Public Class CategoryProfile
    Inherits Profile

    Public Sub New()
        CreateMap(Of Category, CategoryDto.GetDto)()
        CreateMap(Of CategoryDto.PostDto, Category)()
        CreateMap(Of CategoryDto.PutDto, Category)()
    End Sub
End Class

