<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="LauraMacBillingReport.aspx.cs" Inherits="Vendor_Portal.Report.LauraMacBillingReport" %>

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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>LauraMac Billig Report</b></h6>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12">
        <div class="card">
            <table class="table">
                <tr>
                    <td style="width: 100px;"><b>From Date :</b></td>
                    <td style="width: 200px;">
                        <input type="date" class="form-control" id="canopy_LauraMac_FromDate" name="canopy_LauraMac_FromDate" style="width: 200px;" />
                    </td>
                    <td style="width: 100px;"><b>To Date :</b></td>
                    <td style="width: 200px;">
                        <input type="date" class="form-control" id="canopy_LauraMac_ToDate" name="canopy_LauraMac_ToDate" style="width: 200px;" />
                    </td>
                    <td>
                        <button class="btn btn-primary" type="button" id="canopy_LauraMac_btnShow" onclick="return canopy_LauraMac_btnShowDetails();">Show</button>
                    </td>
                </tr>
            </table>

            <div class="card-body">
                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-SummaryWise-tab" data-toggle="pill" href="#custom-tabs-one-LauraMac_Summary" role="tab" aria-controls="custom-tabs-one-SummaryWise" aria-selected="true"><b>Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-LauraMac_LoanWise-tab" data-toggle="pill" href="#custom-tabs-one-LauraMac_LoanWise" role="tab" aria-controls="custom-tabs-one-LauraMac_LoanWise" aria-selected="false"><b>Loanwise Details</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-LauraMac_WorkOrders-tab" data-toggle="pill" href="#custom-tabs-one-LauraMac_WorkOrders" role="tab" aria-controls="custom-tabs-one-LauraMac_WorkOrders" aria-selected="false"><b>Work orders Charges</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-LauraMac_ScriptWiseSummary-tab" data-toggle="pill" href="#custom-tabs-one-LauraMac_ScriptWiseSummary" role="tab" aria-controls="custom-tabs-one-LauraMac_ScriptWiseSummary" aria-selected="false"><b>Script Wise Summary</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-LauraMac_Summary" role="tabpanel" aria-labelledby="custom-tabs-one-LauraMac_Summary-tab">
                                <table class="table table-bordered" id="table_LauraMac_Summary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top">Sr #</th>
                                            <th class="sort border-top">Month</th>
                                            <th class="sort border-top">Year</th>
                                            <th class="sort border-top">Invoice #</th>
                                            <th class="sort border-top">Invoice Date</th>
                                            <th class="sort border-top">Invoice Amount</th>
                                            <th class="sort border-top">Per Order Cost</th>
                                            <th class="sort border-top">Remark</th>
                                            <th class="sort border-top">Deducted Loans Count</th>
                                            <th class="sort border-top">Deducted Amount</th>
                                            <th class="sort border-top">Payable To LM</th>
                                            <th class="sort border-top">Approved Remark</th>
                                            <th class="sort border-top">Approved By</th>
                                            <th class="sort border-top">Approved Date</th>
                                            <th class="sort border-top">Paid Date</th>
                                            <th class="sort border-top">Paid Remark</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-LauraMac_LoanWise" role="tabpanel" aria-labelledby="custom-tabs-one-LauraMac_LoanWise-tab">
                                <table class="table table-bordered" id="table_LauraMac_LoanWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                              <th class="sort border-top">Month</th>
                                              <th class="sort border-top">Year</th>
                                              <th class="sort border-top">Loan #</th>
                                              <th class="sort border-top">Activated Date</th>
                                              <th class="sort border-top">Invocie #</th>
                                              <th class="sort border-top">Script</th>
                                              <th class="sort border-top">Completed Date</th>
                                              <th class="sort border-top">Matched with LM Database (Yes/No)</th>
                                              <th class="sort border-top">If Yes,Transaction ID</th>
                                              <th class="sort border-top">Duplicate</th>
                                              <th class="sort border-top">Billed To Client</th>
                                              <th class="sort border-top">If Yes, Billing Period</th>
                                              <th class="sort border-top">AP System Remark</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-LauraMac_WorkOrders" role="tabpanel" aria-labelledby="custom-tabs-one-LauraMac_WorkOrders-tab">
                                <table class="table table-bordered" id="table_LauraMac_WorkOrders" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top">Invoice #</th>
                                            <th class="sort border-top">LauraMac BIlling Month</th>
                                            <th class="sort border-top">WD #</th>
                                            <th class="sort border-top">Date</th>
                                            <th class="sort border-top">Time Spent(Hrs)</th>
                                            <th class="sort border-top">Hourly Rate</th>
                                            <th class="sort border-top">Cost</th>
                                            <th class="sort border-top">Remark</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-LauraMac_ScriptWiseSummary" role="tabpanel" aria-labelledby="custom-tabs-one-LauraMac_ScriptWiseSummary-tab">
                                <table class="table table-bordered" id="table_LauraMac_ScriptWiseSummary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top">Month</th>
                                            <th class="sort border-top">Year</th>
                                            <th class="sort border-top">Script Name</th>
                                            <th class="sort border-top">Loan #</th>
                                            <th class="sort border-top">Page Per Script</th>
                                            <th class="sort border-top">Data Per Script</th>
                                            <th class="sort border-top">Total Pages</th>
                                            <th class="sort border-top">Total Fields</th>
                                            <th class="sort border-top">Total Page Cost</th>
                                            <th class="sort border-top">Total Field Cost</th>
                                            <th class="sort border-top">Total Cost</th>
                                            <th class="sort border-top">Cost Per Loan</th>
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
