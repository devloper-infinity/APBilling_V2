<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="SciennaSummary.aspx.cs" Inherits="Vendor_Portal.Vendor.SciennaSummary" %>
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

        /*.form-control {
            font-size: 11px !important;
        }*/
    </style>
    <script>
        $(document).ready(function () {
            //addinvoiceDD_BindYear();
            BindSciennaSummaryData("", "");

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
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Scienna Summary</b></h6>
                </div>
            </div>
        </div>
    </div>

       <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <table class="table" style="display:none;">
                    <tr>
                        <td><b>Month:</b></td>
                        <td>
                            <select id="addinvoiceDD_month" name="addinvoiceDD_month" class="form-control" style="width: 250px;">
                                <option value="">Select</option>
                                <option value="January">January</option>
                                <option value="February">February</option>
                                <option value="March">March</option>
                                <option value="April">April</option>
                                <option value="May">May</option>
                                <option value="June">June</option>
                                <option value="July">July</option>
                                <option value="August">August</option>
                                <option value="September">September</option>
                                <option value="October">October</option>
                                <option value="November">November</option>
                                <option value="December">December</option>
                            </select>
                        </td>
                        <td><b>Year :</b></td>
                        <td>
                            <select id="addinvoiceDD_year" name="addinvoiceDD_year" class="form-control" style="width: 250px;">
                                <option value="">Select</option>
                            </select>
                        </td>
                    </tr>
                </table>
               <%-- <hr />--%>
                <table class="table" id="table_SciennaSummary" style="width: 100%;">
                    <thead>
                        <tr>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Month</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Year</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Client</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Project</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Period Ending</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Loans Reviewed</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Per Loan UsageFees</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Usage Fees</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Total</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Sys Remark</th>

                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Tracking Sheet Loans</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Pending Loans</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Billable</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
