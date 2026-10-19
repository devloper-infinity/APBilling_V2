
var LoanLogicSummary_table;
var LoanLogicDetails_table;


function BindCanopy_LoanLogic_SummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "LoanLogicReport.aspx/GetAllLoanLogic_Summary",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LoanLogicSummary_table = $('#table_LoanLogicSummary').DataTable({
                dom: 'ftip',
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
                  /*  { data: 'SrNo' },*/
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
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });

    return false;
}

function BindCanopy_LoanLogic_DetailsGrid() {

    $('#load1').show();

    $.ajax({
        url: "LoanLogicReport.aspx/GetAllLoanLogic_Details",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LoanLogicDetails_table = $('#table_LoanLogicDetails').DataTable({
                dom: 'ftip',
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
                    { data: 'ProjectMonth' },
                    { data: 'LoanNumber' },
                    { data: 'LMLoanNo' },
                    { data: 'PageCount' },
                    { data: 'DocCount' },
                    { data: 'DeliveryDate' },
                    { data: 'FeeType' },
                    { data: 'Quantity' },
                    { data: 'Amount' },
                    { data: 'MatchedwithLM' },
                    { data: 'TransactionID' },
                    { data: 'CompletedDate' },
                    { data: 'Duplicate' },
                    { data: 'BilledToClient' },
                    { data: 'ClientBilling' }

                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });

    return false;
}