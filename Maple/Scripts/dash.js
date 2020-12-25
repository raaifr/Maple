$(function () {
    $('[name = "btndelete"]').on('click', function () {
        var itemid = '';
        var key_member = 'member_delete_';
        var key_book = 'book_delete_';
        var key_author = 'author_delete_';
        var key_pub = 'publisher_delete_';
        var key_staff = 'staff_delete_';

        if ($(this).attr('id').includes(key_member)) {
            //its a member del req
            itemid = $(this).attr('id').replace(key_member, '');

        } else if ($(this).attr('id').includes(key_book)) {
            //its a book del req
            itemid = $(this).attr('id').replace(key_book, '');

            $.ajax({
                type: 'POST',
                url: $("#deletbook").val(),
                data: { bookid: itemid },
                cache: false,
                success: function (data) {
                    if (data.succ) {
                        $('#reserror').addClass('alert-success');
                        $('#reserror').removeClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Record Removed';
                    } else {
                        $('#reserror').removeClass('alert-success');
                        $('#reserror').addClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    }
                    document.getElementById('reserror').removeAttribute('hidden');
                },
                error: function () {
                    $('#reserror').removeClass('alert-success');
                    $('#reserror').addClass('alert-danger');
                    document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    document.getElementById('reserror').removeAttribute('hidden');
                }
            });

        } else if ($(this).attr('id').includes(key_author)) {
            //its an author del req
            itemid = $(this).attr('id').replace(key_author, '');
            $.ajax({
                type: 'POST',
                url: $("#deletauthor").val(),
                data: { authid: itemid },
                cache: false,
                success: function (data) {
                    if (data.succ) {
                        $('#reserror').addClass('alert-success');
                        $('#reserror').removeClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Record Removed';
                    } else {
                        $('#reserror').removeClass('alert-success');
                        $('#reserror').addClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    }
                    document.getElementById('reserror').removeAttribute('hidden');
                },
                error: function () {
                    $('#reserror').removeClass('alert-success');
                    $('#reserror').addClass('alert-danger');
                    document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    document.getElementById('reserror').removeAttribute('hidden');
                }
            });


        } else if ($(this).attr('id').includes(key_pub)) {
            //its a publisher del req
            itemid = $(this).attr('id').replace(key_pub, '');
            //deletpublisher

            $.ajax({
                type: 'POST',
                url: $("#deletpublisher").val(),
                data: { pid: itemid },
                cache: false,
                success: function (data) {
                    if (data.succ) {
                        $('#reserror').addClass('alert-success');
                        $('#reserror').removeClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Record Removed';
                    } else {
                        $('#reserror').removeClass('alert-success');
                        $('#reserror').addClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    }
                    document.getElementById('reserror').removeAttribute('hidden');
                },
                error: function () {
                    $('#reserror').removeClass('alert-success');
                    $('#reserror').addClass('alert-danger');
                    document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    document.getElementById('reserror').removeAttribute('hidden');
                }
            });

        } else if ($(this).attr('id').includes(key_staff)) {
            //its a staff del req
            itemid = $(this).attr('id').replace(key_pub, '');

            $.ajax({
                type: 'POST',
                url: $("#deletestaff").val(),
                data: { pid: itemid },
                cache: false,
                success: function (data) {
                    if (data.succ) {
                        $('#reserror').addClass('alert-success');
                        $('#reserror').removeClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Record Removed';
                    } else {
                        $('#reserror').removeClass('alert-success');
                        $('#reserror').addClass('alert-danger');
                        document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    }
                    document.getElementById('reserror').removeAttribute('hidden');
                },
                error: function () {
                    $('#reserror').removeClass('alert-success');
                    $('#reserror').addClass('alert-danger');
                    document.getElementById('reserror').innerHTML = 'Unable to complete request, ' + data.msg;
                    document.getElementById('reserror').removeAttribute('hidden');
                }
            });


        }
    });
});



$('#writeqr').click(function () {
    /*
     * To emulate card write
     * there is a
     * 40% chance write will fail
     * and 60% change of success
     * */
    var prob = Math.floor((Math.random() * 5) + 1);
    var banner = document.getElementById('banner1');
    //console.log(prob);

    //alert('Writing Card');

    if (prob % 2 == 0) {
        //even
        $('#banner1').removeClass('alert-success');
        $('#banner1').addClass('alert-danger');
        banner.innerHTML = "Card Write Fail. Please Try again";
    } else {
        //odd
        $('#banner1').addClass('alert-success');
        $('#banner1').removeClass('alert-danger');
        banner.innerHTML = "Card Write Success";
    }
    banner.removeAttribute('hidden');

});

$('#btnresetform').click(function () {
    //alert('reseting form');
    resetform();
});


