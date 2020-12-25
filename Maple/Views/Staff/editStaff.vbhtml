@ModelType Maple.StaffModel
@Code
    ViewData("Title") = "editStaff"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Edit Staff</h1>

    <div class="btn-toolbar mb-2 mb-md-0">

    </div>
</div>

<div class="alert alert-danger" id="reserror" role="alert" hidden>
    No Errors here :/
</div>



@If Not IsNothing(TempData(Constants.IDENT_QUERY_ERROR)) AndAlso TempData(Constants.IDENT_QUERY_ERROR) <> "" Then
    @<div id="alert-bignay" Class="alert alert-danger" role="alert">
        <h4 class="alert-heading">There were errors adding the books</h4>
        <p>@TempData(Constants.IDENT_QUERY_ERROR)</p>
        <hr>
        <p class="mb-0">Please Contact Support to resolve this error</p>
    </div>
    @TempData.Remove(Constants.IDENT_QUERY_ERROR)
End If


@If Not IsNothing(TempData(Constants.IDENT_REQ_SUCCESS)) AndAlso TempData(Constants.IDENT_REQ_SUCCESS) Then
    @<div id="alert-yay" Class="alert alert-success" role="alert">
        Record Updated
    </div>
    @TempData.Remove(Constants.IDENT_REQ_SUCCESS)
End If


@Using (Html.BeginForm("updateauthor", "Staff", Nothing, FormMethod.Post))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">

    @Html.HiddenFor(Function(model) model.staffid)

    <div class="form-group">
        @Html.LabelFor(Function(model) model.firstname, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.firstname, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.firstname, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.lastname, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.lastname, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.lastname, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.email, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.email, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.email, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.contact, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.contact, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.contact, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.role, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.DropDownListFor(Function(model) model.selectedRole, Model.RoleList, "-- Role --", New With {.id = "role", .Class = "form-control"})
            @Html.ValidationMessageFor(Function(model) model.role, "", New With {.class = "text-danger"})
        </div>
    </div>

</div>

    @<div Class="form-group p-2">
        <div Class="row" style="float:right;">
            <input type="button" value="Cancel Edits" style="min-width:3rem;" Class="btn btn-outline-danger" onclick="location.href='@Url.Action("viewstaff", "Staff")'" />
            <input type="submit" value="Save Changes" style="min-width:7rem;" Class="btn btn-success ml-2" />
        </div>

    </div>
End Using


