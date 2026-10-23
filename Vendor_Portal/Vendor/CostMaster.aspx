<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="CostMaster.aspx.cs" Inherits="Vendor_Portal.Vendor.CostMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .loading {
            display: none;
            position: fixed;
            top: 350px;
            left: 50%;
            margin-top: -96px;
            margin-left: -96px;
            opacity: .85;
            border-radius: 25px;
            width: 192px;
            height: 192px;
            z-index: 99999;
        }
        .cm-loading-backdrop { display:none; position:fixed; inset:0; background:rgba(255,255,255,.72); z-index:99998; }
        .cm-loading-box { position:absolute; top:40%; left:50%; transform:translate(-50%,-50%); padding:22px 30px; background:#fff; border-radius:8px; box-shadow:0 4px 18px rgba(0,0,0,.25); text-align:center; font-weight:bold; }

        .dataTables_length, .dataTables_info {
            float: left !important;
        }

        label:not(.form-check-label):not(.custom-file-label) {
            font-weight: normal !important;
            border: none !important;
        }

        div.dt-buttons {
            position: static;
            padding-left: 50px;
            float: left;
        }

        .buttons-excel, .buttons-html5 {
            color: #fff;
            box-shadow: none;
            background: linear-gradient(to right, #ffbf96, #fe7096);
            border: 0;
            font-weight: bold;
            margin: 0px 10px;
        }

        .table.dataTable th {
            background: linear-gradient(to bottom, #007bff, 3%, #fff) !important;
            color: #000;
        }

        .table.dataTable tr td {
            background: none !important;
            background-color: #fff !important;
        }
        .cm-security-card { max-width: 520px; margin: 25px auto; }
        .cm-countdown { font-weight: bold; color: #c82333; }
    </style>

    <script>
        var cmCsrfToken = '<%= HttpUtility.JavaScriptStringEncode(CostMasterCsrfToken) %>';
        var cmAuthorized = <%= CostMasterAuthorized ? "true" : "false" %>;
        var cmAccessAllowed = <%= CostMasterAccessAllowed ? "true" : "false" %>;
        var cmExpiryUtc = '<%= HttpUtility.JavaScriptStringEncode(CostMasterExpiryUtc) %>';
        $.ajaxPrefilter(function (options, originalOptions, xhr) {
            if (options.url && options.url.indexOf('CostMaster.aspx/') >= 0) xhr.setRequestHeader('X-CostMaster-CSRF', cmCsrfToken);
        });
        $(document).ready(function () {
            if (!cmAccessAllowed) { $('#cmUnauthorized').show(); $('#cmSecurityPanel,#cmProtected').hide(); return; }
            if (cmAuthorized) { cmUnlock(cmExpiryUtc, false); CostMasterCanopy_BindGrid(); CostMasterIPS_BindGrid(); }
            else cmLock(false);
            document.addEventListener('visibilitychange', function () { if (!document.hidden) cmCheckStatus(); });
        });
        var cmTimer;
        function cmSetBusy(busy, message) {
            $('#cmLoadingText').text(message || 'Please wait...');
            $('#cmLoadingBackdrop').toggle(busy);
            $('#cmSecurityPanel button').prop('disabled', busy);
        }
        function cmPost(method, data, ok, loadingText, onError) {
            if (loadingText) cmSetBusy(true, loadingText);
            $.ajax({ type:'POST', url:'CostMaster.aspx/'+method, data:JSON.stringify(data||{}), dataType:'json', contentType:'application/json; charset=utf-8',
                headers:{'X-CostMaster-CSRF':cmCsrfToken}, success:function(r){ok(r.d);},
                error:function(x){cmShowMessage((x.responseJSON&&x.responseJSON.Message)||'Unable to complete verification.',true);if(onError)onError();},
                complete:function(){if(loadingText)cmSetBusy(false);} });
        }
        function cmRequestOtp(){ cmPost('RequestCostMasterOtp',{employeeCode:$('#cmEmployeeCode').val()},function(m){cmShowMessage(m,false);$('#cmOtpRow').show();},'Sending verification code...'); }
        function cmVerifyOtp(){ cmPost('VerifyCostMasterOtp',{otp:$('#cmOtp').val()},function(r){cmShowMessage(r.message,false);cmUnlock(r.expiryUtc,true);CostMasterCanopy_BindGrid();CostMasterIPS_BindGrid();},'Verifying OTP...'); }
        function cmVerifyAgain(){ cmPost('RestartCostMasterVerification',{},function(m){clearInterval(cmTimer);cmExpiryUtc='';$('#cmEmployeeCode,#cmOtp').val('');$('#cmOtpRow').hide();$('#cmSecurityPanel').show();$('#cmProtected').hide();cmShowMessage(m,false);},'Resetting verification...'); }
        function cmUnlock(expiry,showSuccess){cmExpiryUtc=expiry;$('#cmSecurityPanel').hide();$('#cmProtected').show().find(':input,button,a').prop('disabled',false);if(showSuccess)$('#cmSuccess').text('Verification successful. You can access Costing Master for the next 15 minutes.').show();clearInterval(cmTimer);cmTick();cmTimer=setInterval(cmTick,1000);}
        function cmTick(){var seconds=Math.max(0,Math.ceil((Date.parse(cmExpiryUtc)-Date.now())/1000));$('#cmCountdown').text(String(Math.floor(seconds/60)).padStart(2,'0')+':'+String(seconds%60).padStart(2,'0'));if(seconds<=0)cmLock(true);}
        function cmLock(expired){clearInterval(cmTimer);$('#cmProtected').hide().find(':input,button,a').prop('disabled',true);$('#cmSecurityPanel').show();$('#cmSuccess').hide();if(expired)cmShowMessage('Your Costing Master access has expired. Please verify again.',true);}
        function cmCheckStatus(){cmPost('GetCostMasterAuthorizationStatus',{},function(r){if(!r.authorized)cmLock(true);else if(r.expiryUtc!==cmExpiryUtc)cmUnlock(r.expiryUtc,false);});}
        function cmShowMessage(m,isError){$('#cmSecurityMessage').text(m).toggleClass('text-danger',isError).toggleClass('text-success',!isError);}
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
    <div id="cmLoadingBackdrop" class="cm-loading-backdrop" role="status" aria-live="polite">
        <div class="cm-loading-box"><i class="fas fa-spinner fa-spin fa-2x"></i><div id="cmLoadingText" class="mt-2">Please wait...</div></div>
    </div>
    <div id="cmUnauthorized" class="alert alert-danger" style="display:none">You are not authorized to access Costing Master data.</div>

    <div id="cmSecurityPanel" class="card cm-security-card">
        <div class="card-body">
            <h5>Costing Master verification</h5>
            <p>Enter your Employee Code. A one-time code will be sent to your registered official email.</p>
            <div class="form-group"><label for="cmEmployeeCode">Employee Code</label><input id="cmEmployeeCode" class="form-control" maxlength="50" autocomplete="off" /></div>
            <button type="button" class="btn btn-primary" onclick="cmRequestOtp();">Send OTP</button>
            <div id="cmOtpRow" style="display:none" class="form-group mt-3"><label for="cmOtp">6-digit OTP</label><input id="cmOtp" class="form-control" maxlength="6" inputmode="numeric" autocomplete="one-time-code" />
                <button type="button" class="btn btn-success mt-2" onclick="cmVerifyOtp();">Verify OTP</button></div>
            <div id="cmSecurityMessage" class="mt-3" role="alert"></div>
        </div>
    </div>

    <div id="cmProtected" style="display:none">
    <div class="alert alert-success" id="cmSuccess" style="display:none"></div>
    <div class="alert alert-info">Access remaining: <span id="cmCountdown" class="cm-countdown">15:00</span>
        <button type="button" class="btn btn-sm btn-outline-primary float-right" onclick="cmVerifyAgain();">Verify Again</button></div>

    <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Cost Master</b></h6>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <table class="table">
                    <tr>
                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Company:</b></td>
                        <td>
                            <select id="cm_company" name="cm_company" class="form-control" onchange="return GetVendor(this);" style="width: 300px;">
                                <option value="0">Select</option>
                                <option value="Canopy">Canopy</option>
                                <option value="IPS">IPS</option>
                            </select>
                        </td>


                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Vendor :</b></td>
                        <td>
                            <select id="cm_Vendor" name="cm_Vendor" class="form-control" style="width: 300px;">
                                <option value="0">Select</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Effective Date:</b></td>
                        <td>
                            <input type="date" id="cm_EffectiveDate" name="cm_EffectiveDate" class="form-control" style="width: 300px;" />
                        </td>

                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Type :</b></td>
                        <td>
                            <select id="cm_Type" name="cm_Type" class="form-control" style="width: 300px;">
                                <option value="0">Select</option>
                                <option value="Fixed">Fixed</option>
                                <option value="Variable">Variable</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td><i class="fa fa-star" style="font-size: 5px; color: red;"></i>&nbsp;<b>Base Rate ($) :</b></td>
                        <td>
                            <input type="number" id="cm_BaseRate" name="cm_BaseRate" style="width: 300px; height: 30px;" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <button class="btn btn-primary" type="button" id="cm_btnSubmit" onclick="return cm_Submit();">Submit</button>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <br />
                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-costMaster_canopy-tab" data-toggle="pill" href="#custom-tabs-one-costMaster_canopy" role="tab" aria-controls="custom-tabs-one-costMaster_canopy" aria-selected="true"><b>Canopy</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-costMaster_IPS-tab" data-toggle="pill" href="#custom-tabs-one-costMaster_IPS" role="tab" aria-controls="custom-tabs-one-costMaster_IPS" aria-selected="false"><b>IPS</b></a>
                            </li>
                        </ul>
                    </div>
                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-costMaster_canopy" role="tabpanel" aria-labelledby="custom-tabs-one-costMaster_canopy-tab">
                                <div>

                                    <table class="table" id="table_costMaster_Canopy" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                                <th class="sort border-top">Caompany</th>
                                                <th class="sort border-top">Vendor</th>
                                                <th class="sort border-top">Type</th>
                                                <th class="sort border-top">Slab</th>
                                                <th class="sort border-top" style="text-align: center;">Base Rate</th>
                                                <th class="sort border-top" style="text-align: center;">TRID Rate</th>
                                                <th class="sort border-top" style="text-align: center;">Added Date Time</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-costMaster_IPS" role="tabpanel" aria-labelledby="custom-tabs-one-costMaster_IPS-tab">
                                <div>
                                    <table class="table" id="table_costMaster_IPS" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                                <th class="sort border-top">Caompany</th>
                                                <th class="sort border-top">Vendor</th>
                                                <th class="sort border-top">Type</th>
                                                <th class="sort border-top">Slab</th>
                                                <th class="sort border-top" style="text-align: center;">Base Rate</th>
                                                <th class="sort border-top" style="text-align: center;">TRID Date</th>
                                                <th class="sort border-top" style="text-align: center;">Added Date Time</th>
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
    </div>
</asp:Content>
