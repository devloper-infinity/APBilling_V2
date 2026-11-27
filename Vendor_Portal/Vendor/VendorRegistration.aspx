<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="VendorRegistration.aspx.cs" Inherits="Vendor_Portal.Vendor.VendorRegistration" %>

<asp:Content ID="Content" ContentPlaceHolderID="head" runat="server">
    <style>
    .loading {
        display: none;
        position: fixed;
        inset: 0;
        margin: 0;
        width: auto;
        height: auto;
        opacity: 1;
        border-radius: 0;
        z-index: 3000 !important;
        background: rgba(15,23,42,.28);
        text-align: center;
        align-items: center;
        justify-content: center;
        padding-top: 280px;
    }
    .loading img { max-width: 62px; }
    .loading div { color: #fff; margin-top: 10px; }

    :root {
        --erp-primary: #0f4c81;
        --erp-primary-dark: #0b355d;
        --erp-accent: #0f766e;
        --erp-bg: #f4f7fb;
        --erp-surface: #fff;
        --erp-border: #d8e2ee;
        --erp-border-strong: #b8c7d8;
        --erp-text: #1f2937;
        --erp-muted: #64748b;
        --erp-shadow: 0 10px 26px rgba(15,23,42,.08);
        --erp-radius: 10px;
    }

    body {
        background: var(--erp-bg);
    }

   
    .erp-page {
        padding: 10px 24px 20px;
    }

    .erp-tabs {
        border: 1px solid var(--erp-border) !important;
        border-radius: var(--erp-radius) !important;
        background: #fff !important;
        box-shadow: var(--erp-shadow) !important;
        overflow: hidden;
    }

    .erp-tabs > .card-header {
        border-bottom: 1px solid var(--erp-border) !important;
        background: #fff !important;
        padding: 8px 16px !important; 
    }

    .erp-tabs .card-body {
        padding: 16px !important; 
        background: #fff;
    }

    .nav-tabs {
        border-bottom: 0 !important;
        gap: 8px; 
    }

    .nav-tabs .nav-link {
        display: inline-flex;
        align-items: center;
        gap: 8px;
        border: 0 !important;
        border-radius: 8px !important;
        padding: 8px 16px !important;
        color: var(--erp-muted) !important;
        font-weight: 750 !important;
    }

    .nav-tabs .nav-link.active {
        color: var(--erp-primary) !important;
        background: #eef6fb !important;
    }

    .erp-form-grid {
        display: grid;
        grid-template-columns: repeat(3, minmax(220px, 1fr));
        gap: 14px 16px;
    }

    .erp-field .form-control, .erp-field input, .erp-field select, .erp-field textarea {
        width: 100%;
        min-height: 38px;
        border: 1px solid var(--erp-border-strong) !important;
        border-radius: 7px !important;
        padding: 7px 10px !important;
        color: var(--erp-text) !important;
        background: #fff !important;
    }

    .table, table.dataTable {
        width: 100% !important;
        color: var(--erp-text);
    }

    .table thead th, table.dataTable thead th {
        border-color: var(--erp-border) !important;
        color: #334155 !important;
        background: #eef3f8 !important;
        font-size: 12px;
        text-transform: uppercase;
    }

    .page-header {
        background: linear-gradient(110deg, var(--ap-navy-2, #0b355d), var(--ap-blue, #0f4c81)) !important;
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 10px;
        padding: 12px 20px; 
        color: #f8fafc;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
        margin-bottom: 12px;
    }

    .header-content {
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-wrap: wrap;
        gap: 16px;
    }

    .header-left {
        display: flex;
        align-items: center;
        gap: 14px;
    }

    .header-icon {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 40px;
        height: 40px; 
        background: rgba(0, 0, 0, 0.2); 
        color: #ffffff;
        border-radius: 8px;
        font-size: 18px;
        border: 1px solid rgba(255, 255, 255, 0.15);
    }

    .header-left h1 {
        margin: 0 0 2px 0;
        font-size: 20px; 
        font-weight: 600;
        color: #ffffff;
        letter-spacing: -0.025em;
    }

    .header-left p {
        margin: 0;
        font-size: 13px;
        color: rgba(255, 255, 255, 0.75);
    }

    .erp-icon-btn {
        display: inline-flex;
        align-items: center;
        gap: 7px;
        border: 1px solid rgba(255,255,255,.28);
        border-radius: 8px;
        padding: 6px 12px;
        color: #fff !important;
        background: rgba(255,255,255,.14);
        font-weight: 600;
        text-decoration: none;
    }

    .erp-icon-btn:hover {
        background: rgba(255,255,255,.22);
    }

    @media(max-width: 576px) {
        .erp-page {
            padding: 8px;
        }
    }
</style>
    <style>

body {
    margin-top: 0 !important;
}

.vendorreg-page, .content-wrapper, form {
    margin-top: 0 !important;
    padding-top: 0 !important;
}

.page-header {
    margin-top: 0 !important;
    margin-bottom: 10px !important;
}
body, form, html {
    margin: 0 !important;
    padding: 0 !important;
    width: 100% !important;
}

.content-wrapper, 
.right-side, 
.main-sidebar + .content-wrapper,
div[class*="container"] {
    margin-left: 0 !important;
    margin-right: 0 !important;
    padding-left: 0 !important;
    padding-right: 0 !important;
}

.vendorreg-page, 
.vendorreg-master-modern {
    width: 100vw !important;
    max-width: 100% !important;
    margin: 0 !important;
    padding: 0 !important;
    left: 0 !important;
    position: relative !important;
}

.vendorreg-master-modern .card-body {
    padding-left: 5px !important;
    padding-right: 5px !important;
}
     .page-header {
    background: linear-gradient(110deg, var(--ap-navy-2), var(--ap-blue)) !important;
    border: 1px solid rgba(255, 255, 255, 0.1);
    border-radius: 12px;
    padding: 24px;
    color: #f8fafc;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
}

.header-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-wrap: wrap;
    gap: 16px;
}

.header-left {
    display: flex;
    align-items: center;
    gap: 16px;
}

.header-icon {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 48px;
    height: 48px;
    background: rgba(0, 0, 0, 0.2); 
    color: #ffffff;
    border-radius: 10px;
    font-size: 20px;
    border: 1px solid rgba(255, 255, 255, 0.15);
}

.header-left h1 {
    margin: 0 0 4px 0;
    font-size: 22px;
    font-weight: 600;
    color: #ffffff;
    letter-spacing: -0.025em;
}

.header-left p {
    margin: 0;
    font-size: 14px;
    color: rgba(255, 255, 255, 0.75);
}

.module-badge {
    background: rgba(0, 0, 0, 0.25);
    color: #ffffff;
    border: 1px solid rgba(255, 255, 255, 0.2);
    padding: 6px 14px;
    border-radius: 20px;
    font-size: 13px;
    font-weight: 500;
    letter-spacing: 0.025em;
    backdrop-filter: blur(4px);
}


.vendorreg-master-modern {
    background-color: #f8f9fa;
    padding: 20px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    color: #333;
}
.vendorreg-master-modern .vendorreg-hero {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background: #ffffff;
    padding: 20px 25px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.04);
    margin-bottom: 20px;
}

.vendorreg-master-modern .vendorreg-hero h3 {
    margin: 0;
    font-size: 1.25rem;
    font-weight: 600;
    color: #2c3e50;
}

.vendorreg-master-modern .vendorreg-hero p {
    margin: 5px 0 0 0;
    color: #6c757d;
    font-size: 0.875rem;
}

.vendorreg-master-modern .card {
    border: none;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
    background: #ffffff;
    margin-bottom: 20px;
}

.vendorreg-master-modern .card-body {
    padding: 20px;
}

.vendorreg-master-modern .vendorreg-toolbar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
    padding-bottom: 15px;
    border-bottom: 1px solid #edf2f7;
}

