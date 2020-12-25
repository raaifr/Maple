@ModelType Maple.SearchModel
@Code
    ViewData("Title") = "Card Issue"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"

    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)

    Dim user As UserModel = Nothing
    Dim showdetails As Boolean = False
    If Not IsNothing(Model) AndAlso Not Model.user.Count = 0 Then
        user = Model.user(0)
        showdetails = True
    Else
        showdetails = False
    End If
End Code



<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Member Card Issue</h1>

    <div class="btn-toolbar mb-2 mb-md-0">
        @Using (Html.BeginForm("membercard", "Staff", Nothing, FormMethod.Post, New With {.class = "form-inline"}))
            @<div class="input-group mb-2">
                @Html.EditorFor(Function(model) model.SearchTerm, New With {.htmlAttributes = New With {.class = "form-control", .id = "membersearchbox", .style = "min-width:25rem;", .placeholder = "Enter Member NIC Number"}})
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary" id="btnsearch" type="submit"><i class="fa fa-search"></i></button>
                </div>
            </div>
        End Using
    </div>
</div>

<div id="banner1" class="alert alert-danger alert-dismissible fade show" role="alert" hidden>
    No Errors here
    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
        <span aria-hidden="true">&times;</span>
    </button>
</div>

@If IsNothing(Model) Then
    @<div Class="alert alert-info" id="reserror" role="alert" hidden>
        Search member by their National ID card number to display their membership info here
    </div>
ElseIf Model.user.Count = 0 Then
    @<div Class="alert alert-danger" id="reserror" role="alert" hidden>
        The User Does not Exist
    </div>
Else
    @<div class="row">
        <div Class="col-3 p-2 mr-5 text-center" style="width: 40%;">
            <div>
                @If Not IsNothing(ViewBag.QRCodeImage) Then
                    @<img src="@ViewBag.QRCodeImage" style="height:200px;width:200px" />
                End If

            </div>

            <div>
                <h6 class="text-muted"><strong>Membership Status</strong></h6>
                @If Not IsNothing(ViewBag.membershipstat) Then
                    If ViewBag.membershipstat = True Then
                        @<h1 class="text-success text-center align-content-center" style="background-color: #94e6a7">Active</h1>
                        @<input type = "button" id="writeqr" Class="btn btn-outline-secondary btn-block " value="Write Card" />
                    Else
                        @<h1 class="text-danger text-center align-content-center" style="background-color: #f6c0c6">Expired</h1>
                        @<input type="button" id="writeqr" class="btn btn-outline-secondary btn-block" value="Write Card" disabled />
                    End If
                Else
                    @<h1 class="text-secondary text-center align-content-center" style="background-color: #d3d3d3">Unknown</h1>
                    @<input type = "button" id="writeqr" Class="btn btn-outline-secondary btn-block" value="Write Card" disabled />
                End If
                
            </div>
        </div>

        <div Class="col-3 p-2 pt-5" style="width: 30%;">
            <dl>
                <div class="row">
                    <dt class="pr-3">@Html.DisplayNameFor(Function(model) user.userid)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.userid)</dd>
                </div>

                <div class="row">
                    <dt class="pr-3">Name</dt>
                    <dd>@Html.DisplayFor(Function(model) user.firstname) @Html.DisplayFor(Function(model) user.lastname)</dd>
                </div>

                <div class="row">
                    <dt class="pr-3">@Html.DisplayNameFor(Function(model) user.contact)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.contact)</dd>
                </div>

                <div class="row">
                    <dt class="pr-3">@Html.DisplayNameFor(Function(model) user.email)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.email)</dd>
                </div>
                <a href="#">Send QR Code to email</a>


            </dl>

        </div>


        <div Class="col-3 p-2 pt-5" style="width: 30%;">
            <dl>
                <div Class="row">
                    <dt Class="pr-3">@Html.DisplayNameFor(Function(model) user.country)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.country)</dd>
                </div>

                <div class="row">
                    <dt class="pr-3">@Html.DisplayNameFor(Function(model) user.nid)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.nid)</dd>
                </div>

                <div class="row">
                    <dt class="pr-3">@Html.DisplayNameFor(Function(model) user.dob)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.dob)</dd>
                </div>

                <div class="row">
                    <dt class="pr-3">@Html.DisplayNameFor(Function(model) user.lastpayment)</dt>
                    <dd>@Html.DisplayFor(Function(model) user.lastpayment)</dd>
                </div>


                @If False Then 'role > 200 Then
                    @<div Class="form-check small">
                        <input type="checkbox" Class="form-check-input" id="resetpass">
                        <Label Class="form-check-label" for="resetpass">Reset User Password</Label>
                    </div>
                End If

            </dl>

        </div>

    </div>

End If

