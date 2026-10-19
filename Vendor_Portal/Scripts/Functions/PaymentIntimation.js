var InvoiceID;
var invrec_canopy;
var invrptPayment_html_IPS = ''

function blankForNull(s) {
    return s == "null" || s == null ? "" : s;
}


function BindIPSPayment_Canopy_RportGrid() {

    $('#load1').show();
    invrptPayment_html_IPS = '';
    $.ajax({
        url: "PaymentIntimation.aspx/GetAllIPSInvoicePaymentCanopy_Report",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {
                invrptPayment_html_IPS += '<tr>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.PayTo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Balance) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvRemark) + '</td>';

                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.TotalLoanAmount) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.PaymentDate) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.UTRNo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.BankNo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailID) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrptPayment_html_IPS += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invrptPaymentIntimation_IPS')) {
                invrptPaymentIntimation_IPS.destroy();
            }
            $('#invrptPaymentIntimation_IPS tbody').html(invrptPayment_html_IPS);
            //else
            invrec_IPS = $('#invrptPaymentIntimation_IPS').DataTable({
                dom: 'lBftip',
                scrollX: true,
                destroy: true,
                "paging": true,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                'select': {
                    'style': 'single'
                },

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

function BindIPSPaymentRportGrid() {

    $('#load1').show();
    invrptPayment_html_IPS = '';
    $.ajax({
        url: "PaymentIntimation.aspx/GetAllIPSInvoicePayment_Report",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {
                invrptPayment_html_IPS += '<tr>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.PayTo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Balance) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvRemark) + '</td>';
               
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.TotalLoanAmount) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.PaymentDate) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.UTRNo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.BankNo) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailID) + '</td>';
                invrptPayment_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrptPayment_html_IPS += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invrptPaymentIntimation_IPS')) {
                invrptPaymentIntimation_IPS.destroy();
            }
            $('#invrptPaymentIntimation_IPS tbody').html(invrptPayment_html_IPS);
            //else
            invrec_IPS = $('#invrptPaymentIntimation_IPS').DataTable({
                dom: 'lBftip',
                scrollX: true,
                destroy: true,
                "paging": true,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                'select': {
                    'style': 'single'
                },

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