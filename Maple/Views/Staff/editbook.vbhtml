@ModelType Maple.BookModel
@Code
    ViewData("Title") = "addbook"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code

@Styles.Render("~/Content/jquery-ui.css")

@Html.Hidden("acauthor", Url.Action("reqauthorlist", "Staff"))
@Html.Hidden("acshelf", Url.Action("reqshelflist", "Staff"))
@Html.Hidden("acpublisher", Url.Action("reqpublisherlist", "Staff"))
@Html.Hidden("acseriestitle", Url.Action("reqserieslist", "Staff"))

@Html.Hidden("newpublisher", Url.Action("newpublisher", "Staff"))
@Html.Hidden("newauthor", Url.Action("newauthor", "Staff"))

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Edit Book</h1>

    <div class="btn-toolbar mb-2 mb-md-0">

    </div>

</div>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">

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

@Using (Html.BeginForm("updatebook", "Staff", Nothing, FormMethod.Post))
    @Html.AntiForgeryToken()


    @<div class="row p-2">
        <div Class="col p-2" style="">
            @Html.HiddenFor(Function(model) model.id)

            <div Class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.series_title, htmlAttributes:=New With {.class = "control-label"})
                <div Class="">
                    @Html.EditorFor(Function(model) model.series_title, New With {.htmlAttributes = New With {.id = "seriestitle", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.series_title, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div Class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.book_title, htmlAttributes:=New With {.class = "control-label"})
                <div Class="">
                    @Html.EditorFor(Function(model) model.book_title, New With {.htmlAttributes = New With {.id = "booktitle", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.book_title, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div Class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.publisher, htmlAttributes:=New With {.class = "control-label"})
                <small>
                    <a href="#newpublishermodal" data-toggle="modal" style="float:right;">New Publisher</a>
                </small>
                <div Class="">
                    @Html.EditorFor(Function(model) model.publisher, New With {.htmlAttributes = New With {.id = "publisher", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.publisher, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div Class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.input_author, htmlAttributes:=New With {.class = "control-label"})
                <small>
                    <a href="#newauthormodal" data-toggle="modal" style="float:right;">New Author</a>
                </small>
                <div Class="">
                    @Html.EditorFor(Function(model) model.input_author, New With {.htmlAttributes = New With {.id = "authors", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.input_author, "", New With {.class = "text-danger"})
                </div>
            </div>

        </div>

        <div Class="col p-2" style="">

            <div Class="row p-2 m-2">
                <div Class="form-group mr-4">
                    @Html.LabelFor(Function(model) model.year, htmlAttributes:=New With {.class = "control-label"})
                    <div Class="">
                        @Html.EditorFor(Function(model) model.year, New With {.htmlAttributes = New With {.id = "year", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.year, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group ml-4">
                    @Html.LabelFor(Function(model) model.ddc, htmlAttributes:=New With {.class = "control-label"})
                    <div Class="">
                        @Html.EditorFor(Function(model) model.ddc, New With {.htmlAttributes = New With {.id = "ddc", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.ddc, "", New With {.class = "text-danger"})
                    </div>
                </div>

            </div>

            <div Class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.isbn, htmlAttributes:=New With {.class = "control-label"})
                <div Class="">
                    @Html.EditorFor(Function(model) model.isbn, New With {.htmlAttributes = New With {.id = "isbn", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.isbn, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div Class="form-group p-2 m-2">
                @Html.LabelFor(Function(model) model.tags, htmlAttributes:=New With {.class = "control-label"})
                <div Class="">
                    @Html.EditorFor(Function(model) model.tags, New With {.htmlAttributes = New With {.id = "tags", .class = "form-control col"}})
                    @Html.ValidationMessageFor(Function(model) model.tags, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div Class="row p-2 m-2">
                <div Class="form-group mr-4">
                    @Html.LabelFor(Function(model) model.stock, htmlAttributes:=New With {.class = "control-label"})
                    <div Class="">
                        @Html.EditorFor(Function(model) model.stock, New With {.htmlAttributes = New With {.id = "stock", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.stock, "", New With {.class = "text-danger"})
                    </div>
                </div>
                <div Class="form-group ml-4">
                    @Html.LabelFor(Function(model) model.shelf, htmlAttributes:=New With {.class = "control-label"})
                    <div Class="">
                        @Html.EditorFor(Function(model) model.shelf, New With {.htmlAttributes = New With {.id = "shelf", .class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.shelf, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>


        </div>
    </div>

    @<div Class="form-group p-2">
        <div Class="row" style="float:right;">
            <input type="button" value="Cancel Edits" style="min-width:3rem;" Class="btn btn-outline-danger" onclick="location.href='@Url.Action("findbook", "Staff")'" />
            <input type="submit" value="Save Changes" style="min-width:7rem;" Class="btn btn-success ml-2" />
        </div>

    </div>



End Using


<!-- New Publisher Modal -->
<div Class="modal fade" id="newpublishermodal" data-backdrop="static" data-keyboard="true" tabindex="-1">
    <div Class="modal-dialog">
        <div Class="modal-content">
            <div Class="modal-header">
                <h5 Class="modal-title" id="newauthorheading">New Publisher</h5>
                <Button type="button" Class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </Button>
            </div>
            <div Class="modal-body">
                <form id="newpubform">
                    <div class="form-group">
                        <label for="publisher-name" class="col-form-label">Publisher Name</label>
                        <input type="text" class="form-control col" id="publisher-name" required>
                    </div>
                    <div Class="form-group">
                        <Label for="publisher-address" class="col-form-label">Publisher Address</Label>
                        <input type="text" Class="form-control col" id="publisher-address">
                    </div>
                </form>
            </div>
            <div Class="modal-footer">
                <Button type="button" Class="btn btn-secondary" data-dismiss="modal">Close</Button>
                <Button type="submit" Class="btn btn-info odom-submit" id="createnewpublisher" data-dismiss="modal">Create</Button>
            </div>
        </div>
    </div>
</div>

<!-- New Author Modal -->
<div Class="modal fade" id="newauthormodal" data-backdrop="static" data-keyboard="true" tabindex="-1">
    <div Class="modal-dialog">
        <div Class="modal-content">
            <div Class="modal-header">
                <h5 Class="modal-title" id="newauthorheading">New Author</h5>
                <Button type="button" Class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </Button>
            </div>
            <div Class="modal-body">
                <form id="newauthorform">
                    <div class="form-group">
                        <label for="first-name" class="col-form-label">First Name</label>
                        <input type="text" class="form-control col" id="first-name" required>
                    </div>
                    <div Class="form-group">
                        <Label for="middle-name" class="col-form-label">Middle Name</Label>
                        <input type="text" Class="form-control col" id="middle-name">
                    </div>
                    <div Class="form-group">
                        <Label for="last-name" class="col-form-label">Last Name</Label>
                        <input type="text" Class="form-control col" id="last-name">
                    </div>
                </form>
            </div>
            <div Class="modal-footer">
                <Button type="button" Class="btn btn-secondary" data-dismiss="modal">Close</Button>
                <Button type="submit" Class="btn btn-info odom-submit2 " id="createnewauthor" data-dismiss="modal">Create</Button>
            </div>
        </div>
    </div>
</div>


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/Scripts/jquery-3.5.1.js")
    @Scripts.Render("~/Scripts/jquery-ui.js")
    <!--script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script -->
    @Scripts.Render("~/Scripts/dash.js")
End Section
