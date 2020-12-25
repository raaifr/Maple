Imports System.ComponentModel.DataAnnotations

Public Class SearchModel
    <Display(Name:="Enter Search Term")>
    Public Property SearchTerm As String
    Public Property Book As List(Of BookModel)
    Public Property user As List(Of UserModel)
    Public Property author As List(Of AuthorModel)
    Public Property publisher As List(Of PublisherModel)
    Public Property staff As List(Of StaffModel)
    Public Property selectedCategory As Integer = 1

    Public Property itemid As String
    Public Property CategoryList As IEnumerable(Of SelectListItem) '= {
    'New SelectListItem() With {.Value = "0", .Text = "Auto"},
    'New SelectListItem() With {.Value = "1", .Text = "Book"},
    'New SelectListItem() With {.Value = "2", .Text = "Author"},
    'New SelectListItem() With {.Value = "3", .Text = "Publisher"},
    'New SelectListItem() With {.Value = "4", .Text = "ISBN"}
    '}

End Class
