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

function addinvoiceLaminr_BindYear() {
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
function RemoteUWLaminr_submit() {
    
    var RemoteUW = document.getElementById("RemoteUWLaminr").value;

    var month = document.getElementById("addinvoiceLaminr_month").value;
    var year = document.getElementById("addinvoiceLaminr_year").value;
    var invoicedate = document.getElementById("RemoteUWLaminr_invoicedate").value;
  
    var loancount = document.getElementById("RemoteUWLaminr_NoOfLoans_canopy").value;
   
    var duedate = document.getElementById("cm_DueDateLaminr").value;
    
    var invoiceamount = document.getElementById("RemoteUWLaminr_invoiceamount_canopy").value;
  
    var invoicenumber = document.getElementById("RemoteUWLaminr_invoiceaNo_canopy").value;
    var invoicepdf = document.getElementById("RemoteUWLaminrInvoice").value;

    var ddlinvoicetype = document.getElementById("RemoteUWLaminr");
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
        showCustomAlert("No Of Loans must be greater than 0");
        return false;
    }

    // 2. Check if invoiceamount is 0 or less
    if (Number(invoiceamount) <= 0 && invoiceamount !== "") {
        showCustomAlert("Invoice Amount must be greater than 0");
        return false;
    }
    var missingFields = [];
    if (invoicedate === "") { missingFields.push("Invoice Date"); }
    if (loancount === "") { missingFields.push("No Of Loans"); }
    if (duedate === "") { missingFields.push("Due Date"); }
    if (invoiceamount === "") { missingFields.push("Invoice Amount"); }
    if (invoicenumber === "") { missingFields.push("Invoice No"); }
    if (invoicepdf === "") { missingFields.push("Invoice PDF"); }

    if (invoicetype === "") { missingFields.push("Vendor"); }

    if (missingFields.length > 0) {
        var errorMessage = "Please fill in the following required fields:<br><br>• " + missingFields.join("<br>• ");
        document.getElementById("RemoreUWLaminr_errmsg").innerHTML = errorMessage;
        $('#RemoreUWLaminr_dverror').modal('show');
        return false;
    }

    PageMethods.InsertRemoteUWLaminrInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoteUW, RemoreUWLaminr_OnSuccess, RemoreUWLaminr_OnError);
    return false;
}
function RemoreUWLaminr_OnSuccess(result) {

    InvoiceID = result;
 
    if (result > 0) {
        document.getElementById("RemoreUWLaminr_errmsg").style.color = 'green';
        document.getElementById("RemoreUWLaminr_errmsg").innerHTML = "Invoice added successfully!";
        $("#addinvoiceLaminr_month").val("").trigger('change');
        $("#RemoteUWLaminr_invoiceaNo_canopy").val("");
        $("#RemoteUWLaminr_NoOfLoans_canopy").val("");
        $("#RemoteUWLaminr_invoicedate").val("");
        $("#RemoteUWLaminrInvoice").val("");

        $("#addinvoiceLaminr_year").val("");
        $("#RemoteUWLaminr_invoiceamount_canopy").val("");
        $("#cm_DueDateLaminr").val("");
        $("#RemoteUWLaminr").val("").trigger('change');
       // $("#RemoteUWLaminrInvoiceExcel").val("");

        $('#RemoreUWLaminr_dverror').modal('show');

        $('#RemoreUWLaminr_dverror').one('hidden.bs.modal', function () {
            document.getElementById("load1").style.display = 'block';
        var month = document.getElementById("addinvoiceLaminr_month").value;
        var year = document.getElementById("addinvoiceLaminr_year").value;
        var RemoteUW = document.getElementById("RemoteUWLaminr").value; 
            var excelInput = document.getElementById("RemoteUWLaminrInvoiceExcel");
            if (excelInput.files.length === 0) {       
                document.getElementById("load1").style.display = 'none';
                return false; 

            }
        PageMethods.InsertRemoteUWLaminrExcel(result, month, year, RemoteUW, uploadexcellaminar_OnSuccess, uploadexcel_OnError);

        });
    }
    else {
        document.getElementById("RemoreUWLaminr_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWLaminr_errmsg").style.color = 'red';
        $('#RemoreUWLaminr_dverror').modal('show');
        return false;

    }

    return false;
}
function uploadexcellaminar_OnSuccess(result) {
    document.getElementById("load1").style.display = 'none';

    var month = document.getElementById("addinvoiceLaminr_month").value;
    var year = document.getElementById("addinvoiceLaminr_year").value;
    var ddlinvoicetype = document.getElementById("RemoteUWLaminr");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (result > 0) {
        document.getElementById("RemoreUWLaminr_errmsg").innerHTML = "Excel data added successfully!";
        $("#RemoteUWLaminrInvoiceExcel").val("");
        $('#RemoreUWLaminr_dverror').modal('show');

        if (invoicetype == 'Laminr') {
            BindRmoteUWLaminrAfterImport(InvoiceID, month, year);
        }
        else if (invoicetype == 'LoanLogics')
        {
            BindRmoteUWLoanLogicsImport(InvoiceID, month, year);

        }
        
        return false;
    }
    else if (result == 0) {
       
        return false;
    }
    else {
        document.getElementById("RemoreUWLaminr_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWLaminr_errmsg").style.color = 'red';
        $('#RemoreUWLaminr_dverror').modal('show');
        return false;
    }
    return false;
}

function BindRmoteUWLaminrAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWLaminr_html = '';

    $.ajax({
        url: "AddInvoiceLaminr.aspx/VerifyRemoteUWLaminr",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWLaminr_html += '<tr>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BSmrtID) + '</td>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TimeStamp) + '</td>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoanNumber) + '</td>';
                RemoteUWLaminr_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.INVESTOR) + '</td>';
                RemoteUWLaminr_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWLaminr')) {
                table_RemoteUWLaminr.destroy();
            }
            $('#table_RemoteUWLaminr tbody').html(RemoteUWLaminr_html);

            table_RemoteUWLaminr = $('#table_RemoteUWLaminr').DataTable({
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



function BindRmoteUWLoanLogicsImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWLaminr_html = '';

    $.ajax({
        url: "AddInvoiceLaminr.aspx/VerifyRemoteUWLoanLogics",
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
                table_RemoteUWLaminr.destroy();
            }
            $('#table_RemoteUWLoanLogics tbody').html(RemoteUWLaminr_html);

            table_RemoteUWLaminr = $('#table_RemoteUWLoanLogics').DataTable({
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