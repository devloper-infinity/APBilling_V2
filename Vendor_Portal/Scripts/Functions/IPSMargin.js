var ips_margin_LoanWise_table;
var ips_margin_TypeWise_table;
var ips_margin_DealWise_table;
var ips_margin_ProjectWise_table;

function ips_margin_btnShowDetails() {

    var FromDate = document.getElementById("ips_margin_FromDate").value;
    var ToDate = document.getElementById("ips_margin_ToDate").value;

    if (FromDate == "") {
        alert("Please select From Date.");
        return false;
    }
    if (ToDate == "") {
        alert("Please select To Date.");
        return false;
    }

    if (FromDate != null && ToDate != null) {

        BindIPS_Margin_TypewiseGrid(FromDate, ToDate);
        BindIPS_Margin_ProjectwiseGrid(FromDate, ToDate);
        BindIPS_Margin_DealwiseGrid(FromDate, ToDate);
        BindIPS_Margin_LoanwiseGrid(FromDate, ToDate);
    }
}

function BindIPS_Margin_LoanwiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "IPSMargin.aspx/GetDataForMargin_LoanWise",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $('#table_ips_margin_LoanWise').DataTable({
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
                    { data: 'ProjectName' },
                    { data: 'CountColumn' },
                    { data: 'CreditServicing' },
                    { data: 'DealNo' },
                    { data: 'LoanNo' },
                    { data: 'OrderDate' },
                    { data: 'DueDate' },
                    { data: 'IsBilled' },
                    { data: 'SentToClient' },
                    { data: 'SciennaID' },
                    { data: 'SciennaCount' },
                    { data: 'SciennaCost' },
                    { data: 'SEBilled' },
                    { data: 'SciennaBillNo' },
                    { data: 'CECount' },
                    { data: 'CEID' },
                    { data: 'CE' },
                    { data: 'CEBilled' },
                    { data: 'CEBillNo' },
                    { data: 'TRIDCount' },
                    { data: 'TRID' },
                    { data: 'Review' },
                    { data: 'RemoteRateReview' },
                    { data: 'RemoteReviewer' },
                    { data: 'RemoteReviewerPaid' },
                    { data: 'QC' },
                    { data: 'RemoteRateQC' },
                    { data: 'RemoteQCer' },
                    { data: 'RemoteQCerPaid' },
                    { data: 'TotalCost' },
                    { data: 'ClientBillingInvoiceNumber' },
                    { data: 'ClientBillingCost' },
                    { data: 'NVA' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Margine Report - Loanwise', autoFilter: true,
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

function BindIPS_Margin_TypewiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "IPSMargin.aspx/GetDataForMargin_TypeWise",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ips_margin_TypeWise_table = $('#table_ips_margin_TypeWise').DataTable({
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
                    { data: 'CreditServicing' },
                    { data: 'CountColumn' },
                    { data: 'SciennaCount' },
                    { data: 'SciennaCost' },
                    { data: 'CECount' },
                    { data: 'CE' },
                    { data: 'TRIDCount' },
                    { data: 'TRID' },
                    { data: 'RemoteReviewer' },
                    { data: 'RemoteQCer' },
                    { data: 'TotalCost' },
                    { data: 'ClientBillingCost' },
                    { data: 'NVA' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Margine Report - Typewise', autoFilter: true,
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

function BindIPS_Margin_ProjectwiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "IPSMargin.aspx/GetDataForMargin_ProjectWise",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ips_margin_ProjectWise_table = $('#table_ips_margin_ProjectWise').DataTable({
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
                    { data: 'ProjectName' },
                    { data: 'CountColumn' },
                    { data: 'SciennaCount' },
                    { data: 'SciennaCost' },
                    { data: 'CECount' },
                    { data: 'CE' },
                    { data: 'TRIDCount' },
                    { data: 'TRID' },
                    { data: 'RemoteReviewer' },
                    { data: 'RemoteQCer' },
                    { data: 'TotalCost' },
                    { data: 'ClientBillingCost' },
                    { data: 'NVA' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Margine Report - Projectwise', autoFilter: true,
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

function BindIPS_Margin_DealwiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "IPSMargin.aspx/GetDataForMargin_DealWise",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            ips_margin_DealWise_table = $('#table_ips_margin_DealWise').DataTable({
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
                    { data: 'DealNo' },
                    { data: 'CountColumn' },
                    { data: 'SciennaCount' },
                    { data: 'SciennaCost' },
                    { data: 'CECount' },
                    { data: 'CE' },
                    { data: 'TRIDCount' },
                    { data: 'TRID' },
                    { data: 'RemoteReviewer' },
                    { data: 'RemoteQCer' },
                    { data: 'TotalCost' },
                    { data: 'ClientBillingCost' },
                    { data: 'NVA' }
                ],

                initComplete: function () {

                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'Margine Report - Dealwise', autoFilter: true,
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