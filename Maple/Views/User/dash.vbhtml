@Code
    ViewData("Title") = "dash"
    Layout = "~/Views/Shared/_LayoutUserDash.vbhtml"
End Code


<main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h1 class="h2">Dashboard</h1>
        <div class="btn-toolbar mb-2 mb-md-0">
            <button type="button" class="btn btn-sm btn-outline-secondary dropdown-toggle">
                <i class="fa fa-calendar"></i>
                This week
            </button>
        </div>
    </div>

    <canvas class="my-4 w-100" id="myChart" width="900" height="380"></canvas>

    <h2>Section title</h2>
    <div class="table-responsive">
        <table class="table table-striped table-sm">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Book Name</th>
                    <th>Author</th>
                    <th>Date Reserved</th>
                    <th>Is Borrowed</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1215</td>
                    <td>Artemis Fowl</td>
                    <td>Owen Colfer</td>
                    <td>12/12/2019</td>
                    <td>No</td>
                </tr>
            </tbody>
        </table>

    </div>
</main>

