<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="UpdatePaidDate.aspx.cs" Inherits="Vendor_Portal.Vendor.UpdatePaidDate" %>

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

            BindAPCanopyInvoiceApproval_Grid();
            BindAPIPSInvoiceApproval_Grid();
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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Update Paid Date</b></h6>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-tab_addinvoice" role="tablist">
                            <li class="nav-item" id="li_InvReonc_IPS">
                                <a class="nav-link active" id="custom-tabs-one-home-tab_apCanopy" data-toggle="pill" href="#custom-tabs-one-home_apCanopy" role="tab" aria-controls="custom-tabs-one-home_apCanopy" aria-selected="true"><b>Canopy</b></a>
                            </li>
                            <li class="nav-item" id="li_InvReonc_Conopy">
                                <a class="nav-link" id="custom-tabs-one-profile-tab_apIPS" data-toggle="pill" href="#custom-tabs-one-home-apIPS" role="tab" aria-controls="custom-tabs-one-profile_apIPS" aria-selected="false"><b>IPS</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-home_apCanopy" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_apCanopy">
                                <div style="width: 100%; overflow: auto;">

                                    <table class="table">
                                        <tr>
                                            <td>
                                                <b>Paid Date :</b>
                                            </td>
                                            <td style="width: 250px;">
                                                <input type="date" id="apCanopy_UpPaidDate" name="apCanopy_UpPaidDate" class="form-control" style="width: 250px;" />
                                            </td>
                                            <td><b>UTR # : </b></td>
                                            <td>
                                                <input type="text" id="apCanopy_UTRNo" name="apCanopy_UTRNo" class="form-control" />
                                            </td>
                                            <td><b>Remark : </b></td>
                                            <td>
                                                <textarea id="apCanopy_updateRemark" name="apCanopy_updateRemark" class="form-control"></textarea>
                                            </td>
                                            <td>
                                                <button class="btn btn-primary" type="button" id="btnStep5" onclick="return apCanopy_btnUpdatePaidDate();">Update</button>
                                            </td>
                                        </tr>
                                    </table>

                                    <table class="table" id="table_apCanopy_UpdatePaidDate" style="width: 100%;">
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
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Account # (Last 4 digit)</th>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Email</th>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Remark</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                        <tfoot>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="font-weight: bold; font-size: 13px;"></td>
                                                <td style="font-weight: bold; font-size: 13px;"></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-home-apIPS" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_apIPS">
                                <div style="width: 100%; overflow: auto;">
                                    <table class="table">
                                        <tr>
                                            <td>
                                                <b>Paid Date :</b>
                                            </td>
                                            <td style="width: 250px;">
                                                <input type="date" id="apIPS_UpPaidDate" name="apIPS_UpPaidDate" class="form-control" style="width: 250px;" />
                                            </td>
                                            <td><b>UTR # : </b></td>
                                            <td>
                                                <input type="text" id="apIPS_UTRNo" name="apIPS_UTRNo" class="form-control" />
                                            </td>
                                            <td><b>Remark : </b></td>
                                            <td>
                                                <textarea id="apIPS_updateRemark" name="apIPS_updateRemark" class="form-control"></textarea>
                                            </td>
                                            <td>
                                                <button class="btn btn-primary" type="button" id="btnapIPS" onclick="return apIPS_btnUpdatePaidDate();">Update</button>
                                            </td>
                                        </tr>
                                    </table>

                                    <table class="table" id="table_apIPS_UpdatePaidDate" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Actions</th>
                                                <th class="sort border-top ps-3" style="text-wrap: no wrap; text-align: center;">Sr. #</th>
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
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Account # (Last 4 digit)</th>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Email</th>
                                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Remark</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                        <tfoot>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="font-weight: bold; font-size: 13px;"></td>
                                                <td style="font-weight: bold; font-size: 13px;"></td>
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
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="MISUpdatePaidDate">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Update Paid Date</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <%--  <div class="modal-body">
                    <table class="table">
                        <tr>
                            <td style="width: 100px;"><b>Company</b> :</td>
                            <td>
                                <label id="lblcompany" name="lblcompany" class="form-control"></label>
                            </td>
                            <td><b>Invoice Type : </b></td>
                            <td>
                                <label id="lblInvType" name="lblInvType" class="form-control"></label>
                            </td>
                            <td><b>Invoice # : </b></td>
                            <td>
                                <label id="lblInvoiceNo" name="lblInvoiceNo" class="form-control"></label>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Month : </b></td>
                            <td>
                                <label id="lblMonth" name="lblMonth" class="form-control"></label>
                            </td>
                            <td><b>Year : </b></td>
                            <td>
                                <label id="lblYear" name="lblYear" class="form-control"></label>
                            </td>
                            <td><b>Invoice Amount : </b></td>
                            <td>
                                <label id="lblInvoiceAmount" name="lblInvoiceAmount" class="form-control"></label>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table class="table">
                        <tr>
                            <td>
                                <b>Paid Date :</b>
                            </td>
                            <td>
                                <input type="date" id="mis_UpPaidDate" name="mis_UpPaidDate" class="form-control" />
                            </td>
                            <td><b>Remark : </b></td>
                            <td colspan="5">
                                <textarea id="updateRemark" name="updateRemark" class="form-control"></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary" type="button" id="btnStep5" onclick="return mis_btnUpdatePaidDate();">Update</button>
                </div>--%>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>

    <div class="modal fade" id="waitingpanel_updateDate" tabindex="-1" data-bs-backdrop="static" aria-hidden="true">
        <div class="modal-dialog text-center">
            <img src="../Images/Load.gif" />
            <br />
            <span style="color: #fff; font-size: 24px; font-weight: bold; font-style: italic;" id="spntext">System is updating details. Please wait</span>
            <span style="color: #fff; font-size: 48px; font-weight: bold; font-style: italic; animation: animate 1s linear infinite;">&nbsp;. . . .</span>
        </div>
    </div>

</asp:Content>
