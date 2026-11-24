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
    //var month = document.getElementById("addinvoiceSales_month").value;
   // var year = document.getElementById("addinvoiceSales_year").value;
    var invoicedate = document.getElementById("RemoteUWSales_invoicedate").value;   
    //var loancount = document.getElementById("RemoteUWSales_NoOfLoans_canopy").value;   
    var duedate = document.getElementById("cm_DueDateSales").value;  
    var invoiceamount = document.getElementById("RemoteUWSales_invoiceamount_canopy").value;  
    var invoicenumber = document.getElementById("RemoteUWSales_invoiceaNo_canopy").value;
    //var ddlinvoicetype = document.getElementById("RemoteUWSales");
   // var Rate = document.getElementById("RemoteUWSales_Rate_canopy").value;

    var VendorName = $("#RemoteUWSales_VendorName option:selected").text();
    var Invoicesales_vendortype = document.getElementById("Invoicesales_vendortype").value;

    var Invoicesales_ToCompany = document.getElementById("Invoicesales_ToCompany").value;
    var Invoicesales_EmailAddress = document.getElementById("Invoicesales_EmailAddress").value;

    var RemoteUWSales_ServicePeriodFrom = document.getElementById("RemoteUWSales_ServicePeriodFrom").value;
    var RemoteUWSales_ServicePeriodTo = document.getElementById("RemoteUWSales_ServicePeriodTo").value;

    var saleinvoice_InvoiceCategory = document.getElementById("saleinvoice_InvoiceCategory").value;
    var RemoteUWSales_InvoiceDescription = document.getElementById("RemoteUWSales_InvoiceDescription").value;

    var RemoteUWSales_PaymentTerms = document.getElementById("RemoteUWSales_PaymentTerms").value;
    var Invoicesales_PaymentMethod = document.getElementById("Invoicesales_PaymentMethod").value;
    var RemoteUWSales_BankAccount = document.getElementById("RemoteUWSales_BankAccount").value;
    var RemoteUWSales_PaymentFromCompany = document.getElementById("RemoteUWSales_PaymentFromCompany").value;


   
    if ( VendorName === "Select" || VendorName === "") {
        showAlert("Please select Vendor Name.");
        return false;
    }
    if (invoicedate === "") {
        showAlert("Please select Invoice Date.");
        return false;
    }
    if (duedate === "") {
        showAlert("Please select Due Date.");
        return false;
    }
    if (invoiceamount === "") {
        showAlert("Please enter a valid Invoice Amount.");
        return false;
    }
    if (invoicenumber === "") {
        showAlert("Please enter Invoice Number.");
        return false;
    }
    if (Invoicesales_vendortype === "" || Invoicesales_vendortype === "0") {
        showAlert("Please select Vendor Type.");
        return false;
    }
    if (Invoicesales_ToCompany === "") {
        showAlert("Please enter To Company.");
        return false;
    }
    if (Invoicesales_EmailAddress === "") {
        showAlert("Please enter Email Address.");
        return false;
    }
    if (RemoteUWSales_ServicePeriodFrom === "") {
        showAlert("Please select Service Period From date.");
        return false;
    }
    if (RemoteUWSales_ServicePeriodTo === "") {
        showAlert("Please select Service Period To date.");
        return false;
    }
    if (saleinvoice_InvoiceCategory === "" || saleinvoice_InvoiceCategory === "0") {
        showAlert("Please select Invoice Category.");
        return false;
    }
    if (RemoteUWSales_InvoiceDescription === "") {
        showAlert("Please enter Invoice Description.");
        return false;
    }
    if (RemoteUWSales_PaymentTerms === "" || RemoteUWSales_PaymentTerms === "0") {
        showAlert("Please select Payment Terms.");
        return false;
    }
    if (Invoicesales_PaymentMethod === "" || Invoicesales_PaymentMethod === "0") {
        showAlert("Please select Payment Method.");
        return false;
    }
    if (RemoteUWSales_BankAccount === "" ) {
        showAlert("Please select Bank Account.");
        return false;
    }
    if (RemoteUWSales_PaymentFromCompany === "" || RemoteUWSales_PaymentFromCompany === "0") {
        showAlert("Please select Payment From Company.");
        return false;
    }
  
    $('#load1').show();
    PageMethods.InsertRemoteUWSalesInvoice(
       
      
        invoicedate,
        duedate,
       
        invoiceamount,
        invoicenumber,
       
       
        VendorName,
        Invoicesales_vendortype,
        Invoicesales_ToCompany,
        Invoicesales_EmailAddress,
        RemoteUWSales_ServicePeriodFrom,
        RemoteUWSales_ServicePeriodTo,
        saleinvoice_InvoiceCategory,
        RemoteUWSales_InvoiceDescription,
        RemoteUWSales_PaymentTerms,
        Invoicesales_PaymentMethod,
        RemoteUWSales_BankAccount,
        RemoteUWSales_PaymentFromCompany,
        RemoreUWSales_OnSuccess,
        RemoreUWSales_OnError
    );
    return false;
}

