@ModelType SearchModel
@Code
    ViewData("Title") = "Members"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code


@Html.Hidden("searchmember", Url.Action("viewMember", "Staff"))
@Html.Hidden("editmember", Url.Action("editmember", "Staff"))
@Html.Hidden("deletemember", Url.Action("deletemember", "Staff"))

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">View Member</h1>

    <div class="btn-toolbar mb-2 mb-md-0">
        @Using (Html.BeginForm("viewMember", "Staff", Nothing, FormMethod.Post, New With {.class = "form-inline"}))
            @<div class="input-group mb-2">
                @Html.EditorFor(Function(model) model.SearchTerm, New With {.htmlAttributes = New With {.class = "form-control", .id = "membersearchbox", .style = "min-width:25rem;", .placeholder = Html.DisplayNameFor(Function(model) model.SearchTerm)}})
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




@If IsNothing(Model) Then
    @<div Class="alert alert-info" role="alert">
        Search did not yeild any results
    </div>
Else
    @<div Class="table-responsive">
        <Table Class="table table-striped table-sm">
            <thead>
                <tr>
                    <th>Member Name</th>
                    <th>Contact</th>
                    <th>Email</th>
                    <th>Country</th>
                    <th>NIC</th>
                    <th>DOB</th>
                    <th> </th>
                </tr>
            </thead>
            <tbody>
                @For Each user As UserModel In Model.user

                    @<tr>
                        <td>@user.firstname  @user.lastname </td>
                        <td>@user.contact</td>
                        <td>@user.email</td>
                        <td>@user.country</td>
                        <td>@user.nid</td>
                        <td>@user.dob.ToShortDateString</td>
                        <td>
                            <button type="submit" name="btnedit" id="member_edit_@user.userid" Class="btn btn-sm btn-outline-secondary mx-1 mb-2">
                                <span> <i Class="fa fa-pencil"></i> </span>
                            </button>

                            <Button type="button" name="btndelete" id="member_delete_@user.userid" Class="btn btn-sm btn-outline-danger mx-1 mb-2">
                                <span> <i Class="fa fa-trash"></i> </span>
                            </Button>
                        </td>
                    </tr>

                Next

            </tbody>

        </Table>

    </div>
End If


<div id="diveditmember" hidden>
    <input type="text" placeholder="name" />

</div>