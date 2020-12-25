@ModelType Maple.LoginModel

@Code
    Layout = Nothing
    Dim banner As Boolean
    Dim bText As String = ""

    If Not TempData(Constants.IDENT_REQ_FAIL_ONE) = Nothing And TempData(Constants.IDENT_REQ_FAIL_ONE) = True Then
        banner = True
        bText = "Invalid Username/Password"
        TempData(Constants.IDENT_REQ_FAIL_ONE) = Nothing
    End If
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Login - Kiosk</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
    <link href="~/Content/login.css" rel="stylesheet">
</head>
<body>



    @Using (Html.BeginForm("Auth", "Kiosk", Nothing, FormMethod.Post, New With {.class = "form-signin mb-4 w3"}))
        @Html.AntiForgeryToken()

        @<div>
            <a href="@Url.Action("Index", "Home")">
                <img Class="mb-4" src="~/Content/img/Maple.png" alt="" width="72" height="72">
            </a>

            <h1 class="form-heading">Sign in</h1>

            @If banner Then
                @<div Class="alert alert-danger" role="alert">
                    @bText
                </div>
            End If

            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

            @Html.EditorFor(Function(model) model.Username, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.Username)}})
            @Html.ValidationMessageFor(Function(model) model.Username, "", New With {.class = "text-danger"})

            @Html.EditorFor(Function(model) model.Password, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.Password)}})
            @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})

            <div class="mb-4">
                @Html.CheckBoxFor(Function(model) model.remember, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.LabelFor(Function(model) model.remember, New With {.htmlAttributes = New With {.class = "form-control"}})
            </div>

            <p>Not a member? @Html.ActionLink("Register", "Register", "Auth") with us!</p>
            <button class="btn btn-lg btn-dark btn-block" type="submit" name="btnLogin">Sign in</button>
            <p class="form-heading mt-5 mb-3 text-muted">&copy; @DateTime.Now.Year City Public Libary</p>
        </div>
    End Using


</body>
</html>
