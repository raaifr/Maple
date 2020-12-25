@ModelType Maple.SearchModel
@Code
    ViewData("Title") = "View Staff"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code

@Html.Hidden("deletestaff", Url.Action("deletstaff", "Staff"))


<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Find Staff</h1>

    <div class="btn-toolbar mb-2 mb-md-0">
        @Using (Html.BeginForm("viewstaff", "Staff", Nothing, FormMethod.Post, New With {.class = "form-inline"}))
            @<div class="input-group mb-2">
                @Html.EditorFor(Function(model) model.SearchTerm, New With {.htmlAttributes = New With {.class = "form-control", .style = "min-width:25rem;", .placeholder = Html.DisplayNameFor(Function(model) model.SearchTerm)}})
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary" id="btnsearch" type="submit"><i class="fa fa-search"></i></button>
                </div>
            </div>
        End Using
    </div>
</div>

<div class="alert alert-danger" id="reserror" role="alert" hidden>
    No Errors here :/
</div>



@If IsNothing(Model) OrElse IsNothing(Model.staff) OrElse Model.staff.Count = 0 Then
    @<div Class="alert alert-info" role="alert">
        Search did not yeild any results
    </div>
Else

    @<table class="table table-striped">
        <tbody>

            @For Each staff As StaffModel In Model.staff
                @<tr>
                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) staff.firstname)
                            @Html.DisplayFor(Function(modelItem) staff.lastname)
                        </div>
                    </td>

                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) staff.email)
                        </div>
                    </td>

                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) staff.dob)
                        </div>
                    </td>
                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) staff.contact)
                        </div>
                    </td>
                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) staff.employdate)
                        </div>
                    </td>
                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) staff.role)
                        </div>
                    </td>


                    @If role > 500 Then
                        @<td Class="align-middle">

                            @Using (Html.BeginForm("editStaff", "Staff", New With {.id = staff.staffid}, FormMethod.Get, New With {.class = ""}))
                                @<button type="submit" Class="btn btn-sm btn-outline-secondary mx-1 mb-2">
                                    <span> <i Class="fa fa-pencil"></i> </span>
                                </button>
                            End Using

                            @If role >= 500 And role < 600 Or role >= 2000 Then
                                @<Button type="button" name="btndelete" id="staff_delete_@staff.staffid" Class="btn btn-sm btn-outline-danger mb-2">
                                    <span> <i Class="fa fa-trash"></i> </span>
                                </Button>
                            End If
                        </td>
                    End If


                </tr>

            Next

        </tbody>
    </table>


End if

