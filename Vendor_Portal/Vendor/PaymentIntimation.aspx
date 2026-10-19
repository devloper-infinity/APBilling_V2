<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="PaymentIntimation.aspx.cs" Inherits="Vendor_Portal.Vendor.PaymentIntimation" %>
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

             const urlParams = new URLSearchParams(window.location.search);
             const user = urlParams.get('user');
             if (user == 'IPS') {
                 BindIPSPaymentRportGrid();
             }
             else {
                 BindIPSPayment_Canopy_RportGrid();
             }
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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Payment Intimation Report</b></h6>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>
    <div class="col-lg-12">
        <div class="card-body">
               <div style="width: 100%; overflow: auto;">
                        <table class="table" id="invrptPaymentIntimation_IPS" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Company</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Payment To</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Date</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Loan Count/Hours</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Amount</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Remark</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Payment Amount</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Payment Date </th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Reference Number</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Account No</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Email Id</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Payment Remark</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
        </div>
      </div>


</asp:Content>
