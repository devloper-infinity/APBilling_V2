<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="InvoiceReportCanopy.aspx.cs" Inherits="Vendor_Portal.Vendor.InvoiceReportCanopy" %>

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
     <script>
        $(document).ready(function () {
            BindCanopyReportGrid();
        });
     </script>
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
           <div class="col-lg-15 mt-3 mb-2">
    <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
        <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
            <div>
                <h5 class="m-0 font-weight-bold">
                    <i class="fas fa-copy"></i> All Invoice Report-canopy
            </h5>          
            </div>   
        </div>
    </div>
</div>
    <div class="col-lg-12">
        <div class="card-body">
               <div style="width: 100%; overflow: auto;">
                        <table class="table" id="invrpt_Canopy" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Actions</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">InvoiceId</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Month</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Year</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Type</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Date</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Due Date</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Amount</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Loan Count</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Remark</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Reconcile Process</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Approval Process</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Payment Process</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
        </div>
      </div>

     <div class="modal fade" id="popUpViewLoanDetailsrptCanopy">
        <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
    <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
        <div>
            <h6 class="m-0 font-weight-bold">
                <i class="fas fa-copy"></i> View Loan Details
        </h6>         
                      
            </div>
                               <div style="margin-left: auto;">
        <button type="button" class="close" data-dismiss="modal" onclick="return location.reload();" style="color: red; font-weight: 900; opacity: 1;" data-toggle="tooltip" data-placement="top" title="Close">
    <span>&times;</span>

</button>
        </div>   
    </div>
</div>
                <div class="modal-body">
                    <div style="width: 100%; overflow: auto;">
                        <table class="table" id="viewloanDetailsrptCanopy_table" style="width: 100%;"></table>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>
</asp:Content>
