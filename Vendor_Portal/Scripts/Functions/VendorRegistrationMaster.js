//Get Vendors
function GetVendors_bindgrid() {
    $('#load1').show();

    let vendorreg_html = '';
    $.ajax({
        url: "VendorRegistrationMaster.aspx/GetAllRegisteredVendors",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);

            $.each(dataArray, function (index, value) {
                let vendorId = value.VendorRegistrationID;

                vendorreg_html += '<tr>';
                vendorreg_html += '<td style="text-wrap: nowrap; text-align:center; display:none">' + blankForNull(vendorId) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;"><a class="dropdown-item" href="VendorRegistration.aspx?VendorID=' + vendorId + '" id="ActionsEx"><span style="color: dodgerblue;"><i class="uil fs-0 me-2 uil-pen"></i></span></a></td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull((index + 1)) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.CompanyName) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.VendorType) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.VendorName) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.ContactPerson) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.BillingCycle) + '</td>';
                vendorreg_html += '<td style="text-wrap: nowrap;">' + blankForNull(value.Process) + '</td>';

                vendorreg_html += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#vendorregistration_table')) {
                $('#vendorregistration_table').DataTable().destroy();
            }

            $('#vendorregistration_table tbody').html(vendorreg_html);

            $('#vendorregistration_table').DataTable({
                dom: '<"row mb-3"<"col-md-6"l><"col-md-6"f>>rtip',
               
                destroy: true,
                "paging": true,
                "autoWidth": false,
                select: true,
                "ordering": false,
                processing: true,
                'select': {
                    'style': 'single'
                },
                initComplete: function () {
                    $('#load1').hide();
                }
            });
        },
        error: function (xhr, status, error) {
            $('#load1').hide();
            console.error("Error loading vendors:", error);
        }
    });

    return false;
}

