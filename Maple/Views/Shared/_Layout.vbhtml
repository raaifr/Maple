<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Maple</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/navheader.css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body style="padding-top:0;">
    @Html.Partial("_Partial_header")
    @Html.Partial("_Partial_Navbar")

    <div class="container-fluid">
        @RenderBody()
        <hr />
        <footer>
            <span class="text-muted">&copy; @DateTime.Now.Year City Public Libary</span>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
    @Scripts.Render("~/Scripts/MapleCore.js")
    @Scripts.Render("~/Scripts/dash.js")
</body>
</html>
