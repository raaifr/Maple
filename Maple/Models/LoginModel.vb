Imports System.ComponentModel.DataAnnotations

Public Class LoginModel
    <Display(Name:="Username")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    <StringLength(1000, ErrorMessage:="This error shouldntve happened!!", MinimumLength:=4, ErrorMessage:="Invalid Username")>
    Public Property Username As String

    'Must be at least 10 characters 
    'Must contain at least one one lower Case letter, one upper Case letter, one digit And one special character
    'Valid special characters are @#$%^&+=
    '<RegularExpression("^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage:="Password does not meet the security criteria")>
    <Display(Name:="Password")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Password)>
    <StringLength(1000, ErrorMessage:="Password waaay too long", MinimumLength:=8, ErrorMessage:="Password must be atleast 8 characters long")>
    Public Property Password As String

    <Display(Name:="Remember Me")>
    Public Property remember As Boolean
End Class
