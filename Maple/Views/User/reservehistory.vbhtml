@ModelType IEnumerable(Of Maple.BookModel)
@Code
    ViewData("Title") = "Reservation History"
    Layout = "~/Views/Shared/_LayoutUserDash.vbhtml"
    Dim counter As Integer = 0
End Code


<main role="main" >
    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2">
        <h1 class="h2">Reservation History</h1>
        <div class="btn-toolbar mb-2 mb-md-0">
            <button type="button" class="btn btn-sm btn-outline-secondary dropdown-toggle">
                <i class="fa fa-calendar"></i>
                This week
            </button>
        </div>
    </div>

 
    <div class="table-responsive">
        <table class="table table-striped table-sm">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Book Name</th>
                    <th>Author</th>
                    <th>Date Reserved</th>
                    <th>Active Reservation</th>
                </tr>
            </thead>
            <tbody>
                @For Each book In Model
                    counter += 1
                    @<tr>
                        <td>@counter</td>
                        @If book.series_title = "" Then
                            @<td>@Html.DisplayFor(Function(modelItem) book.book_title) </td>
                        Else
                            @<td>@Html.DisplayFor(Function(modelItem) book.series_title) - @Html.DisplayFor(Function(modelItem) book.book_title) </td>
                        End If
                        
                        <td>@Html.DisplayFor(Function(modelItem) book.authorstring)</td>
                        <td>@Html.DisplayFor(Function(modelItem) book.reservedate)</td>

                        @If Date.Today.Subtract(book.reservedate).Days > 14 Then
                            @<td class="text-center text-danger">No </td>
                        Else
                            @<td class="text-center text-success">Yes </td>
                        End If

                    </tr>
                Next
            </tbody>
        </table>

    </div>
</main>


