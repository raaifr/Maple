<nav id="sidebarMenu" >
    <div class="sidebar-sticky pt-3" >
        <ul class="nav flex-column">
            <li class="nav-item">
                <a class="text-muted font-weight-bold  nav-link" href="#"><i class="fa fa-home"></i>Dashboard</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="@Url.Action("reservehistory", "User")"><i class="fa fa-flag-checkered"></i>Reservations</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="@Url.Action("borrowhistory", "User")"><i class="fa fa-history"></i>Borrow History</a>
            </li>


            <h3 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                Membership
            </h3>

            <ul class="nav flex-column mb-2">
                <li class="nav-item">
                    <a class="nav-link" href="@Url.Action("membershipdetails", "User")"><i class="fa fa-info-circle"></i>Membership Details</a>
                </li>
            </ul>
        </ul>
    </div>
</nav>
