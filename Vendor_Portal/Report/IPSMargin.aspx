<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="IPSMargin.aspx.cs" Inherits="Vendor_Portal.Report.IPSMargin" %>

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
                .buttons-excel, .buttons-html5 {
            color: #fff;
            box-shadow: none;
            background: linear-gradient(to right, #0056b3, #007bff);
            border: 0;
            font-weight: bold;
            margin: 0px 10px;
            border-radius: 4px;
            padding: 6px 12px;
            transition: background 0.3s ease;
        }

            .buttons-excel:hover, .buttons-html5:hover {
                background: linear-gradient(to right, #004085, #0056b3);
                color: #fff;
            }


        .dataTables_scrollHeadInner, .dataTables_scrollHeadInner table {
            width: 100% !important;
        }

        .table thead th {
            background-color: #f1f5f9 !important;
            color: #003366 !important;
            font-weight: bold !important;
            font-size: 13px;
            padding: 12px 15px !important;
            min-width: 50px;
            vertical-align: middle;
        }

            .table thead th:first-child {
                border-top-left-radius: 8px !important;
                border-bottom-left-radius: 8px !important;
            }

            .table thead th:last-child {
                border-top-right-radius: 8px !important;
                border-bottom-right-radius: 8px !important;
            }

        .dataTables_scrollHead {
            border-bottom: 2px solid #0056b3 !important;

        }
        

.card {
    border-top: 3px solid #0056b3 !important;
    box-shadow: 0 0 1px rgba(0,0,0,.125), 0 1px 3px rgba(0,0,0,.2);
    margin-bottom: 1rem;
}

.card-body::after {
    content: "";
    clear: both;
    display: table;
}

.card-header.p-0.pt-1 {
    margin-bottom: 5px !important;
}
           .card-custom {
       border: none;
       border-radius: 12px;
       box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.1);
       background-color: #ffffff;
       margin-bottom: 20px;
   }

   .card-header-custom {
       background: linear-gradient(135deg, #007bff, #0056b3);
       color: white;
       border-top-left-radius: 12px !important;
       border-top-right-radius: 12px !important;
       padding: 12px 20px;
   }
      
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="loading" id="load1">
        <img src="images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
            <div class="col-lg-15 mt-3 mb-2">
    <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
        <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
            <div>
                <h5 class="m-0 font-weight-bold">
                    <i class="fas fa-copy"></i> IPS Margin Report
            </h5>          
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
                            <input type="date" class="form-control" id="ips_margin_FromDate" name="ips_margin_FromDate" style="width: 200px;" />
                        </td>
                        <td style="width: 100px;"><b>To Date :</b></td>
                        <td style="width: 200px;">
                            <input type="date" class="form-control" id="ips_margin_ToDate" name="ips_margin_ToDate" style="width: 200px;" />
                        </td>
                        <td>
                            <button class="btn btn-primary" type="button" id="ips_margin_btnShow" onclick="return ips_margin_btnShowDetails();">Show</button>
                        </td>
                    </tr>
                </table>

                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-margin-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-margin-LoanWise-tab" data-toggle="pill" href="#custom-tabs-one-margin-LoanWise" role="tab" aria-controls="custom-tabs-one-margin-marginLoanWise" aria-selected="true"><b>Loan wise Details</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-margin-TypeWise-tab" data-toggle="pill" href="#custom-tabs-one-margin-TypeWise" role="tab" aria-controls="custom-tabs-one-margin-TypeWise" aria-selected="false"><b>Typewise Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-margin-ProjectWise-tab" data-toggle="pill" href="#custom-tabs-one-margin-ProjectWise" role="tab" aria-controls="custom-tabs-one-margin-ProjectWise" aria-selected="false"><b>Projectwise Summary</b></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="custom-tabs-one-margin-DealWise-tab" data-toggle="pill" href="#custom-tabs-one-margin-DealWise" role="tab" aria-controls="custom-tabs-one-margin-DealWise" aria-selected="false"><b>Dealwise Summary</b></a>
                            </li>
                        </ul>
                    </div>

                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-margin-tabContent">
                            <div class="tab-pane fade show active" id="custom-tabs-one-margin-LoanWise" role="tabpanel" aria-labelledby="custom-tabs-one-margin-LoanWise-tab">
                                <table class="table table-bordered" id="table_ips_margin_LoanWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap; text-wrap: nowrap;" colspan="9">Project/Deal Details</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="4">Scienna</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="5">Compliance Ease</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="4">Remote Underwriters-Review</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="4">Remote Underwriters-QC</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="4">Others</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Project</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Count Column</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Credit/Servicing?</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Deal No</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan#1</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Order Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Due Date</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Sent To Accounts</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Sent To Client</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Sciena ID</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Is Paid?</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Scienna Biil #</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">CE ID</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Is Paid?</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Compliance Biil #</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Reviewer</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Rate</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Is Paid?</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">QCer</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Rate</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Is Paid?</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Client Billing Invoice #</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Client Billing</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">NVA</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-margin-TypeWise" role="tabpanel" aria-labelledby="custom-tabs-one-margin-TypeWise-tab">
                                <table class="table table-bordered" id="table_ips_margin_TypeWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" rowspan="2">Credit/Servicing?</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" rowspan="2">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">Scienna</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">Compliance Ease</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Remote Underwriters-Review</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Remote Underwriters-QC</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="3">Other</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Client Billing</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">NVA</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-margin-ProjectWise" role="tabpanel" aria-labelledby="custom-tabs-one-margin-ProjectWise-tab">
                                <table class="table table-bordered" id="table_ips_margin_ProjectWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" rowspan="2">Project #</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" rowspan="2">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">Scienna</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">Compliance Ease</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Remote Underwriters-Review</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Remote Underwriters-QC</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="3">Other</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Client Billing</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">NVA</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>

                            <div class="tab-pane fade show fade" id="custom-tabs-one-margin-DealWise" role="tabpanel" aria-labelledby="custom-tabs-one-margin-DealWise-tab">
                                <table class="table table-bordered" id="table_ips_margin_DealWise" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" rowspan="2">Deal No</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" rowspan="2">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">Scienna</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">Compliance Ease</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="2">TRID</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Remote Underwriters-Review</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Remote Underwriters-QC</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;" colspan="3">Other</th>
                                        </tr>
                                        <tr>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Loan Count</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Total Cost</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">Client Billing</th>
                                            <th class="sort border-top" style="text-align: center; text-wrap: nowrap;">NVA</th>
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
