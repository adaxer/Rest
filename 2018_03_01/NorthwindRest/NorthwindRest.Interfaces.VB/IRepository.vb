Imports System.Linq.Expressions

Public Interface IRepository(Of T As Class)
    Sub Add(entity As T)

    Sub Delete(entity As T)

    Function Find(Optional filter As Expression(Of Func(Of T, Boolean)) = Nothing) As IQueryable(Of T)

End Interface
