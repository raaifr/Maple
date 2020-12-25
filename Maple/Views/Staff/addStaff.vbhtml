﻿@ModelType Maple.StaffModel
@Code
    ViewData("Title") = "addStaff"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
End Code

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Add Staff</h1>

    <small><a href="#" onclick='fillsamplestaff();' id="fillsample" hidden>Fill Sample Staff</a></small>

</div>

<div Class="alert alert-info" role="alert">
    <h4 class="alert-heading"><span><i class="fa fa-exclamation-circle  mr-2 pr-1"></i>Important!</span></h4>
    <p class="mb-0">Staff ID Card data is Auto Generated by the Application</p>
    <p class="mb-0">Default Password will be assigned to each staff - <strong>WelcomeStaff@890</strong></p>
    <hr>
    <p class="mb-0">Please make sure staff is informed accordingly</p>
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
        Book Added
    </div>
    @TempData.Remove(Constants.IDENT_REQ_SUCCESS)
End If




@Using (Html.BeginForm("addStaff", "Staff", Nothing, FormMethod.Post))
    @Html.AntiForgeryToken()


    @<div class="row p-2">
        <div Class="col p-2" style="">

            <div class="row p-2 m-2">
                <div class="form-group mr-4">
                    @Html.LabelFor(Function(model) model.firstname, htmlAttributes:=New With {.class = "control-label "})
                    <div class=" ">
                        @Html.EditorFor(Function(model) model.firstname, New With {.htmlAttributes = New With {.id = "firstname", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.firstname, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group mr-4">
                    @Html.LabelFor(Function(model) model.lastname, htmlAttributes:=New With {.class = "control-label "})
                    <div class=" ">
                        @Html.EditorFor(Function(model) model.lastname, New With {.htmlAttributes = New With {.id = "lastname", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.lastname, "", New With {.class = "text-danger"})
                    </div>
                </div>

            </div>

            <div class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.email, htmlAttributes:=New With {.class = "control-label"})
                <div class=" ">
                    @Html.EditorFor(Function(model) model.email, New With {.htmlAttributes = New With {.id = "email", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.email, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="row p-2 m-2">
                <div class="form-group mr-4">
                    @Html.LabelFor(Function(model) model.dob, htmlAttributes:=New With {.class = "control-label "})
                    <div class=" ">
                        @Html.EditorFor(Function(model) model.dob, New With {.htmlAttributes = New With {.id = "dob", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.dob, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group ml-4">
                    @Html.LabelFor(Function(model) model.contact, htmlAttributes:=New With {.class = "control-label "})
                    <div class=" ">
                        @Html.EditorFor(Function(model) model.contact, New With {.htmlAttributes = New With {.id = "contact", .class = "form-control col"}})
                        @Html.ValidationMessageFor(Function(model) model.contact, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>

            <div class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.employdate, htmlAttributes:=New With {.class = "control-label "})
                <div class=" ">
                    @Html.EditorFor(Function(model) model.employdate, New With {.htmlAttributes = New With {.id = "empdate", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.employdate, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="row p-2 m-2">
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.role, htmlAttributes:=New With {.class = "control-label "})
                    <div class=" ">
                        @Html.DropDownListFor(Function(model) model.selectedRole, Model.RoleList, "-- Role --", New With {.id = "role", .Class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.role, "", New With {.class = "text-danger"})
                    </div>

                </div>
            </div>


        </div>

        <div Class="col p-2" style="">

        </div>

    </div>


    @<div Class="form-group p-2">
        <div Class="col-md-offset-2 col-md-10">
            <input type="submit" value="Create" Class="btn btn-outline-secondary btn-block" />
        </div>
    </div>



End Using

