var InvoiceID;
var invrec_canopy;
var invrpt_html_Canopy = ''

function blankForNull(s) {
    return s == "null" || s == null ? "" : s;
}


function ia_ShowRptInvoices_Canopy(InvoiceID, InvoiceType) {

    $('#popUpViewLoanDetailsrptCanopy').modal('show');
    BindViewLoanDetails_Canopy(InvoiceID, InvoiceType);
    return false;
}


function BindViewLoanDetails_Canopy(InvoiceID, InvoiceType) {

    var columns = [];

   

    $.ajax({
        url: "InvoiceReportCanopy.aspx/GetAllInvoiceLoansDetailsReport",
        type: "POST",
        data: "{InvoiceID:" + InvoiceID + ", InvoiceType:'" + InvoiceType + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {

            if ($.fn.dataTable.isDataTable('#viewloanDetailsrptCanopy_table')) {
                $('#viewloanDetailsrptCanopy_table').DataTable().destroy();
            }

            dataArray = JSON.parse(data.d);
           
            $.each(dataArray[0], function (key, value) {

                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                columns.push(my_item);
            });
            $('#viewloanDetailsrptCanopy_table').DataTable({
                dom: 'lBftp',
                destroy: true,
                paging: true,
                "autoWidth": true,
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

function BindCanopyReportGrid() {

    $('#load1').show();
    invrpt_html_Canopy = '';
    $.ajax({
        url: "InvoiceReportCanopy.aspx/GetAllIPSInvoiceCanopy_Report",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {
                invrpt_html_Canopy += '<tr>';
                invrpt_html_Canopy += '<td class=""><div class="btn-group">';
            
                invrpt_html_Canopy += '<div type="button" data-toggle="dropdown" aria-expanded="false"><i style="color: dodgerblue; font-size:14px;" class="uil fs-0 me-2 uil-cog"></i>';
                invrpt_html_Canopy += '<span class="sr-only"></span></div><div class="dropdown-menu" role="menu" style="">';
                invrpt_html_Canopy += '<a class="dropdown-item" href="#!" id="ShowRptInvoice" onclick="ia_ShowRptInvoices_Canopy(' + value.InvoiceId + ',\'' + value.InvoiceType + '\');"><span style="color: brown;"><i class="uil fs-0 me-2 uil-search-alt"></i></span>&nbsp;&nbsp;View Loans</a>';
                invrpt_html_Canopy += '<a class="dropdown-item" href="javascript:void(0);" onclick="DownloadInvoice(\'' + blankForNull(value.InvoiceId) + '\',\'' + value.InvoiceType + '\', \'' + blankForNull('canopy') + '\');"><span style="color: brown;"><i class="uil fs-0 me-2 uil-file-download"></i></span>&nbsp;&nbsp;Download Invoice</a> </div></div></td>';

                invrpt_html_Canopy += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Reconcile) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Approval) + '</td>';
                invrpt_html_Canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Payment) + '</td>';
                invrpt_html_Canopy += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invrpt_Canopy')) {
                invrpt_Canopy.destroy();
            }
            $('#invrpt_Canopy tbody').html(invrpt_html_Canopy);
            //else
            invrpt_Canopy = $('#invrpt_Canopy').DataTable({
                dom: 'lBftip',
                scrollX: true,
                scrollCollapse: true,
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