//When click edit then bind value in input  
function EditConfiguration(vendorId) {
    $('#load1').show();
    $('input[id$="hdfVendorID"]').val(vendorId);

    var param = { vendorRegistrationID: parseInt(vendorId) };

    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/GetVendorDetailsByID",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#load1').hide();
            var data = typeof response.d === 'string' ? JSON.parse(response.d) : response.d;

            if (data && data.length > 0) {
                var row = data[0]; 
                $('#vendorreg_individualcomp').val(row.VendorType || '').trigger('change');

                $('#vendorreg_vendorname').val(row.VendorName || '');
                $('#vendorreg_companyname').val(row.CompanyName || '');
                $('#vendorreg_contactperson').val(row.ContactPerson || '');
                $('#vendorreg_contactno').val(row.ContactNo || '');
                $('#vendorreg_email').val(row.EmailID || '');
                $('#vendorreg_weburl').val(row.WebsiteURL || '');
                $('#vendorreg_payvendorname').val(row.PayToVendorName || '');
                $('#vendorreg_acno').val(row.AccountNumber || '');
                $('#vendorreg_address').val(row.Address || '');
                $('#vendorreg_remark').val(row.Remark || '');

                $('#vendorreg_Domain').val((row.Domain || '').trim());
                $('#vendorreg_PaymentMethod').val((row.PaymentMethod || '').trim());
                $('#vendorreg_Routingno').val(row.RoutingNo || '');
                $('#vendorreg_SSNEIN').val(row.TaxSSNEIN || '');
                $('#vendorreg_TaxID').val(row.TaxID || '');

                var vendorIdVal = row.VendorRegistrationID || row.VendorID;
                $('#vendorregother_vendor').val(vendorIdVal).prop('disabled', true);
                $('#vendorreg_expectedstartdate').val(row.StartDate || '');          
                $('#vendorreg_raterevisiondate').val(row.RateRevisionDate || '');
                $('#vendorreg_workscope').val(row.ScopeOfWork || '');
                $('#vendorregotherinfo_remark').val(row.OtherInfoRemark || '');
                $('#vendorreg_isndasigned').val(row.IsNDASigned || '');
                getnda($('#vendorreg_isndasigned')[0]);
                $('#vendorreg_agreementdate').val(row.AgreementDate || '');
                $('#vendorreg_agreementexpirydate').val(row.ExpiryDate || '');
                $('#vendorreg_agreementclientsigned').val(row.SignedByClient || '');
                $('#vendorreg_agreementinfinitysigned').val(row.SignedByInfinity || '');

                // Agreement Copy
                if (row.AgreementCopy && row.AgreementCopy !== "") {
                    var dbPath = row.AgreementCopy.replace(/\\/g, '/');
                    var virtualIndex = dbPath.indexOf('VendorDocument');
                    var fileUrl = '';

                    if (virtualIndex !== -1) {
                        var baseUrl = window.location.origin;
                        fileUrl = baseUrl + '/' + dbPath.substring(virtualIndex);
                    } else {
                        fileUrl = dbPath.replace('~', '');
                    }

                    var fileName = dbPath.split('/').pop();

                    $('#lnk_agreementattachment_download')
                        .attr('href', fileUrl)
                        .attr('download', fileName)
                        .attr('data-original-path', row.AgreementCopy)
                        .show();

                    $('#lbl_agreementattachment_name')
                        .text(fileName)
                        .attr('title', fileName)
                        .show();
                } else {
                    $('#lnk_agreementattachment_download').hide().removeAttr('data-original-path');
                    $('#lbl_agreementattachment_name').text('').hide();
                }

                // Work Document
                if (row.WorkDocument && row.WorkDocument !== "") {
                    var dbPath = row.WorkDocument.replace(/\\/g, '/');
                    var virtualIndex = dbPath.indexOf('VendorDocument');
                    var fileUrl = '';

                    if (virtualIndex !== -1) {
                        var baseUrl = window.location.origin;
                        fileUrl = baseUrl + '/' + dbPath.substring(virtualIndex);
                    } else {
                        fileUrl = dbPath.replace('~', '');
                    }

                    var fileName = dbPath.split('/').pop();

                    $('#lnk_workdoc_download')
                        .attr('href', fileUrl)
                        .attr('download', fileName)
                        .attr('data-original-path', row.WorkDocument)
                        .show();

                    $('#lbl_workdoc_name')
                        .text(fileName)
                        .attr('title', fileName)
                        .show();
                } else {
                    $('#lnk_workdoc_download').hide().removeAttr('data-original-path');
                    $('#lbl_workdoc_name').text('').hide();
                }

                if (row.PaymentDocument && row.PaymentDocument !== "") {
                    var dbPath = row.PaymentDocument.replace(/\\/g, '/');
                    var virtualIndex = dbPath.indexOf('VendorDocument');
                    var fileUrl = virtualIndex !== -1
                        ? window.location.origin + '/' + dbPath.substring(virtualIndex)
                        : dbPath.replace('~', '');

                    var fileName = dbPath.split('/').pop();

                    $('#lnk_paymentdoc_download')
                        .attr('href', fileUrl)
                        .attr('download', fileName)
                        .attr('data-original-path', row.PaymentDocument)
                        .show();

                    $('#lbl_paymentdoc_name')
                        .text(fileName)
                        .attr('title', fileName)
                        .show();
                } else {
                    $('#lnk_paymentdoc_download').hide().removeAttr('data-original-path');
                    $('#lbl_paymentdoc_name').text('').hide();
                }

                var w9DocPath = row.W9FormDocument || row.W9Form || "";
                if (w9DocPath && w9DocPath !== "") {
                    var dbPath = w9DocPath.replace(/\\/g, '/');
                    var virtualIndex = dbPath.indexOf('VendorDocument');
                    var fileUrl = '';

                    if (virtualIndex !== -1) {
                        var baseUrl = window.location.origin;
                        fileUrl = baseUrl + '/' + dbPath.substring(virtualIndex);
                    } else {
                        fileUrl = dbPath.replace('~', '');
                    }

                    var fileName = dbPath.split('/').pop();

                    $('#lnk_w9doc_download')
                        .attr('href', fileUrl)
                        .attr('download', fileName)
                        .attr('data-original-path', w9DocPath)
                        .show();

                    $('#lbl_w9doc_name')
                        .text(fileName)
                        .attr('title', fileName)
                        .show();
                } else {
                    $('#lnk_w9doc_download').hide().removeAttr('data-original-path');
                    $('#lbl_w9doc_name').text('').hide();
                }

                $('#vendorreg_btnsubmitstep1').text('Update');
                $('#vendorreg_btnsubmit').text('Update');

                loadRateConfiguration(vendorId);
            }
        },
        error: function (xhr, status, error) {
            $('#load1').hide();
            console.log("Error fetching vendor details: " + error);
        }
    });
}

//add new vendor function
function addnewvendor() {
    window.location.href = 'VendorRegistration.aspx';
    return false; 
}

//**************** Vendor Registration ********************/

//back to list function
function Backtolist() {
    window.location.href = 'VendorRegistrationMaster.aspx';
    return false; 
}

//Show Popup click add rate
$('#vendorreg_btnaddnewrate').on('click', function () {
    $('#vendorreg_addnewrate').modal('show');
});

//other info show is nda info 
function getnda(selectElement) {
    var nda1Row = document.getElementById('nda1');
    var nda2Row = document.getElementById('nda2');

    if (selectElement.value === "1") {
        nda1Row.style.display = ""; 
        nda2Row.style.display = "";
    } else {
        nda1Row.style.display = "none";
        nda2Row.style.display = "none";
    }
}

