<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="ApproveCost.aspx.cs" Inherits="Vendor_Portal.Vendor.ApproveCost" %>

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
    </style>

    <script>
        var acCsrf='<%= HttpUtility.JavaScriptStringEncode(ApproveCostCsrfToken) %>', acAllowed=<%= ApproveCostAccessAllowed?"true":"false" %>, acAuthorized=<%= ApproveCostAuthorized?"true":"false" %>, acExpiry='<%= HttpUtility.JavaScriptStringEncode(ApproveCostExpiryUtc) %>', acTimer;
        $.ajaxPrefilter(function(o,x,h){if(o.url&&o.url.indexOf('ApproveCost.aspx/')>=0)h.setRequestHeader('X-CostMaster-CSRF',acCsrf);});
        function acSecurePost(m,d,ok,msg){$('#acBusyText').text(msg||'Please wait...');$('#acBusy').show();$.ajax({type:'POST',url:'ApproveCost.aspx/'+m,data:JSON.stringify(d||{}),dataType:'json',contentType:'application/json; charset=utf-8',headers:{'X-CostMaster-CSRF':acCsrf},success:function(r){ok(r.d);},error:function(x){$('#acSecurityMessage').text((x.responseJSON&&x.responseJSON.Message)||'Unable to complete verification.').addClass('text-danger');},complete:function(){$('#acBusy').hide();}});}
        function acRequestOtp(){acSecurePost('RequestApproveCostOtp',{employeeCode:$('#acEmployeeCode').val()},function(m){$('#acSecurityMessage').text(m).removeClass('text-danger');$('#acOtpRow').show();},'Sending verification code...');}
        function acVerifyOtp(){acSecurePost('VerifyApproveCostOtp',{otp:$('#acOtp').val()},function(r){acUnlock(r.expiryUtc);},'Verifying OTP...');}
        function acUnlock(e){acExpiry=e;$('#acSecurityPanel,#acUnauthorized').hide();$('#acProtected').show();clearInterval(acTimer);acTick();acTimer=setInterval(acTick,1000);approvalCostCanopy_BindGrid();approvalCostIPS_BindGrid();}
        function acTick(){var s=Math.max(0,Math.ceil((Date.parse(acExpiry)-Date.now())/1000));$('#acCountdown').text(String(Math.floor(s/60)).padStart(2,'0')+':'+String(s%60).padStart(2,'0'));if(!s){clearInterval(acTimer);$('#acProtected').hide();$('#acSecurityPanel').show();$('#acSecurityMessage').text('Your Approve Cost access has expired. Please verify again.').addClass('text-danger');}}
        $(document).ready(function () {
            if(!acAllowed){$('#acUnauthorized').show();return;} if(acAuthorized)acUnlock(acExpiry);else $('#acSecurityPanel').show();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="acBusy" style="display:none;position:fixed;inset:0;background:rgba(255,255,255,.72);z-index:99999"><div style="position:absolute;top:40%;left:50%;transform:translate(-50%,-50%);background:white;padding:24px;box-shadow:0 4px 18px #888"><i class="fas fa-spinner fa-spin fa-2x"></i><div id="acBusyText">Please wait...</div></div></div>
    <div id="acUnauthorized" class="alert alert-danger" style="display:none">You are not authorized to access Approve Cost data.</div>
    <div id="acSecurityPanel" class="card" style="display:none;max-width:520px;margin:25px auto"><div class="card-body"><h5>Approve Cost verification</h5><input id="acEmployeeCode" class="form-control" placeholder="Employee Code" maxlength="50"/><button type="button" class="btn btn-primary mt-2" onclick="acRequestOtp()">Send OTP</button><div id="acOtpRow" style="display:none" class="mt-3"><input id="acOtp" class="form-control" placeholder="6-digit OTP" maxlength="6"/><button type="button" class="btn btn-success mt-2" onclick="acVerifyOtp()">Verify OTP</button></div><div id="acSecurityMessage" class="mt-3"></div></div></div>
    <div id="acProtected" style="display:none"><div class="alert alert-info">Access remaining: <b id="acCountdown">15:00</b></div>

    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>

    <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Approve Cost</b></h6>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">

                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-approvalCost_canopy-tab" data-toggle="pill" href="#custom-tabs-one-approvalCost_canopy" role="tab" aria-controls="custom-tabs-one-approvalCost_canopy" aria-selected="true"><b>Canopy</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-approvalCost_IPS-tab" data-toggle="pill" href="#custom-tabs-one-approvalCost_IPS" role="tab" aria-controls="custom-tabs-one-approvalCost_IPS" aria-selected="false"><b>IPS</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-approvalCost_canopy" role="tabpanel" aria-labelledby="custom-tabs-one-approvalCost_canopy-tab">
                                <div>
                                    <table class="table" id="table_approvalCostCanopy" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th class="sort border-top ps-3">Action</th>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                                <th class="sort border-top" style="text-align: center;">Caompany</th>
                                                <th class="sort border-top ps-3" style="display: none;">RConfigurationID</th>
                                                <th class="sort border-top" style="text-align: center;">Vendor</th>
                                                <th class="sort border-top" style="text-align: center;">Type</th>
                                                <th class="sort border-top" style="text-align: center;">Slab</th>
                                                <th class="sort border-top" style="text-align: center;">Base Rate</th>
                                                <th class="sort border-top" style="text-align: center;">TRID Rate</th>
                                                <th class="sort border-top" style="text-align: center;">Added Date Time</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-approvalCost_IPS" role="tabpanel" aria-labelledby="custom-tabs-one-approvalCost_IPS-tab">
                                <div>
                                    <table class="table" id="table_approvalCostIPS" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th class="sort border-top ps-3">Action</th>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                                <th class="sort border-top" style="text-align: center;">Caompany</th>
                                                <th class="sort border-top ps-3" style="display: none;">RConfigurationID</th>
                                                <th class="sort border-top" style="text-align: center;">Vendor</th>
                                                <th class="sort border-top" style="text-align: center;">Type</th>
                                                <th class="sort border-top" style="text-align: center;">Slab</th>
                                                <th class="sort border-top" style="text-align: center;">Base Rate</th>
                                                <th class="sort border-top" style="text-align: center;">TRID Rate</th>
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

    <div class="modal fade" id="ac_ApproveCost">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Approve Cost</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table" style="font-size: 12px;">
                        <tr>
                            <td>
                                <label id="ac_VendorName" name="ac_VendorName" class="form-control" style="width: 250px;"></label>
                            </td>
                            <td>
                                <label id="ac_EffectiveDate" name="ac_EffectiveDate" class="form-control" style="width: 200px;"></label>
                            </td>
                            <td>
                                <label id="ac_Type" name="ac_Type" class="form-control" style="width: 200px;"></label>
                            </td>
                            <td>
                                <label id="ac_BaseRate" name="ac_BaseRate" class="form-control" style="width: 200px;"></label>
                            </td>
                        </tr>
                    </table>
                    <table class="table">
                        <tr>
                            <td colspan="4">
                                <b>Remark :</b>
                            </td>
                            <td>
                                <textarea id="ac_Remark" name="ac_Remark" style="width: 300px;"></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary" type="button" id="ac_btnApprove" onclick="ac_ApprvoveCost();">Approve</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    </div>
</asp:Content>
