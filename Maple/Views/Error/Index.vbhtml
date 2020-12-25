@Code
    ViewData("Title") = "Error"
    Layout = "~/Views/Shared/_LayoutError.vbhtml"
End Code



<div class="container">
    <div class="row">
        <div class="col-md-6 align-self-center">
            <div class="container-fluid">
                <img src="~/Content/img/errdef.png" style="width:117%;" />
            </div>
        </div>
        <div class="col-md-6 align-self-center">
            <h1>Oops!</h1>
            <h2>Well thats embarassing.</h2>
            <p>
                Something happened and we are not sure why.
                We are working to fix the issue. In the mean time visit our homepage
                using the button below.
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






