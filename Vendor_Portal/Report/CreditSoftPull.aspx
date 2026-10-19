<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="CreditSoftPull.aspx.cs" Inherits="Vendor_Portal.Report.CreditSoftPull" %>

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
            BindIPS_CreditSoftPull_SummaryGrid();
            BindIPS_CreditSoftPull_DetailsGrid();
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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>KCB</b></h6>
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
                                <a class="nav-link active" id="custom-tabs-one-CreditSoftPull_Summary-tab" data-toggle="pill" href="#custom-tabs-one-CreditSoftPull_Summary" role="tab" aria-controls="custom-tabs-one-CreditSoftPull_Summary" aria-selected="true"><b>KCB Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-CreditSoftPullDetails-tab" data-toggle="pill" href="#custom-tabs-one-CreditSoftPullDetails" role="tab" aria-controls="custom-tabs-one-CreditSoftPullDetails" aria-selected="false"><b>KCB Details</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-CreditSoftPull_Summary" role="tabpanel" aria-labelledby="custom-tabs-one-CreditSoftPull_Summary-tab">
                                <table class="table table-bordered" id="table_CreditSoftPullSummary" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <%--  <th class="sort border-top" style="text-align: center;">Sr #</th>--%>
                                            <th class="sort border-top">Invoice Type</th>
                                            <th class="sort border-top">Month</th>
                                            <th class="sort border-top">Year</th>
                                            <th class="sort border-top">Invoice #</th>
                                            <th class="sort border-top">Invoice Date</th>
                                            <th class="sort border-top">Invoice Amount</th>
                                            <th class="sort border-top">Remark</th>
                                            <th class="sort border-top">Paid Date</th>
                                            <th class="sort border-top">Paid Remark (Charged to client invoice #)</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-CreditSoftPullDetails" role="tabpanel" aria-labelledby="custom-tabs-one-CreditSoftPullDetails-tab">
                                <table class="table table-bordered" id="table_CreditSoftPullDetails" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top">Month</th>
                                            <th class="sort border-top">Year</th>
                                            <th class="sort border-top">Invoice Date</th>
                                            <th class="sort border-top">Customer Name</th>
                                            <th class="sort border-top">Customer #</th>
                                            <th class="sort border-top">File #</th>
                                            <th class="sort border-top">Ref #</th>
                                            <th class="sort border-top">First Name</th>
                                            <th class="sort border-top">Last Name</th>
                                            <th class="sort border-top">Product</th>
                                            <th class="sort border-top">User</th>
                                            <th class="sort border-top">Description</th>
                                            <th class="sort border-top">Payments</th>
                                            <th class="sort border-top">Charges</th>
                                            <th class="sort border-top">Securitization Deal #</th>
                                            <th class="sort border-top">Invoice Number</th>
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
