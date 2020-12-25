<nav class="navbar navbar-expand-md navbar-dark bg-dark justify-content-between" style="background-color: #02002d !important;">

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

    <!-- form class="form-inline" style="margin-right:40px;">

        <select id="catlist" class="form-control">
            <option value="0" selected="selected">- Any -</option>
            <option value="1"> Book</option>
            <option value="2"> Author</option>
            <option value="3"> Publisher</option>
            <option value="366"> ISBN</option>
        </select>

        <div class="input-group">
            <input type="text" name="searchterm" class="form-control" placeholder="Enter search Term">
            <div class="input-group-append">
                <button id="btnSearch" class="btn btn-primary" type="submit">Search</button>
            </div>
        </div>
    </form>
        -->



    @If Not IsNothing(Session(Constants.IDENT_USER_ID)) Then
        @<div class="my-2 my-lg-0">
            @Using (Html.BeginForm("dash", "User", FormMethod.Get))
                @<div class="btn-group dropdown">
                    <button type="submit" class="btn btn-info">
                        Dasdshboard
                    </button>
                    <button type="button" class="btn btn-info dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <div class="dropdown-menu">
                        @Html.ActionLink("Account", "Account", "User", Nothing, New With {.class = "dropdown-item"})
                        <div class="dropdown-divider"></div>
                        @Html.ActionLink("Logout", "Logout", "Auth", Nothing, New With {.class = "dropdown-item"})
                    </div>
                </div>

            End Using

        </div>
    ElseIf Not IsNothing(Session(Constants.IDENT_STAFF_ID)) Then
        @<div class="my-2 my-lg-0">
            @Using (Html.BeginForm("dash", "Staff", FormMethod.Get))
                @<div class="btn-group dropdown">
                    <button type="submit" class="btn btn-info">
                        Dasdshboard
                    </button>
                    <button type="button" class="btn btn-info dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <div class="dropdown-menu">
                        @Html.ActionLink("Logout", "Logout", "Auth", Nothing, New With {.class = "dropdown-item"})
                    </div>
                </div>

            End Using

        </div>
    Else
        @<div class="my-2 my-lg-0">
            @Using (Html.BeginForm("Login", "Auth", FormMethod.Get))
                @<button class="btn btn-outline-light my-2 my-sm-0" type="submit">Login</button>
            End Using

        </div>
    End If





</nav>

