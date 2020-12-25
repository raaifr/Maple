Public Class QueryResultObj
    Public Sub New()

    End Sub

    Public Sub New(ByVal execute As Boolean, ByVal msg As String)
        Exec = execute
        errmsg = msg
    End Sub

    Public Property Exec As Boolean
    Public Property errmsg As String
End Class