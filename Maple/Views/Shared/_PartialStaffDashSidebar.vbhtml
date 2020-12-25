@code
    Dim role As Integer = Session(Constants.IDENT_STAFF_ROLE)
End Code

<div class="sidebar-sticky pt-3">

    <h6 class="sidebar-heading d-flex justify-content-between align-items-center pl-2 mt-2 mb-1 text-muted font-weight-bold">
        <span><i class="fa fa-bolt mr-2 pr-1"></i>Quick Actions</span>
    </h6>
    <ul class="nav flex-column">
        <li class="nav-item sidebar-menu-item pl-2">
            <a class="text-muted font-weight-light nav-link" href="@Url.Action("membercard", "Staff")"><i class="fa fa-id-card-o mr-2 pr-1"></i>Member Cards</a>
        </li>
        @If role >= 200 Then
        @<li class="nav-item sidebar-menu-item pl-2">
            <a class="text-muted font-weight-light nav-link" href="#"><i class="fa fa-id-card-o mr-2 pr-1"></i>Staff Cards</a>
        </li>
        End if
        <li class="nav-item sidebar-menu-item pl-2">
            <a class="text-muted font-weight-light nav-link" href="@Url.Action("Index", "Kiosk")"><i class="fa fa-television mr-2 pr-1"></i>Kiosk</a>
        </li>
    </ul>

    <hr />

    @If role >= 200 Then
        @<h6 class="sidebar-heading d-flex justify-content-between align-items-center pl-2 mt-2 mb-1 text-muted font-weight-bold">
            <span><i class="fa fa-flag mr-2 pr-1"></i>Reports</span>
            <!--
            <a class="d-flex align-items-center text-muted" href="#" aria-label="Add a new report">
                <span data-feather="plus-circle"></span>
            </a>
                -->
        </h6>
        @<ul class="nav flex-column mb-2">
            <li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="#"><i class="fa fa-exchange mr-2 pr-1"></i>Transactions</a>
            </li>
            <li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="#"><i class="fa fa-money mr-2 pr-1"></i>Payments</a>
            </li>
            <li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="#"><i class="fa fa-calendar-times-o mr-2 pr-1"></i>Dues</a>
            </li>
        </ul>
        @<hr />
    End if


    @If role >= 200 Then
        @<h6 class="sidebar-heading d-flex justify-content-between align-items-center pl-2 mt-2 mb-1 text-muted font-weight-bold">
            <span><i class="fa fa-user mr-2 pr-1"></i>Member</span>
        </h6>
        @<ul class="nav flex-column mb-2">
            <li class="nav-item pl-2">
                <!-- problems here. ajax call to redirect action to edit member doesnt work-->
                <a class="text-muted font-weight-light nav-link" href="@Url.Action("viewMember", "Staff")"><i class="fa fa-eye mr-2 pr-1"></i>View Member</a>
            </li>


        </ul>
        @<hr />
    End If


    @If role >= 500 And role < 600 Or role >= 2000 Then
        @<h6 class="sidebar-heading d-flex justify-content-between align-items-center pl-2 mt-2 mb-1 text-muted font-weight-bold">
            <span> <i class="fa fa-users mr-2 pr-1"></i>Staff</span>
        </h6>
        @<ul class="nav flex-column mb-2">
            <li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="@Url.Action("addStaff","Staff")"><i class="fa fa-plus mr-2 pr-1"></i>Add Staff</a>
            </li>
            <li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="@Url.Action("viewstaff", "Staff")"><i class="fa fa-pencil-square-o mr-2 pr-1"></i>View Staff</a>
            </li>
        </ul>
        @<hr />
    End if



    <h6 class="sidebar-heading d-flex justify-content-between align-items-center pl-2 mt-2 mb-1 text-muted font-weight-bold">
        <span> <i class="fa fa-briefcase mr-2 pr-1"></i>Catalogue</span>
    </h6>
    <ul class="nav flex-column mb-2">
        <li class="nav-item pl-2">
            <a class="text-muted font-weight-light nav-link" href="@Url.Action("addbook", "Staff")"><i class="fa fa-plus mr-2 pr-1"></i>Add Book</a>
        </li>
        <li class="nav-item pl-2">
            <a class="text-muted font-weight-light nav-link" href="@Url.Action("findbook", "Staff")"><i class="fa fa-book mr-2 pr-1"></i>Find Book</a>
        </li>

        @If role >= 500 And role < 600 Or role >= 2000 Then
            @<li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="@Url.Action("viewauthor", "Staff")"><i class="fa fa-address-book-o mr-2 pr-1"></i>Find Author</a>
            </li>

            @<li class="nav-item pl-2">
                <a class="text-muted font-weight-light nav-link" href="@Url.Action("viewpublisher", "Staff")"><i class="fa fa-file-text-o mr-2 pr-1"></i>Find Publisher</a>
            </li>
        End if

    </ul>

</div>
