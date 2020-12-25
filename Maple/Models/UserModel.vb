
Imports System.ComponentModel.DataAnnotations
Imports System.Drawing

Public Class UserModel
    <Display(Name:="Roll Number")>
    Public Property userid As Integer

    <Display(Name:="First Name")>
    Public Property firstname As String

    <Display(Name:="Last Name")>
    Public Property lastname As String

    <Display(Name:="Contact")>
    Public Property contact As Long

    <Display(Name:="Email")>
    Public Property email As String

    <Display(Name:="Country")>
    Public Property country As String

    <Display(Name:="National ID")>
    Public Property nid As String

    <Display(Name:="Password")>
    Public Property Password As String

    <Display(Name:="Membercard Data")>
    Public Property membership As String

    <Display(Name:="Date of Birth")>
    <DisplayFormat(DataFormatString:="{0:d}", ApplyFormatInEditMode:=True)>
    Public Property dob As Date


    <Display(Name:="Last Paid Membership")>
    <DisplayFormat(DataFormatString:="{0:d}")>
    Public Property lastpayment As Date


End Class
