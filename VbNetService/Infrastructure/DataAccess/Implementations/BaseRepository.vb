Imports System.Data.Entity
Imports System.Linq.Expressions


Public Class BaseRepository(Of TEntity As {BaseEntity(Of TId)}, TId, TContext As {DbContext, New})
    Implements IBaseRepository(Of TEntity, TId)

    Public Async Function DeleteAsync(ByVal entity As TEntity) As Task Implements IBaseRepository(Of TEntity, TId).DeleteAsync
        Using ctx As New TContext()
            ctx.Entry(entity).State = EntityState.Modified
            'ctx.Set(Of TEntity)().Remove(entity)
            entity.IsDeleted = True
            entity.DeletedDate = DateTime.Now
            Await ctx.SaveChangesAsync()
        End Using
    End Function


    Public Async Function GetAllAsync(Optional ByVal predicate As Expression(Of Func(Of TEntity, Boolean)) = Nothing, Optional ByVal includeList() As String = Nothing) As Task(Of List(Of TEntity)) Implements IBaseRepository(Of TEntity, TId).GetAllAsync
        Using ctx As New TContext()
            Dim dbSet = ctx.Set(Of TEntity)()

            If includeList IsNot Nothing Then
                For Each include As String In includeList
                    dbSet = dbSet.Include(include)
                Next
            End If

            If predicate Is Nothing Then
                Return Await dbSet.ToListAsync()
            End If

            Return Await dbSet.Where(predicate).ToListAsync()
        End Using
    End Function

    Public Async Function GetAsync(ByVal predicate As Expression(Of Func(Of TEntity, Boolean)), Optional ByVal includeList As List(Of String) = Nothing) As Task(Of TEntity) Implements IBaseRepository(Of TEntity, TId).GetAsync
        Using ctx As New TContext()
            Dim dbSet = ctx.Set(Of TEntity)()

            If includeList IsNot Nothing Then
                For Each include As String In includeList
                    dbSet = dbSet.Include(include)
                Next
            End If

            Return Await dbSet.SingleOrDefaultAsync(predicate)
        End Using
    End Function

    Public Async Function AnyAsync(ByVal predicate As Expression(Of Func(Of TEntity, Boolean))) As Task(Of Boolean) Implements IBaseRepository(Of TEntity, TId).AnyAsync
        Using ctx As New TContext()
            Return Await ctx.Set(Of TEntity)().AnyAsync(predicate)
        End Using
    End Function

    Public Async Function AddAsync(ByVal entity As TEntity) As Task(Of TEntity) Implements IBaseRepository(Of TEntity, TId).AddAsync
        Using ctx As New TContext()
            entity.CreatedDate = DateTime.Now
            Dim entityEntry = ctx.Set(Of TEntity)().Add(entity)
            Await ctx.SaveChangesAsync()
            Return entityEntry
        End Using
    End Function


    Public Async Function AddRangeAsync(ByVal entities As ICollection(Of TEntity)) As Task(Of IEnumerable(Of TEntity)) Implements IBaseRepository(Of TEntity, TId).AddRangeAsync
        Using ctx As New TContext()
            Dim _dbSet = ctx.Set(Of TEntity)()

            For Each entity As TEntity In entities
                entity.CreatedDate = DateTime.Now
            Next

            _dbSet.AddRange(entities)
            Await ctx.SaveChangesAsync()
            Return entities
        End Using
    End Function


    Public Async Function UpdateAsync(ByVal entity As TEntity) As Task Implements IBaseRepository(Of TEntity, TId).UpdateAsync
        Using ctx As New TContext()
            entity.UpdatedDate = DateTime.Now
            ctx.Entry(entity).State = EntityState.Modified
            Await ctx.SaveChangesAsync()
        End Using
    End Function


    'Public Async Function GetByIdAsync(ByVal id As TId, Optional includeList As List(Of String) = Nothing) As Task(Of TEntity) Implements IBaseRepository(Of TEntity, TId).GetByIdAsync
    '    Using ctx As New TContext()
    '        Dim query = ctx.Set(Of TEntity)()

    '        If includeList IsNot Nothing Then
    '            For Each include As String In includeList
    '                query = query.Include(include)
    '            Next
    '        End If

    '        Return Await query.FirstOrDefaultAsync(Function(e) e.Id.Equals(id))
    '    End Using
    'End Function


End Class

