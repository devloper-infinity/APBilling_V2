
var Others_UpdatePaidDate_table;
var Others_UpdatePaidDate_html;
const otherchkIds = [];
var other_InvID = 0;

function blankForNull(s) {
    return s == "null" || s == null ? "" : s;
}

function checkPayees_Other(ID) {

    //alert(ID);

    if (ID.checked) {
        if (!otherchkIds.includes(ID.id)) {

            otherchkIds.push(ID.id);
        }
        //alert("1");
        //alert(otherchkIds.length);
    }
    else {
        if (otherchkIds.includes(ID.id)) {
            otherchkIds.splice(otherchkIds.indexOf(ID.id), 1);

        }
        // alert("2");
        //alert(otherchkIds.length);
    }

    return false;
}

function GetInvoices(InvType) {

    var Other_company = document.getElementById("Other_company");
    var Company = Other_company.options[Other_company.selectedIndex].text;

    if (Company == "Select") {
        alert("Please select Company.");
        document.getElementById("Other_company").focus();
        return false;
    }

    var InvoiceType = InvType.options[InvType.selectedIndex].text;

    var select = document.getElementById("Other_udateInvNo");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#Other_udateInvNo").append($("<option></option>").val("Select").html("Select"));
    $.ajax({
        type: "POST",
        url: "UpdatePaidDateOther.aspx/GetInfinity_InvoiceDetails_Approval_Other",
        dataType: "json", contentType: "application/json",
        data: "{InvoiceType:'" + InvoiceType + "',Company:'" + Company + "'}",

        success: function (res) {

            var dataArray = JSON.parse(res.d);

            $.each(dataArray, function (data, value) {

                $("#Other_udateInvNo").append($("<option></option>").val(value.InvoiceId).html(value.InvoiceNo));
            })
        }
    });
}

function bindgrid() {

    var Other_InvType = document.getElementById("Other_InvType");
    var InvType = Other_InvType.options[Other_InvType.selectedIndex].text;

    var Other_udateInvNo = document.getElementById("Other_udateInvNo");
    var InvID = Other_udateInvNo.options[Other_udateInvNo.selectedIndex].value;

    other_InvID = InvID;
    ////alert(InvID);

    if (InvType == "Select") {
        alert("Please select Invoice Type.");
        document.getElementById("Other_InvType").focus();
        return false;
    }

    if (InvID == "Select") {
        alert("Please select Invoice #.");
        document.getElementById("Other_udateInvNo").focus();
        return false;
    }

    //alert(InvType);

    BindOtherInvApproval_Grid(InvID, InvType);
}

