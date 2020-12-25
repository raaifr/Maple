$(document).ready(function () {
    window.membercode = '';
    window.bookcode = '';
    window.step = 1;
    window.scantimeout = 40000;
    console.log("listening!");
});

$("#kiosk_omnibox").keyup(function (event) {
    if (event.keyCode === 13) {
        //enter pressed
        //$("#id_of_button").click();
    }
});

function omniinput(val) {
    var errmsg = document.getElementById('errortext');
    var omnibox = document.getElementById('kiosk_omnibox');

    //we shall give sometime until cancellation
    setTimeout(function () {
        resetkiosk();
    }, scantimeout);

    errmsg.setAttribute("hidden", "");
    if (step == 1) {
        //only membercode accpted
        if (val.length == 20) {
            //alert(val.length + "input for member id detected! code: " + val);
            membercode = val;
            omnibox.value = '';
            step = 2;

            //prompt to scan book id
            document.getElementById('scan_membercode').setAttribute("hidden", "");
            document.getElementById('scan_book').removeAttribute("hidden");
        } else {
            errmsg.innerHTML = "Invalid Member Code"
            errmsg.removeAttribute("hidden");
        }
    } else if (step == 2) {
        if (val.substring(0, 2) == 'bk') {
            alert("input for book detected! code: " + val);
            bookcode = val;
            omnibox.value = '';
        } else {
            errmsg.innerHTML = "Invalid Book Tag"
            errmsg.removeAttribute("hidden");
        }
    }

    //console.log(membercode + ' and ' + bookcode);

    if (!membercode == '' && !bookcode == '') {
        var pdiv = document.getElementById('inputdiv');
        pdiv.setAttribute("hidden", "");
        alert("Borrow Permitted");

        //send values to borrow function in usercontroller
        var surl = $("#borrowbook").val();
        $.ajax({
            type: 'POST',
            url: surl,
            data: { mcode: membercode, bcode: bookcode },
            cache: false,
            success: function (data) {
                var succeed = data.succ;
                if (succeed) {
                    success();
                } else if (succeed = false && data.msg == "overdue") {
                    gotoPayment(membercode, bookcode);
                } else {
                    fail();
                }
            },
            error: function () {
                document.getElementById('errortext').innerHTML = "Connection Failed. Please Reload the page and Try Again"
                document.getElementById('errortext').removeAttribute("hidden");
                //alert("Connection Failed. Please Reload the page and Try Again");
            }
        });

    }

}

function gotoPayment (membercode, bookcode) {
    var purl = $("#paydues").val();
    $.ajax({
        type: 'POST',
        url: purl,
        data: { mcode: membercode, bcode: bookcode },
        cache: false,
        success: function (data) {
            var succeed = data.succ;
            if (succeed) {
                success();
            } else if (succeed = false && data.msg == "overdue") {
                gotoPayment(membercode, bookcode);
            } else {
                fail();
            }
        },
        error: function () {
            document.getElementById('errortext').innerHTML = "Connection Failed. Please Reload the page and Try Again"
            document.getElementById('errortext').removeAttribute("hidden");
            //alert("Connection Failed. Please Reload the page and Try Again");
        }
    });
}

function resetkiosk() {
    membercode = '';
    bookcode = '';
    step = 1;
    document.getElementById('errortext').setAttribute("hidden", "");
    document.getElementById('kiosk_omnibox').value = '';
    document.getElementById('inputdiv').removeAttribute("hidden");
    document.getElementById('scan_membercode').removeAttribute("hidden");
    document.getElementById('scan_book').setAttribute("hidden", "");

    document.getElementById('borrow_success').setAttribute("hidden", "");
    document.getElementById('borrow_fail').setAttribute("hidden", "");
}

function fail() {
    document.getElementById('errortext').setAttribute("hidden", "");
    document.getElementById('kiosk_omnibox').setAttribute("hidden", "");
    document.getElementById('inputdiv').setAttribute("hidden", "");
    document.getElementById('scan_membercode').setAttribute("hidden", "");
    document.getElementById('scan_book').setAttribute("hidden", "");

    document.getElementById('borrow_success').setAttribute("hidden", "");
    document.getElementById('borrow_fail').removeAttribute("hidden");
    setTimeout(function () {
        resetkiosk();
    }, 6000);
}

function success() {
    document.getElementById('errortext').setAttribute("hidden", "");
    document.getElementById('kiosk_omnibox').setAttribute("hidden", "");
    document.getElementById('inputdiv').setAttribute("hidden", "");
    document.getElementById('scan_membercode').setAttribute("hidden", "");
    document.getElementById('scan_book').setAttribute("hidden", "");

    document.getElementById('borrow_success').removeAttribute("hidden");
    document.getElementById('borrow_fail').setAttribute("hidden", "");
    setTimeout(function () {
        resetkiosk();
    }, 5000);
}