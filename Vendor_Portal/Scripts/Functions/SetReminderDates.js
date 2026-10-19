
var srd_records_table;
var srd_records_html;
var srd_ReminderDateID = 0;

function BindSrd_Grid() {

    $('#load1').show();
    srd_records_html = '';

    $.ajax({
        url: "SetReminderDates.aspx/GetAllReminderDates",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (data) {

            var dataArray = JSON.parse(data.d);
            $.each(dataArray, function (index, value) {

                srd_records_html += '<tr>';
                srd_records_html += '<td style="text-align:center;"><a class="dropdown-item" href="#!" id="srd_Actions" onclick="srd_showPopUp(\'' + blankForNull(value.ReminderDateID) + '\',' + index + ');"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';
                srd_records_html += '<td style="text-wrap: nowrap;text-align:center;">' + blankForNull((index + 1)) + '</td>';
                srd_records_html += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.ReminderDateID) + '</td>';
                srd_records_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Company) + '</td>';
                srd_records_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Vendor) + '</td>';
                srd_records_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.ReminderDate) + '</td>';
                srd_records_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.AddedByName) + '</td>';
                srd_records_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.addDate) + '</td>';
                srd_records_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#table_srd_records')) {
                srd_records_table.destroy();
            }
            $('#table_srd_records tbody').html(srd_records_html);

            srd_records_table = $('#table_srd_records').DataTable({
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

function srd_btnsubmit() {

    var srdCompany = document.getElementById("srd_Company");
    var Company = srdCompany.options[srdCompany.selectedIndex].value;

    var srdVendor = document.getElementById("srd_Vendor");
    var Vendor = srdVendor.options[srdVendor.selectedIndex].value;

    var srdDay = document.getElementById("srd_Day");
    var NewDay = srdDay.options[srdDay.selectedIndex].value;

    if (Company == "") {
        alert("Please select company.");
        return false;
    }
    if (Vendor == "") {
        alert("Please select Vendor.");
        return false;
    }
    if (NewDay == "") {
        alert("Please select Date.");
        return false;
    }

    PageMethods.InsertIntoReminderDate(Company, Vendor, NewDay, OnSuccessSetDate, OnErrorSetDate);
    return false;
}

function OnSuccessSetDate(result) {

    if (result > 0) {
        alert("Date set successfully.");
        $("#srd_Day")[0].selectedIndex = 0;
        $("#srd_Company")[0].selectedIndex = 0;
        $("#srd_Vendor")[0].selectedIndex = 0;
        BindSrd_Grid();
        return false;
    }
    else {

        alert("Oops! Error occured while setting date. Please contact administrator");
        location.reload();
        BindMISInvoiceApproval_Grid();
        return false;
    }
}

function OnErrorSetDate(error) {
    alert(error.responseText);
}

function srd_showPopUp(ReminderDateID, Index) {

    var row = srd_records_table.row(Index).data();

    srd_ReminderDateID = ReminderDateID;

    document.getElementById("srd_lblCompany").innerHTML = row[3];
    document.getElementById("srd_lblVendor").innerHTML = row[4];
    document.getElementById("srd_lblPDay").innerHTML = row[5];
    
    $('#srd_UpdatePopUpDate').modal('show');
}

function srd_updateDate() {

    var Company = document.getElementById("srd_lblCompany").innerHTML;
    var Vendor = document.getElementById("srd_lblVendor").innerHTML;

    var UpdateDay = document.getElementById("srd_UpdateDay");
    var NewUpdateDay = UpdateDay.options[UpdateDay.selectedIndex].value;

    if (NewUpdateDay == "") {
        alert("Please select Date.");
        return false;
    }

    PageMethods.UpdateReminderDate(Company, Vendor, NewUpdateDay, srd_ReminderDateID, OnSuccessUpdateDate, OnErrorUpdateDate);
    return false;
}

function OnSuccessUpdateDate(result) {

    if (result > 0) {
        srd_ReminderDateID = 0;
        alert("Date reset successfully.");
        // location.reload();
        $("#srd_UpdateDay")[0].selectedIndex = 0;
        $('#srd_UpdatePopUpDate').modal('hide');
        BindSrd_Grid();
        return false;
    }
    else {

        alert("Oops! Error occured while resetting date. Please contact administrator");
        location.reload();
        BindMISInvoiceApproval_Grid();
        return false;
    }
}

function OnErrorUpdateDate(error) {
    alert(error.responseText);
}
