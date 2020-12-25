@ModelType SearchModel
@Code
    ViewData("Title") = "Author"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"

    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code

@Html.Hidden("deletauthor", Url.Action("deletauthor", "Staff"))

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1 class="h2">Find Author</h1>

    <div class="btn-toolbar mb-2 mb-md-0">
        @Using (Html.BeginForm("viewauthor", "Staff", Nothing, FormMethod.Post, New With {.class = "form-inline"}))
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

@If IsNothing(Model) OrElse IsNothing(Model.author) OrElse Model.author.Count = 0 Then
    @<div Class="alert alert-info" role="alert">
        Search did not yeild any results
    </div>
Else

    @<table class="table table-striped">
        <tbody>

            @For Each author As AuthorModel In Model.author
                @<tr>
                    <td>
                        <div>
                            @Html.DisplayFor(Function(modelItem) author.firstname)
                            @Html.DisplayFor(Function(modelItem) author.middlename)
                        </div>
                        <div class="h3">
                            @Html.DisplayFor(Function(modelItem) author.lastname)
                        </div>
                    </td>

                    @If role > 500 Then
                        @<td Class="align-middle">

                            @Using (Html.BeginForm("editauthor", "Staff", New With {.id = author.id}, FormMethod.Get, New With {.class = ""}))
                                @<button type="submit" Class="btn btn-sm btn-outline-secondary mx-1 mb-2">
                                    <span> <i Class="fa fa-pencil"></i> </span>
                                </button>
                            End Using

                            @If role >= 500 And role < 600 Or role >= 2000 Then
                                @<Button type="button" name="btndelete" id="author_delete_@author.id" Class="btn btn-sm btn-outline-danger mb-2">
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