var InvoiceID = 0;
var RemoteUWLaminr_html = '';
function RemoreUWLaminr_closepopup() {
    $('#RemoreUWLaminr_dverror').modal('hide');
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

function addinvoice1099_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceLaminr_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceLaminr_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceLaminr_year").append($("<option></option>").val(i).html(i));
    }
}
function RemoteUW1099_submit() {
    alert(1);
    var month = document.getElementById("addinvoice1099_month").value;
    var year = document.getElementById("addinvoice1099_year").value;
    var invoicedate = document.getElementById("RemoteUW1099_invoicedate_canopy").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUW1099_NoOfLoans_canopy").value;
    alert(loancount);
    var duedate = document.getElementById("RemoteUW1099_DueDate_canopy").value;
    alert(duedate);
    var invoiceamount = document.getElementById("RemoteUW1099_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUW1099_invoiceaNo_canopy").value;
    alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUW1099_canopy");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }
    PageMethods.InsertRemoteUW1099Invoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoreUW1099_OnSuccess, RemoreUW1099r_OnError);
    return false;
}
function RemoreUW1099_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUW1099_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWLaminr_dverror').modal('show');
        var month = document.getElementById("addinvoice1099_month").value;
        var year = document.getElementById("addinvoice1099_year").value;
        //alert(month);
        //alert(year);
        PageMethods.InsertRemoteUW1099Excel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        return false;
    }
    else {
        document.getElementById("RemoreUW1099_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUW1099_errmsg").style.color = 'red';
        $('#RemoreUW1099_dverror').modal('show');
        return false;
    }


    return false;
}
function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoice1099_month").value;
    var year = document.getElementById("addinvoice1099_year").value;
    alert(month);
    alert(year);
    if (result > 0) {
        document.getElementById("RemoreUW1099_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUW1099_dverror').modal('show');
        BindRmoteUW1099AfterImport(InvoiceID, month, year);
        return false;
    }

    else {
        document.getElementById("RemoreUW1099_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUW1099_errmsg").style.color = 'red';
        $('#RemoreUW1099_dverror').modal('show');
        return false;
    }
    return false;
}

function BindRmoteUW1099AfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWLaminr_html = '';

    $.ajax({
        url: "AddInvoiceRemoteUW1099.aspx/VerifyRemoteUWLaminr",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUW1099_html += '<tr>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BSmrtID) + '</td>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TimeStamp) + '</td>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoanNumber) + '</td>';
                RemoteUW1099_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.INVESTOR) + '</td>';
                RemoteUW1099_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUW1099')) {
                table_RemoteUW1099.destroy();
            }
            $('#table_RemoteUW1099 tbody').html(RemoteUW1099_html);

            table_RemoteUWLaminr = $('#table_RemoteUW1099').DataTable({
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

function RemoreUWLaminr_OnError(error) {
    alert(error.responseText);
}