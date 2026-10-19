
var SummaryWise_table;
var TypeWise_table;
var ProjectWise_table;
var DealWise_table;
var LoanWise_table;

function blankForNull(s) {
    return s == "null" || s == null ? "" : s;

}

function canopy_comp_btnShowDetails() {
    var FromDate = document.getElementById("canopy_comp_FromDate").value;
    var ToDate = document.getElementById("canopy_comp_ToDate").value;

    if (FromDate == "") {
        alert("Please select From Date.");
        return false;
    }
    if (ToDate == "") {
        alert("Please select To Date.");
        return false;
    }


    if (FromDate != null && ToDate != null) {

        BindCanopy_Compliance_SummaryGrid(FromDate, ToDate);
        BindCanopy_Compliance_TypeWiseGrid(FromDate, ToDate);
        BindCanopy_Compliance_ProjectWiseGrid(FromDate, ToDate);
        BindCanopy_Compliance_DealWiseGrid(FromDate, ToDate);
        BindCanopy_Compliance_LoanWiseGrid(FromDate, ToDate);
    }
}

function BindCanopy_Compliance_SummaryGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "ComplianceBillingReportCanopy.aspx/GetComplainceEaseSummary",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);


            SummaryWise_table = $('#table_SummaryWise').DataTable({
                dom: 'Bftip',
                destroy: true,
                orderCellsTop: true,
                scrollX: true,
                "paging": false,
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
                    { data: 'VendorInvoiceNumber' },
                    { data: 'InvoiceDate' },
                    { data: 'PaymentStatus' },
                    { data: 'PaidDate' },
                    { data: 'ComplianceAnalyzerCount' },
                    { data: 'CECost' },
                    { data: 'CETotal' },
                    { data: 'TRIDCount' },
                    { data: 'TRIDCost' },
                    { data: 'TRIDTotal' },
                    { data: 'ComplianceAnalyzerCount' },
                    { data: 'CECost' },
                    { data: 'CETotal' },
                    { data: 'TRIDCount' },
                    { data: 'TRIDCost' },
                    { data: 'TRIDTotal' },
                    { data: 'CostPerLoan' },
                    { data: 'TotalCost' },
                    { data: 'DuplicateLoans' },
                    { data: 'Difference' },
                    { data: 'PaybleToCE' },
                    { data: 'AmountPaid' },
                    { data: 'DeductionAmount' },
                    { data: 'NegativeMarginAmount' },
                    { data: 'AccRemark' }
                ],

                //columnDefs: [
                //    {
                //        targets: 0,
                //        "width": "45px",
                //        render: function (data, type, row, meta) {
                //            //    return '<a class="dropdown-item" href="#!" id="Actions" onclick="assetmaster_EditAsset(\'' + meta.row + '\');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a>';
                //         //   return '<a class="dropdown-item" href="#!" id="CanComSumarryActions" ><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a>';
                //        }
                //    }
                //],

                initComplete: function () {
                    $('#load1').hide();
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-Compliance Summary Billing Report', autoFilter: true, 
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

function BindCanopy_Compliance_TypeWiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "ComplianceBillingReportCanopy.aspx/GetComplainceEaseTypewiseDetails",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            TypeWise_table = $('#table_TypeWise').DataTable({
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
                    { data: 'Type' },
                    { data: 'ComplianceAnalyzerCount' },
                    { data: 'CETotal' },
                    { data: 'TRIDCount' },
                    { data: 'TRIDTotal' },
                    { data: 'DuplicateLoans' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-Compliance Type-Wise Billing Report', autoFilter: true,
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

function BindCanopy_Compliance_ProjectWiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "ComplianceBillingReportCanopy.aspx/GetComplainceEaseProjectwiseDetails",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ProjectWise_table = $('#table_ProjectWise').DataTable({
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
                    { data: 'ProjectName' },
                    { data: 'ComplianceAnalyzerCount' },
                    { data: 'CETotal' },
                    { data: 'TRIDCount' },
                    { data: 'TRIDTotal' },
                    { data: 'DuplicateLoans' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-Compliance Project-Wise Billing Report', autoFilter: true,
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

function BindCanopy_Compliance_DealWiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "ComplianceBillingReportCanopy.aspx/GetComplainceEaseDealwiseDetails",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            DealWise_table = $('#table_DealWise').DataTable({
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
                    { data: 'DealNumber' },
                    { data: 'ComplianceAnalyzerCount' },
                    { data: 'CETotal' },
                    { data: 'TRIDCount' },
                    { data: 'TRIDTotal' },
                    { data: 'DuplicateLoans' },
                    { data: 'ActualLoanCount' },
                    { data: 'BilledCount' },
                    { data: 'DifferenceBilling' }
                ],

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-Compliance Deal-Wise Billing Report', autoFilter: true,
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

function BindCanopy_Compliance_LoanWiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "ComplianceBillingReportCanopy.aspx/GetComplainceEaseLoanwiseDetails",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            LoanWise_table = $('#table_LoanWise').DataTable({
                dom: 'Btip',
                destroy: true,
                scrollX: true,
                orderCellsTop: true,
                "paging": true,
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
                    { data: 'LoanNo' },
                    { data: 'OrderDate' },
                    { data: 'AuditTimeStamp' },
                    { data: 'UserName' },
                    { data: 'DisclosureType' },
                    { data: 'CEID' },
                    { data: 'CERate' },
                    { data: 'TRate' },
                    { data: 'PaymentStatus' },
                    { data: 'CEPaidOn' },
                    { data: 'CEBillNo' },
                    { data: 'IsDuplicate1' },
                    { data: 'Lender' },
                    { data: 'BorrowerName' },
                    { data: 'City' },
                    { data: 'State' },
                    { data: 'Source' },
                    { data: 'SysRemark' },
                    { data: 'UserRemark' },
                    { data: 'ClientBillingPeriod' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Canopy-Compliance Loan-Wise Billing Report', autoFilter: true,
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


