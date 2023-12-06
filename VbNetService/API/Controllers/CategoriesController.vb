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


        <HttpGet>
        <Route("api/Categories/{id}")>
        Public Async Function GetCategoryById(ByVal id As Integer) As Task(Of ApiResponse(Of CategoryDto.GetDto))
            Dim response = Await _categoryBs.GetData(id, 0)
            Return response
        End Function

        <HttpPost>
        <Route("api/Categories")>
        Public Async Function AddUser(<FromBody> dto As CategoryDto.PostDto) As Task(Of ApiResponse(Of CategoryDto.GetDto))
            Dim response = Await _categoryBs.Add(dto, 0)
            Return response
        End Function

        <HttpDelete>
        <Route("api/Categories/{id}")>
        Public Async Function DeleteUser(ByVal id As Long) As Task(Of ApiResponse(Of NoData))
            Dim response = Await _categoryBs.Delete(id, 0)
            Return response
        End Function

        <HttpPut>
        <Route("api/Categories")>
        Public Async Function UpdateUser(<FromBody> dto As CategoryDto.PutDto) As Task(Of ApiResponse(Of NoData))
            Dim response = Await _categoryBs.Update(dto, 0)
            Return response
        End Function

    End Class
End Namespace