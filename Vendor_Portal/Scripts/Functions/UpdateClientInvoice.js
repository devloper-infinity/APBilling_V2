
var updateClientInvoice_table;
var updateClientInvoice_html;
const chkId_UpdateClientInv = [];

function GetVendor_ClientInvoiceDate(Company) {

    var Com = Company.options[Company.selectedIndex].text;

    var select = document.getElementById("updateCI_Vendor");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#updateCI_Vendor").append($("<option></option>").val("Select").html("Select"));
    $.ajax({
        type: "POST", url: "CostMaster.aspx/GetAllVendorAsPerCompany", dataType: "json", contentType: "application/json",
        data: "{Company:'" + Com + "'}",

        success: function (res) {

            var dataArray = JSON.parse(res.d);

            $.each(dataArray, function (data, value) {

                $("#updateCI_Vendor").append($("<option></option>").val(value.Vendor).html(value.Vendor));
            })
        }
    });
}

function GetUpdateClnInv_checkBox(ID) {

    if (ID.checked) {
        if (!chkId_UpdateClientInv.includes(ID.id)) {

            chkId_UpdateClientInv.push(ID.id);
        }
    }
    else {
        if (chkId_UpdateClientInv.includes(ID.id)) {
            chkId_UpdateClientInv.splice(chkId_UpdateClientInv.indexOf(ID.id), 1);
        }
    }
    return false;
}

function BindGrid_UpdateClientInv() {

    $('#load1').show();
    updateClientInvoice_html = '';
    var Company = 'IPS';

    $.ajax({
        url: "UpdateClientInvoice.aspx/GetInfinityInvoicesForUpdatePaidDate",
        type: "POST",
        dataType: "json",
        data: "{Company:'" + Company + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {

                updateClientInvoice_html += '<tr>';
                updateClientInvoice_html += '<td style="text-align:center;"><input type="checkbox" id="' + value.InvoiceId + '" onchange="return GetUpdateClnInv_checkBox(this);" /></td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                updateClientInvoice_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                updateClientInvoice_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_updateClientInvoice')) {
                updateClientInvoice_table.destroy();
            }
            $('#table_updateClientInvoice tbody').html(updateClientInvoice_html);
            //else
            updateClientInvoice_table = $('#table_updateClientInvoice').DataTable({
                dom: 'lftip',
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

function OnClick_updateClientDate() {

    var ci_company = document.getElementById("updateCI_company");
    var Company_val = ci_company.options[ci_company.selectedIndex].text;

    var ci_Vendor = document.getElementById("updateCI_Vendor");
    var Vendor_val = ci_Vendor.options[ci_Vendor.selectedIndex].text;

    var ClientInvNo = document.getElementById("updateCI_InvoiceNo").value;

    var UCI_InvoiceIDs = 0;
    var UCI_chkLen = chkId_UpdateClientInv.length;

    if (UCI_chkLen > 0) {

        for (let i = 0; i < UCI_chkLen; i++) {

            UCI_InvoiceIDs = UCI_InvoiceIDs + "," + chkId_UpdateClientInv[i];
        }

        if (Vendor_val == "Select") {
            alert("Please select Vendor.");
            document.getElementById("updateCI_Vendor").focus();
            return false;
        }

        if (Company_val == "Select") {
            alert("Please select Company.");
            document.getElementById("updateCI_company").focus();
            return false;
        }
        if (ClientInvNo == "") {
            alert("Please enter Client Invoice #.");
            document.getElementById("updateCI_InvoiceNo ").focus();
            return false;
        }

        PageMethods.UpdateClientInvoiceNo(UCI_InvoiceIDs, Company_val, Vendor_val, ClientInvNo, OnSuccessupdateClientDate, OnErrorupdateClientDate);
        return false;
    }
    else {
        alert("Please check records.");
        return false;
    }
}

function OnSuccessupdateClientDate(result) {
    $('#waitingpanel_updateClientInv').modal('hide');

    if (result > 0) {

        alert("Data updated successfully.");
        location.reload();
        BindGrid_UpdateClientInv();
        return false;
    }
    else {

        alert("Oops! Error occured while updating status. Please contact administrator");
        location.reload();
        BindGrid_UpdateClientInv();
        return false;
    }
}

function OnErrorupdateClientDate(error) {
    alert(error.responseText);
}