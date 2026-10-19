var InvoiceID = 0;
var RemoteDD_html = '';
function parseDate(str) {
    var mdy = str.split('/');
    return new Date(mdy[2], mdy[0] - 1, mdy[1]);
}
function datediff(first, second) {
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
}
function blankForNull(s) {
    return s == "null" || s == null ? "" : s;
}


function BindSciennaSummaryData(Month, Year) {
    $('#load1').show();
    RemoteDD_html = '';

    $.ajax({
        url: "SciennaSummary.aspx/ViewSciennaSummary",
        type: "POST",
        dataType: "json",
        data: "{Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteDD_html += '<tr>';
                RemoteDD_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Client) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Project) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PendingLoans) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoansReviewed) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PerLoanUsageFees) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.UsageFees) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Total) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.SysRemark) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PendingLoans) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.SendRemark) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BilledRemark) + '</td>';
                RemoteDD_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_SciennaSummary')) {
                table_SciennaSummary.destroy();
            }
            $('#table_SciennaSummary tbody').html(RemoteDD_html);

            table_VendorDashbordSummary = $('#table_SciennaSummary').DataTable({
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