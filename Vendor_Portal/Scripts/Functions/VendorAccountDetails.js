/* ------------------------ Vendor Account Master Variables ------------------------ */

var IPS_html;
var IPS_Table;
var canopy_html;
var canopy_Table;



/* ------------------------ Approval Cost Variables ------------------------ */

var approvalVendorAccountcanopy_table;
var approvalcanopy_html;

var approvalVendorAccountips_table;
var approvalcostIPS_html;

var ac_ConfID = 0;

/* ------------------------ Vendor Account Master ----------------------- */

function GetVendorforAccount(Company) {

    var Com = Company.options[Company.selectedIndex].text;

    var select = document.getElementById("VendorAccount_Vendor");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#VendorAccount_Vendor").append($("<option></option>").val("Select").html("Select"));
    $.ajax({
        type: "POST", url: "CostMaster.aspx/GetAllVendorAsPerCompany", dataType: "json", contentType: "application/json",
        data: "{Company:'" + Com + "'}",

        success: function (res) {

            var dataArray = JSON.parse(res.d);

            $.each(dataArray, function (data, value) {

                $("#VendorAccount_Vendor").append($("<option></option>").val(value.Vendor).html(value.Vendor));
            })
        }
    });
}


function VendorAccount_Submit() {

    var VendorAccount_company = document.getElementById("VendorAccount_company");
    var VendorCompany = VendorAccount_company.options[VendorAccount_company.selectedIndex].text;

    var VendorAccount_Vendor = document.getElementById("VendorAccount_Vendor");
    var VendorAccount = VendorAccount_Vendor.options[VendorAccount_Vendor.selectedIndex].text;


    var VendorAccount_PaymentType = document.getElementById("VendorAccount_PaymentType");
    var VendorAccountPaymentType = VendorAccount_PaymentType.options[VendorAccount_PaymentType.selectedIndex].text;

    var VendorAccount_PayTo = document.getElementById("VendorAccount_PayTo").value;
    var VendorAccount_AcNo = document.getElementById("VendorAccount_AcNo").value;
    var VendorAccount_EmailId = document.getElementById("VendorAccount_EmailId").value;



    if (VendorCompany == "Select") {
        alert("Please select company.");
        document.getElementById("VendorAccount_company").focus();
        return false;
    }

    if (VendorAccount == "Select") {
        alert("Please select vendor.");
        document.getElementById("VendorAccount_Vendor").focus();
        return false;
    }

    if (VendorAccountPaymentType == "Select") {
        alert("Please select Payment type.");
        document.getElementById("VendorAccount_PaymentType").focus();
        return false;
    }
    if (VendorAccount_PayTo == "") {
        alert("Please enter Pay To.");
        document.getElementById("VendorAccount_PayTo").focus();
        return false;
    }
    if (VendorAccount_AcNo == "") {
        alert("Please enter Ac No.");
        document.getElementById("VendorAccount_AcNo").focus();
        return false;
    } if (VendorAccount_EmailId == "") {
        alert("Please enter Email Id.");
        document.getElementById("VendorAccount_EmailId").focus();
        return false;
    }
    PageMethods.InsertVendorAccountDetails(VendorCompany, VendorAccount, VendorAccountPaymentType, VendorAccount_PayTo, VendorAccount_AcNo, VendorAccount_EmailId, cm_OnSuccessSubmit, cm_OnErrorSubmit);
    return false;
}

function cm_OnSuccessSubmit(result) {
    alert("Data added Successfully.")
    //location.reload();
    PageMethods.cm_BindGrid();
    return false;
}

function cm_OnErrorSubmit(error) {
    alert(error.responseText);
    return false;
}

$(document).ready(function () {
    $('#VendorAccount_PaymentType').change(function () {
        var selectedType = $(this).val();

        if (selectedType === "ECH") {
            $('#VendorAccount_AcNo').prop('disabled', false).val('');
        }
        else if (selectedType === "BillPay") {
            $('#VendorAccount_AcNo').prop('disabled', true).val('Bill Pay');
        }
        else {
            $('#VendorAccount_AcNo').prop('disabled', true).val('');
        }
    });
});



function GetAllVendorAccountDetails_IPS() {

    IPS_html = '';
    $('#load1').show();

    $.ajax({
        url: "VendorAccountDetails.aspx/GetAllVendorAccountDetails_IPS",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {


                IPS_html += '<tr>';
                IPS_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PayTo) + '</td>';
                IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AccNoLastFourDigit) + '</td>';
                IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailID) + '</td>';
                IPS_html += '</tr>';
            });

            $('#table_VendorAccount_IPS tbody').html(IPS_html);

            if ($.fn.dataTable.isDataTable('#table_VendorAccount_IPS')) {
                IPS_Table.destroy();
            }

            IPS_Table = $('#table_VendorAccount_IPS').DataTable({
                dom: 'lftip',
                //scrollX: true,
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
                processing: true,
                ordering: false,
                'select': {
                    'style': 'single'
                },

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
            });
        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    $('#load1').hide();
    return false;
}


