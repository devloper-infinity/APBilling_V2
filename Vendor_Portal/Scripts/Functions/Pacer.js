var InvoiceID = 0;
var RemoteUWPacer_html = '';
function RemoreUWPacer_closepopup() {
    $('#RemoreUWPacer_dverror').modal('hide');
}
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
function addinvoicePacer_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoicePacer_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoicePacer_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoicePacer_year").append($("<option></option>").val(i).html(i));
    }
}
function RemoteUWPacer_submit() {
    alert(1);
    var month = document.getElementById("addinvoicePacer_month").value;
    var year = document.getElementById("addinvoicePacer_year").value;
    var invoicedate = document.getElementById("RemoteUWPacer_invoicedate").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUWPacer_NoOfLoans_canopy").value;
    alert(loancount);
    var duedate = document.getElementById("cm_DueDatePacer").value;
    alert(duedate);
    var invoiceamount = document.getElementById("RemoteUWPacer_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUWPacer_invoiceaNo_canopy").value;
    alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUWPacer");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }
    PageMethods.InsertRemoteUWPacerInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoreUWPacer_OnSuccess, RemoreUWPacer_OnError);
    return false;
}


function RemoreUWPacer_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWPacer_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWPacer_dverror').modal('show');
        var month = document.getElementById("addinvoicePacer_month").value;
        var year = document.getElementById("addinvoicePacer_year").value;
        //alert(month);
        //alert(year);
        PageMethods.InsertRemoteUWPacerExcel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        return false;
    }
    else {
        document.getElementById("RemoreUWPacer_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWPacer_errmsg").style.color = 'red';
        $('#RemoreUWPacer_dverror').modal('show');
        return false;
    }


    return false;
}
function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoicePacer_month").value;
    var year = document.getElementById("addinvoicePacer_year").value;
    alert(month);
    alert(year);
    if (result > 0) {
        document.getElementById("RemoreUWPacer_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUWPacer_dverror').modal('show');
        BindRmoteUWPacerAfterImport(InvoiceID, month, year);
        return false;
    }

    else {
        document.getElementById("RemoreUWPacer_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWPacer_errmsg").style.color = 'red';
        $('#RemoreUWPacer_dverror').modal('show');
        return false;
    }
    return false;
}

function BindRmoteUWPacerAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWPacer_html = '';

    $.ajax({
        url: "AddInvoicePacer.aspx/VerifyRemoteUWPacer",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWPacer_html += '<tr>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Date) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.CustomerName) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.CustomerNumber) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.FileNo) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.RefNo) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.FirstName) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LastName) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Product) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.User) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Description) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Payments) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Charges) + '</td>';
                RemoteUWPacer_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.SysRemark) + '</td>';
                RemoteUWPacer_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWPacer')) {
                table_RemoteUWPacer.destroy();
            }
            $('#table_RemoteUWPacer tbody').html(RemoteUWPacer_html);

            table_RemoteUWPacer = $('#table_RemoteUWPacer').DataTable({
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

function uploadexcel_OnError(error) {
    alert(error.responseText);
}

function RemoreUWPacer_OnError(error) {
    alert(error.responseText);
}