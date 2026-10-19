var InvoiceID = 0;
var RemoteUWLoanLogics_html = '';
function RemoreUWLoanLogics_closepopup() {
    $('#RemoreUWLoanLogics_dverror').modal('hide');
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

function addinvoiceLoanLogics_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceLoanLogics_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceLoanLogics_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceLoanLogics_year").append($("<option></option>").val(i).html(i));
    }
}
function RemoteUWLoanLogics_submit() {
    alert(1);
    var month = document.getElementById("addinvoiceLoanLogics_month").value;
    var year = document.getElementById("addinvoiceLoanLogics_year").value;
    var invoicedate = document.getElementById("RemoteUWLoanLogics_invoicedate").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUWLoanLogics_NoOfLoans_canopy").value;
    alert(loancount);
    var duedate = document.getElementById("cm_DueDateLoanLogics").value;
    alert(duedate);
    var invoiceamount = document.getElementById("RemoteUWLoanLogics_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUWLoanLogics_invoiceaNo_canopy").value;
    alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUWLoanLogics");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }

    var ddlcompany = document.getElementById("RemoteUWLoanLogicsCompany");
    var strCompany = ddlcompany.options[ddlcompany.selectedIndex].value;
    if (strCompany == "") {
        alert("Please select Company");
        return false;
    }
    
    
    PageMethods.InsertRemoteUWLoanLogicsInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, strCompany ,RemoreUWLoanLogics_OnSuccess, RemoreUWLoanLogics_OnError);
    return false;
}
function RemoreUWLoanLogics_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWLoanLogics_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWLoanLogics_dverror').modal('show');
        var month = document.getElementById("addinvoiceLoanLogics_month").value;
        var year = document.getElementById("addinvoiceLoanLogics_year").value;
        //alert(month);
        //alert(year);
        PageMethods.InsertRemoteUWLoanLogicsExcel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        return false;
    }
    else {
        document.getElementById("RemoreUWLoanLogics_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWLoanLogics_errmsg").style.color = 'red';
        $('#RemoreUWLoanLogics_dverror').modal('show');
        return false;
    }


    return false;
}
function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoiceLoanLogics_month").value;
    var year = document.getElementById("addinvoiceLoanLogics_year").value;
    var ddlCompany = document.getElementById("RemoteUWLoanLogicsCompany");
    var strCompany = ddlinvoiceType.options[ddlCompany.selectedIndex].value;
    alert(month);
    alert(year);
    alert(strCompany);
    if (result > 0) {
        document.getElementById("RemoreUWLoanLogics_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUWLoanLogics_dverror').modal('show');
        if (strCompany == "Canopy") {

            BindRmoteUWLoanLogicsAfterImport(InvoiceID, month, year);
        }
        else {

            BindRmoteUWLoanLogicsAfterImportIPS(InvoiceID, month, year);
        }
        return false;
    }

    else {
        document.getElementById("RemoreUWLoanLogics_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWLoanLogics_errmsg").style.color = 'red';
        $('#RemoreUWLoanLogics_dverror').modal('show');
        return false;
    }
    return false;
}

function BindRmoteUWLoanLogicsAfterImportIPS(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWLoanLogics_html = '';

    $.ajax({
        url: "AddInvoiceLoanLogics.aspx/VerifyRemoteUWLoanLogics_IPS",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWLoanLogics_html += '<tr>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoanNumber) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.ProjectMonth) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LMLoanNo) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PageCount) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DocCount) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DeliveryDate) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.FeeType) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Quantity) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Amount) + '</td>';

                RemoteUWLoanLogics_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWLoanLogics')) {
                table_RemoteUWLoanLogics.destroy();
            }
            $('#table_RemoteUWLoanLogics tbody').html(RemoteUWLoanLogics_html);

            table_RemoteUWLoanLogics = $('#table_RemoteUWLoanLogics').DataTable({
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


function BindRmoteUWLoanLogicsAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWLoanLogics_html = '';

    $.ajax({
        url: "AddInvoiceLoanLogics.aspx/VerifyRemoteUWLoanLogics",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWLoanLogics_html += '<tr>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoanNumber) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.ProjectMonth) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LMLoanNo) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PageCount) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DocCount) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DeliveryDate) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.FeeType) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Quantity) + '</td>';
                RemoteUWLoanLogics_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Amount) + '</td>';
                
                RemoteUWLoanLogics_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWLoanLogics')) {
                table_RemoteUWLoanLogics.destroy();
            }
            $('#table_RemoteUWLoanLogics tbody').html(RemoteUWLoanLogics_html);

            table_RemoteUWLoanLogics = $('#table_RemoteUWLoanLogics').DataTable({
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

function RemoreUWLoanLogics_OnError(error) {
    alert(error.responseText);
}