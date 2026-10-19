<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="InvoiceApproval.aspx.cs" Inherits="Vendor_Portal.Vendor.InvoiceApproval" %>

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

       
    </style>


    <style>
        .buttons-excel{

    background:#10B981!important;

    color:#fff!important;

    border-radius:8px;

    border:none;

    padding:7px 18px;

}

        .dataTables_filter input{

    border-radius:8px;

    border:1px solid #D1D5DB;

    padding:6px 12px;

}

        .loading{

    background:white;

    border-radius:15px;

    box-shadow:0 10px 40px rgba(0,0,0,.15);

}

        .dataTables_length select{

    border-radius:8px;

}
    </style>


    <style>
        .invoice-modal {
    border-radius: 18px;
    overflow: hidden;
    border: none;
}

.invoice-modal .modal-header {
    background: linear-gradient(90deg,#2563EB,#1D4ED8);
    color: #fff;
    border: none;
    padding: 20px 25px;
}

.invoice-modal .modal-header h4 {
    margin: 0;
    font-weight: 600;
}

.invoice-modal .modal-header small {
    color: #E0E7FF;
}

.invoice-modal .modal-body {
    background: #F8FAFC;
    padding: 25px;
}

.section-title {
    background: #EEF4FF;
    color: #2563EB;
    padding: 12px 18px;
    border-radius: 10px;
    font-weight: 600;
    margin-bottom: 20px;
}

.info-box {
    background: #fff;
    border: 1px solid #E2E8F0;
    border-radius: 10px;
    min-height: 46px;
    padding: 10px 15px;
    display: flex;
    align-items: center;
    font-weight: 600;
    color: #334155;
}

.amount-box {
    color: #16A34A;
    font-size: 18px;
}

.invoice-modal label {
    font-weight: 600;
    color: #475569;
    margin-bottom: 6px;
}

.invoice-modal .form-control {
    border-radius: 10px;
    border: 1px solid #CBD5E1;
    min-height: 46px;
}

.invoice-modal textarea {
    min-height: 110px;
    resize: vertical;
}

.invoice-modal .modal-footer {
    background: #fff;
    border-top: 1px solid #E5E7EB;
    padding: 18px 25px;
}

.invoice-modal .btn-success {
    background: #16A34A;
    border: none;
    border-radius: 10px;
    min-width: 180px;
}

.invoice-modal .btn-light {
    border-radius: 10px;
    min-width: 120px;
}
    </style>

    <style>
        .dataTables_wrapper {

    padding: 10px;

}

.dataTables_filter input {

    border-radius: 8px !important;

    border: 1px solid #CBD5E1 !important;

    padding: 6px 10px !important;

}

.dataTables_length select {

    border-radius: 8px !important;

}

.dataTables_paginate .paginate_button.current {

    background: #2563EB !important;

    color: white !important;

    border-radius: 6px;

}

.dataTables_paginate .paginate_button:hover {

    background: #3B82F6 !important;

    color: white !important;

}
    </style>

    <style>
       




.loan-modal .table thead th{

  

    font-size: 13px;

    font-weight: 600;

    padding: 13px 10px;

    white-space: nowrap;

}



.card-custom {
    border: none;
    border-radius: 12px;
    box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.1);
    background-color: #ffffff;
    margin-bottom: 20px;
    width: 100% !important;
    max-width: 100% !important;
}