function GetAllVendorAccountDetails_Canopy() {

    canopy_html = '';
    $('#load1').show();

    $.ajax({
        url: "VendorAccountDetails.aspx/GetAllVendorAccountDetails_Canopy",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {


                canopy_html += '<tr>';
                canopy_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                canopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PayTo) + '</td>';
                canopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                canopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                canopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AccNoLastFourDigit) + '</td>';
                canopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailID) + '</td>';
                canopy_html += '</tr>';
            });

            $('#table_VendorAccount_Canopy tbody').html(canopy_html);

            if ($.fn.dataTable.isDataTable('#table_VendorAccount_Canopy')) {
                canopy_Table.destroy();
            }

            canopy_Table = $('#table_VendorAccount_Canopy').DataTable({
                dom: 'lftip',
                //scrollX: true,
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
                processing: true,
                ordering: false,
                'select': {
                    'style': 'single'
                },

                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
            });
        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    $('#load1').hide();
    return false;
}




/* ------------------------ Approval Vendor Account ------------------------ */



function GetAllApproveVendorAccountDetails_IPS() {
    approvalcostIPS_html = '';
    $('#load1').show();

    $.ajax({
        url: "VendorAccountDetails.aspx/GetAllVendorAccountDetails_IPS",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {
                approvalcostIPS_html += '<tr>';
                approvalcostIPS_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="VendorAccount_IPS_Approve" onclick="VendorAccountDetails_ShowApprovePopUpIPS(' + value.VendorPayToID + ',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';

                approvalcostIPS_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;display:none;">' + blankForNull(value.VendorPayToID) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PayTo) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AccNoLastFourDigit) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailID) + '</td>';
                approvalcostIPS_html += '</tr>';
            });

            $('#table_ApproveVendorAccount_IPS tbody').html(approvalcostIPS_html);

            if ($.fn.dataTable.isDataTable('#table_ApproveVendorAccount_IPS')) {
                approvalVendorAccountips_table.destroy();
            }

            approvalVendorAccountips_table = $('#table_ApproveVendorAccount_IPS').DataTable({
                dom: 'lftip',
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
                processing: true,
                ordering: false,
                'select': {
                    'style': 'single'
                },
                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
            });
        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    $('#load1').hide();
    return false;
}

function GetAllApproveVendorAccountDetails_Canopy() {
    approvalcanopy_html = '';
    $('#load1').show();

    $.ajax({
        url: "VendorAccountDetails.aspx/GetAllVendorAccountDetails_Canopy",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {
                approvalcanopy_html += '<tr>';
                approvalcanopy_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="ac_Approve" onclick="VendorAccountDetails_ShowApprovePopUp(' + value.VendorPayToID + ',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';

                approvalcanopy_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                approvalcanopy_html += '<td style="text-wrap: nowrap;display:none;">' + blankForNull(value.VendorPayToID) + '</td>';
                approvalcanopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.PayTo) + '</td>';
                approvalcanopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                approvalcanopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                approvalcanopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AccNoLastFourDigit) + '</td>';
                approvalcanopy_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailID) + '</td>';
                approvalcanopy_html += '</tr>';
            });

            $('#table_ApproveVendorAccount_Canopy tbody').html(approvalcanopy_html);

            if ($.fn.dataTable.isDataTable('#table_ApproveVendorAccount_Canopy')) {
                approvalVendorAccountcanopy_table.destroy();
            }

            approvalVendorAccountcanopy_table = $('#table_ApproveVendorAccount_Canopy').DataTable({
                dom: 'lftip',
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
                processing: true,
                ordering: false,
                'select': {
                    'style': 'single'
                },
                initComplete: function () {
                    $('#load1').hide();
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                },
            });
        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    $('#load1').hide();
    return false;
}

function VendorAccountDetails_ShowApprovePopUp(VendorPayToID, index) {

    var row = approvalVendorAccountcanopy_table.row(index).data();

    ac_ConfID = row[2];
    document.getElementById("ApproveAccount_VendorName").innerHTML = "<b>Vendor Name : </b>" + row[5];
    document.getElementById("ApproveAccount_PayTo").innerHTML = "<b>Pay To : </b>" + row[3];
    document.getElementById("ApproveAccount_AcNo").innerHTML = "<b>Ac No : </b>" + row[6];

    $('#VendorAccount_approved').modal('show');
}

function VendorAccountDetails_ShowApprovePopUpIPS(VendorPayToID, index) {
    var row = approvalVendorAccountips_table.row(index).data();

    ac_ConfID = row[2];
    document.getElementById("ApproveAccount_VendorName").innerHTML = "<b>Vendor Name : </b>" + row[5];
    document.getElementById("ApproveAccount_PayTo").innerHTML = "<b>Pay To : </b>" + row[3];
    document.getElementById("ApproveAccount_AcNo").innerHTML = "<b>Ac No : </b>" + row[6];

    $('#VendorAccount_approved').modal('show');
}

function ApproveAccount_ApprvoveCost() {

    if (ac_ConfID > 0) {

        var Remark = document.getElementById("ApproveAccount_Remark").value;

        if (Remark == "") {
            alert("Please enter remark.");
            document.getElementById("ApproveAccount_Remark").focus();
            return false;
        }

        PageMethods.ApproveVendorAllAccountDetails(ac_ConfID, Remark, ac_OnSuccessSubmit, ac_OnErrorSubmit);
        return false;
    }
    else {
        return false;
    }
}

function ac_OnSuccessSubmit(result) {
    alert("Vendor Account Details  aaproved Successfully.")
    location.reload();
    return false;
}

function ac_OnErrorSubmit(error) {
    alert(error.responseText);
    return false;
}