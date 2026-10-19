<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="ComplianceBillingReportCanopy.aspx.cs" Inherits="Vendor_Portal.Report.ComplianceBillingReportCanopy" %>

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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Compliance Billing Report Canopy</b></h6>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">

                <table class="table">
                    <tr>
                        <td style="width: 100px;"><b>From Date :</b></td>
                        <td style="width: 200px;">
                            <input type="date" class="form-control" id="canopy_comp_FromDate" name="canopy_comp_FromDate" style="width: 200px;" />
                        </td>
                        <td style="width: 100px;"><b>To Date :</b></td>
                        <td style="width: 200px;">
                            <input type="date" class="form-control" id="canopy_comp_ToDate" name="canopy_comp_ToDate" style="width: 200px;" />
                        </td>
                        <td>
                            <button class="btn btn-primary" type="button" id="canopy_comp_btnShow" onclick="return canopy_comp_btnShowDetails();">Show</button>
                        </td>
                    </tr>
                </table>

                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-SummaryWise-tab" data-toggle="pill" href="#custom-tabs-one-SummaryWise" role="tab" aria-controls="custom-tabs-one-SummaryWise" aria-selected="true"><b>Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-TypeWise-tab" data-toggle="pill" href="#custom-tabs-one-TypeWise" role="tab" aria-controls="custom-tabs-one-TypeWise" aria-selected="false"><b>Typewise Details</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-ProjectWise-tab" data-toggle="pill" href="#custom-tabs-one-ProjectWise" role="tab" aria-controls="custom-tabs-one-ProjectWise" aria-selected="false"><b>Projectwise Details</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-DealWise-tab" data-toggle="pill" href="#custom-tabs-one-DealWise" role="tab" aria-controls="custom-tabs-one-DealWise" aria-selected="false"><b>Dealwise Details</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-LoanWise-tab" data-toggle="pill" href="#custom-tabs-one-LoanWise" role="tab" aria-controls="custom-tabs-one-LoanWise" aria-selected="false"><b>Loanwise Details</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-SummaryWise" role="tabpanel" aria-labelledby="custom-tabs-one-SummaryWise-tab">
                                <table class="table table-bordered" id="table_SummaryWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Month</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Year</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Invoice #</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Invoice Date</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Payment Status</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Paid Date</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="6">Compliance Ease</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="6">As per ERP</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Rate Per Loan</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Total Billing</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Duplicate Loans</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Difference</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Payble to Compliance Ease</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Amount Paid</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Deduction</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Negative Margin (Amount in $)</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="3">Account Remark</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;" colspan="3">Compliance Analyzer</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="3">TRID</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="3">Compliance Analyzer</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="3">TRID</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Rate</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Rate</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Rate</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Rate</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-TypeWise" role="tabpanel" aria-labelledby="custom-tabs-one-TypeWise-tab">
                                <table class="table table-bordered" id="table_TypeWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; width: 90px;" rowspan="2">Month</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Year</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Type</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">Compliance Analyzer</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Duplicate Loans</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-ProjectWise" role="tabpanel" aria-labelledby="custom-tabs-one-ProjectWise-tab">
                                <table class="table table-bordered" id="table_ProjectWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; width: 90px;" rowspan="2">Month</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Year</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Project Name</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">Compliance Analyzer</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Duplicate Loans</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-DealWise" role="tabpanel" aria-labelledby="custom-tabs-one-DealWise-tab">
                                <table class="table table-bordered" id="table_DealWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; width: 90px;" rowspan="2">Month</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Year</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Deal #</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">Compliance Analyzer</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Duplicate Loans</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Loan Count From (Tracking Sheet)</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Billed Loan Amount</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Difference (Tracking Loan Count - Billed Count)</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                            <th class="sort border-top" style="text-align: center;">Count</th>
                                            <th class="sort border-top" style="text-align: center;">Total</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-LoanWise" role="tabpanel" aria-labelledby="custom-tabs-one-LoanWise-tab">
                                <table class="table table-bordered" id="table_LoanWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; width: 90px;" rowspan="2">Month</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Year</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Loan #</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Order Date</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Audit TimeStamp</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">User Name</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Disclosure Type</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">CE ID</th>
                                            <th class="sort border-top" style="text-align: center;" colspan="2">Cost</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Payment Status</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Paid On</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">CE Bill #</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Is Duplicate</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Lender</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Borrower Name</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">City</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">State</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Source</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">System Remark</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Operation Remark</th>
                                            <th class="sort border-top" style="text-align: center;" rowspan="2">Client Billing</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center;">Compliance Analyzer</th>
                                            <th class="sort border-top" style="text-align: center;">TRID</th>
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
