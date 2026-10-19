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

function RemoteUW667_submit() {
    var ddlinvoicetype = document.getElementById("RW667Process");
    var invoicetype = ddlinvoicetype.options[ddlinvoicetype.selectedIndex].value;
    if (invoicetype == "") {
        alert("Please select Process");
        return false;
    }

    var ddlinvoicetype1 = document.getElementById("RW667ProjectNo");
    var invoicetype1 = ddlinvoicetype1.options[ddlinvoicetype1.selectedIndex].value;
    if (invoicetype1 == "") {
        alert("Please select Project");
        return false;
    }

    var fromdate = document.getElementById("RW667FromDate").value;
    var Todate = document.getElementById("RW667ToDate").value;
    var DealNo = document.getElementById("RemoteUW667_invoiceamount_DealNo").value;
    

    PageMethods.InsertRemoteUWKCBInvoice(month, year, invoicedate, duedate, loancount, invoiceamount, invoicenumber, RemoreUWKCB_OnSuccess, RemoreUWKCB_OnError);
    return false;
}