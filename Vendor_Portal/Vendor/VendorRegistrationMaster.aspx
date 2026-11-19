<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="VendorRegistrationMaster.aspx.cs" Inherits="Vendor_Portal.Vendor.VendorRegistrationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

/*#vendorregistration_table th:nth-child(7),
#vendorregistration_table td:nth-child(7),
#vendorregistration_table th:nth-child(6),
#vendorregistration_table td:nth-child(6),
#vendorregistration_table th:nth-child(4),
#vendorregistration_table td:nth-child(4),
#vendorregistration_table th:nth-child(5),
#vendorregistration_table td:nth-child(5),
#vendorregistration_table th:nth-child(11),
#vendorregistration_table td:nth-child(11),
#vendorregistration_table th:nth-child(9),
#vendorregistration_table td:nth-child(9),
#vendorregistration_table th:nth-child(8),
#vendorregistration_table td:nth-child(8)
{
    min-width: 150px !important;
    max-width: 200px !important;
    width: 150px !important;
    white-space: normal !important;
}

#vendorregistration_table th:nth-child(12),
#vendorregistration_table td:nth-child(12),
#vendorregistration_table th:nth-child(13),
#vendorregistration_table td:nth-child(13) {
    min-width: 220px !important;
    max-width: 280px !important;
    width: 220px !important;
    white-space: normal !important;
}*/

.dataTables_wrapper .dataTables_length {
    float: left;
}
.dataTables_wrapper .dataTables_filter {
    float: right;
    text-align: right;
}
.dataTables_wrapper .dataTables_filter input {
    margin-left: 0.5em;
    display: inline-block;
    width: auto;
}
.alert-danger {  
    max-width: 1290px; 
    margin: 6px auto 4px auto !important;
}
.alert-info {  
    max-width: 1290px; 
    margin: 6px auto 4px auto !important;
    padding: 8px 16px !important;

}
.cm-countdown { font-weight: bold; color: #c82333; }
 </style>

        <script src="../Scripts/Functions/VendorRegistrationMaster.js" type="text/javascript"></script>
      <script>
          var acCsrf ='<%= HttpUtility.JavaScriptStringEncode(ApproveCostCsrfToken) %>', acAllowed =<%= ApproveCostAccessAllowed?"true":"false" %>, acAuthorized=<%= ApproveCostAuthorized?"true":"false" %>, acExpiry='<%= HttpUtility.JavaScriptStringEncode(ApproveCostExpiryUtc) %>', acTimer;
          $.ajaxPrefilter(function (o, x, h) { if (o.url && o.url.indexOf('VendorRegistrationMaster.aspx/') >= 0) h.setRequestHeader('X-CostMaster-CSRF', acCsrf); });
          function acSecurePost(m, d, ok, msg) { $('#acBusyText').text(msg || 'Please wait...'); $('#acBusy').show(); $.ajax({ type: 'POST', url: 'VendorRegistrationMaster.aspx/' + m, data: JSON.stringify(d || {}), dataType: 'json', contentType: 'application/json; charset=utf-8', headers: { 'X-CostMaster-CSRF': acCsrf }, success: function (r) { ok(r.d); }, error: function (x) { $('#acSecurityMessage').text((x.responseJSON && x.responseJSON.Message) || 'Unable to complete verification.').addClass('text-danger'); }, complete: function () { $('#acBusy').hide(); } }); }
          function acRequestOtp() { acSecurePost('RequestApproveCostOtp', { employeeCode: $('#acEmployeeCode').val() }, function (m) { $('#acSecurityMessage').text(m).removeClass('text-danger'); $('#acOtpRow').show(); }, 'Sending verification code...'); }
          function acVerifyOtp() { acSecurePost('VerifyApproveCostOtp', { otp: $('#acOtp').val() }, function (r) { acUnlock(r.expiryUtc, r.message); }, 'Verifying OTP...'); }
          function acUnlock(e, msg) {
              acExpiry = e; $('#acSecurityPanel,#acUnauthorized').hide(); $('#acProtected').show();
              if (msg) {
                  $('#acVerifyBanner').text(msg).show();
              }
              clearInterval(acTimer); acTick(); acTimer = setInterval(acTick, 1000);
              GetVendors_bindgrid();
              const urlParams = new URLSearchParams(window.location.search);
              const vendorIdFromURL = urlParams.get('VendorID');
              if (vendorIdFromURL) {
                  EditConfiguration(vendorIdFromURL);
              }
           }
          function acTick() { var s = Math.max(0, Math.ceil((Date.parse(acExpiry) - Date.now()) / 1000)); $('#acCountdown').text(String(Math.floor(s / 60)).padStart(2, '0') + ':' + String(s % 60).padStart(2, '0')); if (!s) { clearInterval(acTimer); $('#acProtected').hide(); $('#acSecurityPanel').show(); $('#acSecurityMessage').text('Your Vendor Registration Master access has expired. Please verify again.').addClass('text-danger'); } }
          $(document).ready(function () {
              if (!acAllowed) { $('#acUnauthorized').show(); return; } if (acAuthorized) acUnlock(acExpiry); else $('#acSecurityPanel').show();
          });
      </script>
 <%--<script>

     $(document).ready(function () {
         GetVendors_bindgrid();
         const urlParams = new URLSearchParams(window.location.search);
         const vendorIdFromURL = urlParams.get('VendorID');
         if (vendorIdFromURL) {
             EditConfiguration(vendorIdFromURL);
         }
     });

    
 </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="acBusy" style="display: none; position: fixed; inset: 0; background: rgba(255,255,255,.72); z-index: 99999">
        <div style="position: absolute; top: 40%; left: 50%; transform: translate(-50%,-50%); background: white; padding: 24px; box-shadow: 0 4px 18px #888; text-align: center;"><i class="fas fa-spinner fa-spin fa-2x" style="margin-bottom: 10px;"></i>
            <div id="acBusyText">Please wait...</div>
        </div>
    </div>
    <div id="acUnauthorized" class="alert alert-danger" style="display: none">You are not authorized to access Vendor Registration Master Page data.</div>
    <div id="acSecurityPanel" class="card" style="display: none; max-width: 520px; margin: 25px auto">
        <div class="card-body">
            <h5>Vendor Registration Master verification</h5>
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
                                <h1>Vendor Details Master</h1>
                                <p>Display Vendor details.</p>
                            </div>
                        </div>
                        <div class="header-right">
                            <span class="module-badge">Masters</span>
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
                                <div class="vendorreg-toolbar">
                                    <div class="vendorreg-toolbar-title"><i class="fas fa-list"></i>&nbsp; Vendor List</div>
                                    <button id="vendorreg_btnaddnewvendor" name="vendorreg_btnaddnewvendor" class="btn btn-primary" onclick="return addnewvendor();">
                                        <i class="fas fa-plus-circle"></i><span>Add New Vendor</span>
                                    </button>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-bordered" id="vendorregistration_table" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th style="display: none;">ID</th>
                                                <th>Action</th>
                                                <th>Sr.No.</th>
                                                <th>Company</th>
                                                <th>Vendor Type</th>

                                                <th>Vendor</th>

                                                <th>Contact Person</th>
                                                <th>Contact No</th>
                                                <th>Email ID</th>

                                                <th>Account Number</th>
                                                <th>Billing Cycle</th>

                                                <th>Process</th>

                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

