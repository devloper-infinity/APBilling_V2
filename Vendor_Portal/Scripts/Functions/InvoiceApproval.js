
var flag = false;

function blankForNull(s) {
    return s == "null" || s == null ? "" : s;
}

var invrec_html_IPS ;
var invrec_IPS;
var invrec_canopy;
var invrec_html_canopy = '';

var invrecdetails_canopy_stewart;
var stewart_html = '';
var invrecdetails_canopy_LauraMac;
var LauraMac_html = '';
var InvoiceID;
var popUp_InvID = 0;
var popUp_Company = '';
var popUp_InvType = '';


function BindGrid_InvoiceApproval() {

    $('#load1').show();
    invrec_html_IPS = '';
    const urlParams = new URLSearchParams(window.location.search);
    const filterType = (urlParams.get('type') || 'IPS').toUpperCase();
    $.ajax({
        url: "InvoiceApproval.aspx/" + ((new URLSearchParams(window.location.search).get('type') || 'IPS').toUpperCase() === 'CANOPY' ? "GetAllIPSInvoiceForReconcile_ForApproval_canopy" : "GetAllIPSInvoiceForReconcile_ForApproval"),
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
           
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {
                invrec_html_IPS += '<tr>';
                invrec_html_IPS += '<td class=""><div class="btn-group">';
                invrec_html_IPS += '<div class="btn-group">';
                invrec_html_IPS += '<div type="button" data-toggle="dropdown" aria-expanded="false"><i style="color: dodgerblue; font-size:14px;" class="uil fs-0 me-2 uil-cog"></i>';
                invrec_html_IPS += '<span class="sr-only"></span></div><div class="dropdown-menu" role="menu" style="">';
                invrec_html_IPS += '<a class="dropdown-item" href="#!" id="Actions" onclick="ApproveInvoice(\'' + blankForNull(value.InvoiceId) + '\',' + index + ',1);"><span style="color: forestgreen;"><i class="uil fs-0 me-2 uil-pen"></i></span>&nbsp;&nbsp;Approve</a>';
                invrec_html_IPS += '<a class="dropdown-item" href="#!" id="ShowInvoice" onclick="ia_ShowInvoices(' + value.InvoiceId + ',\'' + value.InvoiceType + '\',\'' + value.Company + '\');"><span style="color: brown;"><i class="uil fs-0 me-2 uil-search-alt"></i></span>&nbsp;&nbsp;View Loans</a>';
                invrec_html_IPS += '<a class="dropdown-item" href="javascript:void(0);" onclick="DownloadInvoice(\'' + blankForNull(value.InvoiceId) + '\',\'' + value.InvoiceType + '\', \'' + blankForNull(value.Company) + '\');"><span style="color: brown;"><i class="uil fs-0 me-2 uil-file-download"></i></span>&nbsp;&nbsp;Download Invoice</a> </div></div></td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.ReceivedDate) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans1) + '</td>';

                //invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.BaseRate) + '</td>';
                //invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.APAmount) + '</td>';

                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrec_html_IPS += '<td style="text-wrap: nowrap;">' + blankForNull(value.Status) + '</td>';
                    invrec_html_IPS += '</tr>';
                
            });

            if ($.fn.dataTable.isDataTable('#invrec_IPS')) {
                invrec_IPS.destroy();
            }
            $('#invrec_IPS tbody').html(invrec_html_IPS);
         
            invrec_IPS = $('#invrec_IPS').DataTable({
                dom: 'lftip',
                destroy: true,
                scrollX: false,
                "paging": true,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                'select': {
                    'style': 'single'
                },

                //columnDefs: [
                //    {
                //        targets: 18,      // Status column index
                //        width: "350px"
                //    }
                //],

                initComplete: function () {
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
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

function DownloadInvoice(invoiceId, InvoiceType, Company ) {
    if (!invoiceId) {
        alert("Invalid Invoice ID!");
        return;
    }

    $.ajax({
        type: "POST",
        url: "InvoiceApproval.aspx/DownloadInvoiceFile",
        data: JSON.stringify({ invoiceId: parseInt(invoiceId), InvoiceType: InvoiceType, Company: Company, }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var res = JSON.parse(response.d);
            if (res.Success) {
                var sampleArr = base64ToArrayBuffer(res.Base64Data);
                saveByteArray(res.FileName, sampleArr);
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Warning',
                    text: res.Message,
                    showConfirmButton: true
                });
            }
        },
        error: function (xhr, status, error) {
            alert("Error downloading file: " + error);
        }
    });
}

// Helper functions for base64 download
function base64ToArrayBuffer(base64) {
    var binaryString = window.atob(base64);
    var len = binaryString.length;
    var bytes = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    return bytes.buffer;
}

function saveByteArray(reportName, byte) {
    var blob = new Blob([byte], { type: "application/octet-stream" });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = reportName;
    link.click();
}

function ApproveInvoice(InvoiceId, selected) {

    var row = invrec_IPS.row(selected).data();

    document.getElementById("lblInvoiceNo").innerHTML = "<b> </b>" + row[7];
    document.getElementById("lblInvoiceAmout").innerHTML = "<b> </b>" + row[11];
    document.getElementById("lblcompany").innerHTML = "<b></b>" + row[3];
    document.getElementById("lblInvoiceType").innerHTML = "<b> </b>" + row[6];
    document.getElementById("lblInvoiceDate").innerHTML = "<b> </b>" + row[8];

    document.getElementById("Approval_LoansDeducted").value = 0;
    document.getElementById("Approval_AmountDeducted").value = 0;
    document.getElementById("Approval_PaybleToVendor").value = row[11];

    popUp_InvID = InvoiceId;
    popUp_Company = row[3];
    popUp_InvType = row[6];

    $('#InvoiceApproval').modal('show');
}

function InvoiceApproval() {

    var remark = document.getElementById("Approval_remark").value;
    if (remark == "") {
        alert("Approve remarks should not be blank.");
        return false;
    }

    var ApStatus = document.getElementById("Approval_Status");
    var Status = ApStatus.options[ApStatus.selectedIndex].value;
    if (Status == "") {
        alert("Status should not be blank.");
        return false;
    }
    var Approval_LoansDeducted = document.getElementById("Approval_LoansDeducted").value;
    var Approval_AmountDeducted = document.getElementById("Approval_AmountDeducted").value;
    var Approval_PaybleToVendor = document.getElementById("Approval_PaybleToVendor").value;

    if (Approval_AmountDeducted == "") {
        alert("Amount Deducted should not be blank.");
        return false;
    }
    $('#waitingpanel').modal('show');
    document.getElementById("spntext").innerHTML = "In process please wait.";

    PageMethods.ApprovalInvoice(popUp_InvID, remark, popUp_Company, popUp_InvType, Status, Approval_LoansDeducted, Approval_AmountDeducted,Approval_PaybleToVendor,OnSuccessRemove, OnErrorRemove);
    return false;
}

function OnSuccessRemove(result) {
    $('#waitingpanel').modal('hide');
    InvoiceID = result;

    if (result > 0) {

        popUp_InvID = 0;
        popUp_Company = '';
        popUp_InvType = '';
        alert("Invoice remark has been added successfully and email has been sent.");
        location.reload();
        BindIPSGrid();
        return false;
    }
    else {

        alert("Oops! Error occured while approved Invoice. Please contact administrator");
        location.reload();
        BindIPSGrid();
        return false;
    }
}

function OnErrorRemove(error) {
    alert(error.responseText);
}

function ia_ShowInvoices(InvoiceID, InvoiceType, Company) {

    $('#popUpViewLoanDetails').modal('show');
    BindViewLoanDetails(InvoiceID, InvoiceType, Company);
    return false;
}

function BindViewLoanDetails(InvoiceID, InvoiceType, Company) {

    var ExcelTitle = "Loan Details : " + InvoiceType;
    document.getElementById("invApp_ViewLoan").innerHTML = "<b>View Loan Details : " + Company + "-" + InvoiceType + "</b>";

    var columns = [];

    $.ajax({
        url: "InvoiceApproval.aspx/GetAllInvoiceLoansDetailsForReport",
        type: "POST",
        data: "{InvoiceID:" + InvoiceID + ", InvoiceType:'" + InvoiceType + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {

            if ($.fn.dataTable.isDataTable('#viewloanDetails_table')) {
                $('#viewloanDetails_table').DataTable().destroy();
            }

            dataArray = JSON.parse(data.d);

            $.each(dataArray[0], function (key, value) {

                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                columns.push(my_item);
            });

            $('#viewloanDetails_table').DataTable({
                dom: 'lBftp',
                destroy: true,
                paging: true,
                "autoWidth": true,
                select: true,
                processing: true,
                'select': {
                    'style': 'single'
                },
                "data": dataArray,
                "columns": columns,

                initComplete: function () {
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
                    $('#load1').hide();
                },
                buttons: [
                    {
                        extend: 'excelHtml5', title: ExcelTitle, autoFilter: true,
                    },
                ],

            });
        }
    });
}

function CheckLoginID_ForRights() {
   
    $.ajax({
        url: "InvoiceApproval.aspx/CheckLoginID_ForRights",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (res) {

          //  alert(res);

        }
    });
}



$(document).on('input', '#Approval_AmountDeducted', function () {
    calculatePayableToVendor();
});

function calculatePayableToVendor() {
    var invoiceAmountText = $('#lblInvoiceAmout').text().trim();
    var invoiceAmount = parseFloat(invoiceAmountText) || 0;

    var deductedAmountText = $('#Approval_AmountDeducted').val().trim();
    var deductedAmount = parseFloat(deductedAmountText) || 0;

    var payableToVendor = invoiceAmount - deductedAmount;

    // if (payableToVendor < 0) {
    //     payableToVendor = 0;
    // }

    $('#Approval_PaybleToVendor').val(payableToVendor.toFixed(2));
}

