Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json
Imports NorthwindRest.DataAccess

<RoutePrefix("api/categories")>
Public Class CategoriesController
    Inherits ApiController

    ' GET api/<controller>
    <HttpGet, Route("all")>
    Public Function GetValues() As IEnumerable(Of Category)
        Dim db As New NorthwindDb()
        db.Configuration.LazyLoadingEnabled = False
        db.Configuration.ProxyCreationEnabled = False
        Dim repo As New GenericRepository(Of Category)(db)
        Dim result = repo.Find().ToList()

        ' Verhindert zirkuläre Bezüge bei Serialisierung
        Dim settings As JsonSerializerSettings = New JsonSerializerSettings() With
        {.ObjectCreationHandling = ObjectCreationHandling.Auto, .PreserveReferencesHandling = PreserveReferencesHandling.Objects}
        Dim json = JsonConvert.SerializeObject(result, settings)
        Return result
    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As String
        Return "value"
    End Function

    ' POST api/<controller>
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub

    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class
