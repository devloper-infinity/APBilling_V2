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