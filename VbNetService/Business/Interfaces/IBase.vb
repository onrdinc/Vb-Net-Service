Imports Infrastructure

Public Interface IBaseBs(Of GetDto, PostDto, PutDto, FilterDto, TId)
    Function GetData(ByVal id As TId, ByVal currentUserId As Long, ByVal ParamArray includeList As String()) As Task(Of ApiResponse(Of GetDto))
    Function GetAll(ByVal dto As FilterDto, ByVal currentUserId As Long, ByVal ParamArray includeList As String()) As Task(Of ApiResponse(Of List(Of GetDto)))
    Function Add(ByVal dto As PostDto, ByVal currentUserId As Long) As Task(Of ApiResponse(Of GetDto))
    Function Update(ByVal dto As PutDto, ByVal currentUserId As Long) As Task(Of ApiResponse(Of NoData))
    Function Delete(ByVal id As TId, ByVal currentUserId As Long) As Task(Of ApiResponse(Of NoData))
End Interface
