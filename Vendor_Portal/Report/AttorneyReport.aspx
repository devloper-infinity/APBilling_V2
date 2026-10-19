<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="AttorneyReport.aspx.cs" Inherits="Vendor_Portal.Report.AttorneyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .loading {
            display: none;
            position: fixed;
            top: 350px;
            left: 50%;
            Margin-top: -96px;
            Margin-left: -96px;
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
            Margin: 0px 10px;
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
            BindIPS_Attorney_BillingSummaryGrid();
            BindIPS_Attorney_InvoiceSummaryGrid();
            BindIPS_Attorney_DetailsGrid();
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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Attorney Report</b></h6>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">

                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-ipsAttorney-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-ipsAttorney-billingSummary-tab" data-toggle="pill" href="#custom-tabs-one-ipsAttorney-billingSummary" role="tab" aria-controls="custom-tabs-one-ipsAttorney-ipsAttorney-billingSummary" aria-selected="true"><b>Loan wise Details</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-ipsAttorney-InvoiceSummary-tab" data-toggle="pill" href="#custom-tabs-one-ipsAttorney-InvoiceSummary" role="tab" aria-controls="custom-tabs-one-ipsAttorney-InvoiceSummary" aria-selected="false"><b>InvoiceSummary Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-ipsAttorney-Details-tab" data-toggle="pill" href="#custom-tabs-one-ipsAttorney-Details" role="tab" aria-controls="custom-tabs-one-ipsAttorney-Details" aria-selected="false"><b>Details Summary</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-ipsAttorney-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-ipsAttorney-billingSummary" role="tabpanel" aria-labelledby="custom-tabs-one-ipsAttorney-billingSummary-tab">
                                <table class="table table-bordered" id="table_ips_Attorney_billingSummary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Number</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Vendor Name</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Amount</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Description</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Charged to client</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Paid to Vendor</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Paid Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Operation's Comments</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Accounts Comments</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-ipsAttorney-InvoiceSummary" role="tabpanel" aria-labelledby="custom-tabs-one-ipsAttorney-InvoiceSummary-tab">
                                <table class="table table-bordered" id="table_ips_Attorney_InvoiceSummary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Type</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Month</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Year</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Number</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Amount</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Operation's Comments</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-ipsAttorney-Details" role="tabpanel" aria-labelledby="custom-tabs-one-ipsAttorney-Details-tab">
                                <table class="table table-bordered" id="table_ips_Attorney_Details" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Invoice Number</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Time Keeper</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Hours</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Amount</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Description</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Operation's Comments</th>
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
