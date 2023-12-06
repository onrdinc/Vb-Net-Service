Imports System.Linq.Expressions
Public Interface IBaseRepository(Of TEntity As {BaseEntity(Of TId)}, TId)
    Function GetAllAsync(Optional ByVal predicate As Expression(Of Func(Of TEntity, Boolean)) = Nothing, Optional ByVal includeList() As String = Nothing) As Task(Of List(Of TEntity))
    Function GetAsync(ByVal predicate As Expression(Of Func(Of TEntity, Boolean)), Optional ByVal includeList() As String = Nothing) As Task(Of TEntity)
    Function AddAsync(ByVal entity As TEntity) As Task(Of TEntity)
    Function UpdateAsync(ByVal entity As TEntity) As Task
    Function DeleteAsync(ByVal entity As TEntity) As Task
    Function AddRangeAsync(ByVal entities As ICollection(Of TEntity)) As Task(Of IEnumerable(Of TEntity))
    Function AnyAsync(ByVal predicate As Expression(Of Func(Of TEntity, Boolean))) As Task(Of Boolean)
    Function GetByIdAsync(ByVal id As TId, Optional ByVal includeList As List(Of String) = Nothing) As Task(Of TEntity)
End Interface

