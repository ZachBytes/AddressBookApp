
function createAddress() {
    $.ajax({
        url: "/api/AddressesAPI/",
        type: "POST",
        contentType: "application/json;",
        datatype: "json",
        dataSrc: "",
        async: false,
        data: JSON.stringify({
            ContactID: $("#AddressContactIDAdd").val(),
            StreetAddress: $("#StreetAdd").val(),
            City: $("#CityAdd").val(),
            State: $("#StateAdd").val(),
            PostalCode: $("#PostalCodeAdd").val()
        }),
        success: function () {
            $('#AddressTable').DataTable().ajax.reload()
            $('#ContactTable').DataTable().ajax.reload()
            $("#showModal3").html("");
            $("#showModal3").modal("hide");
            $("body").removeClass("modal-open");
            $(".modal-backdrop").remove();
        }
    })
}

function editAddress() {
    var addID = $("#AddressIDEdit").val()
    $.ajax({
        url: "/api/AddressesAPI/" + addID,
        type: "PUT",
        contentType: "application/json;",
        datatype: "json",
        dataSrc: "",
        async: false,
        data: JSON.stringify({
            AddressID: $("#AddressIDEdit").val(),
            ContactID: $("#AddressContactIDEdit").val(),
            StreetAddress: $("#StreetAddressEdit").val(),
            City: $("#CityEdit").val(),
            State: $("#StateEdit").val(),
            PostalCode: $("#PostalCodeEdit").val()
        }),
        success: function () {

            $('#AddressTable').DataTable().ajax.reload()

            $("#showModal4").html("");
            $("#showModal4").modal("hide");
            $("body").removeClass("modal-open");
            $(".modal-backdrop").remove();
        }
    });
};

function openContactModal() {

    $.ajax({
        url: '/Contacts/Create'
    }).done(function (msg) {
        $("#showModal").html(msg);
        $('#myModal').modal({ backdrop: 'static', keyboard: false })
        $("#myModal").modal("show");
        var link = document.getElementById("fnValidation");
        link.style.display = "none";
    })

};

function openAddressModal() {
    $.ajax({
        url: "/Addresses/Create"
    }).done(function (msg) {
        $("#showModal3").html(msg);

        $("#AddressContactIDAdd").val($("#contactid").val())

        $('#CreateAddressModal').modal({ backdrop: 'static', keyboard: false })
        $("#CreateAddressModal").modal("show");
    });
};

function deleteContact(x) {
    $.ajax({
        type: "DELETE",
        url: '/api/ContactsAPI/' + x
    }).done(function (msg) {
        $('#ContactTable').DataTable().ajax.reload()

        $("#showModal2").html("");
        $("#showModal2").modal("hide");
        $("body").removeClass("modal-open");
        $(".modal-backdrop").remove();
        //$("#showModal2").html(msg);
        //$('#myModal2').modal({ backdrop: 'static', keyboard: false })
        //$("#myModal2").modal("show");
    })
};

function deleteAddress(x) {
    $.ajax({
        type: "DELETE",
        url: '/api/AddressesAPI/' + x
    }).done(function (msg) {
        $('#AddressTable').DataTable().ajax.reload()
        //$("#showModal2").html(msg);
        //$('#myModal2').modal({ backdrop: 'static', keyboard: false })
        //$("#myModal2").modal("show");
    })
};

function openEditAddressModal(x) {
    $.ajax({
        url: "/Addresses/Edit/" + x
    }).done(function (msg) {
        $("#showModal4").html(msg);
        $('#EditAddressModal').modal({ backdrop: 'static', keyboard: false })
        $("#EditAddressModal").modal("show");
    });
};

$(document).ready(function () {

    //Fuzzy search grid

    $("#ContactTable").DataTable({
        lengthChange: false,
        searching: true,
        responsive: false,
        pageLength: 15,
        processing: false,
        serverSide: false,
        ajax:
        {
            url: "/api/ContactsAPI/",
            type: "GET",
            datatype: "json",
            dataSrc: ""
        },
        columns:
            [
                { data: "contactID" },
                { data: "firstName" },
                { data: "lastName" },
                { data: "addresses[0].addressID", "visible": false },
                { data: "addresses[0].streetAddress", "visible": false },
                { data: "addresses[0].city", "visible": false },
                { data: "addresses[0].state", "visible": false },
                { data: "addresses[0].postalCode", "visible": false }
            ]
    });



    $("#ContactTable").on("dblclick", "tr", function () {
        var contactID = $(this).find('td:first').text()
        $.ajax({
            url: '/Contacts/Edit/' + contactID
        }).done(function (msg) {
            $("#showModal2").html(msg);
            $('#myModal2').modal({ backdrop: 'static', keyboard: false })
            $("#myModal2").modal("show");

            //Address collection with view/edit functions

            $('#AddressTable').DataTable({
                lengthChange: false,
                searching: false,
                responsive: false,
                pageLength: 15,
                processing: false,
                serverSide: false,
                ajax:
                {
                    url: "/api/AddressesAPI/" + contactID,
                    type: "GET",
                    datatype: "json",
                    dataSrc: ""
                },
                columns:
                    [
                        { data: "addressID" },
                        { data: "streetAddress" },
                        { data: "city" },
                        { data: "state" },
                        { data: "postalCode" },
                        { data: "deleteAddress" },
                        { data: "editAddress" }
                    ]
            });
        });
    })

    $("#showModal").on("click", "#createContact", function () {
        //Example of unobtrusive validation
        if (!$("#fn").val()) {
            var ele = document.getElementById("fnValidation");
            ele.style.display = "block";
        }
        else {
            $.ajax({
                url: "/api/ContactsAPI/",
                type: "POST",
                contentType: "application/json;",
                datatype: "json",
                dataSrc: "",
                async: false,
                data: JSON.stringify({ FirstName: $("#fn").val(), LastName: $("#ln").val() }),
                success: function () {
                    var ele = document.getElementById("fnValidation");
                    ele.style.display = "none";
                    $('#ContactTable').DataTable().ajax.reload()

                    $("#showModal").html("");
                    $("#showModal").modal("hide");
                    $("body").removeClass("modal-open");
                    $(".modal-backdrop").remove();
                }
            });
        }

    })

    $("#showModal2").on("click", "#editContact", function () {
        var contactID = $("#contactid").val()
        $.ajax({
            url: "/api/ContactsAPI/" + contactID,
            type: "PUT",
            contentType: "application/json;",
            datatype: "json",
            dataSrc: "",
            async: false,
            data: JSON.stringify({ FirstName: $("#fnedit").val(), LastName: $("#lnedit").val() }),
            success: function () {

                $('#ContactTable').DataTable().ajax.reload()

                $("#showModal2").html("");
                $("#showModal2").modal("hide");
                $("body").removeClass("modal-open");
                $(".modal-backdrop").remove();
            }
        });
    });

    $("#showModal2").on("click", "#deleteContact", function () {
        var contactID = $("#contactid").val()
        $.ajax({
            url: "/api/ContactsAPI/" + contactID,
            type: "DELETE",
            contentType: "application/json;",
            datatype: "json",
            dataSrc: "",
            async: false,
            data: JSON.stringify({ FirstName: $("#fnedit").val(), LastName: $("#lnedit").val() }),
            success: function () {

                $('#ContactTable').DataTable().ajax.reload()

                $("#showModal2").html("");
                $("#showModal2").modal("hide");
                $("body").removeClass("modal-open");
                $(".modal-backdrop").remove();
            }
        });
    });




});