var InvoiceID = 0;
var RemoteUW670_html = '';
function RemoreUW670_closepopup() {
    $('#RemoreUW670_dverror').modal('hide');
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


function RemoteUW670_submit() {
    alert(1);
    var month = document.getElementById("addinvoice670_month").value;
    var year = document.getElementById("addinvoice670_year").value;
    var invoicedate = document.getElementById("RemoteUW670_invoicedate").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUW670_NoOfLoans_canopy").value;
     alert(loancount);
    var duedate = document.getElementById("cm_DueDate670").value;
     alert(duedate);
    var invoiceamount = document.getElementById("RemoteUW670_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUW670_invoicedate").value;
     alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUW670");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }
    PageMethods.InsertRemoteUW670Invoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoreUW670_OnSuccess, RemoreUW670_OnError);
    //  PageMethods.Test1(invoicetype, sh_OnSuccess, sh_OnError);
    return false;
}

function addinvoice670_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoice670_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoice670_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoice670_year").append($("<option></option>").val(i).html(i));
    }
}

function RemoreUW670_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUW670_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUW670_dverror').modal('show');
        var month = document.getElementById("addinvoice670_month").value;
        var year = document.getElementById("addinvoice670_year").value;
        //alert(month);
        //alert(year);
        PageMethods.InsertRemoteUW670Excel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        return false;
    }
    else {
        document.getElementById("RemoreUW670_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUW670_errmsg").style.color = 'red';
        $('#RemoreUW670_dverror').modal('show');
        return false;
    }


    return false;
}

function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoice670_month").value;
    var year = document.getElementById("addinvoice670_year").value;
    alert(month);
    alert(year);
    if (result > 0) {
        document.getElementById("RemoreUW670_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUW670_dverror').modal('show');
        BindRmoteUW670AfterImport(InvoiceID, month, year);
        return false;
    }

    else {
        document.getElementById("RemoreUW670_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUW670_errmsg").style.color = 'red';
        $('#RemoreUW670_dverror').modal('show');
        return false;
    }
    return false;
}


function BindRmoteUW670AfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUW670_html = '';

    $.ajax({
        url: "AddInvoice670.aspx/VerifyRemoteUW670",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUW670_html += '<tr>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.WeekWorked) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Day) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Hours) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Rate) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Total) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.JobDescription) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.MemberName) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.UHours) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.URate) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Amount) + '</td>';
                RemoteUW670_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NVA) + '</td>';
                RemoteUW670_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUW670')) {
                addinvoice_stewartgrid.destroy();
            }
            $('#table_RemoteUW670 tbody').html(RemoteUW670_html);

            table_RemoteUW = $('#table_RemoteUW670').DataTable({
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

function RemoreUW670_OnError(error) {
    alert(error.responseText);
}