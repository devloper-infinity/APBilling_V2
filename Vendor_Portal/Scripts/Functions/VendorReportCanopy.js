$(document).ready(function () {

    $("#btnShow").click(function () {

        LoadSummary();

        LoadDetails();

    });

});

function BindSummaryGrid(data) {

    if ($.fn.DataTable.isDataTable('#tblSummary')) {
        $('#tblSummary').DataTable().destroy();
        $('#tblSummary').empty();
    }

    if (!data || data.length == 0) {
        document.getElementById("Tracking_errmsg").innerHTML = "No Data Found";
        document.getElementById("Tracking_errmsg").style.color = 'red';
        $('#Tracking_dverror').modal('show');
        return;
    }

    var columns = [];

    $.each(Object.keys(data[0]), function (i, key) {
        columns.push({
            title: key,
            data: key,
            defaultContent: ""
        });
    });

    $('#tblSummary').DataTable({
        destroy: true,
        data: data,
        columns: columns,
        responsive: true,
        autoWidth: false
    });
}

function BindDetailGrid(data) {
    if (data == null || data.length == 0) {
        return;
    }

    if ($.fn.DataTable.isDataTable('#tblDetails')) {
        $('#tblDetails').DataTable().clear().destroy();
    }
    $("#tblDetails").empty();

    var columns = [];
    $.each(Object.keys(data[0]), function (i, key) {
        columns.push({ data: key, title: key, defaultContent: "" });
    });

    var table = $("#tblDetails").DataTable({
        destroy: true,
        data: data,
        columns: columns,
        processing: true,
        searching: true,
        paging: true,
        ordering: true,
        info: true,
        pageLength: 25,
        lengthMenu: [
            [25, 50, 100, -1],
            [25, 50, 100, "All"]
        ],
     
        scrollCollapse: true,
        autoWidth: false,
        responsive: false,
        fixedHeader: true,
        language: {
            emptyTable: "No Records Found"
        },
        initComplete: function () {
            var api = this.api();
            setTimeout(function () {
                api.columns.adjust().draw(false);
            }, 200);
        }
    });

    $('a[data-toggle="tab"], button, .nav-link').on('shown.bs.tab', function (e) {
        if ($.fn.DataTable.isDataTable('#tblDetails')) {
            $('#tblDetails').DataTable().columns.adjust();
        }
    });
}