.vendorreg-master-modern .vendorreg-toolbar-title {
    font-size: 1.1rem;
    font-weight: 600;
    color: #2d3748;
    display: flex;
    align-items: center;
}

.vendorreg-master-modern .vendorreg-toolbar-title i {
    color: #3182ce;
    margin-right: 8px;
}

.vendorreg-master-modern .btn-primary {
    background-color: #3182ce;
    border-color: #3182ce;
    padding: 8px 16px;
    font-size: 0.875rem;
    font-weight: 500;
    border-radius: 6px;
    transition: background-color 0.2s ease;
}

.vendorreg-master-modern .btn-primary:hover {
    background-color: #2b6cb0;
    border-color: #2b6cb0;
}

.vendorreg-master-modern .btn i {
    margin-right: 6px;
}

.vendorreg-master-modern .table-responsive {
    overflow-x: auto;
    border-radius: 6px;
}

.vendorreg-master-modern table.table-bordered {
    width: 100% !important;
    border-collapse: collapse;
    background-color: #ffffff;
    text-align: left;
}

.vendorreg-master-modern table.table-bordered th,
.vendorreg-master-modern table.table-bordered td {
    padding: 12px 15px;
    vertical-align: middle;
    font-size: 0.875rem;
    border: 1px solid #e2e8f0;
}