//Add / Update Vendor Registartion
var savedVendorID = 0;
function btnsubmitInsertVendorRegistration() {
    var vendorName = $('#vendorreg_vendorname').val().trim();
    var companyName = $('#vendorreg_companyname').val().trim();
    var contactPerson = $('#vendorreg_contactperson').val().trim();
    var contactNo = $('#vendorreg_contactno').val().trim();
    var email = $('#vendorreg_email').val().trim();
    var webUrl = $('#vendorreg_weburl').val().trim();
    var payVendorName = $('#vendorreg_payvendorname').val().trim();
    var accountNo = $('#vendorreg_acno').val().trim();
    var address = $('#vendorreg_address').val().trim();
    var remark = $('#vendorreg_remark').val().trim();
    var VendorType = $('#vendorreg_individualcomp').val().trim();
    var VendorDomain = $('#vendorreg_Domain').val().trim();
    var VendorPaymentMethod = $('#vendorreg_PaymentMethod').val().trim();
    var VendorRoutingNo = $('#vendorreg_Routingno').val().trim();
    var taxSSNEIN = ($('#vendorreg_SSNEIN').val() || '').trim();
    var taxID = ($('#vendorreg_TaxID').val() || '').trim();

    if (VendorDomain === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please Vendor Domain' }).then(() => { $('#vendorreg_Domain').focus(); });
        return false;
    }
    if (VendorType === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please Vendor Type' }).then(() => { $('#vendorreg_individualcomp').focus(); });
        return false;
    }
    if (vendorName === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Vendor Name.' }).then(() => { $('#vendorreg_vendorname').focus(); });
        return false;
    }
    if (companyName === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select a Company Name.' }).then(() => { $('#vendorreg_companyname').focus(); });
        return false;
    }
    if (contactPerson === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Contact Person name.' }).then(() => { $('#vendorreg_contactperson').focus(); });
        return false;
    }
    if (contactNo === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Contact No' }).then(() => { $('#vendorreg_contactno').focus(); });
        return false;
    }
    if (email === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Email' }).then(() => { $('#vendorreg_email').focus(); });
        return false;
    }
    if (email !== "") {
        var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!emailRegex.test(email)) {
            Swal.fire({ icon: 'warning', title: 'Invalid Input', text: 'Please enter a valid Email ID.' }).then(() => { $('#vendorreg_email').focus(); });
            return false;
        }
    }
    if (payVendorName === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Pay Vendor Name' }).then(() => { $('#vendorreg_payvendorname').focus(); });
        return false;
    }
    if (accountNo === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Account No' }).then(() => { $('#vendorreg_acno').focus(); });
        return false;
    }
    if (VendorPaymentMethod === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please Vendor Payment Method' }).then(() => { $('#vendorreg_PaymentMethod').focus(); });
        return false;
    }
    if (VendorRoutingNo === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Routing No' }).then(() => { $('#vendorreg_Routingno').focus(); });
        return false;
    }
    if (address === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Address' }).then(() => { $('#vendorreg_address').focus(); });
        return false;
    }
    if (taxSSNEIN === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter SSN/EIN' }).then(() => { $('#vendorreg_SSNEIN').focus(); });
        return false;
    }
    if (taxID === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Tax ID' }).then(() => { $('#vendorreg_TaxID').focus(); });
        return false;
    }
    if (Number(accountNo) <= 0) {
        Swal.fire({
            icon: 'warning',
            title: 'Invalid Account Number',
            text: 'Account number must be greater than 0!'
        });
        return false;
    }
    var paymentDocFile = $('#vendorreg_PaymentDocument')[0] ? $('#vendorreg_PaymentDocument')[0].files[0] : null;
    var w9DocFile = $('#vendorreg_W9Form')[0] ? $('#vendorreg_W9Form')[0].files[0] : null;
    var existingPaymentDocPath = $('#lnk_paymentdoc_download').attr('data-original-path') || "";
    var existingW9DocPath = $('#lnk_w9doc_download').attr('data-original-path') || "";

    if (!paymentDocFile && !existingPaymentDocPath) {
        Swal.fire({
            icon: 'warning',
            title: 'Required Field',
            text: 'Please upload Payment Document.'
        }).then(() => {
            $('#vendorreg_PaymentDocument').focus();
        });
        return false;
    }

    const urlParams = new URLSearchParams(window.location.search);
    const vendorIdFromURL = urlParams.get('VendorID') || '0';

    function sendVendorData(paymentDocBase64, w9DocBase64) {
        var formData = {
            vendorName: vendorName,
            companyName: companyName,
            contactPerson: contactPerson,
            contactNo: contactNo,
            emailID: email,
            websiteURL: webUrl,
            address: address,
            payToVendorName: payVendorName,
            accountNumber: accountNo,
            remark: remark,
            vendorId: parseInt(vendorIdFromURL, 10) || 0,
            VendorType: VendorType,
            VendorDomain: VendorDomain,
            VendorPaymentMethod: VendorPaymentMethod,
            VendorRoutingNo: VendorRoutingNo,
            taxSSNEIN: taxSSNEIN,
            taxID: taxID,
            paymentDocument: paymentDocBase64 || existingPaymentDocPath || "",
            w9FormDocument: w9DocBase64 || existingW9DocPath || ""
        };

        $.ajax({
            type: "POST",
            url: "VendorRegistration.aspx/InsertVendorRegistration",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var newVendorID = response.d;

                if (newVendorID > 0) {
                    savedVendorID = newVendorID;
                    $('input[id$="hdfVendorID"]').val(newVendorID);

                    var successMessage = (parseInt(vendorIdFromURL) > 0)
                        ? 'Vendor updated successfully!'
                        : 'Vendor added successfully  and email has been sent.!';

                    Swal.fire({
                        title: 'Success!',
                        text: successMessage,
                        icon: 'success',
                        confirmButtonText: 'OK'
                    }).then((res) => {
                        if (res.isConfirmed) {
                            window.location.href = 'VendorRegistrationMaster.aspx';
                            $('#vendorreg_vendorname').val('');
                            $('#vendorreg_companyname').val('');
                            $('#vendorreg_contactperson').val('');
                            $('#vendorreg_contactno').val('');
                            $('#vendorreg_email').val('');
                            $('#vendorreg_weburl').val('');
                            $('#vendorreg_payvendorname').val('');
                            $('#vendorreg_acno').val('');
                            $('#vendorreg_address').val('');
                            $('#vendorreg_remark').val('');
                            $('#vendorreg_individualcomp').val('').trigger('change');
                            $('input[id$="hdfVendorID"]').val('0');
                            $('#vendorreg_SSNEIN').val('');
                            $('#vendorreg_TaxID').val('');
                            $('#vendorreg_PaymentDocument').val('');
                            $('#vendorreg_W9Form').val('');
                            $('#lnk_paymentdoc_download').hide().removeAttr('data-original-path');
                            $('#lnk_w9doc_download').hide().removeAttr('data-original-path');
                        }
                    });
                    loadVendors();
                }
                else if (newVendorID === -1) {
                    Swal.fire({
                        title: 'Duplicate Entry!',
                        text: 'A vendor with this name already exists.',
                        icon: 'warning',
                        confirmButtonText: 'OK'
                    });
                }
                else {
                    var errorMessage = (parseInt(vendorIdFromURL) > 0)
                        ? 'Something went wrong while updating vendor data.'
                        : 'Something went wrong while saving vendor data.';

                    Swal.fire({
                        title: 'Error!',
                        text: errorMessage,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    title: 'AJAX Error!',
                    text: "An error occurred while saving data: " + error,
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        });
    }

    function processPaymentDoc(callback) {
        if (!paymentDocFile) {
            callback("");
            return;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            callback(paymentDocFile.name + "|" + e.target.result);
        };
        reader.onerror = function () {
            callback("");
        };
        reader.readAsDataURL(paymentDocFile);
    }

    function processW9Doc(callback) {
        if (!w9DocFile) {
            callback("");
            return;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            callback(w9DocFile.name + "|" + e.target.result);
        };
        reader.onerror = function () {
            callback("");
        };
        reader.readAsDataURL(w9DocFile);
    }

    processPaymentDoc(function (paymentDocBase64) {
        processW9Doc(function (w9DocBase64) {
            sendVendorData(paymentDocBase64, w9DocBase64);
        });
    });

    return false;
}


//Add / Update Vendor Other Info

function btnsubmitInsertVendorOtherInfo() {
    var vendorRegistrationID = $('#vendorregother_vendor').val();
    var startDate = $('#vendorreg_expectedstartdate').val();
    var scopeOfWork = $('#vendorreg_workscope').val().trim();
    var isNdaSigned = $('#vendorreg_isndasigned').val();
    var rateRevisionDate = $('#vendorreg_raterevisiondate').val();

    if (vendorRegistrationID === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Vendor.' }).then(() => { $('#vendorregother_vendor').focus(); });
        return false;
    }

    if (startDate === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Start Date.' }).then(() => { $('#vendorreg_expectedstartdate').focus(); });
        return false;
    }
    if (rateRevisionDate === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Rate Revision Date.' }).then(() => { $('#vendorreg_raterevisiondate').focus(); });
        return false;
    }
    if (scopeOfWork === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please enter Scope of Work.' }).then(() => { $('#vendorreg_workscope').focus(); });
        return false;
    }

    if (isNdaSigned === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Is NDA Signed status.' }).then(() => { $('#vendorreg_isndasigned').focus(); });
        return false;
    }

    if (isNdaSigned === "1" && $('#vendorreg_agreementdate').val() === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Agreement Date.' }).then(() => { $('#vendorreg_agreementdate').focus(); });
        return false;
    }
    if (isNdaSigned === "1" && $('#vendorreg_agreementexpirydate').val() === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Agreement Expiry Date.' }).then(() => { $('#vendorreg_agreementexpirydate').focus(); });
        return false;
    }
    if (isNdaSigned === "1" && $('#vendorreg_agreementclientsigned').val() === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Client Signed.' }).then(() => { $('#vendorreg_agreementclientsigned').focus(); });
        return false;
    }
    if (isNdaSigned === "1" && $('#vendorreg_agreementinfinitysigned').val() === "") {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please select Infinity Signed.' }).then(() => { $('#vendorreg_agreementinfinitysigned').focus(); });
        return false;
    }



    var agreementDateVal = $('#vendorreg_agreementdate').val();
    var expiryDateVal = $('#vendorreg_agreementexpirydate').val();

    if (agreementDateVal && expiryDateVal) {
        var agDate = new Date(agreementDateVal);
        var expDate = new Date(expiryDateVal);

        if (expDate <= agDate) {
            Swal.fire({
                icon: 'warning',
                title: 'Invalid Date',
                text: 'Agreement Expiry Date must be after the Agreement Date.'
            }).then(() => {
                $('#vendorreg_agreementexpirydate').focus();
            });
            return false;
        }
    }
    var workDocFile = $('#vendorreg_workdocument')[0] ? $('#vendorreg_workdocument')[0].files[0] : null;
    var agreementFile = $('#vendorreg_agreementattachment')[0] ? $('#vendorreg_agreementattachment')[0].files[0] : null;
    var existingWorkDocPath = $('#lnk_workdoc_download').attr('data-original-path') || "";
    var existingAgreementPath = $('#lnk_agreementattachment_download').attr('data-original-path') || "";

    if (!workDocFile && !existingWorkDocPath) {
        Swal.fire({
            icon: 'warning',
            title: 'Required Field',
            text: 'Please upload Work Document.'
        }).then(() => {
            $('#vendorreg_workdocument').focus();
        });
        return false;
    }

    if (isNdaSigned === "1" && !agreementFile && !existingAgreementPath) {
        Swal.fire({ icon: 'warning', title: 'Required Field', text: 'Please upload Agreement Copy.' }).then(() => { $('#vendorreg_agreementattachment').focus(); });
        return false;
    }
    function sendData(workDocBase64, agreementBase64) {
        var requestData = {
            vendorRegistrationID: parseInt(vendorRegistrationID),
            startDate: startDate,
            rateRevisionDate: $('#vendorreg_raterevisiondate').val(),
            scopeOfWork: scopeOfWork,
            remark: $('#vendorregotherinfo_remark').val(),
            isNDASigned: isNdaSigned,
            agreementDate: $('#vendorreg_agreementdate').val(),
            expiryDate: $('#vendorreg_agreementexpirydate').val(),
            signedByClient: $('#vendorreg_agreementclientsigned').val(),
            signedByInfinity: $('#vendorreg_agreementinfinitysigned').val(),
            workDocument: workDocBase64 || existingWorkDocPath,
            agreementCopy: agreementBase64 || existingAgreementPath
        };

        $.ajax({
            type: "POST",
            url: "VendorRegistration.aspx/AddVendorOtherInfo",
            data: JSON.stringify(requestData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d > 0) {
                    Swal.fire({
                        title: 'Success!',
                        text: 'Vendor Other info saved successfully!',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    }).then((res) => {
                        if (res.isConfirmed) {
                            window.location.href = 'VendorRegistrationMaster.aspx';
                            $('#vendorregother_vendor').val('').trigger('change');
                            $('#vendorreg_expectedstartdate').val('');
                            $('#vendorreg_raterevisiondate').val('');
                            $('#vendorreg_workscope').val('');
                            $('#vendorreg_isndasigned').val('').trigger('change');
                            $('#vendorreg_agreementdate').val('');
                            $('#vendorreg_agreementexpirydate').val('');
                            $('#vendorreg_agreementclientsigned').val('');
                            $('#vendorreg_agreementinfinitysigned').val('');
                            $('#vendorregotherinfo_remark').val('');

                            $('#vendorreg_workdocument').val('');
                            $('#vendorreg_agreementattachment').val('');
                            $('#lnk_workdoc_download').hide().removeAttr('data-original-path');
                            $('#lnk_agreementattachment_download').hide().removeAttr('data-original-path');

                            var nda1Row = document.getElementById('nda1');
                            var nda2Row = document.getElementById('nda2');
                            if (nda1Row) nda1Row.style.display = "none";
                            if (nda2Row) nda2Row.style.display = "none";

                        }
                    });
                } else {
                    Swal.fire({ title: 'Error!', text: "Something went wrong while saving data.", icon: 'error', confirmButtonText: 'OK' });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({ title: 'AJAX Error!', text: "An error occurred: " + error, icon: 'error', confirmButtonText: 'OK' });
            }
        });
    }

    function processWorkDoc(callback) {
        if (!workDocFile) {
            callback("");
            return;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            callback(workDocFile.name + "|" + e.target.result);
        };
        reader.onerror = function () {
            callback("");
        };
        reader.readAsDataURL(workDocFile);
    }

    function processAgreementDoc(callback) {
        if (!agreementFile) {
            callback("");
            return;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            callback(agreementFile.name + "|" + e.target.result);
        };
        reader.onerror = function () {
            callback("");
        };
        reader.readAsDataURL(agreementFile);
    }

    processWorkDoc(function (workDocBase64) {
        processAgreementDoc(function (agreementBase64) {
            sendData(workDocBase64, agreementBase64);
        });
    });

    return false;
}

//Load Vendor For Dropdown
function loadVendors() {
    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/GetVendorList",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var vendors = response.d;
            var dropdowns = ['#vendorreg_vendor', '#vendorregother_vendor'];
            $.each(dropdowns, function (i, selector) {
                var dropdown = $(selector);

                dropdown.empty();
                dropdown.append('<option value="">Select Vendor</option>');

                $.each(vendors, function (index, item) {
                    dropdown.append($('<option></option>')
                        .val(item.VendorRegistrationID)
                        .text(item.VendorName));
                });
            });
            const urlParams = new URLSearchParams(window.location.search);
            const vendorIdFromURL = urlParams.get('VendorID');
            if (vendorIdFromURL) {
                EditConfiguration(vendorIdFromURL);
            }
        },
        error: function (xhr, status, error) {
            console.log("Error fetching vendors: " + error);
        }
    });
}

