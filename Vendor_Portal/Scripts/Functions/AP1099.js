var InvoiceID = 0;
var RemoteUWKEB_html = '';

function addinvoice1099_BindYear() {
    var start = new Date().getFullYear();

    var select = document.getElementById("addinvoice1099_year");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#addinvoice1099_year").append($("<option></option>").val("").html("Select"));
    for (var i = start; i > start - 5; i--) {
        $("#addinvoice1099_year").append($("<option></option>").val(i).html(i));
    }
}

function RemoteUW1099_submit() {
    alert(1);
    var month = document.getElementById("addinvoice1099_month").value;
    var year = document.getElementById("addinvoice1099_year").value;
    var invoicedate = document.getElementById("RemoteUW1099_invoicedate").value;
    alert(invoicedate);
    var loancount = document.getElementById("RemoteUW1099_NoOfLoans_canopy").value;
    alert(loancount);
    var duedate = document.getElementById("cm_DueDateKEB").value;
    alert(duedate);
    var invoiceamount = document.getElementById("RemoteUWKEB_invoiceamount_canopy").value;
    alert(invoiceamount);
    var invoicenumber = document.getElementById("RemoteUW1099_invoiceaNo_canopy").value;
    alert(invoicenumber);

    var ddlinvoicetype = document.getElementById("RemoteUWKEB");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Remote UW");
        return false;
    }
    PageMethods.InsertRemoteUWKEBInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, invoicetype, RemoreUWKEB_OnSuccess, RemoreUWKEB_OnError);
    return false;
}