.vendorreg-master-modern table.table-bordered thead th {
    background-color: #f7fafc;
    color: #4a5568;
    font-weight: 600;
    text-transform: uppercase;
    font-size: 0.75rem;
    letter-spacing: 0.05em;
}

.vendorreg-master-modern table.table-bordered tbody tr:hover {
    background-color: #f8fafc;
}

.btn-primary {
    border-color: #06438b;
    background: #124683;
}
#vendorreg_rateconfigtable th:nth-child(2),
#vendorreg_rateconfigtable td:nth-child(2)
{
    min-width: 150px !important;
    max-width: 200px !important;
    width: 150px !important;
    white-space: normal !important;
}
.alert-info {  
    max-width: 1290px; 
    margin: 6px auto 4px auto !important;
    padding: 8px 16px !important;

}
.alert-danger {  
    max-width: 1290px; 
    margin: 6px auto 4px auto !important;
}

 </style>

        <script src="../Scripts/Functions/VendorRegistrationMaster.js" type="text/javascript"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
     <script>
         var acCsrf ='<%= HttpUtility.JavaScriptStringEncode(ApproveCostCsrfToken) %>', acAllowed =<%= ApproveCostAccessAllowed?"true":"false" %>, acAuthorized=<%= ApproveCostAuthorized?"true":"false" %>, acExpiry='<%= HttpUtility.JavaScriptStringEncode(ApproveCostExpiryUtc) %>', acTimer;
         $.ajaxPrefilter(function (o, x, h) { if (o.url && o.url.indexOf('VendorRegistration.aspx/') >= 0) h.setRequestHeader('X-CostMaster-CSRF', acCsrf); });
         function acSecurePost(m, d, ok, msg) { $('#acBusyText').text(msg || 'Please wait...'); $('#acBusy').show(); $.ajax({ type: 'POST', url: 'VendorRegistration.aspx/' + m, data: JSON.stringify(d || {}), dataType: 'json', contentType: 'application/json; charset=utf-8', headers: { 'X-CostMaster-CSRF': acCsrf }, success: function (r) { ok(r.d); }, error: function (x) { $('#acSecurityMessage').text((x.responseJSON && x.responseJSON.Message) || 'Unable to complete verification.').addClass('text-danger'); }, complete: function () { $('#acBusy').hide(); } }); }
         function acRequestOtp() { acSecurePost('RequestApproveCostOtp', { employeeCode: $('#acEmployeeCode').val() }, function (m) { $('#acSecurityMessage').text(m).removeClass('text-danger'); $('#acOtpRow').show(); }, 'Sending verification code...'); }
         function acVerifyOtp() { acSecurePost('VerifyApproveCostOtp', { otp: $('#acOtp').val() }, function (r) { acUnlock(r.expiryUtc, r.message); }, 'Verifying OTP...'); }
         function acUnlock(e, msg) {
             acExpiry = e;
             $('#acSecurityPanel,#acUnauthorized').hide();
             $('#acProtected').show();
             if (msg) {
                 $('#acVerifyBanner').text(msg).show();
             }

             // Start / reset countdown timer
             if (acTimer) {
                 clearInterval(acTimer);
             }
             acTick();
             acTimer = setInterval(acTick, 1000);

             loadVendors();
             GetEmployeeForRateConfig();
             GetRateConfigProcess();
             const urlParams = new URLSearchParams(window.location.search);
             const vendorIdFromURL = urlParams.get('VendorID');
             var vendorId = vendorIdFromURL ? parseInt(vendorIdFromURL, 10) : 0;

             loadRateConfiguration(vendorId);
             if (parseInt(vendorId, 10) > 0) {
                 $('#li_dochistory_tab').show();
                 loadDocumentHistory(parseInt(vendorId, 10));
             } else {
                 $('#li_dochistory_tab').hide();
                 $('#vendorreg_dochistory_tbody').html('<tr><td colspan="4" class="text-center text-muted">No document history available.</td></tr>');
             }
         }
         function acTick() { var s = Math.max(0, Math.ceil((Date.parse(acExpiry) - Date.now()) / 1000)); $('#acCountdown').text(String(Math.floor(s / 60)).padStart(2, '0') + ':' + String(s % 60).padStart(2, '0')); if (!s) { clearInterval(acTimer); $('#acProtected').hide(); $('#acSecurityPanel').show(); $('#acSecurityMessage').text('Your Vendor Registration Master access has expired. Please verify again.').addClass('text-danger'); } }
         $(document).ready(function () {
             if (!acAllowed) { $('#acUnauthorized').show(); return; } if (acAuthorized) acUnlock(acExpiry); else $('#acSecurityPanel').show();
         });


     </script>
 <%--<script>
     $(document).ready(function () {

         loadVendors();
         GetEmployeeForRateConfig();
         GetRateConfigProcess();
         const urlParams = new URLSearchParams(window.location.search);
         const vendorIdFromURL = urlParams.get('VendorID');
         var vendorId = vendorIdFromURL ? parseInt(vendorIdFromURL) : 0;

         loadRateConfiguration(vendorId);
     });
 </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="acBusy" style="display: none; position: fixed; inset: 0; background: rgba(255,255,255,.72); z-index: 99999">
        <div style="position: absolute; top: 40%; left: 50%; transform: translate(-50%,-50%); background: white; padding: 24px; box-shadow: 0 4px 18px #888; text-align: center;"><i class="fas fa-spinner fa-spin fa-2x" style="margin-bottom: 10px;"></i>
            <div id="acBusyText">Please wait...</div>
        </div>
    </div>
    <div id="acUnauthorized" class="alert alert-danger" style="display: none">You are not authorized to access Vendor Registration Page data.</div>
    <div id="acSecurityPanel" class="card" style="display: none; max-width: 520px; margin: 25px auto">
        <div class="card-body">
            <h5>Vendor Registration verification</h5>
            <input id="acEmployeeCode" class="form-control" placeholder="Employee Code" maxlength="50" />
            <button type="button" class="btn btn-primary mt-2" onclick="acRequestOtp()">Send OTP</button><div id="acOtpRow" style="display: none" class="mt-3">
                <input id="acOtp" class="form-control" placeholder="6-digit OTP" maxlength="6" />
                <button type="button" class="btn btn-success mt-2" onclick="acVerifyOtp()">Verify OTP</button></div>
            <div id="acSecurityMessage" class="mt-3 text-success"></div>
        </div>
    </div>
    <div id="acProtected" style="display: none">
         <div id="acVerifyBanner" class="alert alert-success" style="display:none; max-width: 1290px; margin: 6px auto 4px auto;"></div>
        <div class="alert alert-info">Access remaining: <b id="acCountdown" class="text-danger cm-countdown">15:00</b></div>
        <div style="width: 100%; max-width: 1400px; margin: 0 auto; padding: 4px 20px 20px 20px; box-sizing: border-box;">

            <div class="vendorreg-page">
                <div class="page-header">
                    <div class="header-content">
                        <div class="header-left">
                            <div class="header-icon"><i class="fas fa-clipboard-list"></i></div>
                            <div>
                                <h1>Add/Edit Vendor Configuration Details</h1>
                                <p>Maintain Vendor details, other information and rate details.</p>
                            </div>
                        </div>
                        <div class="header-right">
                            <button id="vendorreg_backtolist" name="vendorreg_backtolist" class="btn btn-primary" onclick="return Backtolist();">
                                <i class="fa fa-arrow-left"></i><span>Back To List</span>
                            </button>
                        </div>
                    </div>
                </div>

                <div class="loading" id="load1" style="display: none;">
                    <div class="loading__box">
                        <img src="../images/Load_1.gif" alt="Loading" />
                        <div class="loading__text">One moment, please...</div>
                    </div>
                </div>

                <div class="vendorreg-page-shell vendorreg-master-modern" style="padding: 12px 0 20px 0;">

                    <div class="col-lg-12" style="padding: 0;">
                        <div class="card">
                            <div class="card-body">

                                <div class="col-lg-12 px-0">
                                    <div class="card erp-tabs">
                                        <div class="card-header p-0 pt-1">
                                            <ul class="nav nav-tabs" id="custom-tabs-one-tab_bpd_tabs" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" id="custom-tabs-one-home-tab_company" data-toggle="pill" href="#custom-tabs-one-home_company" role="tab" aria-controls="custom-tabs-one-home_company" aria-selected="true"><i class="fa fa-building"></i><b>Vendor Information</b></a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" onclick="return bpd_bindSalesInformation();" id="custom-tabs-one-profile-tab_sales" data-toggle="pill" href="#custom-tabs-one-profile_sales" role="tab" aria-controls="custom-tabs-one-profile_sales" aria-selected="false"><i class="fa fa-chart-line"></i><b>Other Information</b></a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" id="custom-tabs-one-profile-tab_deal" data-toggle="pill" href="#custom-tabs-one-profile_deal" role="tab" aria-controls="custom-tabs-one-profile_deal" aria-selected="false"><i class="fa fa-project-diagram"></i><b>Rate Configuration</b></a>
                                                </li>

                                                <li class="nav-item" id="li_dochistory_tab" style="display: none;">
                                                    <a class="nav-link" id="custom-tabs-one-dochistory-tab" data-toggle="pill" href="#custom-tabs-one-dochistory" role="tab" aria-controls="custom-tabs-one-dochistory" aria-selected="false">
                                                        <i class="fa fa-history"></i><b>Document History</b>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="card-body">
                                            <div class="tab-content" id="custom-tabs-one-tabContent_addinvocie">
                                                <div class="tab-pane fade show active" id="custom-tabs-one-home_company" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_company">
                                                    <table class="table">
                                                        
                                                        <tr>
                                                            <td><b>Company Name: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_companyname" name="vendorreg_companyname" class="form-control" style="width: 250px;">
                                                                    <option value="">Select</option>
                                                                    <option value="IPS">IPS</option>
                                                                    <option value="Canopy">Canopy</option>
                                                                    <option value="Medasset">Medasset</option>
                                                                    <option value="LenderBridge AI">LenderBridge AI</option>
                                                                </select>
                                                            </td>
                                                            <td><b>Domain: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_Domain" name="vendorreg_Domain" class="form-control" style="width: 250px;">
                                                                    <option value="">Select</option>
                                                                    <option value="DD">DD</option>
                                                                    <option value="Non-DD">Non-DD</option>

                                                                </select>
                                                            </td>
                                                            <td><b>Vendor Type<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_individualcomp" name="vendorreg_individualcomp" class="form-control" style="width: 250px;">
                                                                    <option value="">Select</option>
                                                                    <option value="Advisor">Advisor </option>
                                                                    <option value="Company">Company</option>
                                                                    <option value="Consultant">Consultant</option>
                                                                    <option value="Remote UW">Remote UW</option>
                                                                    <option value="Sales Representative">Sales Representative</option>


                                                                </select>
                                                            </td>


                                                        </tr>
                                                        <tr>
                                                            <td><b>Vendor Name: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="text" id="vendorreg_vendorname" name="vendorreg_vendorname" class="form-control" style="width: 250px;" /></td>

                                                            <td><b>Contact Person: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="text" id="vendorreg_contactperson" name="vendorreg_contactperson" class="form-control" style="width: 250px;" /></td>
                                                            <td><b>Contact #: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="text" id="vendorreg_contactno" name="vendorreg_contactno" class="form-control" style="width: 250px;" /></td>

                                                        </tr>
                                                        <tr>
                                                            <td><b>Email ID: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="email" id="vendorreg_email" name="vendorreg_email" class="form-control" style="width: 250px;" /></td>
                                                            <td><b>Website URL: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="text" id="vendorreg_weburl" name="vendorreg_weburl" class="form-control" style="width: 250px;" /></td>

                                                            <td><b>Address: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <textarea id="vendorreg_address" name="vendorreg_address" class="form-control" style="width: 250px; resize: none;"></textarea></td>

                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" style="padding-top: 15px; padding-bottom: 10px;"><b><i class="fas fa-credit-card mr-1"></i>Payment Details</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Pay To Vendor Name<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="text" id="vendorreg_payvendorname" name="vendorreg_payvendorname" class="form-control" style="width: 250px;" /></td>
                                                            <td><b>Payment Method<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_PaymentMethod" name="vendorreg_PaymentMethod" class="form-control" style="width: 250px;">
                                                                    <option value="">Select</option>
                                                                    <option value="ACH">ACH </option>
                                                                    <option value="Check">Check </option>
                                                                    <option value="Wire">Wire</option>



                                                                </select>
                                                            </td>
                                                            <td><b>Account No.:<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="text" id="vendorreg_acno" name="vendorreg_acno" class="form-control" style="width: 250px;" /></td>


                                                        </tr>
                                                        <tr>
                                                              <td><b>Routing No.: <span style="color: red;">*</span></b></td>
  <td>
      <input type="text" id="vendorreg_Routingno" name="vendorreg_Routingno" class="form-control" style="width: 250px;" /></td>

                                                                                                                     <td><b>Payment Document:<span style="color: red;">*</span></b></td>
