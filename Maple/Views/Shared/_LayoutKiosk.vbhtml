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
    @Styles.Render("~/Content/kiosk.css")
</head>
<body>


    <div id="kioskcontent">

        <div id="kheader" class="text-center">
            <!-- Html.Partial("_PartialKioskHeader")-->
            <img src="~/Content/img/Maple.png" style="height:100%; width:auto; object-fit:cover;" />
        </div>

        <div id="kbody">
            @RenderBody()

            
        </div>


    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
    @Scripts.Render("~/Scripts/kiosk.js")
</body>
</html>
