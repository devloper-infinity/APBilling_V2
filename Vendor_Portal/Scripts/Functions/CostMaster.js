/* ------------------------ Cost Master Variables ------------------------ */

var cm_table;
var cm_html;
var cm_IPS_table;
var cm_IPS_html;

/* ------------------------ Approval Cost Variables ------------------------ */

var approvalcost_table;
var approvalcost_html;

var approvalcostIPS_table;
var approvalcostIPS_html;
var ac_ConfID = 0;

/* ------------------------ Cost Master Functions ------------------------ */

function CostMasterCanopy_BindGrid() {

    cm_html = '';
    $('#load1').show();

    $.ajax({
        url: "CostMaster.aspx/GetAllVendorRateConfiguration_Canopy",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {

                var addeddate = eval(value.AddedDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1).toLocaleDateString(\"en-US\")"));

                cm_html += '<tr>';
                cm_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                cm_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                cm_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                cm_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.RType) + '</td>';
                cm_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Slab) + '</td>';
                cm_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Rate) + '</td>';
                cm_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.TRate) + '</td>';
                cm_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(addeddate) + '</td>';
                cm_html += '</tr>';
            });

            $('#table_costMaster_Canopy tbody').html(cm_html);

            if ($.fn.dataTable.isDataTable('#table_costMaster_Canopy')) {
                cm_table.destroy();
            }

            cm_table = $('#table_costMaster_Canopy').DataTable({
                dom: 'lftip',
                //scrollX: true,
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
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
    $('#load1').hide();
    return false;
}

function CostMasterIPS_BindGrid() {

    cm_html = '';
    $('#load1').show();

    $.ajax({
        url: "CostMaster.aspx/GetAllVendorRateConfiguration_IPS",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {

                var addeddate = eval(value.AddedDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1).toLocaleDateString(\"en-US\")"));

                cm_IPS_html += '<tr>';
                cm_IPS_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.RType) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Slab) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Rate) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.TRate) + '</td>';
                cm_IPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(addeddate) + '</td>';
                cm_IPS_html += '</tr>';
            });

            $('#table_costMaster_IPS tbody').html(cm_IPS_html);

            if ($.fn.dataTable.isDataTable('#table_costMaster_IPS')) {
                cm_IPS_table.destroy();
            }

            cm_IPS_table = $('#table_costMaster_IPS').DataTable({
                dom: 'lftip',
                //scrollX: true,
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
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

    $('#load1').hide();
    return false;
}

function GetVendor(Company) {

    var Com = Company.options[Company.selectedIndex].text;

    var select = document.getElementById("cm_Vendor");
    let options = select.getElementsByTagName('option');

    for (var i = options.length; i--;) {
        select.removeChild(options[i]);
    }

    $("#cm_Vendor").append($("<option></option>").val("Select").html("Select"));
    $.ajax({
        type: "POST", url: "CostMaster.aspx/GetAllVendorAsPerCompany", dataType: "json", contentType: "application/json",
        data: "{Company:'" + Com + "'}",

        success: function (res) {

            var dataArray = JSON.parse(res.d);

            $.each(dataArray, function (data, value) {

                $("#cm_Vendor").append($("<option></option>").val(value.Vendor).html(value.Vendor));
            })
        }
    });
}

function cm_Submit() {

    var cm_Vendor = document.getElementById("cm_Vendor");
    var Vendor = cm_Vendor.options[cm_Vendor.selectedIndex].text;

    var EffectiveDate = document.getElementById("cm_EffectiveDate").value;

    var cm_Type = document.getElementById("cm_Type");
    var Type = cm_Type.options[cm_Type.selectedIndex].text;

    var BaseRate = document.getElementById("cm_BaseRate").value;

    var cm_company = document.getElementById("cm_company");
    var company = cm_company.options[cm_company.selectedIndex].text;


    if (Vendor == "Select") {
        alert("Please select vendor.");
        document.getElementById("cm_Vendor").focus();
        return false;
    }

    if (company == "Select") {
        alert("Please select company.");
        document.getElementById("cm_company").focus();
        return false;
    }
    if (EffectiveDate == "") {
        alert("Please select Effective Date.");
        document.getElementById("cm_EffectiveDate ").focus();
        return false;
    }
    if (Type == "Select") {
        alert("Please select type.");
        document.getElementById("cm_Type").focus();
        return false;
    }
    if (BaseRate == "") {
        alert("Please enter Base Rate.");
        document.getElementById("cm_BaseRate").focus();
        return false;
    }

    PageMethods.InsertVendorRateConfigurationForbilling(company, Vendor, EffectiveDate, Type, BaseRate, cm_OnSuccessSubmit, cm_OnErrorSubmit);
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

/* ------------------------ Approval Cost Functions ------------------------ */

function approvalCostCanopy_BindGrid() {

    approvalcost_html = '';
    $('#load1').show();

    $.ajax({
        url: "ApproveCost.aspx/GetAllVendorRateConfiguration_ForApproval_Canopy",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {

                var addeddate = eval(value.AddedDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1).toLocaleDateString(\"en-US\")"));

                approvalcost_html += '<tr>';

                approvalcost_html += '<td style="text-wrap: nowrap;display:none;">' + blankForNull(value.RConfigurationID) + '</td>';
                approvalcost_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="ac_Approve" onclick="ac_ShowApprovePopUp(' + value.RConfigurationID + ',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';
                approvalcost_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Company) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Vendor) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.RType) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Slab) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Rate) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.TRate) + '</td>';
                approvalcost_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(addeddate) + '</td>';
                approvalcost_html += '</tr>';
            });

            $('#table_approvalCostCanopy tbody').html(approvalcost_html);

            if ($.fn.dataTable.isDataTable('#table_approvalCostCanopy')) {
                approvalcost_table.destroy();
            }

            approvalcost_table = $('#table_approvalCostCanopy').DataTable({
                dom: 'lftip',
                scrollX: true,
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
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
    $('#load1').hide();
    return false;
}

function approvalCostIPS_BindGrid() {

    approvalcostIPS_html = '';
    $('#load1').show();

    $.ajax({
        url: "ApproveCost.aspx/GetAllVendorRateConfiguration_ForApproval_IPS",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {

                var addeddate = eval(value.AddedDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1).toLocaleDateString(\"en-US\")"));

                approvalcostIPS_html += '<tr>';

                approvalcostIPS_html += '<td style="text-wrap: nowrap;display:none;">' + blankForNull(value.RConfigurationID) + '</td>';
                approvalcostIPS_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="ac_Approve" onclick="ac_ShowApprovePopUp(' + value.RConfigurationID + ',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Company) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Vendor) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.RType) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Slab) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.Rate) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(value.TRate) + '</td>';
                approvalcostIPS_html += '<td style="text-wrap: nowrap; text-align: center">' + blankForNull(addeddate) + '</td>';
                approvalcostIPS_html += '</tr>';
            });

            $('#table_approvalCostIPS tbody').html(approvalcostIPS_html);

            if ($.fn.dataTable.isDataTable('#table_approvalCostIPS')) {

                approvalCostIPS_table.destroy();
            }

            approvalCostIPS_table = $('#table_approvalCostIPS').DataTable({
                dom: 'lftip',
                //scrollX: true,
                destroy: true,
                "autoWidth": true,
                paging: true,
                select: true,
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
    $('#load1').hide();
    return false;
}

function ac_ShowApprovePopUp(RConfigurationID, index) {

    var row = approvalcost_table.row(index).data();

    ac_ConfID = row[0];
    document.getElementById("ac_VendorName").innerHTML = "<b>Vendor Name : </b>" + row[4];
    document.getElementById("ac_EffectiveDate").innerHTML = "<b>Effective Date : </b>" + row[8];
    document.getElementById("ac_Type").innerHTML = "<b>Type : </b>" + row[5];
    document.getElementById("ac_BaseRate").innerHTML = "<b>Base Rate : </b>" + row[7];

    $('#ac_ApproveCost').modal('show');
}

function ac_ShowApprovePopUpIPS(RConfigurationID, index) {

    var row = approvalcostIPS_table.row(index).data();

    ac_ConfID = row[0];
    document.getElementById("ac_VendorName").innerHTML = "<b>Vendor Name : </b>" + row[4];
    document.getElementById("ac_EffectiveDate").innerHTML = "<b>Effective Date : </b>" + row[8];
    document.getElementById("ac_Type").innerHTML = "<b>Type : </b>" + row[5];
    document.getElementById("ac_BaseRate").innerHTML = "<b>Base Rate : </b>" + row[7];

    $('#ac_ApproveCost').modal('show');
}

function ac_ApprvoveCost() {

    if (ac_ConfID > 0) {

        var Remark = document.getElementById("ac_Remark").value;

        if (Remark == "") {
            alert("Please enter remark.");
            document.getElementById("ac_Remark").focus();
            return false;
        }

        PageMethods.ApproveVendorRateConfigurastion(ac_ConfID, Remark, ac_OnSuccessSubmit, ac_OnErrorSubmit);
        return false;
    }
    else {
        return false;
    }
}

function ac_OnSuccessSubmit(result) {
    alert("Cost aaproved Successfully.")
    location.reload();
    return false;
}

function ac_OnErrorSubmit(error) {
    alert(error.responseText);
    return false;
}
