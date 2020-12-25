Public Class dil_BookModel
    Public Property id As Integer
    Public Property series_title As String
    Public Property book_title As String
    Public Property publisher As String
    Public Property isbn As String
    Public Property ddc As Double
    Public Property tags As String
    Public Property year As Integer
    Public Property stock As Integer
    Public Property shelf As String

    Public Property authors As List(Of String)
End Class
