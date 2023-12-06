Imports Infrastructure
Imports Models

Public Class CategoryRepository
    Inherits BaseRepository(Of Category, Integer, Context)
    Implements ICategoryRepository

    Public Async Function GetByIdAsync(id As Integer, Optional includeList As List(Of String) = Nothing) As Task(Of Category) Implements ICategoryRepository.GetByIdAsync
        Return Await GetAsync(Function(prd) prd.Id = id, includeList)

    End Function


End Class