<td>
    <div class="d-flex align-items-center">
        <input type="file" id="vendorreg_PaymentDocument" name="vendorreg_PaymentDocument" class="form-control" style="width: 250px;" />
        <a id="lnk_paymentdoc_download" href="#" target="_blank" download title="Download Existing File" style="display: none; margin-left: 12px; text-decoration: none; cursor: pointer; font-size: 1.15rem; color: #0d6efd;">
            <i class="fas fa-download"></i>
        </a>
    </div>
    <div id="lbl_paymentdoc_name" style="display: none; font-size: 12px; color: #495057; margin-top: 4px; word-break: break-all;"></div>
</td>

                                                           

                                                        </tr>


                                                        <tr>
                                                            <td colspan="6" style="padding-top: 15px; padding-bottom: 10px;"><b><i class="fas fa-file-invoice-dollar mr-1"></i>Tax Information</b></td>
                                                        </tr>
                                                          <tr>
      <td><b>SSN/EIN: <span style="color: red;">*</span></b></td>
      <td>
          <input type="text" id="vendorreg_SSNEIN" name="vendorreg_SSNEIN" class="form-control" style="width: 250px;" /></td>

      <td><b>Tax ID: <span style="color: red;">*</span></b></td>
      <td>
          <input type="text" id="vendorreg_TaxID" name="vendorreg_TaxID" class="form-control" style="width: 250px;" /></td>
      <td><b>W-9 Form: <span style="color: red;">*</span></b></td>
