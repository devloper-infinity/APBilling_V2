<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="UpdatePaidDateOther.aspx.cs" Inherits="Vendor_Portal.Vendor.UpdatePaidDateOther" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .loading {
            display: none;
            position: fixed;
            top: 350px;
            left: 50%;
            margin-top: -96px;
            margin-left: -96px;
            /*  background-color: #ccc;*/
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
            /*     background-color: #28a745;
            border-color: #28a745;*/
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


          //  BindOtherInvApproval_Grid(347, "Remote UW");

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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Update Paid Date Other</b></h6>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <table class="table">
                    <tr>
                        <td>
                            <b>Company :</b>
                        </td>
                        <td>
                            <select id="Other_company" name="Other_company" class="form-control" style="width: 300px;">
                                <option value="Select">Select</option>
                                <option value="Canopy">Canopy</option>
                                <option value="IPS">IPS</option>
                            </select>
                        </td>
                        <td>
                            <b>Invoice Type :</b>
                        </td>
                        <td>
                            <select id="Other_InvType" name="Other_InvType" onchange="return GetInvoices(this);" class="form-control" style="width: 300px;">
                                <option value="Select">Select</option>
                                <option value="670">670</option>
                                <option value="Abstractor">Abstractor</option>
                                <option value="Remote UW ">Remote UW</option>
                            </select>
                        </td>
                        <td><b>Invoice #</b></td>
                        <td>
                            <select id="Other_udateInvNo" name="Other_udateInvNo" onchange="return bindgrid(this);" class="form-control" style="width: 300px;">
                                <option value="Select">Select</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Paid Date :</b>
                        </td>
                        <td style="width: 250px;">
                            <input type="date" id="Others_UpPaidDate" name="Others_UpPaidDate" class="form-control" style="width: 300px;" />
                        </td>
                        <td><b>Remark : </b></td>
                        <td colspan="3">
                            <textarea id="Others_updateRemark" name="Others_updateRemark" class="form-control" style="width: 720px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <button class="btn btn-primary" type="button" id="btnOthers" onclick="return Others_btnUpdatePaidDate();">Update</button>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <br />
                <table class="table" id="table_Others_UpdatePaidDate" style="width: 100%;">
                    <thead>
                        <tr>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Actions</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">VendorPayToID</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice #</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Payee</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align:center;">Loan Count</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Base Rate</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align:center;">Total Amount</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align:center;">Account # (Last 4 Digit)</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Email ID</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">UTR #</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; display:none;">TotalAmt</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td style="font-weight:bold; font-size:13px;"></td>
                            <td></td>
                            <td style="font-weight:bold; font-size:13px;"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>

    <div class="modal fade" id="waitingpanel_updateDateOther" tabindex="-1" data-bs-backdrop="static" aria-hidden="true">
        <div class="modal-dialog text-center">
            <img src="../Images/Load.gif" />
            <br />
            <span style="color: #fff; font-size: 24px; font-weight: bold; font-style: italic;" id="spntext">System is updating details. Please wait</span>
            <span style="color: #fff; font-size: 48px; font-weight: bold; font-style: italic; animation: animate 1s linear infinite;">&nbsp;. . . .</span>
        </div>
    </div>
</asp:Content>
