
var apCanopy_UpdatePaidDate_table;
var apCanopy_UpdatePaidDate_html;

var apIPS_UpdatePaidDate_table;
var apIPS_UpdatePaidDate_html;

const chkIds = [];
const IPS_chkIds = [];

/*-------------- Canopy -------------*/

function GetCheckedCheckboxes_Date(ID) {

    if (ID.checked) {
        if (!chkIds.includes(ID.id)) {

            chkIds.push(ID.id);
        }
    }
    else {
        if (chkIds.includes(ID.id)) {
            chkIds.splice(chkIds.indexOf(ID.id), 1);
        }
    }
    return false;
}

function BindAPCanopyInvoiceApproval_Grid() {

    var Company = 'Canopy';

    $('#load1').show();
    apCanopy_UpdatePaidDate_html = '';

    $.ajax({
        url: "UpdatePaidDate.aspx/GetInfinityInvoicesForUpdatePaidDate",
        type: "POST",
        dataType: "json",
        data: "{Company:'" + Company + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {

                apCanopy_UpdatePaidDate_html += '<tr>';
                /* apCanopy_UpdatePaidDate_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="mis_Actions" onclick="mis_ShowUpdateDatePopUP(\'' + blankForNull(value.InvoiceId) + '\',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';*/
                apCanopy_UpdatePaidDate_html += '<td style="text-align:center;"><input type="checkbox" id="' + value.InvoiceId + '" onchange="return GetCheckedCheckboxes_Date(this);" /></td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AcNo) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Email) + '</td>';
                apCanopy_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                apCanopy_UpdatePaidDate_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_apCanopy_UpdatePaidDate')) {
                apCanopy_UpdatePaidDate_table.destroy();
            }
            $('#table_apCanopy_UpdatePaidDate tbody').html(apCanopy_UpdatePaidDate_html);
            //else
            apCanopy_UpdatePaidDate_table = $('#table_apCanopy_UpdatePaidDate').DataTable({
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
                    totalLoan = api.column(10).data().reduce((a, b) => intVal(a) + intVal(b), 0);
                    totalAmt = api.column(11).data().reduce((a, b) => intVal(a) + intVal(b), 0);

                    // Total over this page

                    // Update footer
                    api.column(9).footer().innerHTML = Number(totalLoan).toFixed(2);
                    api.column(10).footer().innerHTML = Number(totalAmt).toFixed(2);
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

function apCanopy_btnUpdatePaidDate() {

    $('#waitingpanel_updateDate').modal('show');

    var InvoiceIDs = 0;

    var ToAddress = "jim@infinity-data.com,anita@infinity-data.com";
    var ToCC = "s.chandrakant@infinity-data.com";
    var ToBCC = "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us,b.shubhangi@infinityinternationals.us";

    //var ToAddress = "b.shubhangi@infinityinternationals.us";
    //var ToCC = "b.shubhangi@infinityinternationals.us";

    var chkLen = chkIds.length;

    if (chkLen > 0) {

        for (let i = 0; i < chkLen; i++) {
            InvoiceIDs = InvoiceIDs + "," + chkIds[i];
        }

        var PaymentDate = document.getElementById("apCanopy_UpPaidDate").value;
        var Remark = document.getElementById("apCanopy_updateRemark").value;
        var UTR = document.getElementById("apCanopy_UTRNo").value;

        if (PaymentDate == "") {
            alert("Please select Date.");
            return true;
        }
        if (UTR == "") {
            alert("Please enter UTR #.");
            return true;
        }
        if (Remark == "") {
            alert("Please enter Remark.");
            return true;
        }

        PageMethods.UpdateRemark(InvoiceIDs, Remark, PaymentDate, 'Canopy', UTR, ToAddress, ToCC, ToBCC, OnSuccessUpdatePaidDate, OnErrorUpdatePaidDate);
        return false;
    }
    else {
        alert("Please select records");
        return true;
    }
}

function OnSuccessUpdatePaidDate(result) {
    $('#waitingpanel_updateDate').modal('hide');

    InvoiceID = result;
    if (result > 0) {

        alert("Date updated successfully.");
        location.reload();
        BindAPCanopyInvoiceApproval_Grid();
        return false;
    }
    else {

        alert("Oops! Error occured while updating status. Please contact administrator");
        location.reload();
        BindAPCanopyInvoiceApproval_Grid();
        return false;
    }
}

function OnErrorUpdatePaidDate(error) {
    alert(error.responseText);
}


/*-------------- IPS -------------*/

function BindAPIPSInvoiceApproval_Grid() {

    var Company = 'IPS';

    $('#load1').show();
    apIPS_UpdatePaidDate_html = '';

    $.ajax({
        url: "UpdatePaidDate.aspx/GetInfinityInvoicesForUpdatePaidDate",
        type: "POST",
        dataType: "json",
        data: "{Company:'" + Company + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {

                apIPS_UpdatePaidDate_html += '<tr>';
                apIPS_UpdatePaidDate_html += '<td style="text-align:center;"><input type="checkbox" id="' + value.InvoiceId + '" onchange="return ips_GetCheckedCheckboxes_Date(this);" /></td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AcNo) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Email) + '</td>';
                apIPS_UpdatePaidDate_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                apIPS_UpdatePaidDate_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_apIPS_UpdatePaidDate')) {
                apIPS_UpdatePaidDate_table.destroy();
            }
            $('#table_apIPS_UpdatePaidDate tbody').html(apIPS_UpdatePaidDate_html);
            //else
            apIPS_UpdatePaidDate_table = $('#table_apIPS_UpdatePaidDate').DataTable({
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
                    totalLoan = api.column(10).data().reduce((a, b) => intVal(a) + intVal(b), 0);
                    totalAmt = api.column(11).data().reduce((a, b) => intVal(a) + intVal(b), 0);

                    // Total over this page

                    // Update footer
                    api.column(9).footer().innerHTML = Number(totalLoan).toFixed(2);
                   api.column(10).footer().innerHTML = Number(totalAmt).toFixed(2);
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

function ips_GetCheckedCheckboxes_Date(ID) {

    if (ID.checked) {
        if (!IPS_chkIds.includes(ID.id)) {

            IPS_chkIds.push(ID.id);
        }
    }
    else {
        if (IPS_chkIds.includes(ID.id)) {
            IPS_chkIds.splice(IPS_chkIds.indexOf(ID.id), 1);
        }
    }
    return false;
}

function apIPS_btnUpdatePaidDate() {

    $('#waitingpanel_updateDate').modal('show');
    document.getElementById("spntext").innerHTML = "System is updating details. Please wait.";

    var ToAddress = "jim@infinity-data.com,anita@infinity-data.com";
    var ToCC = "s.chandrakant@infinity-data.com";
    var ToCC = "cm@infinity-data.com, hetal@infinity-data.com,s.chandrakant@infinity-data.com";
    var ToBCC = "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us,b.shubhangi@infinityinternationals.us";

    // var ToAddress = "b.shubhangi@infinityinternationals.us";
    // var ToCC = "b.shubhangi@infinityinternationals.us";

    var IPS_InvoiceIDs = 0;
    var IPS_chkLen = IPS_chkIds.length;

    if (IPS_chkLen > 0) {

        for (let i = 0; i < IPS_chkLen; i++) {

            IPS_InvoiceIDs = IPS_InvoiceIDs + "," + IPS_chkIds[i];
        }

        var IPS_PaymentDate = document.getElementById("apIPS_UpPaidDate").value;
        var IPS_Remark = document.getElementById("apIPS_updateRemark").value;
        var IPS_UTR = document.getElementById("apIPS_UTRNo").value;

        if (IPS_PaymentDate == "") {
            alert("Please select Date.");
            return false;
        }
        if (IPS_UTR == "") {
            alert("Please enter UTR #.");
            return false;
        }
        if (IPS_Remark == "") {
            alert("Please enter Remark.");
            return false;
        }

        PageMethods.UpdateRemark(IPS_InvoiceIDs, IPS_Remark, IPS_PaymentDate, 'IPS', IPS_UTR, ToAddress, ToCC, ToBCC, OnSuccessUpdatePaidDate_IPS, OnErrorUpdatePaidDate_IPS);
        return false;
    }
    else {
        alert("Please select records.");
        return true;
    }
}

function OnSuccessUpdatePaidDate_IPS(result) {

    $('#waitingpanel_updateDate').modal('hide');

    InvoiceID = result;

    if (result > 0) {

        alert("Date updated successfully.");
        location.reload();
        BindAPIPSInvoiceApproval_Grid();
        return false;
    }
    else {

        alert("Oops! Error occured while updating status. Please contact administrator");
        location.reload();
        BindAPIPSInvoiceApproval_Grid();
        return false;
    }
}

function OnErrorUpdatePaidDate_IPS(error) {
    alert(error.responseText);
}