<td>
    <div class="d-flex align-items-center gap-3">
        <input type="file" id="vendorreg_W9Form" name="vendorreg_W9Form" class="form-control" style="width: 250px;" />
        <a id="lnk_w9doc_download" href="#" target="_blank" download title="Download Existing File" style="display: none; text-decoration: none; cursor: pointer; font-size: 1.2rem; color: #0d6efd; margin-left: 12px;">
            <i class="fas fa-download"></i>
        </a>
    </div>
    <div id="lbl_w9doc_name" style="display: none; font-size: 12px; color: #495057; margin-top: 4px; word-break: break-all;"></div>
</td>
  </tr>
                                                        <tr>
    <td><b>Remark:</b></td>
    <td colspan="5">
        <textarea id="vendorreg_remark" name="vendorreg_remark" class="form-control" rows="2" style="width: 100%; resize: none;"></textarea>
    </td>
</tr>
                                                        <tr>
                                                            <td colspan="6" style="text-align: center; padding-top: 20px;">
                                                                <button id="vendorreg_btnsubmitstep1" class="btn btn-primary" onclick="return btnsubmitInsertVendorRegistration();">Add</button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <div class="tab-pane fade" id="custom-tabs-one-profile_sales" role="tabpanel" aria-labelledby="custom-tabs-one-profile-tab_sales">
                                                    <table class="table" style="width: 100%">
                                                        <tr>

                                                            <td><b>Vendor: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select class="form-control" id="vendorregother_vendor" name="vendor" >
                                                                    <option value="">Select Vendor</option>
                                                                </select></td>
                                                            <td><b>Start Date:<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="date" id="vendorreg_expectedstartdate" name="vendorreg_expectedstartdate" class="form-control" style="width: 250px;" /></td>
                                                            <td><b>Rate Revision Date: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="date" id="vendorreg_raterevisiondate" name="vendorreg_raterevisiondate" class="form-control" style="width: 250px;" /></td>

                                                        </tr>

                                                        <tr>
                                                            <td><b>Work Document:<span style="color: red;">*</span></b></td>
