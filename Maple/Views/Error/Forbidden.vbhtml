@Code
    ViewData("Title") = "Forbidden"
    Layout = "~/Views/Shared/_LayoutError.vbhtml"
End Code

<div class="container">
    <div class="row">
        <div class="col-md-6 align-self-center">
            <div class="container-fluid">
                <img src="~/Content/img/403.png" style="width:117%;" />
            </div>
        </div>
        <div class="col-md-6 align-self-center">
            <h1>403</h1>
            <h2>Someone's into mischief</h2>
            <p>
                You do not have sufficient permissions to access that page.
                You can click the button below to go back to the homepage.
            </p>
            <br />
            <input type="button"
                   class="btn btn-maple"
                   style="        min-width: 8rem;background:#02002d!important;color:#fff!important;"
                   value="Home"
                   onclick="location.href='@Url.Action("Index", "Home")'" />
        </div>
    </div>
</div>





