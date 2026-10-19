var InvoiceID = 0;
var RemoteUWSales_html = '';
function RemoreUWSales_closepopup() {
    $('#RemoreUWSales_dverror').modal('hide');
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

function RemoteUWSales_submit() {
    var month = document.getElementById("addinvoiceSales_month").value;
    var year = document.getElementById("addinvoiceSales_year").value;
    var invoicedate = document.getElementById("RemoteUWSales_invoicedate").value;
    
    var loancount = document.getElementById("RemoteUWSales_NoOfLoans_canopy").value;
    
    var duedate = document.getElementById("cm_DueDateSales").value;
    
    var invoiceamount = document.getElementById("RemoteUWSales_invoiceamount_canopy").value;
    
    var invoicenumber = document.getElementById("RemoteUWSales_invoiceaNo_canopy").value;
    var ddlinvoicetype = document.getElementById("RemoteUWSales");

    var Rate = document.getElementById("RemoteUWSales_Rate_canopy").value;


    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select InvoiceType");
        return false;
    }
    PageMethods.InsertRemoteUWSalesInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, invoicetype,Rate,RemoreUWSales_OnSuccess, RemoreUWSales_OnError);
    return false;
}

function addinvoiceSales_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoiceSales_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoiceSales_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoiceSales_year").append($("<option></option>").val(i).html(i));
    }
}

function RemoreUWSales_OnSuccess(result) {

    alert(result);

    InvoiceID = result;
    alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWSales_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWSales_dverror').modal('show');
        //var month = document.getElementById("addinvoiceSales_month").value;
        //var year = document.getElementById("addinvoiceSales_year").value;
        //var ddlinvoicetype = document.getElementById("RemoteUWSales");
        //var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
        ////alert(month);
        ////alert(year);

        //if (invoicetype == 'TrueResource')
        //{
        //    PageMethods.InsertRemoteUWCanopuSalesExcel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        //}
        //else {
        //    PageMethods.InsertRemoteUWSalesExcel(result, month, year, uploadexcel_OnSuccess, uploadexcel_OnError);
        //}
        return false;
    }
    else {
        document.getElementById("RemoreUWSales_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWSales_errmsg").style.color = 'red';
        $('#RemoreUWSales_dverror').modal('show');
        return false;
    }


    return false;
}


function uploadexcel_OnSuccess(result) {
    var month = document.getElementById("addinvoiceSales_month").value;
    var year = document.getElementById("addinvoiceSales_year").value;
    var ddlinvoicetype = document.getElementById("RemoteUWSales");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    
    if (result > 0) {
        document.getElementById("RemoreUWSales_errmsg").innerHTML = "Excel data added successfully!";
        $('#RemoreUWSales_dverror').modal('show');

        if (invoicetype == 'TrueResource') {
            BindRmoteUWSalesAfterImport(InvoiceID, month, year);
        }
        else {
                BindRmoteUWSalesAfterImport(InvoiceID, month, year);
        }
        return false;
    }

    else {
        document.getElementById("RemoreUWSales_errmsg").innerHTML = "Oops! Error occured while adding invoice. Please contact administrator!";
        document.getElementById("RemoreUWSales_errmsg").style.color = 'red';
        $('#RemoreUWSales_dverror').modal('show');
        return false;
    }
    return false;
}


function BindRmoteUWSalesAfterImport(InvID, Month, Year) {
    $('#load1').show();
    RemoteUWSales_html = '';

    $.ajax({
        url: "AddInvoiceSales.aspx/VerifyRemoteUWSales",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ", Month:'" + Month + "', Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//

            $.each(dataArray, function (index, value) {
                RemoteUWSales_html += '<tr>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Date) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.StartTime) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EndTime) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BreakTime) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TotalTime) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.ClientNo) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DealNo) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TaskName) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Target) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoansReviewed) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoansCorrectionMade) + '</td>';
                RemoteUWSales_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoErrorsFiles) + '</td>';
                RemoteUWSales_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_RemoteUWSales')) {
                table_RemoteUWSales.destroy();
            }
            $('#table_RemoteUWSales tbody').html(RemoteUWSales_html);

            table_RemoteUWSales = $('#table_RemoteUWSales').DataTable({
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

function RemoreUWSales_OnError(error) {
    alert(error.responseText);
}