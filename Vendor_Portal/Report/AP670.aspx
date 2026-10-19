<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="AP670.aspx.cs" Inherits="Vendor_Portal.Report.AP670" %>

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
            /*background: linear-gradient(to bottom, #c5c5c5, 3%, #fff) !important;*/
            color: #000;
        }

        .table.dataTable tr td {
            background: none !important;
            background-color: #fff !important;
        }
    </style>

    <script>

        $(document).ready(function () {
            //  Bind_AP670_UnPaidSummaryGrid();
            BindUnpaidGrid();
            Bind_AP670_PaidSummaryGrid();
        });

    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="loading" id="load1">
        <img src="images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>

    <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>AP 670 Report</b></h6>
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
                                <a class="nav-link active" id="custom-tabs-one-AP670_UnPaidSummary-tab" data-toggle="pill" href="#custom-tabs-one-AP670_UnPaidSummary" role="tab" aria-controls="custom-tabs-one-AP670_UnPaidSummary" aria-selected="true"><b>Un-Paid Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-AP670_PaidSummary-tab" data-toggle="pill" href="#custom-tabs-one-AP670_PaidSummary" role="tab" aria-controls="custom-tabs-one-AP670_PaidSummary" aria-selected="false"><b>Paid Summary</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-AP670_UnPaidSummary" role="tabpanel" aria-labelledby="custom-tabs-one-AP670_UnPaidSummary-tab">

                                  <%--  <button id="creditcons_btnexport" class="btn btn-primary" onclick="return creditcons_Submit()">Export to excel</button>
                            onclick="return BindMagnaGrid();"--%>
                                <asp:Button ID="btn1" runat="server" Style="display: none;" OnClick="btn1_Click"  />
                                <table class="table table-bordered" id="table_AP670_UnPaidSummary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;" colspan="7">Client Billing</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="4">UW's Billing</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="3">Accounts Remark</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Week Worked</th>
                                            <th class="sort border-top">Day</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Hours Worked</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Rate in USD</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Total Charges in US $</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Job Description</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Member Name</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">UW's hours worked</th>
                                            <th class="sort border-top">Rate</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Total Amount</th>
                                            <th class="sort border-top">NVA</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Payment Status</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Paid Date</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Accounts Remark</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-AP670_PaidSummary" role="tabpanel" aria-labelledby="custom-tabs-one-AP670_PaidSummary-tab">
                                <table class="table table-bordered" id="table_AP670_PaidSummary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;" colspan="7">Client Billing</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="4">UW's Billing</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="3">Accounts Remark</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Week Worked</th>
                                            <th class="sort border-top">Day</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Hours Worked</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Rate in USD</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Total Charges in US $</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Job Description</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Member Name</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">UW's hours worked</th>
                                            <th class="sort border-top">Rate</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Total Amount</th>
                                            <th class="sort border-top">NVA</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Payment Status</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Paid Date</th>
                                            <th class="sort border-top" style="text-wrap: nowrap;">Accounts Remark</th>
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


</asp:Content>
