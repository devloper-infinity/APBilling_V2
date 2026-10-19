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

function addinvoiceDD_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceDD_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceDD_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceDD_year").append($("<option></option>").val(i).html(i));
    }
}

function BindDashboardData(Month, Year) {
    $('#load1').show();
    RemoteDD_html = '';

    $.ajax({
        url: "Dashboard.aspx/ViewDashborddata",
        type: "POST",
        dataType: "json",
        data: "{Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteDD_html += '<tr>';
                RemoteDD_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Balance) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.isReconcile) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.isApproved) + '</td>';
                RemoteDD_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.isPaid) + '</td>';
                RemoteDD_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_VendorDashbordSummary')) {
                table_VendorDashbordSummary.destroy();
            }
            $('#table_VendorDashbordSummary tbody').html(RemoteDD_html);

            table_VendorDashbordSummary = $('#table_VendorDashbordSummary').DataTable({
                dom: 'lBftip',
                destroy: true,
                "paging": true,
                responsive: false,
                scrollX: true,
                "autoWidth": false,
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