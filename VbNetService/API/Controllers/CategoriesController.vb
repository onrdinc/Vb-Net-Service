Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Http
Imports Business
Imports Infrastructure
Imports Models

Namespace Controllers
    Public Class CategoriesController
        Inherits ApiController

        Private ReadOnly _categoryBs As ICategoryBs

        Public Sub New(categoryBs As ICategoryBs)
            _categoryBs = categoryBs
        End Sub

        <HttpPost>
        <Route("api/Categories/GetAll")>
        Public Async Function GetAll(<FromBody> ByVal dto As CategoryDto.FilterDto) As Task(Of ApiResponse(Of List(Of CategoryDto.GetDto)))
            Dim response = Await _categoryBs.GetAll(dto, 0)
            Return response
        End Function




    End Class
End Namespace