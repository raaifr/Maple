Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection
Imports Dapper

Public Class SqlDataAccess
    Public Shared Function getConnectionString(Optional conName = "MapleDatabase") As String
        Dim cs As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(conName)
        Return cs.ConnectionString
    End Function

    Public Shared Function getConnection() As SqlConnection
        Return New SqlConnection(getConnectionString)
    End Function

    Public Shared Function loadData(Of T)(sql As String, data As T) As List(Of T)
        Using connection As IDbConnection = New SqlConnection(getConnectionString())
            Return connection.Query(Of T)(sql, data).ToList()
        End Using
    End Function

    Public Shared Function saveData(Of T)(sql As String, data As T) As Integer
        Using connection As IDbConnection = New SqlConnection(getConnectionString())
            Return connection.Execute(sql, data)
        End Using
    End Function

    Public Shared Function DataReaderMapToList(Of T)(ByVal dr As IDataReader) As List(Of T)
        Dim list As New List(Of T)
        Dim obj As T
        While dr.Read()
            obj = Activator.CreateInstance(Of T)()
            For Each prop As PropertyInfo In obj.GetType().GetProperties()
                If Not Object.Equals(dr(prop.Name), DBNull.Value) Then
                    prop.SetValue(obj, dr(prop.Name), Nothing)
                End If
            Next
            list.Add(obj)
        End While
        Return list
    End Function

End Class