function RemoreUWSales_OnSuccess(result) {
    $('#load1').hide();

   // alert(result);

    InvoiceID = result;
    //alert(InvoiceID);

    if (result > 0) {
        document.getElementById("RemoreUWSales_errmsg").innerHTML = "Invoice added successfully!";
        $('#RemoreUWSales_dverror').modal('show');
        clearRemoteUWSalesForm();
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
    $('#load1').hide();
    alert(error.responseText);
}

function clearRemoteUWSalesForm() {
    document.getElementById("RemoteUWSales_VendorName").value = "";
    document.getElementById("Invoicesales_ToCompany").value = "";
    document.getElementById("RemoteUWSales_invoiceaNo_canopy").value = "";
    document.getElementById("RemoteUWSales_ServicePeriodFrom").value = "";
    document.getElementById("saleinvoice_InvoiceCategory").value = "";
    document.getElementById("RemoteUWSales_invoiceamount_canopy").value = "";
    document.getElementById("RemoteUWSales_PaymentTerms").value = "";
    document.getElementById("RemoteUWSales_BankAccount").value = "";

    document.getElementById("Invoicesales_vendortype").value = "";
    document.getElementById("Invoicesales_EmailAddress").value = "";
    document.getElementById("RemoteUWSales_invoicedate").value = "";
    document.getElementById("RemoteUWSales_ServicePeriodTo").value = "";
    document.getElementById("RemoteUWSales_InvoiceDescription").value = "";
    document.getElementById("cm_DueDateSales").value = "";
    document.getElementById("Invoicesales_PaymentMethod").value = "";
    document.getElementById("RemoteUWSales_PaymentFromCompany").value = "";
    document.getElementById("RemoteUWSalesInvoiceExcel").value = "";

    document.getElementById("RWSalesFromDate").value = "";
    document.getElementById("RWSalesToDate").value = "";
}

//Load Vendor For Dropdown
function GetSalesVendors() {
    $.ajax({
        type: "POST",
        url: "AddInvoiceSales.aspx/GetSalesVendors",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var vendors = response.d;
            var dropdowns = ['#RemoteUWSales_VendorName'];
            $.each(dropdowns, function (i, selector) {
                var dropdown = $(selector);

                dropdown.empty();
                dropdown.append('<option value="">Select</option>');

                $.each(vendors, function (index, item) {
                    dropdown.append($('<option></option>')
                        .val(item.VendorRegistrationID)
                        .text(item.VendorName));
                });
            });
           
        },
        error: function (xhr, status, error) {
            console.log("Error fetching vendors: " + error);
        }
    });
}

$(document).ready(function () {
    $('#RemoteUWSales_VendorName').on('change', function () {
        var vendorId = $(this).val();
        var vendorTypeInput = $('#Invoicesales_vendortype');
        var toCompanyInput = $('#Invoicesales_ToCompany');
        var toEmailInput = $('#Invoicesales_EmailAddress');
        var toPaymentMethod = $('#Invoicesales_PaymentMethod');
        var toPaymentFromCompany = $('#RemoteUWSales_PaymentFromCompany');
        var toBankAccount = $('#RemoteUWSales_BankAccount');
        var toPaymentTerms = $('#RemoteUWSales_PaymentTerms');
        var toinvoiceamount = $('#RemoteUWSales_invoiceamount_canopy');

        if (!vendorId) {
            vendorTypeInput.val('');
            toCompanyInput.val('');
            toEmailInput.val('');
            toPaymentMethod.val('');
            toPaymentFromCompany.val('');
            toBankAccount.val('');
            toPaymentTerms.val('');
            toinvoiceamount.val('');
            return;
        }

        $.ajax({
            type: "POST",
            url: "AddInvoiceSales.aspx/GetVendorTypes",
            data: JSON.stringify({ vendorRegistrationID: parseInt(vendorId, 10) }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var data = typeof response.d === "string" ? JSON.parse(response.d) : response.d;

                if (data && data.length > 0) {
                    vendorTypeInput.val(data[0].VendorType || '');
                    toCompanyInput.val(data[0].CompanyName || '');
                    toEmailInput.val(data[0].EmailID || '');
                    toPaymentMethod.val(data[0].PaymentMethod || '');
                    toPaymentFromCompany.val(data[0].CompanyName || '');
                    toBankAccount.val(data[0].AccountNumber || '');
                    toPaymentTerms.val(data[0].BillingCycle || '');
                    toinvoiceamount.val(data[0].Rate || '');
                } else {
                    vendorTypeInput.val('');
                    toCompanyInput.val('');
                    toEmailInput.val('');
                    toPaymentMethod.val('');
                    toPaymentFromCompany.val('');
                    toBankAccount.val('');
                    toPaymentTerms.val('');
                    toinvoiceamount.val('');
                }
            },
            error: function (xhr, status, error) {
                console.log("Error fetching vendor details: " + xhr.responseText);
                vendorTypeInput.val('');
                toCompanyInput.val('');
                toEmailInput.val('');
                toPaymentMethod.val('');
                toPaymentFromCompany.val('');
                toBankAccount.val('');
                toPaymentTerms.val('');
                toinvoiceamount.val('');
            }
        });
    });
});

function showAlert(message) {
    // Jar tu SweetAlert2 (Swal.fire) vaprat asshil tar he use kar:
    if (typeof Swal !== 'undefined') {
        Swal.fire({
            icon: 'warning',
            title: 'Required Field',
            text: message,
            confirmButtonColor: '#3085d6'
        });
    }
    // Jar juna SweetAlert (swal) asel tar he chalel:
    else {
        swal("Required Field", message, "warning");
    }
}