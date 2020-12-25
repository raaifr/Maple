﻿@ModelType Maple.RegisterModel

@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Member Fee</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
    <link href="~/Content/payments.css" rel="stylesheet">
</head>
<body>

    @If Not TempData(Constants.IDENT_SESS_EXP) = Nothing And TempData(Constants.IDENT_SESS_EXP) = True Then
        @<div class="alert alert-danger" role="alert">
            Your session has expired. Please register again.
        </div>
        TempData(Constants.IDENT_SESS_EXP) = Nothing
    End If


    @Using (Html.BeginForm("MakePayement", "Payment", Nothing, FormMethod.Post, New With {.class = "form-signin mb-4"}))
        @Html.AntiForgeryToken()

        @<h1 class="form-heading mb-5">Membership Payment</h1>

        @<div class="row">
            <div class="col-sm-6">
                <div class="form-control">
                    <h5> <strong> Payer Details</strong></h5>
                    <p>
                        @Html.DisplayFor(Function(model) model.firstname)  @Html.DisplayFor(Function(model) model.lastname)
                    </p>
                    <p>
                        @Html.DisplayFor(Function(model) model.nid)
                    </p>
                    <p>
                        @Html.DisplayFor(Function(model) model.email)
                    </p>
                    <p>
                        @Html.DisplayFor(Function(model) model.contact)
                    </p>

                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-control" style="text-align:left!important;">
                    <h5> <strong> Payment Details</strong></h5>
                    <table class="table">

                        <tbody>
                            <tr>
                                <td> Library Membership <br /><i><small style="font-size:x-small;">(billed monthly)</small></i></td>
                                <td> <i class="fa fa-usd" aria-hidden="true"></i> 199</td>
                            </tr>
                            <tr>
                                <td> Membership overdue Fees</td>
                                <td> <i class="fa fa-usd" aria-hidden="true"></i> 0</td>
                            </tr>
                            <tr>
                                <td> Total</td>
                                <td>
                                    <i class="fa fa-usd" aria-hidden="true">
                                        199
                                    </i>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>


        @<p> By Clicking on 'Make Payment' you agree to our <a href="registration_form.php">Data Security Policy</a> and <a href="registration_form.php">Data Privacy Policy</a></p>
        @<button class="btn btn-lg btn-success" style="width:100%; max-width:230px" type="submit" name="btnLogin">Make Payment</button>
        @<p class="form-heading mt-5 mb-3 text-muted">&copy; @DateTime.Now.Year City Public Libary</p>

    End Using


</body>
</html>
