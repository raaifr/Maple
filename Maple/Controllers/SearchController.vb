Imports System.Web.Mvc

Namespace Controllers

    Public Class SearchController
        Inherits Controller

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Search(model As SearchModel) As ActionResult
            If ModelState.IsValid Then
                If model.SearchTerm = "" Then Return RedirectToAction("Index", "Home")

                Dim books As List(Of BookModel) = BookProcess.findBook(model.SearchTerm, model.selectedCategory, True)

                Dim Members = System.Enum.GetNames(GetType(BookProcess.Category))
                Dim values = System.Enum.GetValues(GetType(BookProcess.Category))
                Dim list As New List(Of SelectListItem)
                For i = 0 To Members.Length - 1
                    Dim itm As New SelectListItem
                    itm.Text = Members(i).ToString
                    itm.Value = Integer.Parse(values(i))
                    list.Add(itm)
                Next
                model.CategoryList = list
                If Not IsNothing(books) Then
                    ModelState.Clear()
                    'Convert and display
                    model.Book = books

                End If

            End If
            Return View(model)
        End Function



    End Class
End Namespace