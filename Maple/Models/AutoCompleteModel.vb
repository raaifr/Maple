Public Class AutoCompleteModel
    Public Property autocompleteList As List(Of idvaluePair)
End Class

Public Class idvaluePair
    Public Sub New()

    End Sub
    Public Sub New(ByVal pid As Integer, pval As String)
        id = pid
        value = pval
    End Sub
    Public Property id As Integer
    Public Property value As String
End Class
