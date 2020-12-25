Imports System.ComponentModel.DataAnnotations

Public Class RegisterModel
    <Display(Name:="First Name")>
    <Required(ErrorMessage:="First Name is a required field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Who has a first name that long??")>
    Public Property firstname As String

    <Display(Name:="Last Name")>
    <Required(ErrorMessage:="Last Name is a required field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Who has a last name that long??")>
    Public Property lastname As String

    <Display(Name:="Mobile Number")>
    <Required(ErrorMessage:="Mobile number is a required field")>
    <DataType(DataType.Text)>
    Public Property contact As Long

    <Display(Name:="Email")>
    <Required(ErrorMessage:="Email is a required field")>
    <DataType(DataType.EmailAddress)>
    <StringLength(300, ErrorMessage:="Invalid Email")>
    Public Property email As String

    <DataType(DataType.Text)>
    Public Property country As String

    <Display(Name:="NIC / Passport")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="That NIC / Passport number is too long!")>
    Public Property nid As String

    <Display(Name:="Date of Birth")>
    <Required(ErrorMessage:="Date of birth is a required field")>
    <DataType(DataType.Date)>
    <DisplayFormat(DataFormatString:="{0:d}")>
    Public Property dob As Date

    'Must be at least 10 characters 
    'Must contain at least one one lower Case letter, one upper Case letter, one digit And one special character
    'Valid special characters are @#$%^&+=
    '<RegularExpression("^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage:="Password does not meet the security criteria")>
    <Display(Name:="Password")>
    <Required(ErrorMessage:="You will have to set a password for your account")>
    <DataType(DataType.Password)>
    <StringLength(1000, ErrorMessage:="Password WAYY too long", MinimumLength:=8, ErrorMessage:="Password must be atleast 8 characters long")>
    Public Property Password As String

    <Display(Name:="Confirm Password")>
    <Required(ErrorMessage:="You need to confirm your chosen password")>
    <DataType(DataType.Password)>
    <Compare("Password", ErrorMessage:="Both Passwords must match")>
    Public Property ConfirmPassword As String

    Public Property selectedCountry As Integer = 1

    Public Property CountryList As IEnumerable(Of SelectListItem)


End Class
