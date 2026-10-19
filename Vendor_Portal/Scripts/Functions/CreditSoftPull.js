
var CreditSoftPullSummary_table;
var CreditSoftPullDetails_table;

function BindIPS_CreditSoftPull_SummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "CreditSoftPull.aspx/GetAllCreditSoftPull_Summary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            CreditSoftPullSummary_table = $('#table_CreditSoftPullSummary').DataTable({
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
                 /*   { data: 'SrNo' },*/
                    { data: 'InvoiceType' },
                    { data: 'Month' },
                    { data: 'Year' },
                    { data: 'InvoiceNo' },
                    { data: 'InvoiceDate' },
                    { data: 'Balance' },
                    { data: 'Remark' },
                    { data: 'IsPaidDate' },
                    { data: 'PaymentRemark' }
                ],


                initComplete: function () {
                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Credit Soft Pull Summary', autoFilter: true,
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

function BindIPS_CreditSoftPull_DetailsGrid() {

    $('#load1').show();

    $.ajax({
        url: "CreditSoftPull.aspx/GetAllCreditSoftPull_Details",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            CreditSoftPullDetails_table = $('#table_CreditSoftPullDetails').DataTable({
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
                    { data: 'Month' },
                    { data: 'Year' },
                    { data: 'Date' },
                    { data: 'CustomerName' },
                    { data: 'CustomerNumber' },
                    { data: 'FileNo' },
                    { data: 'RefNo' },
                    { data: 'FirstName' },
                    { data: 'LastName' },
                    { data: 'Description' },
                    { data: 'Product' },
                    { data: 'User' },
                    { data: 'Payments' },
                    { data: 'Charges' },
                    { data: 'SysRemark' },
                    { data: 'VendorInvoiceNumber' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Credit Soft Pull Details', autoFilter: true,
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