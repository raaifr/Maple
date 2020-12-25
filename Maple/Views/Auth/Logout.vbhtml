@Code
    Layout = Nothing
End Code
<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Maple</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/Logout.css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
</head>

<body>

    <div class="hero-text">
        <div class="container-fluid">
            <h1 id="cliptext">See You!</h1>
        </div>
        <h2>We are sad to see you go :(</h2>
        <p>we hope you have found a book of your interest and enjoy it. Stay Curious!</p>
        <br />
        <p>Click @Html.ActionLink("Here", "Index", "Home") if you are not redirected automatically</p>
    </div>
</body>

</html>