//Rate Process Unble when ratetype id fix
$(document).ready(function () {
    $('#vendorreg_rtype').on('change', function () {
        var selectedValue = $(this).val();
        if (selectedValue === 'Fix') {
            $('#vendorreg_Process').prop('disabled', false);
        } else {
            $('#vendorreg_Process').prop('disabled', true).val('');
        }
    });
});

//Load Rate Configuration
function loadRateConfiguration(vendorId) {
    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/GetRateConfiguration",
        data: JSON.stringify({ VendorId: vendorId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);

            var thead = $("#vendorreg_rateconfigtable thead tr");
            var tbody = $("#vendorreg_rateconfigtable tbody");

            thead.empty();
            tbody.empty();

            thead.append('<th style="width: 80px; text-align: center;">Action</th>');
            thead.append('<th style="width: 60px; text-align: center;">Sr. No.</th>');
            thead.append('<th>Vendor</th>');
            thead.append('<th>Form Date</th>');
            thead.append('<th>Rate Type</th>');
            thead.append('<th>Rate</th>');
            thead.append('<th>Process</th>');
            thead.append('<th>Billing Cycle</th>');
            thead.append('<th>Employee Name</th>');

            thead.append('<th>Approve Status</th>');


            if (data.length > 0) {
                $.each(data, function (index, row) {
                    var srNo = index + 1;
                    var rowData = JSON.stringify(row).replace(/"/g, '&quot;');

                    var tr = $("<tr></tr>");

                    tr.append('<td style="text-align: center;"><a href="javascript:void(0);" onclick="editRateRecord(' + rowData + ')" title="Edit" style="background: transparent; border: none; padding: 2px 6px; color: #007bff; text-decoration: none; font-size: 18px;">&#9998;</a></td>');

                    tr.append('<td style="text-align: center;">' + srNo + '</td>');

                    tr.append("<td>" + (row.VendorName !== null ? row.VendorName : '') + "</td>");
                    tr.append("<td>" + (row.FormDate !== null ? row.FormDate : '') + "</td>");
                    tr.append("<td>" + (row.RateType !== null ? row.RateType : '') + "</td>");
                    tr.append("<td>" + (row.Rate !== null ? row.Rate : '') + "</td>");
                    tr.append("<td>" + (row.Process !== null ? row.Process : '') + "</td>");
                    tr.append("<td>" + (row.BillingCycle !== null ? row.BillingCycle : '') + "</td>");
                    tr.append("<td>" + (row.PsuedoName !== null ? row.PsuedoName : '') + "</td>");

                    var status = row.IsApprovedStatus !== null ? row.IsApprovedStatus : '';
                    var badgeClass = '';
                    if (status.toLowerCase() === 'pending') {
                        badgeClass = 'display: inline-flex; align-items: center; gap: 6px; padding: 6px 12px; font-size: 12px; font-weight: 500; border-radius: 9999px; background-color: rgba(251, 191, 36, 0.15); color: #b45309; border: 1px solid rgba(251, 191, 36, 0.3);';
                    } else if (status.toLowerCase() === 'approved') {
                        badgeClass = 'display: inline-flex; align-items: center; gap: 6px; padding: 6px 12px; font-size: 12px; font-weight: 500; border-radius: 9999px; background-color: rgba(16, 185, 129, 0.15); color: #047857; border: 1px solid rgba(16, 185, 129, 0.3);';
                    } else {
                        badgeClass = 'display: inline-flex; align-items: center; gap: 6px; padding: 6px 12px; font-size: 12px; font-weight: 500; border-radius: 9999px; background-color: rgba(107, 114, 128, 0.15); color: #4b5563;';
                    }

                    tr.append("<td><span style='" + badgeClass + "'><span style='width: 6px; height: 6px; border-radius: 50%; background-color: currentColor;'></span>" + status + "</span></td>");

                    tbody.append(tr);
                });
            } else {
                tbody.append('<tr><td colspan="7" class="text-center" style="font-weight: bold; color: gray;">No data available</td></tr>');
            }
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
        }
    });
}

