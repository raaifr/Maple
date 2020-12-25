$("input").click(function () {
    
    if (this.id.includes("srch_btn_reserve_")) {
        var errbanner = document.getElementById('reserror')
        var bookid = this.id.replace("srch_btn_reserve_", "");
        var srchkey = document.getElementById('membersearchbox').value;
        var surl = $("#bookreserve").val();
        //todo build all search filters and pass it into the data block, data:{}

        //errbanner.setAttribute("hidden", "");

        $.ajax({
            type: 'POST',
            url: surl,
            data: { bkid: bookid, searchtrm: srchkey},
            cache: false,
            success: function (data) {
                var succeed = data.succ;
                console.log(succeed);
                if (succeed) {
                    $("#reserror").addClass('alert-success');
                    $("#reserror").removeClass('alert-danger');
                } else {
                    $("#reserror").removeClass('alert-success');
                    $("#reserror").addClass('alert-danger');
                }
                errbanner.removeAttribute('hidden');
                document.getElementById('reserror').innerHTML = data.msg;
            },
            error: function () {
                document.getElementById('reserror').innerHTML = "Connection Failed. Please Reload the page and Try Again"
                document.getElementById('reserror').removeAttribute("hidden");
                //alert("Connection Failed. Please Reload the page and Try Again");
            }
        });


    }
});