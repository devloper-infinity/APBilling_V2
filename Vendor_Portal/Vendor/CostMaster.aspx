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
            CostMasterCanopy_BindGrid();
            CostMasterIPS_BindGrid();
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
</asp:Content>
