Imports System.ComponentModel.DataAnnotations

Public Class PaymentModel
    Public Property id As Integer
    Public Property userid As Integer
    Public Property paymentType As String
    <DisplayFormat(DataFormatString:="{0:d}")>
    Public Property paymentDate As Date
    Public Property amount As Double
    Public Property remarks As String

    '###################################'
    '       FOR EXTRA FUNC
    '###################################'
    Public Property overdue As Double = 0.0
    Public Property user As UserModel

    Public Property bookcount As Integer = 0
End Class

Public Class PaymentType

    '###################################'
    '       DB VALUES
    '###################################'
    Public Shared Membership As String = "Membership"
    Public Shared OverDue As String = "OverDue"
    Public Shared Other As String = "Other"
End Class
