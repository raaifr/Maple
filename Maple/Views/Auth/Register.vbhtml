@ModelType Maple.RegisterModel

@Code
    Layout = Nothing


End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Register - Maple</title>
    <link runat="server" rel="shortcut icon" href="/Content/core-images/favicon.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="/Content/core-images/favicon.ico" type="image/ico" />
    @Styles.Render("~/Content/css")
    @Styles.Render("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css")
    @Scripts.Render("~/bundles/modernizr")
    <link href="~/Content/register.css" rel="stylesheet">
</head>
<body>
    <div class="container register">
        @Using (Html.BeginForm("Register", "Auth", FormMethod.Post, New With {.class = "register-form text-center"}))
            @Html.AntiForgeryToken()


            @<a href="@Url.Action("Index", "Home")">
                <img Class="mb-4" src="~/Content/img/Maple.png" alt="" width="72" height="72">
            </a>

            @<h3 Class="mb-5">Member Registration</h3>
            @Html.ValidationSummary(True, "", New With {.class = "text-danger  text-left"})

            @<div Class="row align-items-center justify-content-center">
                <div Class="col-3">

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.firstname, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.firstname)}})
                        @Html.ValidationMessageFor(Function(model) model.firstname, "", New With {.class = "text-danger  text-left"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.lastname, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.lastname)}})
                        @Html.ValidationMessageFor(Function(model) model.lastname, "", New With {.class = "text-danger  text-left"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.contact, New With {.htmlAttributes = New With {.id = "contactin", .class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.contact)}})
                        @Html.ValidationMessageFor(Function(model) model.contact, "", New With {.class = "text-danger text-left"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.email, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.email)}})
                        @Html.ValidationMessageFor(Function(model) model.email, "", New With {.class = "text-danger text-left"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.dob, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.dob)}})
                        @Html.ValidationMessageFor(Function(model) model.dob, "", New With {.class = "text-danger text-left"})
                    </div>

                </div>

                <div Class="col-3">
                    <div class="form-group">
                        @Html.DropDownListFor(Function(model) model.selectedCountry, Model.CountryList, "-- Country --", New With {.Class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.selectedCountry, "", New With {.class = "text-danger text-left"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.nid, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.nid)}})
                        @Html.ValidationMessageFor(Function(model) model.nid, "", New With {.class = "text-danger text-left"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.Password, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.Password)}})
                        @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger text-left"})
                    </div>


                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.ConfirmPassword, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = Html.DisplayNameFor(Function(model) model.ConfirmPassword)}})
                        @Html.ValidationMessageFor(Function(model) model.ConfirmPassword, "", New With {.class = "text-danger text-left"})
                    </div>

                    <div class="form-group">

                    </div>

                </div>

            </div>

            @<p class="">Already a member?  @Html.ActionLink("Login", "Login", "Auth") Instead</p>
            @<Button type="submit" Class="btn btn-lg btn-dark btn-med">Register</Button>

            @<p Class="form-heading mt-5 mb-3 text-muted">&copy; @DateTime.Now.Year City Public Libary</p>
        End Using
    </div>
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/Scripts/misc.js")
</body>
</html>
