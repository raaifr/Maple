Imports System.ComponentModel.DataAnnotations

Public Class ProfileModel
    <Display(Name:="First Name")>
    Public Property firstname As String

    <Display(Name:="Last Name")>
    Public Property lastname As String

    <Display(Name:="ID /PP Number")>
    Public Property nid As String

    <Display(Name:="Date of Birth")>
    Public Property dob As String

    <Display(Name:="Mobile Number")>
    <DataType(DataType.Text)>
    <StringLength(100, ErrorMessage:="Invalid Mobile Number")>
    Public Property contact As String

    <Display(Name:="Email")>
    <DataType(DataType.EmailAddress)>
    <StringLength(300, ErrorMessage:="Invalid Email")>
    Public Property email As String

    <Display(Name:="Old Password")>
    <DataType(DataType.Password)>
    <StringLength(1000, ErrorMessage:="Password way too long", MinimumLength:=8, ErrorMessage:="Password must be atleast 8 characters long")>
    Public Property old_password As String

    <Display(Name:="New Password")>
    <DataType(DataType.Password)>
    <StringLength(1000, ErrorMessage:="Password way too long", MinimumLength:=8, ErrorMessage:="Password must be atleast 8 characters long")>
    Public Property Password As String

    <Display(Name:="Confirm New Password")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Password)>
    <Compare("Password", ErrorMessage:="Both Passwords must match")>
    Public Property ConfirmPassword As String
End Class
