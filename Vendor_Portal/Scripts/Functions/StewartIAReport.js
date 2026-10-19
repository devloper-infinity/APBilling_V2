
var StewartIASummary_table;
var StewartIADetails_table;

function BindCanopy_Stewart_SummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "StewartIAReport.aspx/GetAllStewartIA_Summary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            StewartIASummary_table = $('#table_StewartIASummary').DataTable({
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
                    { data: 'SrNo' },
                    { data: 'InvoiceType' },
                    { data: 'Month' },
                    { data: 'Year' },
                    { data: 'InvoiceNo' },
                    { data: 'InvoiceDate' },
                    { data: 'Balance' },
                    { data: 'Remark' },
                    { data: 'PaidDate' },
                    { data: 'PaidRemark' }
                ],

                
                initComplete: function () {
                    $('#load1').hide();
                
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy- StewartIA Summary', autoFilter: true,
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

function BindCanopy_Stewart_DetailsGrid() {

    $('#load1').show();

    $.ajax({
        url: "StewartIAReport.aspx/GetAllStewartIA_Details",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            StewartIADetails_table = $('#table_StewartIADetails').DataTable({
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
                    { data: 'InvoiceDate' },
                    { data: 'InvoiceNumber' },
                    { data: 'OrderDate' },
                    { data: 'CompleteDate' },
                    { data: 'BusinessDays' },
                    { data: 'Fee' },
                    { data: 'LoanNumber' },
                    { data: 'CaseNumber' },

                    { data: 'MatchedwithLM' },
                    { data: 'TransactionID' },
                    { data: 'Duplicate' },
                    { data: 'CompletedDate' },
                    { data: 'BilledToClient' },
                    { data: 'ClientBilling' },
                    { data: 'ClientBillingCost' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy- StewartIA Details', autoFilter: true,
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