//Edit rate Configuration
var isAuthorizedUser = true;
function editRateRecord(row) {

   
    $('#vendorreg_vendor').val(row.VendorRegistrationID); 

    if (row.FormDate) {
        var formattedDate = row.FormDate.split('T')[0]; 
        $('#vendorreg_formdate').val(formattedDate);
    }
    $('#vendorreg_rateid').val(row.RateID);
    $('#vendorreg_EmployeeId').val(row.RateConfigEmpId);

    $('#vendorreg_rtype').val(row.RateType);
    var processName = row.Process; 

    if (processName) {
        $('#vendorreg_Process option').filter(function () {
            return $(this).text().trim() === processName.trim();
        }).prop('selected', true);
    } else {
        $('#vendorreg_Process').val('');
    }

    $('#vendorreg_Process').trigger('change');
    $('#vendorreg_rate').val(row.Rate);
    $('#vendorreg_BillingCycle').val(row.BillingCycle);
    $('#vendorreg_EmployeeList').val(row.EmployeeId);

    $('#vendorreg_btnsubmitrate').text('Update');

    var currentStatus = row.IsApprovedStatus ? row.IsApprovedStatus.toLowerCase() : '';
    var currentUserId = parseInt($('#hdfCurrentUserId').val()) || 0;
    if (currentUserId === 235 && currentStatus === 'pending' || currentUserId === 5 && currentStatus === 'pending') {
        $('#vendorreg_btnIsaaprove').show();
    } else {
        $('#vendorreg_btnIsaaprove').hide();
    }


    $('#vendorreg_addnewrate').modal('show');
}

