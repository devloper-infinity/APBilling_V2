var InvoiceID = 0;
var RemoteUWAttorney_html = '';
function RemoreUWAttorney_closepopup() {
    $('#RemoreUWAttorney_dverror').modal('hide');
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

function addinvoiceAttorney_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceAttorney_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceAttorney_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceAttorney_year").append($("<option></option>").val(i).html(i));
    }
}
function RemoteUWAttorney_submit() {
    alert(1);
    var month = document.getElementById("addinvoiceAttorney_month").value;
    var year = document.getElementById("addinvoiceAttorney_year").value;
    var invoicedate = document.getElementById("RemoteUWAttorney_invoicedate").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUWAttorney_NoOfLoans_canopy").value;
    alert(loancount);
    var duedate = document.getElementById("cm_DueDateAttorney").value;
    alert(duedate);
    var invoiceamount = document.getElementById("RemoteUWAttorney_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUWAttorney_invoiceaNo_canopy").value;
    alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUWAttorney");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }
    PageMethods.InsertRemoteUWAttorneyInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoreUWAttorney_OnSuccess, RemoreUWAttorney_OnError);
    return false;
}
function RemoreUWAttorney_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWAttorney_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWAttorney_dverror').modal('show');
        var month = document.getElementById("addinvoiceAttorney_month").value;
        var year = document.getElementById("addinvoiceAttorney_year").value;
        //alert(month);
        //alert(year);
        PageMethods.InsertRemoteUWAttorneyExcel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        return false;
    }
    else {
        document.getElementById("RemoreUWAttorney_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWAttorney_errmsg").style.color = 'red';
        $('#RemoreUWAttorney_dverror').modal('show');
        return false;
    }


    return false;
}
function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoiceAttorney_month").value;
    var year = document.getElementById("addinvoiceAttorney_year").value;
    alert(month);
    alert(year);
    if (result > 0) {
        document.getElementById("RemoreUWAttorney_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUWAttorney_dverror').modal('show');
        BindRmoteUWAttorneyAfterImport(InvoiceID, month, year);
        return false;
    }

    else {
        document.getElementById("RemoreUWAttorney_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWAttorney_errmsg").style.color = 'red';
        $('#RemoreUWAttorney_dverror').modal('show');
        return false;
    }
    return false;
}

function BindRmoteUWAttorneyAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWAttorney_html = '';

    $.ajax({
        url: "AddInvoiceBlankRome.aspx/VerifyRemoteUWAttorney",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWAttorney_html += '<tr>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Date) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TimeKeeper) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Hours) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Amount) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Description) + '</td>';
                RemoteUWAttorney_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.OprRemark) + '</td>';
                RemoteUWAttorney_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWAttorney')) {
                table_RemoteUWAttorney.destroy();
            }
            $('#table_RemoteUWAttorney tbody').html(RemoteUWAttorney_html);

            table_RemoteUWAttorney = $('#table_RemoteUWAttorney').DataTable({
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

function RemoreUWAttorney_OnError(error) {
    alert(error.responseText);
}