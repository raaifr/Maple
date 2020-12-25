@ModelType Maple.ProfileModel
@Code
    ViewData("Title") = "Profile"
    Dim dobirth As Date = DateTime.Parse(Model.dob)
    Dim age As Integer = DateTime.Now.Year - dobirth.Year
End Code

@If Not IsNothing(TempData(Constants.IDENT_REQ_SUCCESS)) And TempData(Constants.IDENT_REQ_SUCCESS) = True Then
    @<div class="alert alert-success" role="alert">
        Information Updated Successfully
    </div>
    TempData(Constants.IDENT_REQ_SUCCESS) = Nothing
ElseIf Not IsNothing(TempData(Constants.IDENT_REQ_FAIL_ONE)) And TempData(Constants.IDENT_REQ_FAIL_ONE) = True Then
    If Not IsNothing(TempData(Constants.IDENT_REQ_FAIL_REASON)) Then
        @<div Class="alert alert-danger" role="alert">
            Error updating your information. Invalid Password
        </div>
    Else
        @<div Class="alert alert-danger" role="alert">
            There were errors updating your information - @TempData(Constants.IDENT_REQ_FAIL_REASON)
        </div>
    End If
    TempData(Constants.IDENT_REQ_FAIL_REASON) = Nothing
    TempData(Constants.IDENT_REQ_FAIL_ONE) = Nothing
ElseIf Not IsNothing(TempData(Constants.IDENT_USER_QR)) And TempData(Constants.IDENT_USER_QR) = True Then
    @<div Class="alert alert-success" role="alert">
        QR Code Sent to registered email address
    </div>
    TempData(Constants.IDENT_REQ_FAIL_ONE) = Nothing
End If


<div class="row">


    <div class="col-2 p-3" style="width:40%">
        @If Not IsNothing(ViewBag.QRCodeImage) Then
            @<img src="@ViewBag.QRCodeImage" alt="" style="height:200px;width:200px" />
        End If
        <p>Send QR via @Html.ActionLink("Email", "sendqr", "User")</p>

    </div>
    <div class="col p-2" style="width:60%">
        <div>
            <h1> @Html.DisplayFor(Function(mode) Model.firstname, New With {.class = ""}) @Html.DisplayFor(Function(mode) Model.lastname, New With {.class = "text-light"}) </h1>
            <p>Makes @age years on @dobirth.Month @dobirth.Day this year. (Born .@dobirth.Year) </p>
        </div>

        <p>Fill in <strong>Only</strong> what you need to update</p>
        <div Class="container-fluid ">
            @Using (Html.BeginForm("UpdateInfo", "User", Nothing, FormMethod.Post, New With {.class = "form-inline mb-2"}))
                @Html.AntiForgeryToken()
                @<div Class="col-md-6">
                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.contact, New With {.htmlAttributes = New With {.class = "form-control mb-3", .style = "min-width:20rem;", .Value = Html.ValueFor(Function(model) model.contact), .placeholder = Html.DisplayNameFor(Function(model) model.contact)}})
                        @Html.ValidationMessageFor(Function(model) model.contact, "", New With {.class = "text-danger"})
                    </div>

                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.email, New With {.htmlAttributes = New With {.Class = "form-control mb-3", .style = "min-width:20rem;", .Value = Html.ValueFor(Function(model) model.email), .placeholder = Html.DisplayNameFor(Function(model) model.email)}})
                        @Html.ValidationMessageFor(Function(model) model.email, "", New With {.class = "text-danger"})
                    </div>

                    <div Class="form-group">
                        @Html.EditorFor(Function(model) model.old_password, New With {.htmlAttributes = New With {.class = "form-control mb-3", .style = "min-width:20rem;", .placeholder = Html.DisplayNameFor(Function(model) model.Password)}})
                        @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
                    </div>

                    <div Class="form-group">
                        @Html.EditorFor(Function(model) model.Password, New With {.htmlAttributes = New With {.class = "form-control mb-3", .style = "min-width:20rem;", .placeholder = Html.DisplayNameFor(Function(model) model.Password)}})
                        @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
                    </div>

                    <div Class="form-group">
                        @Html.EditorFor(Function(model) model.ConfirmPassword, New With {.htmlAttributes = New With {.class = "form-control mb-3", .style = "min-width:20rem;", .placeholder = Html.DisplayNameFor(Function(model) model.ConfirmPassword)}})
                        @Html.ValidationMessageFor(Function(model) model.ConfirmPassword, "", New With {.class = "text-danger"})
                    </div>



                    <Button type="submit" Class="btn btn-lg btn-dark mt-1">Update Info</Button>
                </div>


            End Using
        </div>




    </div>
</div>

