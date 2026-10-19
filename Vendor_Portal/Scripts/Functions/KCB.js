var InvoiceID = 0;
var RemoteUWKCB_html = '';
function RemoreUWKCB_closepopup() {
    $('#RemoreUWKCB_dverror').modal('hide');
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

function addinvoiceKCB_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceKCB_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceKCB_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceKCB_year").append($("<option></option>").val(i).html(i));
    }
}
function RemoteUWKCB_submit() {
    alert(1);
    var month = document.getElementById("addinvoiceKCB_month").value;
    var year = document.getElementById("addinvoiceKCB_year").value;
    var invoicedate = document.getElementById("RemoteUWKCB_invoicedate").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUWKCB_NoOfLoans_canopy").value;
    alert(loancount);
    var duedate = document.getElementById("cm_DueDateKCB").value;
    alert(duedate);
    var invoiceamount = document.getElementById("RemoteUWKCB_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUWKCB_invoiceaNo_canopy").value;
    alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUWKCB");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }
    PageMethods.InsertRemoteUWKCBInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoreUWKCB_OnSuccess, RemoreUWKCB_OnError);
    return false;
}
function RemoreUWKCB_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWKCB_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWKCB_dverror').modal('show');
        var month = document.getElementById("addinvoiceKCB_month").value;
        var year = document.getElementById("addinvoiceKCB_year").value;
        //alert(month);
        //alert(year);
        PageMethods.InsertRemoteUWKCBExcel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        return false;
    }
    else {
        document.getElementById("RemoreUWKCB_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWKCB_errmsg").style.color = 'red';
        $('#RemoreUWKCB_dverror').modal('show');
        return false;
    }


    return false;
}
function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoiceKCB_month").value;
    var year = document.getElementById("addinvoiceKCB_year").value;
    alert(month);
    alert(year);
    if (result > 0) {
        document.getElementById("RemoreUWKCB_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUWKCB_dverror').modal('show');
        BindRmoteUWKCBAfterImport(InvoiceID, month, year);
        return false;
    }

    else {
        document.getElementById("RemoreUWKCB_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWKCB_errmsg").style.color = 'red';
        $('#RemoreUWKCB_dverror').modal('show');
        return false;
    }
    return false;
}

function BindRmoteUWKCBAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWKCB_html = '';

    $.ajax({
        url: "AddInvoiceKCB.aspx/VerifyRemoteUWKCB",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWKCB_html += '<tr>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Date) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.CustomerName) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.CustomerNumber) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.FileNo) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.RefNo) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.FirstName) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LastName) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Product) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.User) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Description) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Payments) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Charges) + '</td>';
                RemoteUWKCB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.SysRemark) + '</td>';
                RemoteUWKCB_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWKCB')) {
                table_RemoteUWKCB.destroy();
            }
            $('#table_RemoteUWKCB tbody').html(RemoteUWKCB_html);

            table_RemoteUWKCB = $('#table_RemoteUWKCB').DataTable({
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

function RemoreUWKCB_OnError(error) {
    alert(error.responseText);
}