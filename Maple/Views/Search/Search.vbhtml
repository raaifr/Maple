@ModelType Maple.SearchModel
@Code
    ViewData("Title") = "Search"
    Dim role = -1
    If Not IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
        role = Session(Constants.IDENT_STAFF_ROLE)
    End If
End Code

@Styles.Render("~/Content/searchpage.css")
@Styles.Render("~/Content/themectrls.css")

<div class="container-fluid mt-3 mb-5">
    @Using (Html.BeginForm("Search", "Search", Nothing, FormMethod.Post, New With {.class = "form-inline", .style = "margin-right:40px;"}))
        @Html.AntiForgeryToken()

        @<div Class="input-group">
            @Html.DropDownListFor(Function(model) model.selectedCategory, Model.CategoryList, "-- Search Category --", New With {.Class = "form-control"})

            @Html.EditorFor(Function(model) model.SearchTerm, New With {.htmlAttributes = New With {.class = "form-control", .id = "SearchTerm", .style = "min-width:30rem;", .placeholder = Html.DisplayNameFor(Function(model) model.SearchTerm)}})
            <div Class="input-group-append">
                <Button id="btnSearch" Class="btn btn-primary" type="submit">Search</Button>
            </div>
        </div>
    End Using

</div>

<div id="reserror" class="alert alert-danger" role="alert" hidden>
    No Errors here
</div>

<table class="table table-striped">
    <tbody>

        @For Each item As BookModel In Model.Book
            @<tr>
                <td>
                    <div>
                        @Html.DisplayFor(Function(modelItem) item.series_title)
                    </div>
                    <div class="h3">
                        @Html.DisplayFor(Function(modelItem) item.book_title)
                    </div>
                    <div class="text-muted">
                        @Html.DisplayFor(Function(modelItem) item.publisher) - @Html.DisplayFor(Function(modelItem) item.year)
                    </div>
                    <div>
                        @For Each itm As String In item.authors
                            @<a class="mr-1" href="#">@Html.DisplayFor(Function(modelItem) itm)</a>
                        Next
                    </div>
                </td>
                @If IsNothing(Session(Constants.IDENT_STAFF_ROLE)) Then
                    @If item.borrowed < item.stock Then
                        @<td Class="align-middle text-success">
                            <h4>Available</h4>
                        </td>
                        @<td Class="align-middle">
                            <div class="container-fluid m-1 text-center">
                                @Html.Hidden("bookreserve", Url.Action("Reserve", "User"))
                                <input type="button" id="srch_btn_reserve_@item.id" value="Reserve" style="min-width: 11rem;" Class="btn btn-info" />
                            </div>
                        </td>

                    Else
                        @<td Class="align-middle text-danger">
                            <h4>Not Available</h4>
                        </td>
                        @<td Class="align-middle">
                            <div class="container-fluid m-1 text-center">
                                <button type="button" style="min-width:11rem;" Class="btn btn-outline-info" disabled>N/A</button>
                            </div>
                        </td>

                    End If
                end if
                @If role > 0 Then
                    @<td Class="align-middle">
                        <button type="submit" name="btnedit" id="book_edit_@item.id" Class="btn btn-sm btn-outline-secondary mb-2">
                            <span> <i Class="fa fa-pencil"></i> </span>
                        </button>
                        @If role >= 500 And role < 600 Or role >= 2000 Then
                            @<Button type="button" name="btndelete" id="book_delete_@item.id" Class="btn btn-sm btn-outline-danger mb-2">
                                <span> <i Class="fa fa-trash"></i> </span>
                            </Button>
                        End If
                    </td>
                End If


            </tr>

        Next

    </tbody>
</table>



