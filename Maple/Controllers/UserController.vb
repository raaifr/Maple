Imports System.Drawing
Imports System.IO
Imports QRCoder
Imports Communications
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Web.Services

Namespace Controllers
    Public Class UserController
        Inherits Controller

        ' GET: User
        Function dash() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Return View()
        End Function

        Function reservehistory() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim booklist As List(Of BookModel) = BookProcess.getreservehistory(Session.Item(Constants.IDENT_USER_ID))
            Return View(booklist)
        End Function

        Function borrowhistory() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim booklist As List(Of BookModel) = BookProcess.getborrowhistory(Session.Item(Constants.IDENT_USER_ID))
            Return View(booklist)
        End Function

        Function membershipdetails() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Then
                Return RedirectToAction("Login", "Auth")
            End If

            Using ms As New MemoryStream
                Using bmp As Bitmap = generateqr(Session.Item(Constants.IDENT_USER_MEMBER))
                    bmp.Save(ms, Imaging.ImageFormat.Png)
                    ViewBag.QRCodeImage = "data:image/png;base64," & Convert.ToBase64String(ms.ToArray())
                End Using
            End Using

            ViewBag.membershipstat = isMembershipValid(UserProcess.getLastPayment(Session(Constants.IDENT_USER_ID)))


            Dim phist As List(Of PaymentModel) = UserProcess.getPaymenthistory(Session(Constants.IDENT_USER_ID))
            Return View(phist)
        End Function

        Function reqRenew() As ActionResult
            Session.Add(Constants.IDENT_USER_PAY_SUCCESS_REDIRECT, New String() {"membershipdetails", "User"})
            Return RedirectToAction("Payment", "Payment")
        End Function

        Function reqoverduefine(ByVal mcode As String, ByVal bcode As String) As ActionResult
            Session.Add(Constants.IDENT_USER_PAY_SUCCESS_REDIRECT, New String() {"acceptOverdueBook", "User"})
            Session.Add(Constants.IDENT_USER_PAYREQ, True)
            Session.Add(Constants.IDENT_PAYTYPE, PaymentType.OverDue)
            Dim user As UserModel = UserProcess.refUser(mcode)
            Dim pymodel As New PaymentModel With {
                .paymentType = PaymentType.OverDue,
                .userid = user.userid
            }
            Session.Add(Constants.IDENT_MODEL2, pymodel)
            Return RedirectToAction("Payment", "Payment")

        End Function

        Function Account() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Dim user As UserModel = UserProcess.getUser(Session.Item(Constants.IDENT_USER_ID))
            Dim prof As New ProfileModel With {
                .firstname = user.firstname,
                .lastname = user.lastname,
                .nid = user.nid,
                .dob = user.dob
                }

            Using ms As New MemoryStream
                Using bmp As Bitmap = generateqr(Session.Item(Constants.IDENT_USER_MEMBER))
                    bmp.Save(ms, Imaging.ImageFormat.Png)
                    ViewBag.QRCodeImage = "data:image/png;base64," & Convert.ToBase64String(ms.ToArray())
                End Using
            End Using

            Return View(prof)
        End Function

        Function sendqr() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Then
                Return RedirectToAction("Login", "Auth")
            End If

            emailqr()

            TempData(Constants.IDENT_USER_QR) = True
            Return RedirectToAction("Account")
        End Function

        Function UpdateInfo(model As ProfileModel) As ActionResult
            'TODO rewrite this mess! //TODONE - mess is clean enuf
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Then
                Return RedirectToAction("Login", "Auth")
            End If
            Try
                Dim updatecarriedout As Boolean = False

                If Not IsNothing(model.contact) Then
                    UserProcess.UpdateUserContact(Session(Constants.IDENT_USER_ID), model.contact)
                    TempData(Constants.IDENT_REQ_SUCCESS) = True
                    updatecarriedout = True
                End If

                If model.old_password <> "" And model.Password <> "" And model.ConfirmPassword <> "" Then
                    If Not IsNothing(UserProcess.AuthenticateUser(Session(Constants.IDENT_USER_ID), model.old_password)) Then
                        UserProcess.UpdateUserContact(Session(Constants.IDENT_USER_ID), model.Password)
                        TempData(Constants.IDENT_REQ_SUCCESS) = True
                        updatecarriedout = True
                    Else
                        TempData(Constants.IDENT_REQ_FAIL_REASON) = "Invalid Password(s). You Need to fillout Old Password, New Password and Confirm New Password Fields Correctly in order to update your password."
                        TempData(Constants.IDENT_REQ_FAIL_ONE) = True
                        updatecarriedout = True
                        Return RedirectToAction("Account")
                    End If
                End If

                If model.email <> "" Then
                    UserProcess.UpdateUserEmail(Session(Constants.IDENT_USER_ID), model.email)
                    TempData(Constants.IDENT_REQ_SUCCESS) = True
                    updatecarriedout = True
                End If

                If Not updatecarriedout Then
                    TempData(Constants.IDENT_REQ_FAIL_REASON) = "No Info Updated. ERR: NULL VALUES"
                    TempData(Constants.IDENT_REQ_FAIL_ONE) = True
                    updatecarriedout = True
                End If

            Catch ex As Exception
                'TempData(Constants.IDENT_REQ_FAIL_REASON) = ex.Message
                TempData(Constants.IDENT_REQ_FAIL_ONE) = True
            End Try

            Return RedirectToAction("Account")
        End Function

        Function qrcode() As ActionResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Then
                Return RedirectToAction("Login", "Auth")
            End If

            emailqr()

            TempData(Constants.IDENT_USER_QR) = True
            Return View()
        End Function

        Public Function emailqr() As Boolean
            Dim model As UserModel = UserProcess.getUser(Session(Constants.IDENT_USER_ID))

            Dim Data As Attachment
            Dim ms As New MemoryStream 'memory stream disposed by communications.email.sendemail method
            Dim bmp As Bitmap = generateqr(Session.Item(Constants.IDENT_USER_MEMBER))
            bmp.Save(ms, Imaging.ImageFormat.Jpeg)
            Data = New Attachment(ms, MediaTypeNames.Image.Jpeg)

            Dim eBody As New StringBuilder
            eBody.AppendLine("Dear " & model.firstname.Trim & " " & model.lastname.Trim & ",")
            eBody.AppendLine("Please find your Member Code attached. You can use this code to borrow and return books. Simply scan the code on your phone at one our kiosks placed around the complex.")
            eBody.AppendLine("Your Membership Code is: " & model.membership)
            eBody.AppendLine("If You Need Any Further Assistance, you can always get more info at https://www.docs.maple.com/")
            eBody.AppendLine("Thank You")
            eBody.AppendLine("Maple Library Team")

            Dim mail As New Email
            'TODO: change recevier to model.email
            mail.SendEmail(mail.Self, Email.Prefixes.Maple.QRCode, "Your Member Code", eBody.ToString, New Attachment() {Data})
            Return True
        End Function

        Function acceptOverdueBook() As ActionResult
            If Not IsNothing(Session(Constants.IDENT_USER_PAY_SUCCESS) And Session(Constants.IDENT_USER_PAY_SUCCESS)) Then
                TransactionProcess.updateTransaction(Session(Constants.IDENT_USER_ID), Session(Constants.IDENT_BOOK_ID), DateTime.Now)
            End If
            Return RedirectToAction("koisk", "Kiosk")
        End Function

        Function Borrow(ByVal mcode As String, ByVal bcode As String) As JsonResult
            If IsNothing(Session(Constants.IDENT_USER_ID)) Or IsNothing(Session(Constants.IDENT_USER_MEMBER)) Or Session(Constants.IDENT_USER_MEMBER) <> "kioskid" Then
                ''borrows can only happen on onsite kiosks
                'Return RedirectToAction("Index", "Kiosk")
                'Return Json(New jsonresultobject(False, "Invalid Authorization"), JsonRequestBehavior.AllowGet)
            End If

            Dim bookid As Integer = BookProcess.decodebookCode(bcode)
            Dim book As BookModel = BookProcess.getBook(bookid)
            Dim user As UserModel = UserProcess.refUser(mcode)
            'check if both book and member exists along with membership activs status
            If IsNothing(book) Then
                Return Json(New jsonresultobject(False, "Invalid Book"), JsonRequestBehavior.AllowGet)
            End If
            If IsNothing(user) Then
                Return Json(New jsonresultobject(False, "Invalid Member"), JsonRequestBehavior.AllowGet)
            End If
            If Not isMembershipValid(user.lastpayment) Then
                Return Json(New jsonresultobject(False, "Please Renew Membership"), JsonRequestBehavior.AllowGet)
            End If

            ''check if book is over due. if over due - goto payment and then come back
            Dim lst As List(Of OverDueModel) = BookProcess.checkOverdue(user.userid)
            For Each itm In lst
                If itm.bookid = bookid And Not Session(Constants.IDENT_USER_PAY_SUCCESS) Then
                    'this book is overdue
                    Return Json(New jsonresultobject(False, "overdue"), JsonRequestBehavior.AllowGet)
                End If
            Next

            If (TransactionProcess.checkReturn(bookid, user.userid)) Then
                TransactionProcess.updateTransaction(user.userid, bookid, DateTime.Now)
                Return Json(New jsonresultobject(True, ""), JsonRequestBehavior.AllowGet)
            Else
                TransactionProcess.addTransaction(user.userid, bookid, DateTime.Now, Nothing)
                Return Json(New jsonresultobject(True, ""), JsonRequestBehavior.AllowGet)
            End If
            Return Json(New jsonresultobject(False, "Unknown Error"), JsonRequestBehavior.AllowGet)
        End Function

        <WebMethod>
        Function Reserve(ByVal bkid As String, ByVal searchtrm As String) As JsonResult
            Dim res As New BookModel
            If IsNothing(Session(Constants.IDENT_USER_ID)) Then
                'TODO set something to store all search term and filters if redirected to login
                Session(Constants.IDENT_SEARCH2LOGIN) = True
                Session(Constants.IDENT_SEARCHTERM) = searchtrm
                Return Json(New jsonresultobject(False, "You must be logged In To make any reservations"), JsonRequestBehavior.AllowGet)
                'Return RedirectToAction("Login", "Auth")
            End If

            Dim bookid As Integer = CInt(bkid)
            Dim userid As Integer = CInt(Session(Constants.IDENT_USER_ID))
            Dim rdate As Date = DateTime.Now

            If Not isMembershipValid(UserProcess.getLastPayment(userid)) Then
                Return Json(New jsonresultobject(False, "Please Renew Membership"), JsonRequestBehavior.AllowGet)
            End If

            'check if book is already reserved by user
            If TransactionProcess.checkRevervations(bookid, userid) Then Return Json(New jsonresultobject(False, "You already have this book in your reservations"), JsonRequestBehavior.AllowGet)

            Dim bookinfo As BookModel = BookProcess.getBook(bookid, True)
            If bookinfo.reserved < bookinfo.stock Then
                'can be reserved
                TransactionProcess.addReservation(bookid, userid, rdate)
                Return Json(New jsonresultobject(True, "Book Reserved. Please borrow the book within 48hrs to ensure availability"), JsonRequestBehavior.AllowGet)
            Else
                'not available
                Return Json(New jsonresultobject(False, "Sorry, This book is not available for reservation at this moment"), JsonRequestBehavior.AllowGet)
            End If

        End Function

        Public Shared Function generateqr(ByVal content As String) As Bitmap
            Dim qrGenerator As QRCodeGenerator = New QRCodeGenerator()
            Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q)
            Dim qCode As QRCode = New QRCode(qrCodeData)
            Dim logo As Bitmap
            Using ms As New MemoryStream(qrLogo.logo)
                logo = Bitmap.FromStream(ms)
            End Using
            Dim col As Color = Color.FromArgb(8, 0, 70)
            Return qCode.GetGraphic(20, col, Color.White, logo)
        End Function

        Public Function img2Byte(ByVal img As Image) As Byte()
            Using stream As New MemoryStream()
                img.Save(stream, Imaging.ImageFormat.Png)
                Return stream.ToArray()
            End Using
        End Function

        ''' <summary>
        ''' Checks if the users membership is still valid.
        ''' As in if the last membership payment is within 30 days plus a grace period of 5 days
        ''' </summary>
        ''' <param name="paymentdate">last date of membership payment</param>
        ''' <returns>True if the last payment date is within the range of 35 days</returns>
        Public Shared Function isMembershipValid(paymentdate As Date) As Boolean
            Return (paymentdate - DateTime.Now).TotalDays < 35
        End Function
    End Class


    Public Class jsonresultobject
        Public Sub New()

        End Sub

        Public Sub New(bool As Boolean, str As String)
            succ = bool
            msg = str
        End Sub
        Public Property succ As Boolean
        Public Property msg As String
    End Class
End Namespace