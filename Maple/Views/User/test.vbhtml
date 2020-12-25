@ModelType IEnumerable(Of Maple.BookModel)
@Code
ViewData("Title") = "test"
Layout = "~/Views/Shared/_LayoutUserDash.vbhtml"
End Code

<h2>test</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.series_title)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.book_title)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.publisher)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.isbn)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ddc)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.tags)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.year)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.stock)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.shelf)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.borrowed)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.reserved)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.series_title)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.book_title)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.publisher)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.isbn)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ddc)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.tags)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.year)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.stock)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.shelf)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.borrowed)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.reserved)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.id }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.id }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.id })
        </td>
    </tr>
Next

</table>