function BindOtherInvApproval_Grid(InvID, InvType) {

    $('#load1').show();
    Others_UpdatePaidDate_html = '';

    //alert("InvoiceID: " + InvID + ", InvType: '" + InvType);

    $.ajax({
        url: "UpdatePaidDateOther.aspx/GetAllInvoiceForUpdatePaidDateAsPerPayees",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ",InvType:'" + InvType + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {

                Others_UpdatePaidDate_html += '<tr>';
                Others_UpdatePaidDate_html += '<td style="text-align:center;"><input type="checkbox" id="' + value.VendorPayToID + '" onchange="return checkPayees_Other(this);" /></td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.VendorPayToID) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Payee) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoanCount) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BaseRate) + '</td>';
                /*  Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.TotalAmount) + '</td>';*/
                // Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;"><input type="text"  id="other_TotalAmt_' + value.TotalAmount + '" style="width:250px;"/></td>';  readonly="readonly"
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;text-align:center;"><input type="number" id="other_TotalAmt_' + value.VendorPayToID + '"readonly="readonly" style="text-align:center; width:50px;" value="' + blankForNull(value.TotalAmount) + '" /></td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AccountNo) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.EmailId) + '</td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap;"><input type="text" id="other_utrNo_' + value.VendorPayToID + '" style="width:250px;"/></td>';
                Others_UpdatePaidDate_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.TotalAmount) + '</td>';
                Others_UpdatePaidDate_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_Others_UpdatePaidDate')) {
                Others_UpdatePaidDate_table.destroy();
            }
            $('#table_Others_UpdatePaidDate tbody').html(Others_UpdatePaidDate_html);

            Others_UpdatePaidDate_table = $('#table_Others_UpdatePaidDate').DataTable({
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

                footerCallback: function (row, data, start, end, display) {
                    let api = this.api();

                    // Remove the formatting to get integer data for summation
                    let intVal = function (i) {
                        return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
                    };

                    // Total over all pages
                    totalLoan = api.column(5).data().reduce((a, b) => intVal(a) + intVal(b), 0);
                    totalAmt = api.column(11).data().reduce((a, b) => intVal(a) + intVal(b), 0);

                    // Total over this page

                    // Update footer
                    api.column(4).footer().innerHTML = Number(totalLoan).toFixed(2);
                    api.column(6).footer().innerHTML = Number(totalAmt).toFixed(2);
                }
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });

    return false;
}

function Others_btnUpdatePaidDate() {

    $('#waitingpanel_updateDateOther').modal('show');
    document.getElementById("spntext").innerHTML = "System is updating details. Please wait.";

    var Other_UTR;
    var Other_TotalAmt;
    var PayeeIDs = 0;
    var Othe_chkLen = otherchkIds.length;

    if (Othe_chkLen > 0) {

        for (let i = 0; i < Othe_chkLen; i++) {

            PayeeIDs = PayeeIDs + "," + otherchkIds[i];
            var Remark = document.getElementById("other_utrNo_" + otherchkIds[i]).value;

            var TotalAmt = document.getElementById("other_TotalAmt_" + otherchkIds[i]).value;
            Other_TotalAmt = Other_TotalAmt + "|" + TotalAmt;

            if (Remark != "") {
                Other_UTR = Other_UTR + "|" + Remark;
            }
            else {
                alert("Please enter UTR #");
                return false;
            }
        }

        var OtherCompany = document.getElementById("Other_company");
        var Company = OtherCompany.options[OtherCompany.selectedIndex].text;

        var OtherInvType = document.getElementById("Other_InvType");
        var Vendor = OtherInvType.options[OtherInvType.selectedIndex].text;

        var OtherInvID = document.getElementById("Other_udateInvNo");
        var InvoiceID = OtherInvID.options[OtherInvID.selectedIndex].val;

        var Other_PaymentDate = document.getElementById("Others_UpPaidDate").value;
        var Other_Remark = document.getElementById("Others_updateRemark").value;

        if (Company == "Select") {
            alert("Please select company.");
            document.getElementById("Other_company").focus();
            return false;
        }
        if (Vendor == "Select") {
            alert("Please select Invoice Type.");
            document.getElementById("Other_InvType").focus();
            return false;
        }
        if (InvoiceID == "Select") {
            alert("Please select Invoice #.");
            document.getElementById("Other_udateInvNo").focus();
            return false;
        }
        if (Other_PaymentDate == "") {
            alert("Please select Date.");
            return false;
        }

        if (Other_Remark == "") {
            alert("Please enter Remark.");
            return false;
        }

        PageMethods.UpdatePaidDateOthers(PayeeIDs, other_InvID, Company, Vendor, Other_Remark, Other_PaymentDate, Other_UTR, Other_TotalAmt, OnSuccessUpdatePaidDate_Other, OnErrorUpdatePaidDate_Other);
        return false;
    }
    else {
        alert("Please select records.");
        return true;
    }

}

function OnSuccessUpdatePaidDate_Other(result) {

    $('#waitingpanel_updateDateOther').modal('hide');

    InvoiceID = result;

    if (result > 0) {

        alert("Date updated successfully.");
        location.reload();

        return false;
    }
    else {

        alert("Oops! Error occured while updating status. Please contact administrator");
        location.reload();
        return false;
    }
}

function OnErrorUpdatePaidDate_Other(error) {
    alert(error.responseText);
}







