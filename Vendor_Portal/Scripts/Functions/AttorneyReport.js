var ips_Attorney_billingSummary_table;
var ips_Attorney_InvoiceSummary_table;
var ips_Attorney_Details_table;


function BindIPS_Attorney_BillingSummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "AttorneyReport.aspx/GetAllAttorneyDetails_BillingSummary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ips_Attorney_billingSummary_table = $('#table_ips_Attorney_billingSummary').DataTable({
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
                    /*   { data: 'SrNo' },*/
                    { data: 'InvoiceDate' },
                    { data: 'InvoiceNo' },
                    { data: 'VendorName' },
                    { data: 'BillAmount' },
                    { data: 'Description' },
                    { data: 'PaidAmount' },
                    { data: 'AccPaidAmount' },
                    { data: 'PaidDate' },
                    { data: 'Remark' },
                    { data: 'PaymentRemark' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Attorney Report - Billing Summary', autoFilter: true,
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

function BindIPS_Attorney_InvoiceSummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "AttorneyReport.aspx/GetAllAttorneyDetails_InvoiceSummary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ips_Attorney_InvoiceSummary_table = $('#table_ips_Attorney_InvoiceSummary').DataTable({
                dom: 'Bftip',
                destroy: true,
                //  scrollX: true,
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
                    { data: 'InvoiceType' },
                    { data: 'Month' },
                    { data: 'Year' },
                    { data: 'InvoiceNo' },
                    { data: 'InvoiceDate' },
                    { data: 'Balance' },
                    { data: 'Remark' }
                    /*   { data: 'SrNo' },*/
                 
                ],

                initComplete: function () {

                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Attorney Report- Invoice Summary', autoFilter: true,
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

function BindIPS_Attorney_DetailsGrid() {

    $('#load1').show();

    $.ajax({
        url: "AttorneyReport.aspx/GetAllAttorneyDetails",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ips_Attorney_Details_table = $('#table_ips_Attorney_Details').DataTable({
                dom: 'Bftip',
                destroy: true,
                //  scrollX: true,
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
                    /*   { data: 'SrNo' },*/
                    { data: 'InvoiceDate' },
                    { data: 'InvoiceNo' },
                    { data: 'Date' },
                    { data: 'TimeKeeper' },
                    { data: 'Hours' },
                    { data: 'Amount' },
                    { data: 'Description' },
                    { data: 'OprRemark' }
                ],

                initComplete: function () {
                 
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Attorney Report-Deails', autoFilter: true,
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
