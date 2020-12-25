@ModelType IEnumerable(Of Maple.BookModel)
@Code
    ViewData("Title") = "Borrow History"
    Layout = "~/Views/Shared/_LayoutUserDash.vbhtml"
    Dim counter As Integer = 0
End Code


<main role="main">
    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2">
        <h1 class="h2">Borrow History</h1>
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
                    <th>Date Borrowed</th>
                    <th>Date Returned</th>
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
                        <td>@Html.DisplayFor(Function(modelItem) book.borrowdate)</td>

                        @If IsNothing(book.returndate) Or book.returndate.Year = 1 Then
                            @If Math.Abs(Date.Today.Subtract(book.borrowdate).Days) > book.due Then
                                @<td class="text-center text-danger font-weight-bold">OVER DUE</td>
                            Else
                                @<td class="text-center text-info">DUE: @book.borrowdate.AddDays(14).ToString("dddd, MMMM dd")</td>
                            End If
                        Else
                            @If Math.Abs(book.returndate.Subtract(book.borrowdate).Days) > book.due Then
                                @<td class="text-center text-danger">@Html.DisplayFor(Function(modelItem) book.returndate)</td>
                            Else
                                @<td class="text-center">@Html.DisplayFor(Function(modelItem) book.returndate)</td>
                            End If
                        End If


                    </tr>
                Next
            </tbody>
        </table>

    </div>
</main>