$(document).on('click', '#vendorreg_btnaddnewrate', function () {
    $('#vendorreg_addnewrate input, #vendorreg_addnewrate select').val('');
    $('#vendorreg_btnsubmitrate').text('ADD');
    $('#vendorreg_btnIsaaprove').hide();
});


//Add Rate
$(document).on('click', '#vendorreg_btnsubmitrate', function () {
    var rateID = $('#vendorreg_rateid').val() || 0;
    var savedEmployeeId = $('#vendorreg_EmployeeId').val() || 0;

    var vendorRegistrationID = $('#vendorreg_vendor').val();
    var fromDate = $('#vendorreg_formdate').val();
    var rateType = $('#vendorreg_rtype').val();
    var rate = $('#vendorreg_rate').val();

    var processDropdown = $('#vendorreg_Process');
    var Process = (processDropdown.val() === "") ? null : processDropdown.find('option:selected').text();
    var BillingCycle = $('#vendorreg_BillingCycle').val();
    var EmployeeID = $('#vendorreg_EmployeeList').val() || 0;

    if (!vendorRegistrationID || !fromDate || !rateType || !rate || !BillingCycle) {
        Swal.fire({
            icon: 'warning',
            title: 'Required Field',
            text: 'Please fill in all required fields!'
        });
        return;
    }
    if (isNaN(parseFloat(rate)) || parseFloat(rate) <= 0) {
        Swal.fire({
            icon: 'warning',
            title: 'Invalid Rate',
            text: 'Rate must be greater than 0!'
        });
        $('#vendorreg_rate').focus();
        return;
    }
    var isUpdate = parseInt(rateID) > 0;

    var htParam = {
        vendorRegistrationID: vendorRegistrationID,
        fromDate: fromDate,
        rateType: rateType,
        rate: rate,
        Process: Process,
        rateID: rateID,
        BillingCycle: BillingCycle,
        EmployeeID: EmployeeID,
        savedEmployeeId: savedEmployeeId


    };

    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/InsertVendorRate",
        data: JSON.stringify(htParam),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var result = response.d;

            if (result > 0 || (isUpdate && result >= 0)) {
                var successText = isUpdate ? 'Rate updated successfully!' : 'Rate added successfully!';

                Swal.fire({
                    title: 'Success!',
                    text: successText,
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then((res) => {
                    if (res.isConfirmed) {
                        $('#vendorreg_addnewrate').modal('hide');
                        loadRateConfiguration(vendorRegistrationID);                    }
                });
            } else {
                var errorText = isUpdate ? 'Something went wrong while updating rate data.' : 'Something went wrong while saving rate data.';
                Swal.fire({
                    title: 'Error!',
                    text: errorText,
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        },
        error: function (err) {
            console.log(err.responseText);
            Swal.fire({
                title: 'Technical Error!',
                text: 'Something went wrong',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });
});



// Approve Rate
$(document).on('click', '#vendorreg_btnIsaaprove', function () {
    var rateID = $('#vendorreg_rateid').val();
    var vendorRegistrationID = $('#vendorreg_vendor').val();

    if (!rateID || rateID == "0") {
        Swal.fire({
            icon: 'warning',
            title: 'Warning',
            text: 'Please select a valid rate record to approve!'
        });
        return;
    }

    Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to approve this rate?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes, Approve!'
    }).then((result) => {
        if (result.isConfirmed) {

            var param = {
                rateID: parseInt(rateID),
                vendorRegistrationID: parseInt(vendorRegistrationID)
            };

            $.ajax({
                type: "POST",
                url: "VendorRegistration.aspx/ApproveVendorRate",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    if (result > 0) {
                        Swal.fire({
                            title: 'Approved!',
                            text: 'Rate approved successfully.',
                            icon: 'success',
                            confirmButtonText: 'OK'
                        }).then(() => {
                            $('#vendorreg_addnewrate').modal('hide');
                            loadRateConfiguration(vendorRegistrationID); 
                        });
                    } else {
                        Swal.fire({
                            title: 'Error!',
                            text: 'Something went wrong while approving.',
                            icon: 'error',
                            confirmButtonText: 'OK'
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.log("Error: " + error);
                    Swal.fire({
                        title: 'Technical Error!',
                        text: 'error.',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                }
            });

        }
    });
});


//Load EmployeeForRate 
function GetEmployeeForRateConfig() {
    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/GetEmployeeForRateConfig",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var vendors = response.d;
            var dropdowns = ['#vendorreg_EmployeeList'];
            $.each(dropdowns, function (i, selector) {
                var dropdown = $(selector);

                dropdown.empty();
                dropdown.append('<option value="">Select</option>');

                $.each(vendors, function (index, item) {
                    dropdown.append($('<option></option>')
                        .val(item.EmployeeID)
                        .text(item.PsuedoName));
                });
            });
           
        },
        error: function (xhr, status, error) {
            console.log("Error fetching vendors: " + error);
        }
    });
}



//Load Process 
function GetRateConfigProcess() {
    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/GetRateConfigProcess",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var Process = response.d;
            var dropdowns = ['#vendorreg_Process'];
            $.each(dropdowns, function (i, selector) {
                var dropdown = $(selector);

                dropdown.empty();
                dropdown.append('<option value="">Select</option>');

                $.each(Process, function (index, item) {
                    dropdown.append($('<option></option>')
                        .val(item.ProcessId)
                        .text(item.Process));
                });
            });

        },
        error: function (xhr, status, error) {
            console.log("Error fetching vendors: " + error);
        }
    });
}


function buildVendorDocInfo(rawPath) {
    if (!rawPath || String(rawPath).trim() === "") {
        return null;
    }

    var normalized = String(rawPath).replace(/\\/g, '/').trim();
    var virtualIndex = normalized.toLowerCase().indexOf('vendordocument');
    var fileUrl = '';

    if (virtualIndex !== -1) {
        fileUrl = window.location.origin + '/' + normalized.substring(virtualIndex);
    } else {
        fileUrl = normalized.replace(/^~/, '');
        if (!fileUrl.startsWith('/') && !fileUrl.startsWith('http')) {
            fileUrl = '/' + fileUrl;
        }
    }

    var fileName = normalized.split('/').pop() || normalized;
    return {
        fileName: fileName,
        fileUrl: fileUrl
    };
}

function formatHistoryDate(dateVal) {
    if (!dateVal) return '';
    var str = String(dateVal).trim();
    var msMatch = /\/Date\((\d+)\)\//.exec(str);
    var dt = msMatch ? new Date(parseInt(msMatch[1], 10)) : new Date(str.replace(/^(\d{4}-\d{2}-\d{2})\s+/, '$1T'));
    if (isNaN(dt.getTime())) return str;

    var day = String(dt.getDate()).padStart(2, '0');
    var month = String(dt.getMonth() + 1).padStart(2, '0');
    var year = dt.getFullYear();
    var hours = String(dt.getHours()).padStart(2, '0');
    var minutes = String(dt.getMinutes()).padStart(2, '0');

    return day + '-' + month + '-' + year + ' ' + hours + ':' + minutes;
}
function escapeHtml(str) {
    return String(str || '')
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
}

function loadDocumentHistory(vendorId) {
    if (!vendorId || parseInt(vendorId, 10) <= 0) return;

    $('#vendorreg_dochistory_tbody').html(
        '<tr><td colspan="4" class="text-center">Loading document history...</td></tr>'
    );

    $.ajax({
        type: "POST",
        url: "VendorRegistration.aspx/GetVendorsDocumentHistory",
        data: JSON.stringify({ VendorId: parseInt(vendorId, 10) }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var rows = typeof response.d === 'string' ? JSON.parse(response.d) : response.d;
            var docColumns = [
                { field: 'AgreementCopy', label: 'Agreement Copy' },
                { field: 'PaymentDocument', label: 'Payment Document' },
                { field: 'W9FormDocument', label: 'W-9 Form' },
                { field: 'WorkDocument', label: 'Work Document' }
            ];

            var html = '';

            if (rows && rows.length > 0) {
                $.each(docColumns, function (_, col) {
                    var isFirstRow = true;
                    $.each(rows, function (_, histRow) {
                        var docInfo = buildVendorDocInfo(histRow[col.field]);
                        if (docInfo) {
                            var formattedDate = formatHistoryDate(histRow.ActionDate);

                            html += '<tr>';
                            html += '<td>' + (isFirstRow ? '<strong>' + escapeHtml(col.label) + '</strong>' : '') + '</td>';
                            html += '<td>' + escapeHtml(formattedDate) + '</td>';
                            html += '<td>' + escapeHtml(docInfo.fileName) + '</td>';
                            html += '<td class="text-center">' +
                                '<a href="' + escapeHtml(docInfo.fileUrl) + '" ' +
                                'download="' + escapeHtml(docInfo.fileName) + '" ' +
                                'target="_blank" class="doc-download-icon" title="Download ' + escapeHtml(docInfo.fileName) + '">' +
                                '<i class="fas fa-download"></i>' +
                                '</a>' +
                                '</td>';
                            html += '</tr>';

                            isFirstRow = false;
                        }
                    });
                });
            }

            if (html === '') {
                html = '<tr><td colspan="4" class="text-center text-muted">No documents found in history.</td></tr>';
            }

            $('#vendorreg_dochistory_tbody').html(html);
        },
        error: function (xhr, status, error) {
            console.error("Error loading document history:", error);
            $('#vendorreg_dochistory_tbody').html(
                '<tr><td colspan="4" class="text-center text-danger">Failed to load document history.</td></tr>'
            );
        }
    });
}