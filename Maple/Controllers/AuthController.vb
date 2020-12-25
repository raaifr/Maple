Imports System.Net.Mail

Public Class AuthController
    Inherits System.Web.Mvc.Controller


    Function Login() As ActionResult
        Return View()
    End Function

    Function Register() As ActionResult
        Dim rm As RegisterModel = New RegisterModel
        'this code is implemented within the register.vbhtml
        Dim list As New List(Of SelectListItem)
        Dim i As Integer = -1
        For Each country In Constants.COUNTRY_LIST
            i += 1
            Dim itm As New SelectListItem
            itm.Text = country
            itm.Value = i
            list.Add(itm)
        Next
        rm.CountryList = list
        Return View(rm)
    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Function Login(model As LoginModel) As ActionResult
        If ModelState.IsValid Then
            'if username is an email, its staff login.
            If IsValidEmail(model.Username) Then
                Dim staffmodel As StaffModel = StaffProcess.AuthenticateStaff(model.Username, model.Password)
                If Not IsNothing(staffmodel) Then
                    ModelState.Clear()
                    'Dim manager As SessionIDManager = New SessionIDManager()
                    'TempData(Constants.IDENT_REQ_SUCCESS) = True
                    Session.Add(Constants.IDENT_STAFF_ID, staffmodel.staffid)
                    Session.Add(Constants.IDENT_STAFF_ROLE, StaffProcess.getRole(staffmodel.staffid))
                    Session.Add(Constants.IDENT_STAFF_NAME, New String() {staffmodel.firstname, staffmodel.lastname})
                    Session.Add(Constants.IDENT_STAFF_EMP_NUMBER, staffmodel.employid)
                    FormsAuthentication.SetAuthCookie(model.Username, model.remember)
                    Return RedirectToAction("dash", "Staff")
                Else
                    TempData(Constants.IDENT_REQ_FAIL_ONE) = True
                End If
            Else
                Dim userModel As UserModel = UserProcess.AuthenticateUser(model.Username, model.Password)

                If Not IsNothing(userModel) Then
                    ModelState.Clear()
                    'Dim manager As SessionIDManager = New SessionIDManager()
                    'TempData(Constants.IDENT_REQ_SUCCESS) = True
                    Session.Add(Constants.IDENT_USER_ID, userModel.userid)
                    Session.Add(Constants.IDENT_USER_NAME, New String() {userModel.firstname, userModel.lastname})
                    Session.Add(Constants.IDENT_USER_MEMBER, userModel.membership)
                    FormsAuthentication.SetAuthCookie(model.Username, model.remember)
                    Return RedirectToAction("Index", "Home")
                Else
                    TempData(Constants.IDENT_REQ_FAIL_ONE) = True
                End If
            End If

        End If
        Return View()

    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Function Register(model As RegisterModel) As ActionResult
        If ModelState.IsValid Then
            ModelState.Clear()
            'we cannot add if the user does not pay the membership fee now can we?

            'Dim membershipid As String = generatekey(model.firstname, model.lastname, model.contact, model.contact)
            'InsertUser(model.firstname, model.lastname, model.contact, model.email, model.country, model.nid, model.Password, membershipid)

            'Dim dil_userModel As DataInterfaceLibrary.dil_UserModel = AuthenticateUser(model.nid, model.Password)

            'Session.Add(Constants.IDENT_USER_ID, dil_userModel.userid)
            'Session.Add(Constants.IDENT_USER_NAME, New String() {dil_userModel.firstname, dil_userModel.lastname})
            'Session.Add(Constants.IDENT_USER_MEMBER, membershipid)

            'TempData(Constants.IDENT_REQ_SUCCESS) = True
            Dim paymodel As New PaymentModel With {
                .paymentType = PaymentType.Membership,
                .amount = 199,
                .user = New UserModel With {
                    .firstname = model.firstname,
                    .lastname = model.lastname,
                    .email = model.email,
                    .contact = model.contact,
                    .nid = model.nid}
            }
            model.country = Constants.COUNTRY_LIST(model.selectedCountry)
            Session.Add(Constants.IDENT_MODEL1, model) 'register model
            Session.Add(Constants.IDENT_MODEL2, paymodel)
            Session.Add(Constants.IDENT_NEWPAY, True)
            Session.Add(Constants.IDENT_PAYTYPE, PaymentType.Membership)
            Session.Add(Constants.IDENT_USER_PAYREQ, True)
            Session.Add(Constants.IDENT_USER_PAY_SUCCESS_REDIRECT, New String() {"Index", "Home"})
            Return RedirectToAction("Payment", "Payment")
        End If
        Dim list As New List(Of SelectListItem)
        Dim i As Integer = -1
        For Each country In Constants.COUNTRY_LIST
            i += 1
            Dim itm As New SelectListItem
            itm.Text = country
            itm.Value = i
            list.Add(itm)
        Next
        model.CountryList = list
        Return View(model)
    End Function


    Function Logout(model As LoginModel) As ActionResult
        TempData(Constants.IDENT_REQ_SUCCESS) = Nothing
        Session.Remove(Constants.IDENT_USER_ID)
        Session.Remove(Constants.IDENT_USER_NAME)
        Session.Remove(Constants.IDENT_USER_MEMBER)

        Session.Remove(Constants.IDENT_STAFF_ID)
        Session.Remove(Constants.IDENT_STAFF_NAME)
        Session.Remove(Constants.IDENT_STAFF_EMP_NUMBER)

        Session.Clear()
        Session.Abandon()
        FormsAuthentication.SignOut()
        Return View() 'RedirectToAction("Index", "Home")
    End Function


    Private Function IsValidEmail(ByVal s As String) As Boolean
        Try
            Dim address As MailAddress = New MailAddress(s)
        Catch
            Return False
        End Try
        Return True
    End Function

End Class
