@Code
    ViewData("Title") = "NotFound"
    Layout = "~/Views/Shared/_LayoutError.vbhtml"
End Code

<div class="container">
    <div class="row">
        <div class="col-md-6 align-self-center">
            <div class="container-fluid">
                <img src="~/Content/img/404.png" style="width:117%;" />
            </div>
        </div>
        <div class="col-md-6 align-self-center">
            <h1>404</h1>
            <h2>UH OH! You're lost.</h2>
            <p>
                The page you are looking for does not exist.
                How you got here is a mystery. But you can click the button below
                to go back to the homepage.
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



