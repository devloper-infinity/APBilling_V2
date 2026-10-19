<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="UpdateClientInvoice.aspx.cs" Inherits="Vendor_Portal.Vendor.UpdateClientInvoice" %>

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

            BindGrid_UpdateClientInv();

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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Update Client Invoice </b></h6>
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
                            <select id="updateCI_company" name="updateCI_company" class="form-control" onchange="return GetVendor_ClientInvoiceDate(this);" style="width: 250px;">
                                <option value="0">Select</option>
                                <option value="Canopy">Canopy</option>
                                <option value="IPS">IPS</option>
                            </select>
                        </td>
                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Vendor :</b></td>
                        <td>
                            <select id="updateCI_Vendor" name="updateCI_Vendor" class="form-control" style="width: 250px;">
                                <option value="0">Select</option>
                            </select>
                        </td>
                        <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Client Invoice #:</b></td>
                        <td>
                            <input type="text" id="updateCI_InvoiceNo" name="updateCI_InvoiceNo" class="form-control" style="width: 250px;" />
                        </td>
                        <td>
                            <button class="btn btn-primary" type="button" id="updateClientDate" onclick="return OnClick_updateClientDate();">Update</button>
                        </td>
                    </tr>
                </table>
                <hr />
                <table class="table" id="table_updateClientInvoice" style="width: 100%;">
                    <thead>
                        <tr>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Actions</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">InvoiceId</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Company</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Month</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Year</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Type</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice #</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Date</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Due Date</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Amount</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Loan Count</th>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap;">Remark</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="modal fade" id="waitingpanel_updateClientInv" tabindex="-1" data-bs-backdrop="static" aria-hidden="true">
        <div class="modal-dialog text-center">
            <img src="../Images/Load.gif" />
            <br />
            <span style="color: #fff; font-size: 24px; font-weight: bold; font-style: italic;" id="spntext">System is updating details. Please wait</span>
            <span style="color: #fff; font-size: 48px; font-weight: bold; font-style: italic; animation: animate 1s linear infinite;">&nbsp;. . . .</span>
        </div>
    </div>

</asp:Content>
