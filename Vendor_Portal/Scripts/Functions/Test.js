
var chkall_Compliance = [];
var InvoiceID_Canopy;
var invrec_IPS_html = '';
var invrec_IPS_table;

var compliance_Conopy_table;
var Commonreconcile_table;
var ips_rc_Compliance_html = '';
var ird_Comp_InvoiceID = 0;
var ird_CommonInvoiceID = 0;
var ird_Vendor = "";
var ird_Month = "";
var ird_Year = "";

function BindIPSGridForReconcile() {


    $('#load1').show();

    invrec_IPS_html = '';

    $.ajax({
        url: "InvoiceReconciliation.aspx/GetAllIPSInvoiceForReconcile",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);
            // alert(data.d);
            $.each(dataArray, function (index, value) {

                invrec_IPS_html += '<tr>';
                invrec_IPS_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="rc_Actions" onclick="rc_InvoicesRedirect(\'' + blankForNull(value.InvoiceType) + '\',\'' + blankForNull(value.Month) + '\',\'' + blankForNull(value.Year) + '\',\'' + blankForNull(value.InvoiceId) + '\',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrec_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrec_IPS_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#reconcile_invrec_IPS')) {
                invrec_IPS_table.destroy();
            }
            $('#reconcile_invrec_IPS tbody').html(invrec_IPS_html);

            invrec_IPS_table = $('#reconcile_invrec_IPS').DataTable({
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

function rc_InvoicesRedirect(InvType, month, year, InvoiceId, index) {
    var url = "InvoiceReconciliationDetails.aspx?Type=" + InvType + "&Month=" + month + "&Year=" + year + "&InvoiceID=" + InvoiceId;
    var iframe = document.getElementById('invoiceFrame');

    iframe.src = url;

    iframe.onload = function () {
        try {
            var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
            var style = iframeDoc.createElement('style');

            style.innerHTML = `
                nav, .navbar, .main-header, #header, .top-menu-bar { 
                    display: none !important; 
                }
                
                body, .wrapper, .main-content {
                    padding-top: 0px !important;
                    margin-top: 0px !important;
                }
            `;

            iframeDoc.head.appendChild(style);
        } catch (e) {
            console.log("Error hiding main menu: ", e);
        }
    };

    document.getElementById('invoiceModal').style.display = 'flex';
}


function recinvoice_closepopup() {

    const urlParams = new URLSearchParams(window.location.search);
    const InvoiceType = urlParams.get('Type');

    if (InvoiceType == "LauraMac") {
        //  BindLauraMaForreconcile();
    }
    else if (InvoiceType == "Stewart_IA") {
        BindStewartForeconcile();
    }
    else if (InvoiceType == "Compliance") {
        IPS_Reconcile_BindGrid();
    }
    $('#recinvoice_dverror').modal('hide');

    return false;
}

function ird_btnSubmitReconcile() {

    var chkBox = document.getElementById("select-all");

    //var Remark = document.getElementById("ird_Remark").value;
    var Remark = document.getElementById("txt_reconcileRemark").value;

    if (chkBox.checked == true) {
        PageMethods.VerifyCompliance(ird_Comp_InvoiceID, Remark, comp_OnSuccessSubmit, comp_OnErrorSubmit);
    }

    else if (chkBox.checked == false) {
        alert("Please select loans!");
        return true;
    }
    return false;
}

function BindCanopyGrid() {

    // $('#load1').show();

    invrec_html_canopy = '';
    $.ajax({
        url: "InvoiceReconciliation.aspx/GetAllCanopyInvoiceForReconcile",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//
            $.each(dataArray, function (index, value) {
                invrec_html_canopy += '<tr>';
                //invrec_html_canopy += '<td><div class="btn-group">';
                //invrec_html_canopy += '<div class="btn-group">';
                //invrec_html_canopy += '<div type="button" data-toggle="dropdown" aria-expanded="false"><i style="color: dodgerblue; font-size:14px;" class="uil fs-0 me-2 uil-cog"></i>';
                //invrec_html_canopy += '<span class="sr-only"></span></div><div class="dropdown-menu" role="menu" style="">';
                //invrec_html_canopy += '<a class="dropdown-item" href="#!" id="CnpActions" onclick="Cnp_ReconcileInvoice_redirect(\'' + blankForNull(value.InvoiceType) + '\',\'' + blankForNull(value.Month) + '\',\'' + blankForNull(value.Year) + '\',' + index + ',1);"><span style="color: forestgreen;"><i class="uil fs-0 me-2 uil-align-center-v"></i></span>&nbsp;&nbsp;Reconcile Invoice</a>';
                //invrec_html_canopy += '<a class="dropdown-item" href="#!" id="CnpActionsEx" onclick="Cnp_Step3Approval(\'' + blankForNull(value.InvoiceType) + '\',\'' + blankForNull(value.Month) + '\',\'' + blankForNull(value.Year) + '\',' + index + ');"><span style="color: red;"><i class="uil fs-0 me-2 uil-document-layout-left"></i></span>&nbsp;&nbsp;View Loan Details</a><div class="dropdown-divider"></div></div></div></td>';

                invrec_html_canopy += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="cnpActions" onclick="cnp_ReconcileInvoice_redirect(\'' + blankForNull(value.InvoiceType) + '\',\'' + blankForNull(value.Month) + '\',\'' + blankForNull(value.Year) + '\',\'' + blankForNull(value.InvoiceId) + '\',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-align-center-v"></i></span></a></td>';
                //invrec_html_canopy += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="cnpActions" onclick="cnp_ReconcileInvoice_redirect(\'' + blankForNull(value.InvoiceType) + '\',\'' + blankForNull(value.Month) + '\',\'' + blankForNull(value.Year) + '\',\'' + blankForNull(value.InvoiceId) + '\',' + index + ');" title="Reconsile"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-align-center-v" style="font-size:16px;"></i></span></a></td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.BalanceNew) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.NoOfLoans) + '</td>';
                invrec_html_canopy += '<td style="text-wrap: nowrap;">' + blankForNull(value.Remark) + '</td>';
                invrec_html_canopy += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invrec_canopy')) {
                invrec_canopy.destroy();
            }
            $('#invrec_canopy tbody').html(invrec_html_canopy);
            //else
            invrec_canopy = $('#invrec_canopy').DataTable({
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
                    jQuery('.dataTable').wrap('<div class="dataTables_scroll" />');
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

function cnp_ReconcileInvoice_redirect(InvType, month, year, InvoiceId, index) {
    var url = "InvoiceReconciliationDetails.aspx?Type=" + InvType + "&Month=" + month + "&Year=" + year + "&InvoiceID=" + InvoiceId;
    var iframe = document.getElementById('invoiceFrame');

    iframe.src = url;

    iframe.onload = function () {
        try {
            var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
            var style = iframeDoc.createElement('style');
            style.innerHTML = `
                nav, .navbar, .main-header, #header, .top-menu-bar { 
                    display: none !important; 
                }
                
                .card, .panel, [class*="reconciliation-header"] {
                    display: block !important;
                }
                body, .wrapper, .main-content {
                    padding-top: 0px !important;
                    margin-top: 0px !important;
                }
            `;
            iframeDoc.head.appendChild(style);
        } catch (e) {
            console.log("Error hiding main menu: ", e);
        }
    };

    document.getElementById('invoiceModal').style.display = 'flex';
}

function closeInvoiceModal() {
    document.getElementById('invoiceModal').style.display = 'none';
    document.getElementById('invoiceFrame').src = ''; 
}

function BindStewartForeconcile() {

    document.getElementById("dvstewart").style.display = '';
    document.getElementById("dvLauraMac").style.display = 'none';
    document.getElementById("dvCompliance").style.display = 'none';

    $('#load1').show();
    const urlParams = new URLSearchParams(window.location.search);
    const Month = urlParams.get('Month');
    const Year = urlParams.get('Year');
    const InvoiceID = urlParams.get('InvoiceID');

    stewart_html = '';
    $.ajax({
        url: "InvoiceReconciliationDetails.aspx/GetStewartDataForReconcile",
        type: "POST",
        dataType: "json",
        data: "{Month:'" + Month + "',Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);//
            $.each(dataArray, function (index, value) {
                stewart_html += '<tr>';
                if (blankForNull(value.IsVerify) == "1") {
                    stewart_html += '<td style="text-wrap: nowrap;text-align:center;"><input type="checkbox" disabled="disabled" checked="checked" id="chkRec_' + blankForNull(value.BillingId) + '" /></td>';
                    document.getElementById("chkall").checked = true;
                    document.getElementById("chkall").disabled = true;
                }
                else {
                    stewart_html += '<td style="text-wrap: nowrap;text-align:center;"><input type="checkbox" id="chkRec_' + blankForNull(value.BillingId) + '" /></td>';
                    document.getElementById("chkall").checked = false;
                    document.getElementById("chkall").disabled = false;
                }
                stewart_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.BillingId) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceID) + '</td>';
                stewart_html += '<td style="text`-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.LoanNumber) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.CompleteDate) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull(value.Fee) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull(value.BilledRemark) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;text-align:center;display:none;">' + blankForNull(value.DisputeValue) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap;text-align:center;"><input type="number" id="rec_disputevalue_' + index + '" style="text-align:center; width:50px;" value="' + blankForNull(value.DisputeValue) + '" /></td>';
                stewart_html += '<td style="text-wrap: nowrap;"><input type="text" id="rec_userremark_' + index + '" style="width:250px;" value="' + blankForNull(value.UserRemark) + '" /></td>';
                stewart_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.SysRemarkNew) + '</td>';
                stewart_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.UserRemark) + '</td>';
                stewart_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invrecdetails_canopy_stewart')) {
                invrecdetails_canopy_stewart.destroy();
            }
            $('#invrecdetails_canopy_stewart tbody').html(stewart_html);
            //else
            invrecdetails_canopy_stewart = $('#invrecdetails_canopy_stewart').DataTable({
                dom: 'lBftip',
                scrollx: true,
                destroy: true,
                "paging": false,
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
                buttons: [
                    {
                        text: 'Verify',
                        class: 'html5',
                        action: function (e, dt, node, config) {
                            flag = false;
                            dt.rows().every(function (rowIdx, tableLoop, rowLoop) {
                                var data = this.data()
                                var billid = data[2];

                                if (document.getElementById("chkRec_" + billid).checked == true) {
                                    var disputevalue = document.getElementById("rec_disputevalue_" + rowIdx).value;
                                    var disputeremark = document.getElementById("rec_userremark_" + rowIdx).value;
                                    if (parseFloat(disputevalue) > 0 && disputeremark == "") {
                                        alert("Please enter remark for dispute record for loan # " + data[6]);
                                        document.getElementById("rec_userremark_" + rowIdx).focus();
                                        flag = true;
                                        return false;
                                    }
                                    else if (disputeremark == "") {
                                        alert("Please enter remark for loan # " + data[6]);
                                        document.getElementById("rec_userremark_" + rowIdx).focus();
                                        flag = true;
                                        return false;
                                    }
                                    else if (disputeremark == "RecordNotFound") {
                                        alert("Please enter remark for loan # " + data[6]);
                                        document.getElementById("rec_userremark_" + rowIdx).focus();
                                        flag = true;
                                        return false;
                                    }
                                    else {

                                        if (flag == false) {
                                            var billingid = data[2];
                                            var userremark = disputeremark;
                                            PageMethods.VerifyLoans(billingid, disputevalue, userremark, verifystewart_OnSuccess, verifystewart_OnError);
                                            flag = false;
                                        }
                                    }
                                    if (flag == true) {
                                        return false;
                                    }
                                }
                            });
                        }
                    }
                ],

                footerCallback: function (row, data, start, end, display) {
                    let api = this.api();

                    // Remove the formatting to get integer data for summation
                    let intVal = function (i) {
                        return typeof i === 'string'
                            ? i.replace(/[\$,]/g, '') * 1
                            : typeof i === 'number'
                                ? i
                                : 0;
                    };

                    // Total over all pages
                    total = api
                        .column(8)
                        .data()
                        .reduce((a, b) => intVal(a) + intVal(b), 0);

                    clienttotal = api
                        .column(9)
                        .data()
                        .reduce((a, b) => intVal(a) + intVal(b), 0);

                    disputetotal = api
                        .column(10)
                        .data()
                        .reduce((a, b) => intVal(a) + intVal(b), 0);

                    // Total over this page

                    // Update footer
                    api.column(6).footer().innerHTML =
                        total;
                    api.column(7).footer().innerHTML =
                        clienttotal;
                }


            });

            //$('#fnalize tbody').on('click', 'tr', function () {
            //    row = table.row(this).data();
            //});
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    return false;
}

function BindCanopyComplianceGrid() {

    document.getElementById("dvstewart").style.display = 'none';
    document.getElementById("dvLauraMac").style.display = 'none';
    document.getElementById("dvCompliance").style.display = '';

    $('#load1').show();

    const urlParams = new URLSearchParams(window.location.search);
    const Month = urlParams.get('Month');
    const Year = urlParams.get('Year');
    ird_Comp_InvoiceID = urlParams.get('InvoiceID');

    ips_rc_Compliance_html = '';
    var columns = [];

    $.ajax({
        url: "InvoiceReconciliationDetails.aspx/GetComplianceRecordForReconsile",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + ird_Comp_InvoiceID + ", Month:'" + Month + "',Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $.each(dataArray[0], function (key, value) {

                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                columns.push(my_item);
            });

            //if ($.fn.dataTable.isDataTable('#table_ips_rc_Compliance')) {
            //    compliance_Conopy_table.destroy();
            //}

            //  $('#table_ips_rc_Compliance tbody').html(ips_rc_Compliance_html);
            //else
            compliance_Conopy_table = $('#table_ips_rc_Compliance').DataTable({
                dom: 'Blftip',
                scrollx: true,
                destroy: true,
                "paging": false,
                "autoWidth": true,
                select: true,
                "ordering": false,
                processing: true,
                'select': {
                    'style': 'single'
                },
                "data": dataArray,
                "columns": columns,

                initComplete: function () {
                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: 'IPS Compliance Records', autoFilter: true,
                    },
                ],
                columnDefs: [
                    {
                        targets: 0,
                        "width": "45px",
                        render: function (data, type, row, meta) {
                            return '<td style="text-wrap: nowrap;text-align:center;"><input type="checkbox" onclick="return getselected(this,\'' + meta.row + '\');" />';
                            //return '<input type="button" class="btn-primary" id=viewdetails-"' + meta.row + '" value="Details" onclick="return ViewPolicyDetails(\'' + meta.row + '\');" />&nbsp;<input type="button" class="btn-default" id=viewtasks-"' + meta.row + '" value="Tasks"  onclick="return ViewTaskDetails(\'' + meta.row + '\');"/>';
                        }
                    }
                ],
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    return false;
}

function getAllSelectCompliance(chkallCompliance) {

    alert(compliance_Conopy_table);

    if (chkallCompliance.checked == true) {

        var data = compliance_Conopy_table.rows().data();

        alert(data);

        data.each(function (value, index) {
            var ComEaseID = value[2];
            document.getElementById("chkComp_" + ComEaseID).checked = true;
        });
    }
    else {
        var data = compliance_Conopy_table.rows().data();

        data.each(function (value, index) {
            var ComEaseID = value[2];
            document.getElementById("chkComp_" + ComEaseID).checked = false;
        });
    }
}

/*----------------------- Generalised Reconcile ------------------------*/

// function getselected(chk, row) {
//     //var rows = $('#table_Commonreconcile').DataTable().rows(row).data();
//     //alert(rows[0].chkAll);
//     if (chk.checked) {
//         var table = $('#table_Commonreconcile').DataTable();

//         var nodes = table.rows().nodes();

//         $(nodes).find('input[type="checkbox"]').each(function () {
//             this.checked = chk.checked;
//         });
//     }
//     else {
//         var data = $('#table_Commonreconcile').DataTable().rows().data();
//         data.each(function (value, index) {

//             document.getElementById("chkId_" + value.chkAll).checked = false;
//         });
//     }
// }

function BindCanopyGeneralisedGrid(Type, InvID, Month, Year) {

    $('#load1').show();

    var filename = Type + '-' + Month + '-' + Year + ' Invoice Reconciallation';
    var columns = [];

    ird_Vendor = Type;
    ird_CommonInvoiceID = InvID;
    ird_Month = Month;
    ird_Year = Year;

    $.ajax({
        url: "InvoiceReconciliationDetails.aspx/GetVendorRecordForReconcile_Generilised",
        type: "POST",
        dataType: "json",
        data: "{InvoiceID:" + InvID + ",VendorType:'" + Type + "', Month:'" + Month + "',Year:'" + Year + "'}",
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            var dataArray = JSON.parse(data.d);
            var header = {};
            header.title = '<input type="checkbox" id="select-all" onclick="return getselected(this);">';
            header.data = null;
            columns.push(header);

            $.each(dataArray[0], function (key, value) {

                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                columns.push(my_item);
            });

            Commonreconcile_table = $('#table_Commonreconcile').DataTable({
                dom: 'Blftip',
                scrollx: true,
                destroy: true,
                "paging": true,
                "autoWidth": false,
                select: true,
                "ordering": false,
                processing: true,
                'select': {
                    'style': 'single'
                },
                "data": dataArray,
                "columns": columns,

                initComplete: function () {

                    $('#load1').hide();
                    $('#table_Commonreconcile th, #table_Commonreconcile td').css('min-width', '100px');
                },

                buttons: [
                    {
                        extend: 'excelHtml5', title: filename, autoFilter: true,
                    },
                ],
                columnDefs: [

                    {
                        targets: 0,
                        "width": "45px",
                        render: function (data, type, row, meta) {
                            return '<input type="checkbox" id="chkId_' + row.chkAll + '" onclick="return getselected(this,\'' + meta.row + '\');" />';//<td style="text-wrap: nowrap;text-align:center;"></td>';
                        }
                    },
                    {
                        targets: 1,
                        visible: false,
                    },
                    {
                        targets: columns.length - 1,
                        render: function (data, type, row, meta) {

                            //alert(row.chkAll);
                            //alert("txt_reconcileRemark_" + row.chkAll);

                            return '<td style="text-wrap: nowrap;text-align:center;"><input type="text" id="txt_reconcileRemark_' + row.chkAll + '"  style="width:500px;" value="' + blankForNull(row.UserRemark) + '"/></td>';
                        }
                    }
                ],
            });
        },

        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    return false;
}

// function ird_btnSubmitCommonReconcile() {

//     $('#waitingpanel').modal('show');

//     var chkBox = document.getElementById("select-all");
//     var All_Remark;
//     var All_ID;

//     var data = $('#table_Commonreconcile').DataTable().rows().data();

//     data.each(function (value, index) {

//         var ID = value.chkAll;
//         All_ID = All_ID + "|" + ID;

//         var Remark = document.getElementById("txt_reconcileRemark_" + value.chkAll).value;
//         All_Remark = All_Remark + "|" + Remark;
//     });

//     if (chkBox.checked == true) {

//         PageMethods.ReconcileLoan_Generalised(ird_Vendor, ird_Month, ird_Year, All_ID, All_Remark, comp_OnSuccessSubmit, comp_OnErrorSubmit);
//     }
//     else if (chkBox.checked == false) {
//         alert("Please select loans!");
//         return true;
//     }
//     return false;
// }
function getselected(chk, row) {
    if (chk.checked) {
        var table = $('#table_Commonreconcile').DataTable();
        var nodes = table.rows().nodes();

        $(nodes).find('input[type="checkbox"]').each(function () {
            this.checked = chk.checked;
        });
    }
    else {
        var data = $('#table_Commonreconcile').DataTable().rows().data();
        data.each(function (value, index) {
            var chkEl = document.getElementById("chkId_" + value.chkAll);
            if (chkEl) chkEl.checked = false;
        });
    }
}

function ird_btnSubmitCommonReconcile() {
    var chkBox = document.getElementById("select-all");
    var All_ID = "undefined";
    var All_Remark = "undefined";
    var selectedCount = 0;

    var data = $('#table_Commonreconcile').DataTable().rows().data();

    data.each(function (value, index) {
        var chkItem = document.getElementById("chkId_" + value.chkAll);

        if (chkItem && chkItem.checked) {
            selectedCount++;
            var ID = value.chkAll;
            All_ID = All_ID + "|" + ID;

            var Remark = document.getElementById("txt_reconcileRemark_" + value.chkAll).value;
            All_Remark = All_Remark + "|" + Remark;
        }
    });

    if (selectedCount === 0) {
        alert("Please select loans!");
        return false;
    }

    $('#waitingpanel').modal('show');

    // C# method call
    PageMethods.ReconcileLoan_Generalised(ird_Vendor, ird_Month, ird_Year, All_ID, All_Remark, comp_OnSuccessSubmit, comp_OnErrorSubmit);

    return false;
}
function comp_OnSuccessSubmit(result) {

    $('#waitingpanel').modal('hide');
    InvoiceID_Canopy = result;
    ird_Comp_InvoiceID = 0;

    if (result > 0) {

        alert("Loans verified successfully!");
        location.href = "InvoiceReconciliation.aspx";
        return false;
    }
    else {
        alert("Oops! Error occured while verifying loans. Please contact administrator!");
        return false;
    }

    return false;
}

function comp_OnErrorSubmit(error) {
    alert(error.responseText);
}

function CheckLoginID_ForRights() {

    $.ajax({
        url: "AddInvoice.aspx/CheckLoginID_ForRights",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {

            //  alert(data.d);
            var LoginID = data.d;

            if (LoginID == '216' || LoginID == '277' || LoginID == '292') {

                $('#li_InvReonc_Conopy').hide();
            }
            else {
                $('#li_InvReonc_Conopy').show();
            }
        }
    });
}