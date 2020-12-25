
<nav class="navbar navbar-expand-md navbar-dark bg-dark justify-content-between" style="background-color: #02002d !important;">
    <a class="navbar-brand" href="@Url.Action("Index", "Home")">
        <img src="~/Content/img/Maple.png" width="30" height="30" alt="" loading="lazy">
    </a>

    <button class="navbar-toggler" type="button" data-toggle="collapse" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="main-navbar">
        <ul class="nav navbar-nav">
            <!-- ...ActionLink(Link Name, ControllerAction, Controller Name, new {  class = "nav-link" }) -->
            <li class="nav-item">@Html.ActionLink("Home", "Index", "Home", Nothing, New With {.class = "nav-link"})</li>
            <li class="nav-item">@Html.ActionLink("Catalogue", "Catalogue", "Home", Nothing, New With {.class = "nav-link"})</li>
            <li class="nav-item">@Html.ActionLink("Ask Us", "Contact", "Home", Nothing, New With {.class = "nav-link"})</li>
        </ul>


    </div>


    @If Not IsNothing(Session(Constants.IDENT_USER_ID)) Then
        @<div class="my-2 my-lg-0">
            @Using (Html.BeginForm("Logout", "Auth"))
                @<button class="btn btn-outline-light my-2 my-sm-0" type="submit">Logout</button>
            End Using

        </div>
    End If





</nav>

