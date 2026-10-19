var Canopy_margin_LoanWise_table;
var Canopy_margin_TypeWise_table;
var Canopy_margin_DealWise_table;
var Canopy_margin_ProjectWise_table;

function Canopy_margin_btnShowDetails() {

    var FromDate1 = document.getElementById("Canopy_margin_FromDate").value;
    var ToDate1 = document.getElementById("Canopy_margin_ToDate").value;

    alert(FromDate1);
    alert(ToDate1);

    if (FromDate1 == "") {
        alert("Please select From Date.");
        return false;
    }
    if (ToDate1 == "") {
        alert("Please select To Date.");
        return false;
    }

    if (FromDate1 != null && ToDate1 != null) {

        //BindCanopy_Margin_TypewiseGrid(FromDate, ToDate);
        //BindCanopy_Margin_ProjectwiseGrid(FromDate, ToDate);
        //BindCanopy_Margin_DealwiseGrid(FromDate, ToDate);
        BindCanopy_Margin_LoanwiseGrid(FromDate1, ToDate1);
    }
}

function BindCanopy_Margin_LoanwiseGrid(FromDate, ToDate) {

    $('#load1').show();

    $.ajax({
        url: "CanopyMargin.aspx/GetDataForMarginCanopy_LoanWise",
        type: "POST",
        dataType: "json",
        data: "{FromDate:'" + FromDate + "',ToDate:'" + ToDate + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $('#table_Canopy_margin_LoanWise').DataTable({
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
                    { data: 'ProjectName' },
                    { data: 'DealNo' },
                    { data: 'LoanNo' },
                    { data: 'OrderDate1' },
                    { data: 'DueDate' },
                    { data: 'IsBilled' },
                    { data: 'SentToClient' },

                    { data: 'SciennaCount' },
                    { data: 'SciennaCost' },
                    { data: 'SEBilled' },
                    { data: 'SciennaBillNo' },

                    { data: 'CEID' },
                    { data: 'CECount' },
                    { data: 'CE' },
                    { data: 'CEBilled' },
                    { data: 'CEBillNo' },

                    { data: 'FieldReviewCount' },
                    { data: 'FieldReview' },
                    { data: 'SEBilled' },
                    { data: 'SciennaBillNo' },

                    { data: 'AVMCount' },
                    { data: 'AVM' },
                    { data: 'SEBilled' },
                    { data: 'SciennaBillNo' },

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
                        extend: 'excelHtml5', title: 'Canopy Margine Report - Loanwise', autoFilter: true,
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