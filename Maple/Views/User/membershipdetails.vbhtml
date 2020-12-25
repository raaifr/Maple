@Modeltype IEnumerable(Of PaymentModel)

@Code
    ViewData("Title") = "membershipdetails"
    Layout = "~/Views/Shared/_LayoutUserDash.vbhtml"
End Code


<div class="row" role="main">


    <div class="col p-2" style="width:60%; min-height:20rem;">
        <h2>Payment History</h2>
        <div class="table-responsive">
            <table class="table table-striped table-sm">
                <thead>
                    <tr>
                        <th>Payment Date</th>
                        <th>Payment Type</th>
                        <th>Amount</th>
                        <th>Remarks</th>
                    </tr>
                </thead>
                <tbody>
                    @For Each invoice In Model
                        @<tr>
                            <td>@Html.DisplayFor(Function(modelItem) invoice.paymentDate)</td>
                            <td>@Html.DisplayFor(Function(modelItem) invoice.paymentType)</td>
                            <td>@Html.DisplayFor(Function(modelItem) invoice.amount)</td>
                            @If invoice.remarks = "" Then
                                @<td>NIL</td>
                            Else
                                @<td>@Html.DisplayFor(Function(modelItem) invoice.remarks)</td>
                            End If

                        </tr>
                    Next

                </tbody>
            </table>

        </div>
    </div>


    <div class="col-3 p-3 text-center" style="width: 40%">
        <div>
            @If Not IsNothing(ViewBag.QRCodeImage) Then
                @<img src="@ViewBag.QRCodeImage" style="height:200px;width:200px" />
            End If
            
        </div>

        <div>
            <h6 class="text-muted"><strong>Membership Status</strong></h6>
            @If Not IsNothing(ViewBag.membershipstat) Then
                If ViewBag.membershipstat = True Then
                    @<h1 class="text-success text-center align-content-center" style="background-color: #94e6a7">Active</h1>
                Else
                    @<h1 class="text-danger text-center align-content-center" style="background-color: #f6c0c6">Expired</h1>
                    @<div>
                        <a Class="text-primary" href="@Url.Action("reqRenew", "User")">Renew Membership</a>
                    </div>
                End If
            Else
                @<h1 class="text-secondary text-center align-content-center" style="background-color: #d3d3d3">Unknown</h1>
            End If

        </div>


    </div>


</div>


