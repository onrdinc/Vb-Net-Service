Imports Infrastructure
Imports Models
Public Interface ICategoryRepository
    Inherits IBaseRepository(Of Category, Integer)


    Function GetByIdAsync(ByVal id As Integer, Optional ByVal includeList As List(Of String) = Nothing) As Task(Of Category)
End Interface
