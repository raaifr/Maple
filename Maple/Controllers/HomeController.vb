Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Dim sm As SearchModel = New SearchModel

        Dim Members = System.Enum.GetNames(GetType(BookProcess.Category))
        Dim values = System.Enum.GetValues(GetType(BookProcess.Category))
        Dim list As New List(Of SelectListItem)
        For i = 0 To Members.Length - 1
            Dim itm As New SelectListItem
            itm.Text = Members(i).ToString
            itm.Value = Integer.Parse(values(i))
            list.Add(itm)
        Next
        sm.CategoryList = list
        Return View(sm)
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function

    Function test() As ActionResult
        Return View()
    End Function
End Class


