Imports System.Drawing
Imports System.Web.Mvc

Namespace Controllers
    Public Class StaffController
        Inherits Controller

        Function dash() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Session.Add(Constants.IDENT_STAFF_ROLE, StaffProcess.getRole(Session(Constants.IDENT_STAFF_ID)))
            End If
            Return View(StaffProcess.getstaff(Session(Constants.IDENT_STAFF_ID)))
        End Function

        'todo
#Region "        VIEW / EDIT / DELETE MEMBER           "
        Function viewMember() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)

            If role < 200 Then
                Return RedirectToAction("accessdenied")
            End If

            Return View()
        End Function

        <HttpPost>
        Function viewMember(ByVal model As SearchModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)

            If role < 200 Then
                Return RedirectToAction("accessdenied")
            End If

            Dim userlist As List(Of UserModel) = UserProcess.finduser(model.SearchTerm)
            model.user = userlist
            Return View(model)
        End Function

#End Region

#Region "        VIEW / EDIT / DELETE BOOK           "

        Function findbook() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If

            Return View()
        End Function

        <HttpPost>
        Function findbook(model As SearchModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If model.SearchTerm = "" Then Return View()
            Dim booklist As List(Of BookModel) = BookProcess.findBook(model.SearchTerm)
            model.Book = booklist
            Return View(model)

        End Function

        <HttpGet>
        Function editbook(id As Integer) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If id < 0 Then Return RedirectToAction("findbook")
            Dim book As BookModel = BookProcess.getBook(id)
            book.input_author = book.authorstring
            Return View(book)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function updatebook(model As BookModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Or Not ModelState.IsValid Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim res As QueryResultObj = BookProcess.updatebook(model.id, model.series_title, model.book_title, model.publisher, model.input_author, model.isbn, model.ddc, model.tags, model.year, model.stock, model.shelf)
            TempData(Constants.IDENT_REQ_SUCCESS) = res.Exec
            TempData(Constants.IDENT_QUERY_ERROR) = res.errmsg
            ModelState.Clear()
            Return RedirectToAction("editbook", New With {.id = model.id})
        End Function

        Function deletebook(ByVal bookid As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return Json(New jsonresultobject(False, "Invalid Authorization"))
            End If
            Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
            If role < 500 Then
                Return Json(New jsonresultobject(False, "Invalid Permissions"))
            End If
            BookProcess.deletebook(CInt(bookid))
            Return Json(New jsonresultobject(True, ""))
        End Function

#End Region

#Region "        VIEW / EDIT / DELETE AUTHOR           "

        Function viewauthor() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If

            Return View()
        End Function

        <HttpPost>
        Function viewauthor(model As SearchModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) > 500 And model.SearchTerm <> "" Then
                model.author = BookProcess.findAuthor(model.SearchTerm)
            End If
            Return View(model)
        End Function

        <HttpGet>
        Function editauthor(ByVal id As Integer)
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim model As AuthorModel = BookProcess.getAuthor(id)
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function updateauthor(ByVal model As AuthorModel)
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse Not ModelState.IsValid OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim res As QueryResultObj = BookProcess.updateAuthor(model.id, model.firstname, model.middlename, model.lastname)
            TempData(Constants.IDENT_REQ_SUCCESS) = Res.Exec
            TempData(Constants.IDENT_QUERY_ERROR) = res.errmsg
            ModelState.Clear()
            Return RedirectToAction("editauthor", New With {.id = model.id})
        End Function

        Function deletauthor(ByVal authid As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return Json(New jsonresultobject(False, "Invalid Authorization"))
            End If
            Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
            If role < 500 Then
                Return Json(New jsonresultobject(False, "Invalid Permissions"))
            End If
            BookProcess.deleteauthor(CInt(authid))
            Return Json(New jsonresultobject(True, ""))
        End Function


#End Region

#Region "        VIEW / EDIT / DELETE PUBLISHER           "

        Function viewpublisher() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Return View()
        End Function

        <HttpPost>
        Function viewpublisher(model As SearchModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) > 500 And model.SearchTerm <> "" Then
                model.publisher = BookProcess.findpublisher(model.SearchTerm)
            End If
            Return View(model)
        End Function

        <HttpGet>
        Function editpublisher(ByVal id As Integer)
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim model As PublisherModel = BookProcess.getpublisher(id)
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function updatepublisher(ByVal model As PublisherModel)
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse Not ModelState.IsValid OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim res As QueryResultObj = BookProcess.updatePublisher(model.id, model.name, model.address)
            TempData(Constants.IDENT_REQ_SUCCESS) = res.Exec
            TempData(Constants.IDENT_QUERY_ERROR) = res.errmsg
            ModelState.Clear()
            Return RedirectToAction("editpublisher", New With {.id = model.id})
        End Function

        Function deletpublisher(ByVal pid As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return Json(New jsonresultobject(False, "Invalid Authorization"))
            End If
            Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
            If role < 500 Then
                Return Json(New jsonresultobject(False, "Invalid Permissions"))
            End If
            BookProcess.deletepublisher(CInt(pid))
            Return Json(New jsonresultobject(True, ""))
        End Function


#End Region

#Region "        VIEW / EDIT / DELETE STAFF           "
        Function viewstaff() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Return View()
        End Function

        <HttpPost>
        Function viewstaff(model As SearchModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) > 500 And model.SearchTerm <> "" Then
                model.staff = StaffProcess.findstaff(model.SearchTerm)
            End If
            Return View(model)
        End Function

        <HttpGet>
        Function editStaff(ByVal id As Integer) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim model As StaffModel = StaffProcess.getstaff(id)
            Dim list As New List(Of SelectListItem)
            Dim i As Integer = -1

            For Each rl In StaffProcess.getRoles()
                i += 1
                Dim itm As New SelectListItem
                itm.Text = rl
                itm.Value = i
                list.Add(itm)
            Next
            model.RoleList = list
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function updatestaff(ByVal model As StaffModel)
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse Not ModelState.IsValid OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim res As QueryResultObj = StaffProcess.updateStaff(model)
            TempData(Constants.IDENT_REQ_SUCCESS) = res.Exec
            TempData(Constants.IDENT_QUERY_ERROR) = res.errmsg
            ModelState.Clear()
            Return RedirectToAction("editStaff", New With {.id = model.staffid})
        End Function

        Function deletstaff(ByVal staffid As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return Json(New jsonresultobject(False, "Invalid Authorization"))
            End If
            Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
            If role < 500 Then
                Return Json(New jsonresultobject(False, "Invalid Permissions"))
            End If
            StaffProcess.deletestaff(CInt(staffid))
            Return Json(New jsonresultobject(True, ""))
        End Function
#End Region

#Region "        VIEW / EDIT / DELETE MEMBER           "

#End Region

#Region "        MEMBER CARD FUNC           "


        Function membercard() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If

            Return View()
        End Function

        <HttpPost>
        Function membercard(ByVal model As SearchModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Or Not ModelState.IsValid Then
                Return RedirectToAction("Login", "Auth")
            End If

            If model.SearchTerm = "" Then Return View()

            Dim user As UserModel = UserProcess.refUser2(model.SearchTerm)
            If user.nid = Nothing Then
                ModelState.Clear()
                Return View()
            End If

            Dim lst As New List(Of UserModel)
            lst.Add(user)
            model.user = lst

            Using ms As New IO.MemoryStream
                Using bmp As Bitmap = UserController.generateqr(user.membership)
                    bmp.Save(ms, Imaging.ImageFormat.Png)
                    ViewBag.QRCodeImage = "data:image/png;base64," & Convert.ToBase64String(ms.ToArray())
                End Using
            End Using

            ViewBag.membershipstat = UserController.isMembershipValid(UserProcess.getLastPayment(user.userid))
            ModelState.Clear()
            Return View(model)
        End Function

#End Region


#Region "        ADD FUNCTIONS(BOOK, PUBLISHER, AUTHOR, STAFF)      "


        Function addbook() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            TempData.Remove(Constants.IDENT_REQ_SUCCESS)
            Return View()
        End Function

        <HttpPost>
        Function addbook(model As BookModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Or IsNothing(model) Or Not ModelState.IsValid Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim res As QueryResultObj = BookProcess.InsertBook(model.series_title, model.book_title, model.publisher, model.input_author, model.isbn, model.ddc, model.tags, model.year, model.stock, model.shelf)
            If res.Exec Then
                TempData.Add(Constants.IDENT_REQ_SUCCESS, True)
            Else
                TempData.Add(Constants.IDENT_REQ_SUCCESS, False)
                TempData.Add(Constants.IDENT_QUERY_ERROR, res.errmsg)
            End If
            ModelState.Clear()
            Return View()
        End Function

        Function newpublisher(ByVal name As String, ByVal address As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return Json(New jsonresultobject(False, "Invalid Auth"))
            End If
            'Return Json(New jsonresultobject(True, name))
            Try
                BookProcess.insertPublisher(name, address)
                Return Json(New jsonresultobject(True, name))
            Catch ex As Exception
                Return Json(New jsonresultobject(False, "Publisher Add FAIL"))
            End Try
        End Function

        Function newauthor(ByVal firstname As String, ByVal midname As String, ByVal lastname As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
                Return Json(New jsonresultobject(False, "Invalid Auth"))
            End If
            'Return Json(New jsonresultobject(True, firstname & " " & midname & " " & lastname))
            Try
                BookProcess.insertAuthor(firstname, midname, lastname)
                Return Json(New jsonresultobject(True, firstname & " " & midname & " " & lastname))
            Catch ex As Exception
                Return Json(New jsonresultobject(False, "Author Add FAIL"))
            End Try
        End Function

        Function addStaff() As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")
            Dim list As New List(Of SelectListItem)
            Dim i As Integer = -1

            For Each rl In StaffProcess.getRoles()
                i += 1
                Dim itm As New SelectListItem
                itm.Text = rl
                itm.Value = i
                list.Add(itm)
            Next
            Dim model As New StaffModel
            model.RoleList = list
            Return View(model)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function addStaff(ByVal model As StaffModel) As ActionResult
            If IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse Not ModelState.IsValid OrElse IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            If Session(Constants.IDENT_STAFF_ROLE) < 500 Then Return RedirectToAction("accessdenied")

            model.role = StaffProcess.getRoles()(model.selectedRole)
            model.password = StaffProcess.DefaultPassword
            model.employid = StaffProcess.generateEmployeecard(model.contact)

            Dim res As QueryResultObj = StaffProcess.InsertStaff(model)
            If res.Exec Then
                TempData.Add(Constants.IDENT_REQ_SUCCESS, True)
            Else
                TempData.Add(Constants.IDENT_REQ_SUCCESS, False)
                TempData.Add(Constants.IDENT_QUERY_ERROR, res.errmsg)
            End If
            ModelState.Clear()
            Dim list As New List(Of SelectListItem)
            Dim i As Integer = -1

            For Each rl In StaffProcess.getRoles()
                i += 1
                Dim itm As New SelectListItem
                itm.Text = rl
                itm.Value = i
                list.Add(itm)
            Next
            model.RoleList = list
            Return View(model)
        End Function

#End Region

#Region "        AUTO COMPELTE LIST LOADER "
        Function reqauthorlist() As JsonResult
            Return Json(BookProcess.getAuthors.ToArray, JsonRequestBehavior.AllowGet)
        End Function
        Function reqshelflist() As JsonResult
            Return Json(BookProcess.getshelves.ToArray, JsonRequestBehavior.AllowGet)
        End Function
        Function reqpublisherlist() As JsonResult
            Return Json(BookProcess.getpublisher.ToArray, JsonRequestBehavior.AllowGet)
        End Function
        Function reqserieslist() As JsonResult
            Return Json(BookProcess.getseriesnames.ToArray, JsonRequestBehavior.AllowGet)
        End Function
#End Region

        Function accessdenied() As ActionResult
            'you do not have sufficient permission to access that feature
            Return View()
        End Function
    End Class


End Namespace