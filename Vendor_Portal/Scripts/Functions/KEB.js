var InvoiceID = 0;
var RemoteUWKEB_html = '';
function RemoreUWKEB_closepopup() {
    $('#RemoreUWKEB_dverror').modal('hide');
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

function RemoteUWKEB_submit() {

    var month = document.getElementById("addinvoiceKEB_month").value;
    var year = document.getElementById("addinvoiceKEB_year").value;
    var invoicedate = document.getElementById("RemoteUWKEB_invoicedate").value;
    var loancount = document.getElementById("RemoteUWKEB_NoOfLoans_canopy").value;
    var duedate = document.getElementById("cm_DueDateKEB").value;
    var invoiceamount = document.getElementById("RemoteUWKEB_invoiceamount_canopy").value;
    var invoicenumber = document.getElementById("RemoteUWKEB_invoiceaNo_canopy").value;
    var invoicepdf = document.getElementById("RemoteUWKEBInvoice").value;
    var ddlinvoicetype = document.getElementById("RemoteUWKEB");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    function showCustomAlert(message) {
        Swal.fire({
            html: '<h6 class="font-weight-bold text-dark m-0">' + message + '</h6>',
            showConfirmButton: true,
            confirmButtonText: 'Okay',
            buttonsStyling: false,
            width: '380px',
            customClass: {
                popup: 'modal-content shadow-sm',
                confirmButton: 'btn btn-primary-custom btn-sm px-4'
            },
            background: '#fff',
            backdrop: 'rgba(0,0,0,0.4)'
        });
    }

    // 1. Check if loancount is 0 or less
    if (Number(loancount) <= 0 && loancount !== "") {
        showCustomAlert("No Of Hours/Loans must be greater than 0");
        return false;
    }

    // 2. Check if invoiceamount is 0 or less
    if (Number(invoiceamount) <= 0 && invoiceamount !== "") {
        showCustomAlert("Invoice Amount must be greater than 0");
        return false;
    }
    var missingFields = [];
    if (invoicedate === "") { missingFields.push("Invoice Date"); }
    if (loancount === "") { missingFields.push("No Of Hours/Loans"); }
    if (duedate === "") { missingFields.push("Due Date"); }
    if (invoiceamount === "") { missingFields.push("Invoice Amount"); }
    if (invoicenumber === "") { missingFields.push("Invoice No"); }
    if (invoicepdf === "") { missingFields.push("Invoice PDF"); }
    if (invoicetype === "") { missingFields.push("Vendor"); }
    if (missingFields.length > 0) {
        var errorMessage = "Please fill in the following required fields:<br><br>• " + missingFields.join("<br>• ");
        document.getElementById("RemoreUWKEB_errmsg").innerHTML = errorMessage;
        $('#RemoreUWKEB_dverror').modal('show');
        return false;
    }
    document.getElementById("load1").style.display = 'block'; 

    PageMethods.InsertRemoteUWKEBInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, invoicetype,RemoreUWKEB_OnSuccess, RemoreUWKEB_OnError);
    return false;
}

function addinvoiceKEB_BindYear() {
    document.getElementById("addinvoiceKEB_month").focus();
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceKEB_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceKEB_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceKEB_year").append($("<option></option>").val(i).html(i));
    }
}

