
var AP670_UnPaidSummary_table;
var AP670_PaidSummary_table;

function Bind_AP670_UnPaidSummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "AP670.aspx/GetAP670_UnPaidSummary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            AP670_UnPaidSummary_table = $('#table_AP670_UnPaidSummary').DataTable({
                dom: 'Bftip',
                destroy: true,
                scrollX: true,
                "paging": true,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                filter: true,
                'select': {
                    'style': 'single'
                },
                "serverSide": false,
                "data": dataArray,
                columns: [
                    { data: 'WeekWorked' },
                    { data: 'Day' },
                    { data: 'Hours' },
                    { data: 'Rate' },
                    { data: 'Total' },
                    { data: 'JobDescription' },
                    { data: 'MemberName' },
                    { data: 'UHours' },
                    { data: 'URate' },
                    { data: 'Amount' },
                    { data: 'NVA' },
                    { data: 'PayStatus' },
                    { data: 'PaidDate' },
                    { data: 'AccRemark' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'AP670 Un-Paid Summary', autoFilter: true,
                    },
                ],
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });

    return false;
}

function Bind_AP670_PaidSummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "AP670.aspx/GetAllAP670_PaidSummary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            AP670_PaidSummary_table = $('#table_AP670_PaidSummary').DataTable({
                dom: 'Bftip',
                destroy: true,
                "paging": true,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                filter: true,
                'select': {
                    'style': 'single'
                },
                "serverSide": false,
                "data": dataArray,
                columns: [
                    { data: 'WeekWorked' },
                    { data: 'Day' },
                    { data: 'Hours' },
                    { data: 'Rate' },
                    { data: 'Total' },
                    { data: 'JobDescription' },
                    { data: 'MemberName' },
                    { data: 'UHours' },
                    { data: 'URate' },
                    { data: 'Amount' },
                    { data: 'NVA' },
                    { data: 'PayStatus' },
                    { data: 'PaidDate' },
                    { data: 'AccRemark' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'AP-670 Paid Summary', autoFilter: true,
                    },
                ],
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });

    return false;
}

function BindUnpaidGrid() {

    $('#load1').show();

    $.ajax({
        url: "AP670.aspx/GetAP670_UnPaidSummary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            AP670_UnPaidSummary_table = $('#table_AP670_UnPaidSummary').DataTable({
                dom: 'Bftip',
                destroy: true,
                orderCellsTop: true,
                "paging": true,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                filter: true,
                'select': {
                    'style': 'single'
                },
                "serverSide": false,
                "data": dataArray,
                columns: [
                    { data: 'WeekWorked' },
                    { data: 'Day' },
                    { data: 'Hours' },
                    { data: 'Rate' },
                    { data: 'Total' },
                    { data: 'JobDescription' },
                    { data: 'MemberName' },
                    { data: 'UHours' },
                    { data: 'URate' },
                    { data: 'Amount' },
                    { data: 'NVA' },
                    { data: 'PayStatus' },
                    { data: 'PaidDate' },
                    { data: 'AccRemark' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'AP-670 Paid Summary', autoFilter: true,
                    },
                ],
            });
        },

        error: function (error) {

            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });

    return false;
}