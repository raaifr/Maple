@ModelType Maple.SearchModel
@Code
    ViewData("Title") = "Delete Book"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"

    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code



<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Delete Member</h1>

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




