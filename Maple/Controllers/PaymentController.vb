Namespace Controllers
    Public Class PaymentController
        Inherits Controller
        Public overdue_rate = 12.35
        Public book_overdue_rate = 1.15

        ''' <summary>
        ''' Main Payment Function. Any amounts will be calculated here. Only pass the amount if the payment type is other.
        ''' </summary>
        ''' <returns>payment gateway page</returns>
        Function Payment() As ActionResult
            'check if payment has been requested by system
            If IsNothing(Session(Constants.IDENT_USER_PAYREQ)) Or Not Session(Constants.IDENT_USER_PAYREQ) Or IsNothing(Session(Constants.IDENT_MODEL2)) Or IsNothing(Session(Constants.IDENT_PAYTYPE)) Then
                Return RedirectToAction("Index", "Error")
                'Return RedirectToAction("Login", "Auth")
            End If

            Dim pm As PaymentModel = Session.Item(Constants.IDENT_MODEL2)

            If Not IsNothing(Session(Constants.IDENT_NEWPAY)) _
                And Not Session(Constants.IDENT_NEWPAY) _
                And Not IsNothing(Session(Constants.IDENT_USER_ID)) Then
                pm.userid = Session(Constants.IDENT_USER_ID)
            End If

            'calculate over due amount
            If Not Session(Constants.IDENT_NEWPAY) Then
                Select Case Session(Constants.IDENT_PAYTYPE)
                    Case PaymentType.Membership
                        pm.amount = 199.0
                        Dim lastpaid As Date = UserProcess.getLastPayment(Session(Constants.IDENT_USER_ID))
                        Dim diff As Integer = Date.Today.Subtract(lastpaid).Days
                        If diff > 35 Then
                            pm.overdue = (Math.Abs(diff - 35) * overdue_rate)
                        Else
                            pm.overdue = 0.0
                        End If
                    Case PaymentType.OverDue
                        Dim ov As List(Of OverDueModel) = BookProcess.checkOverdue(Session(Constants.IDENT_USER_ID))
                        For Each itm In ov
                            Dim ovCount As Integer = itm.borrowdate.Subtract(Date.Today).Days
                            If ovCount > itm.due Then
                                pm.amount += (book_overdue_rate * ovCount)
                                pm.bookcount += 1
                            End If
                        Next
                    Case PaymentType.Other
                        pm.overdue = 0.0
                End Select

            End If


            Return View(pm)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Payment(model As PaymentModel) As ActionResult
            Try
                model = Session.Item(Constants.IDENT_MODEL2)

                If Not IsNothing(Session(Constants.IDENT_NEWPAY)) AndAlso Session(Constants.IDENT_NEWPAY) Then
                    If IsNothing(Session(Constants.IDENT_MODEL1)) Then
                        Return RedirectToAction("Login", "Auth")
                    End If

                    Dim rgmodel As RegisterModel = Session(Constants.IDENT_MODEL1)

                    Dim membershipid As String = UserProcess.generatekey(rgmodel.firstname, rgmodel.lastname, rgmodel.contact, rgmodel.nid)
                    UserProcess.InsertUser(rgmodel.firstname, rgmodel.lastname, rgmodel.contact, rgmodel.email, rgmodel.country, rgmodel.nid, rgmodel.Password, membershipid, rgmodel.dob)
                    Dim user As UserModel = UserProcess.AuthenticateUser(rgmodel.nid, rgmodel.Password)
                    UserProcess.addPayment(user.userid, model.paymentType, DateTime.Now, model.amount + model.overdue, model.remarks)

                    Session.Add(Constants.IDENT_USER_ID, user.userid)
                    'do we really need to set this?
                    'Session.Add(Constants.IDENT_USER_NAME, New String() {user.firstname, user.lastname})
                    Session.Add(Constants.IDENT_USER_MEMBER, membershipid)
                    Session.Add(Constants.IDENT_USER_LOGIN, True)

                    Session.Remove(Constants.IDENT_USER_PAYREQ)
                    Session.Remove(Constants.IDENT_MODEL1)
                    Session.Remove(Constants.IDENT_MODEL2)
                    Session.Add(Constants.IDENT_USER_PAY_SUCCESS, True)

                Else
                    If IsNothing(Session(Constants.IDENT_USER_ID)) Then
                        Return RedirectToAction("Login", "Auth")
                    End If
                    'Dim user As UserModel = UserProcess.getUser(Session(Constants.IDENT_USER_ID))
                    model.remarks = "Over Due Fee: " & model.overdue
                    UserProcess.addPayment(model.userid, model.paymentType, DateTime.Now, model.amount + model.overdue, model.remarks)

                    Session.Remove(Constants.IDENT_USER_PAYREQ)
                    Session.Remove(Constants.IDENT_MODEL1)
                    Session.Remove(Constants.IDENT_MODEL2)
                    Session.Remove(Constants.IDENT_PAYTYPE)
                    Session.Add(Constants.IDENT_USER_PAY_SUCCESS, True)
                End If


                Return RedirectToAction("paymentsuccess")

            Catch ex As Exception
                Return RedirectToAction("Index", "Error")
            End Try
        End Function

        Function paymentsuccess() As ActionResult
            'If Not IsNothing(Session(Constants.IDENT_USER_PAY_SUCCESS)) And Session(Constants.IDENT_USER_PAY_SUCCESS) Then
            Return View()
        End Function

    End Class
End Namespace