function fillsamplebook() {
    //alert('it works');
    document.getElementById('seriestitle').value = "";
    document.getElementById('booktitle').value = "The family at Red-Roofs";
    document.getElementById('publisher').value = "Awards Publications Limtied";
    document.getElementById('authors').value = "Enid Blyton";
    document.getElementById('year').value = "2003";
    document.getElementById('ddc').value = "823.32";
    document.getElementById('isbn').value = "9780861639489";
    document.getElementById('tags').value = "adventure,mystery,kids,fiction";
    document.getElementById('stock').value = "2";
    document.getElementById('shelf').value = "M233";
    //alert('Form has been auto filled. Please Re Check Data before submitting!!')
}


function resetform() {
    //alert('it works');
    document.getElementById('seriestitle').value = "";
    document.getElementById('booktitle').value = "";
    document.getElementById('publisher').value = "";
    document.getElementById('authors').value = "";
    document.getElementById('year').value = "";
    document.getElementById('ddc').value = "";
    document.getElementById('isbn').value = "";
    document.getElementById('tags').value = "";
    document.getElementById('stock').value = "";
    document.getElementById('shelf').value = "";
    var a1 = $("#alert-yay");
    var b1 = document.getElementById("alert-yay");
    var a2 = $("#alert-nay");
    var b2 = document.getElementById("alert-nay");
    var a3 = $("#alert-bignay");
    var b3 = document.getElementById("alert-bignay");
    if (!a1.length == 0) {
        b1.setAttribute("hidden", "")
    }
    if (!a2.length == 0) {
        b2.setAttribute("hidden", "")
    }
    if (!a3.length == 0) {
        b3.setAttribute("hidden", "")
    }
}



/*
 * Modal form submit
 */
//add new publisher
$(function () {
    $('body').on('click', '.odom-submit', function (e) {
        //alert('publisher');
        var pubname = document.getElementById('publisher-name').value;
        var pubaddress = document.getElementById('publisher-address').value;
        var npurl = $("#newpublisher").val();

        $.ajax({
            type: 'POST',
            url: npurl,
            data: { name: pubname, address: pubaddress },
            cache: false,
            success: function (data) {
                document.getElementById('publisher').value = data.msg;
                document.getElementById('publisher-name').vaue = '';
                document.getElementById('publisher-address').vaue = '';
                $('#newpublishermodal').modal('hide');
            },
            error: function () {
                document.getElementById('publisher').value = "Publisher add Failed";
                document.getElementById('publisher-name').vaue = '';
                document.getElementById('publisher-address').vaue = '';
                $('#newpublishermodal').modal('hide');
            }
        });
        document.getElementById('publisher-name').vaue = '';
        document.getElementById('publisher-address').vaue = '';
        $('#newpublishermodal').modal('hide');
    });
});

//add new author
$(function () {
    $('body').on('click', '.odom-submit2', function (e) {
        //alert('author');
        var fname = document.getElementById('first-name').value;
        var mname = document.getElementById('middle-name').value;
        var lname = document.getElementById('last-name').value;
        var naurl = $("#newauthor").val();

        $.ajax({
            type: 'POST',
            url: naurl,
            data: { firstname: fname, midname: mname, lastname: lname },
            cache: false,
            success: function (data) {
                var authortextbox = document.getElementById('authors');
                if (authortextbox.value == '') {
                    authortextbox.value = data.msg;
                    document.getElementById('first-name').vaue = '';
                    document.getElementById('middle-name').vaue = '';
                    document.getElementById('last-name').vaue = '';
                    $('#newauthormodal').modal('hide');
                } else {
                    authortextbox.value = authortextbox.value + "," + data.msg;
                    document.getElementById('first-name').vaue = '';
                    document.getElementById('middle-name').vaue = '';
                    document.getElementById('last-name').vaue = '';
                    $('#newauthormodal').modal('hide');
                }
            },
            error: function () {
                document.getElementById('authors').value = "Author add Failed";
                document.getElementById('first-name').vaue = '';
                document.getElementById('middle-name').vaue = '';
                document.getElementById('last-name').vaue = '';
                $('#newauthormodal').modal('hide');
            }
        });

        document.getElementById('publisher-name').vaue = '';
        document.getElementById('publisher-address').vaue = '';
        $('#newauthormodal').modal('hide');
    });
});



/*
 * Auto complete
 */

$(document).ready(function () {
    $.ajax({
        url: $("#acauthor").val(),
        type: "POST",
        dataType: "json",
        success: function (data) {
            //console.log(data);
            $("#authors").autocomplete({
                source: data
            });
        }
    });

    $.ajax({
        url: $("#acshelf").val(),
        type: "POST",
        dataType: "json",
        success: function (data) {
            $("#shelf").autocomplete({
                source: data
            });
        }
    });

    $.ajax({
        url: $("#acpublisher").val(),
        type: "POST",
        dataType: "json",
        success: function (data) {
            $("#publisher").autocomplete({
                source: data
            });
        }
    });

    $.ajax({
        url: $("#acseriestitle").val(),
        type: "POST",
        dataType: "json",
        success: function (data) {
            $("#seriestitle").autocomplete({
                source: data
            });
        }
    });


});

