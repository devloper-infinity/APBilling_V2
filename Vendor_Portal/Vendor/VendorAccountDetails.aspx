<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="VendorAccountDetails.aspx.cs" Inherits="Vendor_Portal.Vendor.VendorAccountDetails" %>

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
           
        });
    </script>
        <script src="../Scripts/Functions/VendorAccountDetails.js" type="text/javascript"></script>

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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Vendor Account Details Master</b></h6>
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
                            <select id="VendorAccount_company" name="VendorAccount_company" class="form-control" onchange="return GetVendorforAccount(this);" style="width: 300px;">
                                <option value="0">Select</option>
                                <option value="Canopy">Canopy</option>
                                <option value="IPS">IPS</option>
                            </select>
                        </td>


                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Vendor :</b></td>
                        <td>
                            <select id="VendorAccount_Vendor" name="VendorAccount_Vendor" class="form-control" style="width: 300px;">
                                <option value="0">Select</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                       

                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Payment Type :</b></td>
                        <td>
                            <select id="VendorAccount_PaymentType" name="VendorAccount_PaymentType" class="form-control" style="width: 300px;">
                                <option value="0">Select</option>
                                <option value="ECH">ECH</option>
                                <option value="BillType">Bill Type</option>
                            </select>
                        </td>

                           <td><i class="fa fa-star" style="font-size: 5px; color: red;"></i>&nbsp;<b>Pay To :</b></td>
   <td>
       <input type="number" id="VendorAccount_PayTo" name="VendorAccount_PayTo" style="width: 300px; height: 30px;" />
   </td>
                    </tr>
                    <tr>
                        <td><i class="fa fa-star" style="font-size: 5px; color: red;"></i>&nbsp;<b>Ac No :</b></td>
                        <td>
                            <input type="number" id="VendorAccount_AcNo" name="VendorAccount_AcNo" style="width: 300px; height: 30px;" />
                        </td>

                           <td><i class="fa fa-star" style="font-size: 5px; color: red;"></i>&nbsp;<b>Email Id :</b></td>
   <td>
       <input type="number" id="VendorAccount_EmailId" name="VendorAccount_EmailId" style="width: 300px; height: 30px;" />
   </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <button class="btn btn-primary" type="button" id="VendorAccount_btnSubmit" onclick="return VendorAccount_Submit();">Submit</button>
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
                                <a class="nav-link active" id="custom-tabs-one-VendorAccount_canopy-tab" data-toggle="pill" href="#custom-tabs-one-VendorAccount_canopy" role="tab" aria-controls="custom-tabs-one-VendorAccount_canopy" aria-selected="true"><b>Canopy</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-VendorAccount_IPS-tab" data-toggle="pill" href="#custom-tabs-one-VendorAccount_IPS" role="tab" aria-controls="custom-tabs-one-VendorAccount_IPS" aria-selected="false"><b>IPS</b></a>
                            </li>
                        </ul>
                    </div>
                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-VendorAccount_canopy" role="tabpanel" aria-labelledby="custom-tabs-one-VendorAccount_canopy-tab">
                                <div>

                                    <table class="table" id="table_VendorAccount_Canopy" style="width: 100%;">
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

                            <div class="tab-pane fade show fade" id="custom-tabs-one-VendorAccount_IPS" role="tabpanel" aria-labelledby="custom-tabs-one-VendorAccount_IPS-tab">
                                <div>
                                    <table class="table" id="table_VendorAccount_IPS" style="width: 100%;">
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
