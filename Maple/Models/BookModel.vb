Imports System.ComponentModel.DataAnnotations

Public Class BookModel
    Public Property id As Integer

    <Display(Name:="Series Title")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Thats too long a name for a series Title")>
    Public Property series_title As String

    <Display(Name:="Book Title")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Thats too long a name for a Book Title")>
    Public Property book_title As String

    <Display(Name:="Publisher")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    <StringLength(500, ErrorMessage:="Thats too long")>
    Public Property publisher As String

    <Display(Name:="ISBN")>
    <StringLength(13, ErrorMessage:="Invalid ISBN")>
    Public Property isbn As String

    <Display(Name:="Dewey Decimal Classification")>
    <Required(ErrorMessage:="Required Field")>
    Public Property ddc As Double

    <Display(Name:="Tags")>
    <StringLength(500, ErrorMessage:="Too Many Tags")>
    Public Property tags As String

    <Display(Name:="Year")>
    <Required(ErrorMessage:="Required Field")>
    Public Property year As Integer

    <Display(Name:="Stock Count")>
    <Required(ErrorMessage:="Required Field")>
    Public Property stock As Integer

    <Display(Name:="Shelf")>
    <Required(ErrorMessage:="Required Field")>
    Public Property shelf As String

    <Display(Name:="Author(s)")>
    <Required(ErrorMessage:="Required Field")>
    <DataType(DataType.Text)>
    Public Property input_author As String

    Public Property authors As List(Of String)
    Public ReadOnly Property authorstring As String
        Get
            Return String.Join(", ", authors)
        End Get
    End Property
    Public Property borrowed As Integer = 0
    Public Property reserved As Integer = 0

    <DisplayFormat(DataFormatString:="{0:d}")>
    Public Property reservedate As Date

    <DisplayFormat(DataFormatString:="{0:d}")>
    Public Property returndate As Date

    <DisplayFormat(DataFormatString:="{0:d}")>
    Public Property borrowdate As Date

    Public Property due As Integer = 14

End Class