.card-header-custom {
    background: linear-gradient(135deg, #007bff, #0056b3);
    color: white;
    border-top-left-radius: 12px !important;
    border-top-right-radius: 12px !important;
    padding: 12px 20px;
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


    </style>


    <style>
       

       .table thead {
    border-radius: 8px !important;
    overflow: hidden;
}

.table thead th {
    background-color: #f1f5f9 !important;
    color: #003366 !important;
    border-bottom: 2px solid #0056b3 !important;
    
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


      
        .dataTables_wrapper {
            display: block !important;
        }

        .dataTables_length {
            float: left !important;
            margin-bottom: 10px !important;
        }

        .dataTables_filter {
            float: right !important;
            margin-bottom: 10px !important;
            text-align: right !important;
        }

        .table {
            clear: both !important;
            margin-top: 5px !important;
        }

        .dataTables_info {
            float: left !important;
            margin-top: 10px !important;
        }

        .dataTables_paginate {
            float: right !important;
            margin-top: 10px !important;
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
        #Approval_remark {
    resize: none;
}

    </style>

    <script>
        $(document).ready(function () {

            CheckLoginID_ForRights();
            BindGrid_InvoiceApproval();

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
                       <i class="fas fa-file-invoice-dollar"></i> Invoice Approval
               </h5>
                   <small>Review, approve and manage vendor invoices.
               </small>

               </div>
               <div style="margin-left: auto;">
                   <button type="button" class="btn btn-link p-0 ml-3 text-white" onclick="location.reload();" style="font-size: 14px; text-decoration: none;" data-toggle="tooltip" data-placement="bottom" title="Refresh Page">
                       <i class="fas fa-sync-alt"></i>
                   </button>
               </div>
           </div>
       </div>
   </div>

    <div class="col-lg-15">
        <div class="card">
            <div class="card-body">
                <div class="tab-content" id="custom-tabs-one-tabContent_addinvocie">
                    <div class="tab-pane fade show active" id="custom-tabs-one-home_infinity" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_infinity">
                        <div style="width: 100%; overflow: auto;">
                            <table class="table table-striped table-hover" id="invrec_IPS" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th class="sort border-top ps-c3" style="text-wrap: nowrap;">Actions</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">InvoiceId</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Company</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Month</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Year</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Type</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice #</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Date</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Received Date</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Due Date</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Amount</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">No Of Loans</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">No Of Hours</th>
                                        <%--<th class="sort border-top ps-3" style="text-wrap: nowrap;">Base Rate</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Calculated amount</th>--%>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Remark</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;" >Current Status</th>  
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>

                    <div class="tab-pane fade" id="custom-tabs-one-profile_canopy" role="tabpanel" aria-labelledby="custom-tabs-one-profile-tab_canopy">
                        <div style="width: 100%; overflow: auto;">
                            <table class="table" id="invrec_canopy" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Actions</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">InvoiceId</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Company</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Month</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Year</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Type</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice #</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Date</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Due Date</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Invoice Amount</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Loan Count</th>
                                        <th class="sort border-top ps-3" style="text-wrap: nowrap;">Production Verification Remark</th>
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

    

    <div class="modal fade" id="InvoiceApproval">
    <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content invoice-modal">

            <!-- Header -->
           <div class="modal-header py-2 px-3">
                <div>
                    <h4 class="mb-0">
                        <i class="fas fa-file-invoice-dollar"></i>
                        Invoice Approval
                    </h4>
                    <small>Review and approve vendor invoice</small>
                </div>
                <button type="button" class="close text-white" data-dismiss="modal">
                    <span>&times;</span>
                </button>
            </div>

            <!-- Body -->
           <div class="modal-body py-2 px-3">
                <div class="row">

                    <!-- LEFT COLUMN: Invoice Information (5 Fields) -->
                    <div class="col-md-6 border-right">
                        <div class="section-title mb-4">
                            <i class="fas fa-info-circle"></i>
                            Invoice Information
                        </div>

                        <!-- 1. Company -->
                        <div class="form-group row align-items-center mb-2">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Company</label>
                            <div class="col-sm-8">
                                <div class="info-box form-control" style="background-color: #f8f9fa; height: 38px; display: flex; align-items: center; font-weight: bold;">
                                    <span id="lblcompany"></span>
                                </div>
                            </div>
                        </div>

                        <!-- 2. Invoice Type -->
                        <div class="form-group row align-items-center mb-2">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Invoice Type</label>
                            <div class="col-sm-8">
                                <div class="info-box form-control" style="background-color: #f8f9fa; height: 38px; display: flex; align-items: center; font-weight: bold;">
                                    <span id="lblInvoiceType"></span>
                                </div>
                            </div>
                        </div>

                        <!-- 3. Invoice No -->
                        <div class="form-group row align-items-center mb-2">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Invoice No</label>
                            <div class="col-sm-8">
                                <div class="info-box form-control" style="background-color: #f8f9fa; height: 38px; display: flex; align-items: center; font-weight: bold;">
                                    <span id="lblInvoiceNo"></span>
                                </div>
                            </div>
                        </div>

                        <!-- 4. Invoice Date -->
                        <div class="form-group row align-items-center mb-2">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Invoice Date</label>
                            <div class="col-sm-8">
                                <div class="info-box form-control" style="background-color: #f8f9fa; height: 38px; display: flex; align-items: center; font-weight: bold;">
                                    <span id="lblInvoiceDate"></span>
                                </div>
                            </div>
                        </div>

                        <!-- 5. Invoice Amount -->
                        <div class="form-group row align-items-center mb-2">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Invoice Amount</label>
                            <div class="col-sm-8">
                                <div class="info-box amount-box form-control" style="background-color: #f8f9fa; height: 38px; display: flex; align-items: center; font-weight: bold; color: #28a745;">
                                    <span id="lblInvoiceAmout"></span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- RIGHT COLUMN: Approval Details (5 Fields) -->
                    <div class="col-md-6">
                        <div class="section-title mb-4">
                            <i class="fas fa-check-circle"></i>
                            Approval Details
                        </div>

                        <!-- 1. Status -->
                        <div class="form-group row align-items-center mb-3">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;"">Status</label>
                            <div class="col-sm-8">
                                <select id="Approval_Status" class="form-control" style="height: 38px;">
                                    <option value="">Select</option>
                                    <option value="Approve">Approve</option>
                                    <option value="Hold">Hold</option>
                                </select>
                            </div>
                        </div>

                        <!-- 2. Loans Deducted -->
                        <div class="form-group row align-items-center mb-3">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Loans Deducted</label>
                            <div class="col-sm-8">
                                <input type="number" id="Approval_LoansDeducted" class="form-control" style="height: 38px;">
                            </div>
                        </div>

                        <!-- 3. Amount Deducted -->
                        <div class="form-group row align-items-center mb-3">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Amount Deducted</label>
                            <div class="col-sm-8">
                                <input type="number" id="Approval_AmountDeducted" class="form-control" style="height: 38px;">
                            </div>
                        </div>

                        <!-- 4. Payable To Vendor -->
                        <div class="form-group row align-items-center mb-3">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Payable To Vendor</label>
                            <div class="col-sm-8">
                                <input type="text" id="Approval_PaybleToVendor" class="form-control" style="height: 38px;" readonly>
                            </div>
                        </div>

                        <!-- 5. Remark -->
                        <div class="form-group row align-items-start mb-3">
                            <label class="col-sm-4 col-form-label" style="color: #000000; font-size: 15px; font-weight: 500 !important;">Remark</label>
                            <div class="col-sm-8">
                                <textarea id="Approval_remark" rows="4" class="form-control"></textarea>
                            </div>
                        </div>
                    </div>

                </div> <!-- .row end -->
            </div> <!-- .modal-body end -->

            <!-- Footer -->
            <div class="modal-footer d-flex justify-content-center">
               
                <button class="btn btn-success px-4" id="btnStep5" onclick="return InvoiceApproval();">
                    <i class="fas fa-check-circle"></i> Approve Invoice
                </button>
            </div>

        </div>
    </div>
</div>




        
    <div class="modal fade" id="popUpViewLoanDetails">
    <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content loan-modal">

            <!-- Header -->
            

           <div class="col-lg-12 mt-3 mb-2">
    <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
        <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
            <div>
                <h5 class="m-0 font-weight-bold">
                    <i class="fas fa-file-invoice-dollar"></i> Loan Details
            </h5>
                 <small class="text-white-50">

      <label id="invApp_ViewLoan" name="anvApp_ViewLoan" class="mb-0 text-white font-weight-bold""></label>

  </small>

            </div>
            <div style="margin-left: auto;">
        <button type="button" class="close" data-dismiss="modal" onclick="return location.reload();" style="color: red; font-weight: 900; opacity: 1;" data-toggle="tooltip" data-placement="top" title="Close">
    <span>&times;</span>

</button>
            </div>
        </div>
    </div>
</div>

            <!-- Body -->
            
 <div class="col-lg-12 mt-1"> 
      <div class="card shadow-sm border-0">
    
            <div class="modal-body">

                <div class="table-responsive">

                    <table class="table table-hover table-striped mb-0"
                        id="viewloanDetails_table"
                        style="width:100%;">

                    </table>

                </div>

            </div>

            <!-- Footer -->

            <div class="modal-footer">

                <small class="text-muted mr-auto">

                    <i class="fas fa-info-circle"></i>

                    Loan level details for the selected invoice.

                </small>
            </div>

          
               </div>


</div>

        </div>
    </div>
</div>

    <%--<div class="modal fade" id="popUpViewLoanDetails">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <label id="invApp_ViewLoan" name="anvApp_ViewLoan"></label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="return location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="width: 100%; overflow: auto;">
                        <table class="table" id="viewloanDetails_table" style="width: 100%;"></table>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="return location.reload();">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>--%>
 

    <div class="modal fade" id="recinvoice_dverror">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="recinvoice_errmsg"></h5>
                </div>
                <div class="modal-footer align-content-center">
                    <button class="btn btn-primary" type="button" id="addinvoice_btnMessage" onclick="return recinvoice_closepopup();">Okay</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="waitingpanel" tabindex="-1" data-bs-backdrop="static" aria-hidden="true">
        <div class="modal-dialog text-center">
            <img src="../Images/Load.gif" />
            <br />
            <span style="color: #0a0a0a; font-size: 24px; font-weight: bold; font-style: italic;" id="spntext">System is updating details. Please wait</span>
            <span style="color: #0a0a0a; font-size: 48px; font-weight: bold; font-style: italic; animation: animate 1s linear infinite;">&nbsp;. . . .</span>
        </div>
    </div>
</asp:Content>
