@Code
    Layout = Nothing
    Dim msg As String = "Thank you! We have received your payment"
    Dim newmember = False
    Dim btnvalue As String = "Take Me Back"
    Dim action As String = Session(Constants.IDENT_USER_PAY_SUCCESS_REDIRECT)(0)
    Dim controller As String = Session(Constants.IDENT_USER_PAY_SUCCESS_REDIRECT)(1)
    If Session(Constants.IDENT_USER_PAY_SUCCESS_REDIRECT)(0) = "Index" Then
        msg = "Thank you for Registering with us! We have received your payment. You may now collect your member card at the library counter OR Have your QR code issued here."
        newmember = True
        btnvalue = "Collect Card"
    End If
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Success - Maple</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/success.css")
    @Styles.Render("~/Content/themectrls.css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>

    <div class="container-fluid text-center col-md-5">

        <div class="success-icon">
            <div class="success-icon__tip"></div>
            <div class="success-icon__long"></div>
        </div>
        <br><br> <h2 style="color:#0fad00">Success</h2>

        <p style="font-size:20px;color:#5C5C5C;">
            @msg
        </p>

        <input type="button"
               class="btn btn-outline-success"
               style="min-width: 8rem;"
               value="@btnvalue"
               onclick="location.href='@Url.Action(action, controller)'" />
       
        @If newmember Then
            @<input type="button"
                    class="btn btn-maple"
                    style="min-width: 8rem;background:#02002d!important;color:#fff!important;"
                    value="Send Me QR Code"
                    onclick="location.href='@Url.Action("qrcode", "User")'" />
        End If
        <br><br>
    </div>




</body>
</html>
