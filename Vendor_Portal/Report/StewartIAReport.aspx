<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="StewartIAReport.aspx.cs" Inherits="Vendor_Portal.Report.StewartIAReport" %>

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
            BindCanopy_Stewart_SummaryGrid();
            BindCanopy_Stewart_DetailsGrid();
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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>StewartIA Report</b></h6>
                </div>
            </div>
        </div>
    </div>

    <div class="card-body">
        <div class="card card-tabs">
            <div class="card-header p-0 pt-1">
                <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="custom-tabs-one-StewartIA_Summary-tab" data-toggle="pill" href="#custom-tabs-one-StewartIA_Summary" role="tab" aria-controls="custom-tabs-one-StewartIA_Summary" aria-selected="true"><b>StewartIA Summary</b></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="custom-tabs-one-StewartIADetails-tab" data-toggle="pill" href="#custom-tabs-one-StewartIADetails" role="tab" aria-controls="custom-tabs-one-StewartIADetails" aria-selected="false"><b>StewartIA Details</b></a>
                    </li>
                </ul>
            </div>

            <div class="card-body">
                <div class="tab-content" id="custom-tabs-one-tabContent">
                    <div class="tab-pane fade show active" id="custom-tabs-one-StewartIA_Summary" role="tabpanel" aria-labelledby="custom-tabs-one-StewartIA_Summary-tab">
                        <table class="table table-bordered" id="table_StewartIASummary" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="sort border-top" style="text-align: center;">Sr #</th>
                                    <th class="sort border-top" style="text-align: center;">Invoice Type</th>
                                    <th class="sort border-top" style="text-align: center;">Month</th>
                                    <th class="sort border-top" style="text-align: center;">Year</th>
                                    <th class="sort border-top" style="text-align: center;">Invoice #</th>
                                    <th class="sort border-top" style="text-align: center;">Invoice Date</th>
                                    <th class="sort border-top" style="text-align: center;">Invoice Amount</th>
                                    <th class="sort border-top" style="text-align: center;">Remark</th>
                                    <th class="sort border-top" style="text-align: center;">Paid Date</th>
                                    <th class="sort border-top" style="text-align: center;">Paid Remark</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>

                    <div class="tab-pane fade show fade" id="custom-tabs-one-StewartIADetails" role="tabpanel" aria-labelledby="custom-tabs-one-StewartIADetails-tab">
                        <table class="table table-bordered" id="table_StewartIADetails" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="sort border-top" style="text-align: center;">Month</th>
                                    <th class="sort border-top" style="text-align: center;">Year</th>
                                    <th class="sort border-top" style="text-align: center;">Invoice Date</th>
                                    <th class="sort border-top" style="text-align: center;">Invoice #</th>
                                    <th class="sort border-top" style="text-align: center;">Order Date</th>
                                    <th class="sort border-top" style="text-align: center;">Complete Date</th>
                                    <th class="sort border-top" style="text-align: center;">Business Days</th>
                                    <th class="sort border-top" style="text-align: center;">Amount</th>
                                    <th class="sort border-top" style="text-align: center;">Loan Number</th>
                                    <th class="sort border-top" style="text-align: center;">Case Number</th>
                                    <th class="sort border-top" style="text-align: center;">Matched with LM Database (Yes/No)</th>
                                    <th class="sort border-top" style="text-align: center;">If Yes,Transaction ID</th>
                                    <th class="sort border-top" style="text-align: center;">Duplicate</th>  
                                    <th class="sort border-top" style="text-align: center;">Completed Date</th>
                                    <th class="sort border-top" style="text-align: center;">Billed To Client</th>
                                    <th class="sort border-top" style="text-align: center;">Client Billing</th>  
                                    <th class="sort border-top" style="text-align: center;">Client Billing Cost</th>  
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
