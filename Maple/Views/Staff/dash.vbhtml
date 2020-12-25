@ModelType Maple.StaffModel
@Code
    ViewData("Title") = "Dashboard"
    Layout = "~/Views/Shared/_LayoutStaffDash.vbhtml"
    Dim name = "welcome    .... who're  are you please?"
    If Not IsNothing(Session(Constants.IDENT_STAFF_ID)) OrElse Not IsNothing(Model) Then
        name = "Welcome " & Model.firstname & " " & Model.lastname
    End If
End Code

<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
    <h1>@name</h1>

</div>

<p>Select a Task to do from the side bar.</p>


