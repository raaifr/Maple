@ModelType Maple.SearchModel

@Code
    ViewData("Title") = "Home Page"
End Code



@Styles.Render("~/Content/HomePage.css")

<div class="row justify-content-around">

    <div class="col-1 mt-4">
    </div>


    <div class="col-6 mt-4 mb-4" >
        @Using (Html.BeginForm("Search", "Search", Nothing, FormMethod.Post, New With {.class = "form-inline", .style = "margin-right:40px;"}))
            @Html.AntiForgeryToken()


            @Html.DropDownListFor(Function(model) model.selectedCategory, Model.CategoryList, "-- Search Category --", New With {.Class = "form-control"})

            @<div Class="input-group">
                @Html.EditorFor(Function(model) model.SearchTerm, New With {.htmlAttributes = New With {.class = "form-control", .style = "min-width:20rem;", .placeholder = Html.DisplayNameFor(Function(model) model.SearchTerm)}})
                <div Class="input-group-append">
                    <Button id="btnSearch" Class="btn btn-primary" type="submit">Search</Button>
                </div>
            </div>
        End Using

    </div>

    <div class="col-1 mt-4">
    </div>

</div>



<div class="row mt-3 text-center">
    <div class="col">
        <img src="~/Content/img/kids.jpg" class="img-responsive image-thumbnail" />
        <h3>For Kids</h3>
        <p>
            If you are a teenager check out this page for book recommendations, teen news & updates, and a list of activities specifically for you and your friends.
        </p>
        <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301865">Learn more &raquo;</a></p>
    </div>
    <div class="col-md-4">
        <img src="~/Content/img/adolecent.jpg" class="img-responsive image-thumbnail" />
        <h3>For Teens</h3>
        <p>If you are a teenager check out this page for book recommendations, teen news & updates, and a list of activities specifically for you and your friends. </p>
        <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301866">Learn more &raquo;</a></p>
    </div>
    <div class="col-md-4">
        <img src="~/Content/img/adult.jpg" class="img-responsive image-thumbnail" />
        <h3>Adult Section</h3>
        <p>You can easily find a web hosting company that offers the right mix of features and price for your applications.</p>
        <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301867">Learn more &raquo;</a></p>
    </div>
</div>
</div>