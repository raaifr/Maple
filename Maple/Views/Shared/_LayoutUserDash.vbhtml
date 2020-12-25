<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Maple</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body style="padding-top:0;">
    @Html.Partial("_PartialNavbarDash")


    <div class="container-fluid">
        <div class="row">
            <div class="col-2 px-1 position-fixed" id="sticky-sidebar">
                @Html.Partial("_PartialUserDashSidebar")
            </div>
            <div class="col offset-2" id="main">
                @RenderBody()
            </div>
        </div>


        <hr />


        <footer>
            <span class="text-muted"> &copy; @DateTime.Now.Year City Public Libary</span>
        </footer>


    </div>




    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
    @Scripts.Render("~/Scripts/MapleCore.js")
</body>
</html>
