Imports System.ComponentModel.DataAnnotations

Public Class StaffModel
    <Display(Name:="Roll Number")>
    Public Property staffid As Integer

    <Display(Name:="First Name")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Who has a name that long??")>
    Public Property firstname As String

    <Display(Name:="Last Name")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Who has a name that long??")>
    Public Property lastname As String

    <Display(Name:="Email")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.EmailAddress)>
    <StringLength(300, ErrorMessage:="Invalid Email")>
    Public Property email As String

    <Display(Name:="Date of Birth")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Date)>
    <DisplayFormat(DataFormatString:="{0:d}", ApplyFormatInEditMode:=True)>
    Public Property dob As Date

    <Display(Name:="Mobile Number")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.PhoneNumber)>
    Public Property contact As Long

    <Display(Name:="Date of Employment")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Date)>
    Public Property employdate As Date

    <Display(Name:="Staff ID")>
    Public Property employid As String

    <Display(Name:="Staff Role")>
    Public Property role As String

    <Display(Name:="Password")>
    Public Property password As String

    <Display(Name:="Access Code")>
    Public Property accessCode As Integer

    Public Property selectedRole As Integer = 2

    Public Property RoleList As IEnumerable(Of SelectListItem)
End Class