<td>
    <div class="d-flex align-items-center gap-3">
        <input type="file" id="vendorreg_workdocument" name="vendorreg_workdocument" class="form-control" style="width: 250px;" />
        <a id="lnk_workdoc_download" href="#" target="_blank" download title="Download Existing File" style="display: none; text-decoration: none; cursor: pointer; font-size: 1.2rem; color: #0d6efd; margin-left: 12px;">
            <i class="fas fa-download"></i>
        </a>
    </div>
    <div id="lbl_workdoc_name" style="display: none; font-size: 12px; color: #495057; margin-top: 4px; word-break: break-all;"></div>
</td>
                                                            <td><b>Scope of Work:<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <textarea id="vendorreg_workscope" name="vendorreg_workscope" class="form-control" style="width: 250px; resize: none;"></textarea></td>
                                                            <td><b>Remark:</b></td>
                                                            <td>
                                                                <textarea id="vendorregotherinfo_remark" name="vendorregotherinfo_remark" class="form-control" style="width: 250px;"></textarea></td>
                                                        </tr>


                                                        <tr>
                                                            <td><b>Is NDA Signed? <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_isndasigned" name="vendorreg_isndasigned" class="form-control" style="width: 250px;" onchange="return getnda(this);">
                                                                    <option value="">Select</option>
                                                                    <option value="1">Yes</option>
                                                                    <option value="0">No</option>
                                                                    <option value="2">Not Applicable</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr id="nda1" style="display: none;">
                                                            <td><b>Agreement Date:<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="date" id="vendorreg_agreementdate" name="vendorreg_agreementdate" class="form-control" style="width: 250px;"></td>
                                                            <td><b>Expiry Date: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <input type="date" id="vendorreg_agreementexpirydate" name="vendorreg_agreementexpirydate" class="form-control" style="width: 250px;"></td>
                                                            <td><b>Signed by Client:<span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_agreementclientsigned" name="vendorreg_agreementclientsigned" class="form-control" style="width: 250px;">
                                                                    <option value="">Select</option>
                                                                    <option value="1">Yes</option>
                                                                    <option value="0">No</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr id="nda2" style="display: none;">
                                                            <td><b>Signed by Infinity: <span style="color: red;">*</span></b></td>
                                                            <td>
                                                                <select id="vendorreg_agreementinfinitysigned" name="vendorreg_agreementinfinitysigned" class="form-control" style="width: 250px;">
                                                                    <option value="">Select</option>
                                                                    <option value="1">Yes</option>
                                                                    <option value="0">No</option>
                                                                </select>
                                                            </td>
                                                            <td><b>Agreement Copy: <span style="color: red;">*</span></b></td>
