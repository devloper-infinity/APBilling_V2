var InvoiceID;
var invrec_canopy;
var invrpt_html_IPS = ''

function blankForNull(s) {
    return s == "null" || s == null ? "" : s;
}


function BindIPSRportGrid() {

    $('#load1').show();
    invrpt_html_IPS = '';
    $.ajax({
        url: "InvoiceReport.aspx/GetAllIPSInvoice_Report",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {
                invrpt_html_IPS += '<tr>';
                invrpt_html_IPS += '<td class=""><div class="btn-group">';
                invrpt_html_IPS += '<div class="btn-group">';
                invrpt_html_IPS += '<div type="button" data-toggle="dropdown" aria-expanded="false"><i style="color: dodgerblue; font-size:14px;" class="uil fs-0 me-2 uil-cog"></i>';
                invrpt_html_IPS += '<span class="sr-only"></span></div><div class="dropdown-menu" role="menu" style="">';
                invrpt_html_IPS += '<a class="dropdown-item" href="#!" id="ShowRptInvoice" onclick="ia_ShowRptInvoices(' + value.InvoiceId + ',\'' + value.InvoiceType + '\');"><span style="color: brown;"><i class="uil fs-0 me-2 uil-search-alt"></i></span>&nbsp;&nbsp;View Loans</a>';
                invrpt_html_IPS += '<a class="dropdown-item" href="javascript:void(0);" onclick="DownloadInvoice(\'' + blankForNull(value.InvoiceId) + '\',\'' + value.InvoiceType + '\', \'' + blankForNull('IPS') + '\');"><span style="color: brown;"><i class="uil fs-0 me-2 uil-file-download"></i></span>&nbsp;&nbsp;Download Invoice</a> </div></div></td>';


                invrpt_html_IPS += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Reconcile) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Approval) + '</td>';
                invrpt_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Payment) + '</td>';
                invrpt_html_IPS += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invrpt_IPS')) {
                invrec_IPS.destroy();
            }
            $('#invrpt_IPS tbody').html(invrpt_html_IPS);
            //else
            invrec_IPS = $('#invrpt_IPS').DataTable({
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


function ia_ShowRptInvoices(InvoiceID, InvoiceType) {

    $('#popUpViewLoanDetailsrpt').modal('show');
    BindViewLoanDetails_Report(InvoiceID, InvoiceType);
    return false;
}


function BindViewLoanDetails_Report(InvoiceID, InvoiceType) {

    var columns = [];

   

    $.ajax({
        url: "InvoiceReport.aspx/GetAllInvoiceLoansDetailsReport",
        type: "POST",
        data: "{InvoiceID:" + InvoiceID + ", InvoiceType:'" + InvoiceType + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {

            if ($.fn.dataTable.isDataTable('#viewloanDetailsrpt_table')) {
                $('#viewloanDetailsrpt_table').DataTable().destroy();
            }

            dataArray = JSON.parse(data.d);
           
            $.each(dataArray[0], function (key, value) {

                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                columns.push(my_item);
            });
            $('#viewloanDetailsrpt_table').DataTable({
                dom: 'lBftp',
                destroy: true,
                paging: true,
                "autoWidth": false,
                select: true,
                processing: true,
                'select': {
                    'style': 'single'
                },
                "data": dataArray,
                "columns": columns,

                initComplete: function () {
                    $('#load1').hide();

                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: 'View Loan Detials', autoFilter: true,
                    },
                ],

            });
        }
    });
}