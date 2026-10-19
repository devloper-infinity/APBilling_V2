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
        $(document).ready(function () {
            approvalCostCanopy_BindGrid();
            approvalCostIPS_BindGrid();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
</asp:Content>