<td>
    <div class="d-flex align-items-center gap-3">
        <input type="file" id="vendorreg_agreementattachment" name="vendorreg_agreementattachment" class="form-control" style="width: 250px;" />
        <a id="lnk_agreementattachment_download" href="#" target="_blank" download title="Download Existing File" style="display: none; text-decoration: none; cursor: pointer; font-size: 1.2rem; color: #0d6efd; margin-left: 12px;">
            <i class="fas fa-download"></i>
        </a>
    </div>
    <div id="lbl_agreementattachment_name" style="display: none; font-size: 12px; color: #495057; margin-top: 4px; word-break: break-all;"></div>
</td>

                                                        </tr>
                                                       
                                                        <tr>
                                                            <td colspan="6" style="text-align: center; padding-top: 20px;">
                                                                <button type="button" id="vendorreg_btnsubmit" name="vendorreg_btnsubmit" class="btn btn-primary" onclick="return btnsubmitInsertVendorOtherInfo();">Add</button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>


                                                <div class="tab-pane fade" id="custom-tabs-one-profile_deal" role="tabpanel" aria-labelledby="custom-tabs-one-profile_deal">
                                                    <button type="button" id="vendorreg_btnaddnewrate" name="vendorreg_btnaddnewrate" class="btn btn-primary" data-toggle="modal" data-target="#vendorreg_addnewrate">Add Rate</button>
                                                    <hr />
                                                    <table class="table" id="vendorreg_rateconfigtable" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>
                                                </div>

                                                <div class="tab-pane fade" id="custom-tabs-one-dochistory" role="tabpanel" aria-labelledby="custom-tabs-one-dochistory-tab">
                                                    <div class="table-responsive">
                                                        <table id="vendorreg_dochistorytable" class="table table-bordered table-striped">
                                                            <thead>
                                                                <tr>
                                                                    <th>Vendor Document Type</th>
                                                                    <th>Date</th>
                                                                    <th>Document Name</th>
                                                                    <th>Download</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="vendorreg_dochistory_tbody">
                                                                <tr>
                                                                    <td colspan="4" class="text-center text-muted">No document history available.</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                                                        
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                            
            </div>

        </div>
            
    </div>
    <!-- Modal Popup for Add New Rate -->
    <div class="modal fade" id="vendorreg_addnewrate">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add New Rate</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="vendorreg_rateid" value="0" />
                    <input type="hidden" id="hdfCurrentUserId" value='<%= HttpContext.Current.User.Identity.Name %>' />
                    <input type="hidden" id="vendorreg_EmployeeId" value="0" />

                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label for="vendorreg_vendor">Vendor <span style="color: red;">*</span></label>
                            <select class="form-control" id="vendorreg_vendor" name="vendor">
                                <option value="">Select Vendor</option>
                            </select>
                        </div>

                        <div class="col-md-6 form-group">
                            <label for="vendorreg_formdate">From Date <span style="color: red;">*</span></label>
                            <input type="date" class="form-control" id="vendorreg_formdate" name="FormDate">
                        </div>

                        <div class="col-md-6 form-group">
                            <label for="vendorreg_rtype">Rate Type <span style="color: red;">*</span></label>
                            <select class="form-control" id="vendorreg_rtype" name="RType">
                                <option value="">Select</option>
                                <option value="Fix">Fix</option>
                                <option value="Slab">Slab</option>
                                <option value="Variable">Variable</option>
                            </select>
                        </div>

                        <div class="col-md-6 form-group">
                            <label for="vendorreg_Process">Process <span style="color: red;">*</span></label>
                            <select class="form-control" id="vendorreg_Process" name="Process" disabled>
                                <option value="">Select</option>
                            </select>
                        </div>

                        <div class="col-md-6 form-group">
                            <label for="vendorreg_rate">Rate <span style="color: red;">*</span></label>
                            <input type="number" step="0.01" class="form-control" id="vendorreg_rate" name="Rate" placeholder="Enter rate">
                        </div>

                        <div class="col-md-6 form-group">
                            <label for="vendorreg_rtype">Billing Cycle <span style="color: red;">*</span></label>
                            <select class="form-control" id="vendorreg_BillingCycle" name="vendorreg_BillingCycle">
                                <option value="">Select</option>
                                <option value="Monthly">Monthly</option>
                                <option value="Bi-Monthly">Bi-Monthly</option>
                                <option value="Weekly">Weekly</option>
                            </select>
                        </div>
                        <div class="col-md-6 form-group">
                            <label for="vendorreg_EmployeeList">Employee </label>
                            <select class="form-control" id="vendorreg_EmployeeList" name="vendorreg_EmployeeList">
                                <option value="">Select Employee</option>
                            </select>
                        </div>
                    </div>

                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                    <div class="d-flex align-items-center">
                        <button class="btn btn-primary" style="margin-right: 10px;" type="button" id="vendorreg_btnsubmitrate">Add</button>
                        <button class="btn btn-success btn-lg px-4 fw-bold shadow-sm" type="button" id="vendorreg_btnIsaaprove" style="display: none;">Approve</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    
</asp:Content>
