
var LauraMac_Summary_table;
var LauraMac_LoanWise_table;
var LauraMac_WorkOrders_table;
var LauraMac_ScriptWiseSummary_table;

function canopy_LauraMac_btnShowDetails() {

    var FromDate = document.getElementById("canopy_LauraMac_FromDate").value;
    var ToDate = document.getElementById("canopy_LauraMac_ToDate").value;

    if (FromDate == "") {
        alert("Please select From Date.");
        return false;
    }
    if (ToDate == "") {
        alert("Please select To Date.");
        return false;
    }

    if (FromDate != null && ToDate != null) {

        BindCanopy_LauraMac_SummaryGrid();
        BindCanopy_LauraMac_LoanWiseGrid(FromDate, ToDate);
        BindCanopy_LauraMac_WorkOrderGrid();
        BindCanopy_LauraMac_ScriptWiseSummaryGrid(FromDate, ToDate);
    }
}

function BindCanopy_LauraMac_SummaryGrid() {

    $('#load1').show();

    $.ajax({
        url: "LauraMacBillingReport.aspx/GetAllLauraMac",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LauraMac_Summary_table = $('#table_LauraMac_Summary').DataTable({
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
                    { data: 'Month' },
                    { data: 'Year' },
                    { data: 'InvoiceNo' },
                    { data: 'InvoiceDate' },
                    { data: 'Balance' },
                    { data: 'IsReconcileRemark' },
                    { data: 'Remark' },
                    { data: 'ProjectId' },
                    { data: 'IsApproved1Remark' },
                    { data: 'PayToLM' },
                    { data: 'IsApprovedRemark' },
                    { data: 'IsApprvoedName' },
                    { data: 'ApprovedDate' },
                    { data: 'IsPaidDate' },
                    { data: 'PaidRemark' }
                ],


                initComplete: function () {
                    $('#load1').hide();

                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-LauraMac Summary Billing Report', autoFilter: true,
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

function BindCanopy_LauraMac_LoanWiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "LauraMacBillingReport.aspx/GetLauraMacBillingLoanWiseDetails",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "', ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LauraMac_LoanWise_table = $('#table_LauraMac_LoanWise').DataTable({
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
                    { data: 'MM' },
                    { data: 'YY' },
                    { data: 'LoanNo' },
                    { data: 'InvoiceActivatedDate' },
                    { data: 'VendorInvoiceNumber' },
                    { data: 'Script' },
                    { data: 'CompletedDate' },
                    { data: 'MatchWihtLM' },
                    { data: 'TransactionID' },
                    { data: 'Duplicate' },
                    { data: 'BilledToClient' },
                    { data: 'BillingPeriod' },
                    { data: 'APSystemRemark' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-LauraMac Loan Wise Details', autoFilter: true,
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

function BindCanopy_LauraMac_WorkOrderGrid() {

    $('#load1').show();

    $.ajax({
        url: "LauraMacBillingReport.aspx/GetLauraMacBillingWorkOrdersDetails",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LauraMac_WorkOrders_table = $('#table_LauraMac_WorkOrders').DataTable({
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
                    { data: 'SrNo' },
                    { data: 'InvoiceNo' },
                    { data: 'WorkOrderNo' },
                    { data: 'WorkDate' },
                    { data: 'TimeSpent' },
                    { data: 'HourlyRate' },
                    { data: 'Cost' },
                    { data: 'Remark' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-LauraMac Billing Order Details', autoFilter: true,
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

function BindCanopy_LauraMac_ScriptWiseSummaryGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "LauraMacBillingReport.aspx/GetLauraMacBillingScriptChargesDetails",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "', ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LauraMac_ScriptWiseSummary_table = $('#table_LauraMac_ScriptWiseSummary').DataTable({
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
                    { data: 'MM' },
                    { data: 'YY' },
                    { data: 'ScriptName' },
                    { data: 'LoanNo' },
                    { data: 'PagePerScript' },
                    { data: 'DataPerScript' },
                    { data: 'TotalPages' },
                    { data: 'TotalFields' },
                    { data: 'TotalPageCost' },
                    { data: 'TotalFieldCost' },
                    { data: 'TotalCost' },
                    { data: 'CostPerLoan' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-LauraMac Billing Script Charges Details', autoFilter: true,
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

