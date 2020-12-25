@ModelType Maple.SearchModel
@Code
    ViewData("Title") = "Find Book"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code

@Html.Hidden("searchbook", Url.Action("searchbook", "Staff"))
@Html.Hidden("deletbook", Url.Action("deletebook", "Staff"))

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Find Book</h1>

    <div class="btn-toolbar mb-2 mb-md-0">
        @Using (Html.BeginForm("findbook", "Staff", Nothing, FormMethod.Post, New With {.class = "form-inline"}))
            @<div class="input-group mb-2">
                @Html.EditorFor(Function(model) model.SearchTerm, New With {.htmlAttributes = New With {.class = "form-control", .id = "booksearchbox", .style = "min-width:25rem;", .placeholder = Html.DisplayNameFor(Function(model) model.SearchTerm)}})
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
                    <th>Series Title</th>
                    <th>Book Title</th>
                    <th>Author(s)</th>
                    <th>Publisher</th>
                    <th>ISBN</th>
                    <th>DDC</th>
                    <th>Tags</th>
                    <th>Year</th>
                    <th>Stock</th>
                    <th>Shelf</th>
                    <th> </th>
                </tr>
            </thead>
            <tbody>
                @For Each book As BookModel In Model.Book

                    @<tr>
                        @If book.series_title = "" Then
                            @<td>N/A</td>
                        Else
                            @<td>@book.series_title</td>
                        End If
                        <td>@book.book_title</td>
                        <td>@book.authorstring</td>
                        <td>@book.publisher </td>
                        <td>@book.isbn</td>
                        <td>@book.ddc</td>
                        <td>@book.tags</td>
                        <td>@book.year</td>
                        <td>@book.stock</td>
                        <td>@book.shelf</td>
                        <td>
                            @Using (Html.BeginForm("editbook", "Staff", New With {.id = book.id}, FormMethod.Get, New With {.class = "form-inline"}))
                                @<button type="submit" Class="btn btn-sm btn-outline-secondary mx-1 mb-2">
                                    <span> <i Class="fa fa-pencil"></i> </span>
                                </button>
                            End Using
                            @If role >= 500 And role < 600 Or role >= 2000 Then
                                @<Button type="button" name="btndelete" id="book_delete_@book.id" Class="btn btn-sm btn-outline-danger mx-1 mb-2">
                                    <span> <i Class="fa fa-trash"></i> </span>
                                </Button>
                            End If
                        </td>
                    </tr>

                Next

            </tbody>

        </Table>

    </div>
End If


