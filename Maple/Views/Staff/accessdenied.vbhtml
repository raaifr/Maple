
@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Staff - Maple</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/error.css")
    @Styles.Render("~/Content/themectrls.css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-6 align-self-center">
                <div class="container-fluid">
                    <img src="~/Content/img/403.png" style="width:117%;" />
                </div>
            </div>
            <div class="col-md-6 align-self-center">
                <h1>Access Denied</h1>
                <h2>insufficient Permissions</h2>
                <p>
                    You do not have sufficient permissions /rights to access that feature.
                    If you would like to use the feature you can contact an admin staff anytime.
                </p>
                <br />
                <input type="button"
                       class="btn btn-maple"
                       style="min-width: 8rem;background:#02002d!important;color:#fff!important;"
                       value="Staff Dashboard"
                       onclick="location.href='@Url.Action("dash", "Staff")'" />
            </div>
        </div>
    </div>






</body>
</html>
