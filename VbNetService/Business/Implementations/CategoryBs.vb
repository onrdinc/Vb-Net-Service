Imports AutoMapper
Imports AutoMapper.Configuration
Imports DataAccess
Imports Infrastructure
Imports Microsoft.AspNetCore.Http
Imports Models

Public Class CategoryBs
    Implements ICategoryBs
    Private ReadOnly _repo As ICategoryRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(ByVal repo As ICategoryRepository, ByVal mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function GetData(id As Integer, currentUserId As Long, ParamArray includeList() As String) As Task(Of ApiResponse(Of CategoryDto.GetDto)) Implements IBaseBs(Of CategoryDto.GetDto, CategoryDto.PostDto, CategoryDto.PutDto, CategoryDto.FilterDto, Integer).GetData
        Try
            Dim category = Await _repo.GetByIdAsync(id)


            Dim response = _mapper.Map(Of CategoryDto.GetDto)(category)
            Return ApiResponse(Of CategoryDto.GetDto).Success(StatusCodes.Status200OK, response)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Async Function GetAll(dto As CategoryDto.FilterDto, currentUserId As Long, ParamArray includeList() As String) As Task(Of ApiResponse(Of List(Of CategoryDto.GetDto))) Implements IBaseBs(Of CategoryDto.GetDto, CategoryDto.PostDto, CategoryDto.PutDto, CategoryDto.FilterDto, Integer).GetAll
        Try
            Dim categories = Await _repo.GetAllAsync()
            Dim returnList = _mapper.Map(Of List(Of CategoryDto.GetDto))(categories)
            Return ApiResponse(Of List(Of CategoryDto.GetDto)).Success(StatusCodes.Status200OK, returnList)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Async Function Add(dto As CategoryDto.PostDto, currentUserId As Long) As Task(Of ApiResponse(Of CategoryDto.GetDto)) Implements IBaseBs(Of CategoryDto.GetDto, CategoryDto.PostDto, CategoryDto.PutDto, CategoryDto.FilterDto, Integer).Add
        Try
            Dim category = _mapper.Map(Of Category)(dto)
            category.CreatedBy = currentUserId
            Dim insert = Await _repo.AddAsync(category)
            Dim c = _mapper.Map(Of CategoryDto.GetDto)(insert)
            Return ApiResponse(Of CategoryDto.GetDto).Success(StatusCodes.Status201Created, c)

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Async Function Update(dto As CategoryDto.PutDto, currentUserId As Long) As Task(Of ApiResponse(Of NoData)) Implements IBaseBs(Of CategoryDto.GetDto, CategoryDto.PostDto, CategoryDto.PutDto, CategoryDto.FilterDto, Integer).Update
        Try
            Dim c = Await _repo.GetByIdAsync(dto.Id)

            Dim category = _mapper.Map(Of Category)(dto)
            category.CreatedDate = c.CreatedDate
            category.CreatedBy = c.CreatedBy
            category.UpdatedBy = currentUserId
            Await _repo.UpdateAsync(category)
            Return ApiResponse(Of NoData).Success(StatusCodes.Status200OK)

        Catch ex As Exception

        End Try
    End Function

    Public Async Function Delete(id As Integer, currentUserId As Long) As Task(Of ApiResponse(Of NoData)) Implements IBaseBs(Of CategoryDto.GetDto, CategoryDto.PostDto, CategoryDto.PutDto, CategoryDto.FilterDto, Integer).Delete
        Try
            Dim category = Await _repo.GetByIdAsync(id)

            category.DeletedBy = currentUserId
            Await _repo.DeleteAsync(category)
            Return ApiResponse(Of NoData).Success(StatusCodes.Status200OK)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
