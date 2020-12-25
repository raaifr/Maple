@Code
    Layout = Nothing
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
            Your QR Code has been sent to the registered email addresss.
        </p>

        @If Not IsNothing(ViewBag.QRCodeImage) Then
            '@<img src="@ViewBag.QRCodeImage" alt="" style="height:150px;width:150px" />
        End If

        @Html.ActionLink("Home Page", "Index", "Home", Nothing, New With {.class = "btn-success-outline"})
       
        <br><br>
    </div>




</body>
</html>