function RemoreUWKEB_OnSuccess(result) {
    document.getElementById("load1").style.display = 'none';
    //alert(result);

    InvoiceID = result;
    //alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWKEB_errmsg").innerHTML = "Invoice added successfully!";

        var month = document.getElementById("addinvoiceKEB_month").value;
        var year = document.getElementById("addinvoiceKEB_year").value;
        var ddlinvoicetype = document.getElementById("RemoteUWKEB");
        var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;

        $("#addinvoiceKEB_month").val("").trigger('change');
        $("#RemoteUWKEB_invoiceaNo_canopy").val("");
        $("#RemoteUWKEB_NoOfLoans_canopy").val("");
        $("#RemoteUWKEB_invoicedate").val("");
        $("#RemoteUWKEBInvoice").val("");

        $("#addinvoiceKEB_year").val("");
        $("#RemoteUWKEB_invoiceamount_canopy").val("");
        $("#cm_DueDateKEB").val("");
        $("#RemoteUWKEB").val("").trigger('change');
      //  $("#RemoteUWKEBInvoiceExcel").val("");



        $('#RemoreUWKEB_dverror').modal('show');
        $('#RemoreUWKEB_dverror').one('hidden.bs.modal', function () {

        document.getElementById("load1").style.display = 'block'; 

        //alert(month);
        //alert(year);

       // alert(invoicetype);
            var excelInput = document.getElementById("RemoteUWKEBInvoiceExcel");
            if (excelInput.files.length === 0) {
                document.getElementById("load1").style.display = 'none';
                return false;

            }

            if (invoicetype == 'KEB' || invoicetype == 'LynnHott' || invoicetype == 'LucyBeltran' || invoicetype == 'CoreyDaise')
        {
            PageMethods.InsertRemoteUWKEBExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);
            
        }
        else if (invoicetype == 'Compliance')
        {
            PageMethods.InsertRemoteUWComplianceExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

        }
        else if (invoicetype == 'LauraMac') {
            PageMethods.InsertLauraMacExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

        }
        else if (invoicetype == 'LoanLogics') {
            PageMethods.InsertLoanLogicsExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

        }
        else if (invoicetype == 'KCB') {
            PageMethods.InsertKCBExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

        }
        else if (invoicetype == 'Pacer') {
            PageMethods.InsertPacerExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

        }
        else if (invoicetype == 'Magna') {
            PageMethods.InsertRemoteUWComplianceExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

        }
            else if (invoicetype == 'Attorney') {
                PageMethods.InsertAttorneyExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);

            }
        else
    
        {
            PageMethods.InsertRemoteUWCanopuKEBExcel(result, month, year, uploadIPSexcel_OnSuccess, uploadexcel_OnError);
         }
        });

        return false;
       
    }
    else {
        document.getElementById("RemoreUWKEB_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWKEB_errmsg").style.color = 'red';
        $('#RemoreUWKEB_dverror').modal('show');
        return false;
    }


    return false;
}


function uploadIPSexcel_OnSuccess(result) {
    document.getElementById("load1").style.display = 'none'; 
    var month = document.getElementById("addinvoiceKEB_month").value;
    var year = document.getElementById("addinvoiceKEB_year").value;
    var ddlinvoicetype = document.getElementById("RemoteUWKEB");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    //alert(month);
    //alert(year);
    if (result == "1") {
        document.getElementById("RemoreUWKEB_errmsg").innerHTML = "Excel data added successfully!";
        $("#RemoteUWKEBInvoiceExcel").val("");
        $('#RemoreUWKEB_dverror').modal('show');

        if (invoicetype == 'TrueResource') {
            BindRmoteUWKEBAfterImport(InvoiceID, month, year);
        }
        else {
                BindRmoteUWKEBAfterImport(InvoiceID, month, year);
        }
        return false;
    }

    else {
        document.getElementById("RemoreUWKEB_errmsg").innerHTML = result;
        document.getElementById("RemoreUWKEB_errmsg").style.color = 'red';
        $('#RemoreUWKEB_dverror').modal('show');
        return false;
    }
    return false;
}


function BindRmoteUWKEBAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWKEB_html = '';

    $.ajax({
        url: "AddInvoiceKEB.aspx/VerifyRemoteUWKEB",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWKEB_html += '<tr>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Date) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.StartTime) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EndTime) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BreakTime) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TotalTime) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.ClientNo) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DealNo) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TaskName) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Target) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoansReviewed) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoansCorrectionMade) + '</td>';
                RemoteUWKEB_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoErrorsFiles) + '</td>';
                RemoteUWKEB_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWKEB')) {
                table_RemoteUWKEB.destroy();
            }
            $('#table_RemoteUWKEB tbody').html(RemoteUWKEB_html);

            table_RemoteUWKEB = $('#table_RemoteUWKEB').DataTable({
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

function RemoreUWKEB_OnError(error) {
    alert(error.responseText);
    document.getElementById("load1").style.display = 'none';
}