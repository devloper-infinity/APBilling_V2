<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="InvoiceReconciliationDetails.aspx.cs" Inherits="Vendor_Portal.Vendor.InvoiceReconciliationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<style>
.page-card {
    background: #fff;
    border-radius: 15px;
    box-shadow: 0 8px 25px rgba(0,0,0,.08);
    margin-bottom: 20px;
    overflow: hidden;
}

.page-header {
    background: linear-gradient(90deg, #67b8ff, #2d8cf0);
    color: #fff;
    padding: 18px 25px;
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

.btn-reconcile {
    background: linear-gradient(90deg, #34c759, #28a745);
    border: none;
    color: #fff;
    padding: 12px 35px;
    border-radius: 30px;
    font-size: 16px;
    font-weight: 600;
    transition: .3s;
}

.btn-reconcile:hover {
    transform: translateY(-2px);
    box-shadow: 0 5px 15px rgba(0,0,0,.20);
}

.buttons-excel {
    background: linear-gradient(90deg, #69b8ff, #2d8cf0) !important;
    color: #fff !important;
    border: none !important;
    border-radius: 8px !important;
    padding: 7px 15px !important;
    font-weight: 600;
}

.badge-success {
    background: #28a745;
    padding: 6px 12px;
    border-radius: 20px;
    font-weight: 500;
}

input[type=text] {
    border: 1px solid #d9e3ef;
    border-radius: 8px;
    padding: 6px 10px;
    transition: .3s;
}

input[type=text]:focus {
    border-color: #2d8cf0;
    box-shadow: 0 0 8px rgba(45,140,240,.20);
    outline: none;
}

input[type=checkbox] {
    width: 18px;
    height: 18px;
    cursor: pointer;
    accent-color: #2d8cf0;
}

/* --- Loaders --- */
.loading {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(255,255,255,.75);
    display: none;
    justify-content: center;
    align-items: center;
    z-index: 999999;
}

.loading img {
    width: 80px;
}

.table-loader {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(255,255,255,.92);
    display: none;
    justify-content: center;
    align-items: center;
    z-index: 999;
    border-radius: 12px;
}

.loader-box {
    text-align: center;
}

.loader-box img {
    width: 70px;
}

.content-header .callout-info {
    border-left-color: #0056b3 !important;
    background-color: #e6f0fa !important;
    color: #2d7ecf !important;
}

.nav-tabs .nav-item.show .nav-link, 
.nav-tabs .nav-link.active {
    color: #fff !important;
    background-color: #0056b3 !important;
    border-color: #0056b3 #0056b3 #fff !important;
}

.nav-tabs .nav-link {
    color: #2d7ecf !important;
}

.nav-tabs .nav-link:hover {
    background-color: #f0f4f8 !important;
    border-color: #dee2e6 #dee2e6 #fff !important;
}

.tab-content,
.tab-pane {
    padding: 0px;
    margin: 0px;
}

.table thead th {
    background-color: #f1f5f9 !important;
    color: #003366 !important;
    border-bottom: 2px solid #0056b3 !important;
}
    .dt-buttons,
    .dt-button,
    button.dt-button {
        width: auto !important;
        display: inline-block !important;
        padding: 6px 12px !important;
        margin-top: -6px !important;
        margin-bottom: 0px !important;
        padding: 3px 8px !important;
        font-size: 10px !important;
        height: auto !important;
    }
.dataTables_length {
    display: inline-block !important;
    float: left !important;
    margin-right: 20px !important; 
    margin-bottom: 10px !important;
}

.dataTables_filter {
    display: inline-block !important;
    float: right !important;
    margin-bottom: 10px !important;
}
.dataTables_wrapper::after {
    content: "";
    clear: both;
    display: table;
}
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

</style>


    <script>

        $(document).ready(function () {

            const urlParams = new URLSearchParams(window.location.search);
            const Type = urlParams.get('Type');
            const InvNo = urlParams.get('InvoiceID');
            const Month = urlParams.get('Month');
            const Year = urlParams.get('Year');

            BindCanopyGeneralisedGrid(Type, InvNo, Month, Year);
        });


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>

    <div id="divLoader" class="table-loader">

    <div class="loader-box">

        <img src="../Images/Load.gif" />

        <h5>Loading Records...</h5>

        <small>Please wait while fetching data.</small>

    </div>

</div>

    <div class="col-lg-15 mt-3 mb-0">
        <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
            <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
                <div>
                    <h5 class="m-0 font-weight-bold">
                        <i class="fas fa-copy mr-1"></i>Reconciliation Details
                </h5>
                    <small>Review, compare and reconcile invoice records.
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

 <div class="col-lg-15 mt-1"> 
    <div class="card shadow-sm border-0">
        <div class="card-body p-4">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <button class="btn btn-reconcile" type="button" id="ird_btnCommonReconcile" onclick="ird_btnSubmitCommonReconcile();">Reconcile</button>
                </div>
                <div class="table-responsive">
                    <table id="table_Commonreconcile"
                        class="table table-hover table-striped"
                        style="width: 100%">
                    </table>
                </div>
            </div>
        </div>

           
           <div id="dvCompliance" style="display: none;">
                    <div style="width: 100%; overflow: auto;">
                        <table class="table">
                            <tr>
                                <td>
                                    <button class="btn btn-primary" type="button" id="ird_btnReconcile" onclick="ird_btnSubmitReconcile();">Reconcile</button>
                                </td>
                            </tr>
                        </table>
                        <table class="table" id="table_ips_rc_Compliance" style="width: 100%;">  
                        </table>
                    </div>
                </div>

                <div id="dvstewart" style="display: none;">
                    <div style="width: 100%; overflow: auto;">
                        <table class="table" id="invrecdetails_canopy_stewart" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">
                                        <input type="checkbox" id="chkall" onclick="return getallSelectdeselect(this);" />
                                    </th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">BillingId</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">InvoiceId</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Month</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Year</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Loan #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Complete Date</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Fee</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Client Billing Cost</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center; display: none;">Fee</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Dispute</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Remark</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">System Remark</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">User Remark</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                            <tfoot>
                                <tr>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>

                <div id="dvLauraMac" style="display: none;">
                    <div style="width: 100%; overflow: auto;">
                        <table class="table" id="invrecdetails_canopy_LauraMac" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">
                                        <input type="checkbox" id="chkallLMac" onclick="return getallSelectdeselectLMac(this);" />
                                    </th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">LauraMacID</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">InvoiceId</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Month</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Year</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Loan #</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Activated Date</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">Complete Date</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Transaction ID</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">System Remark</th>
                                    <th class="sort border-top ps-3" style="text-wrap: nowrap;">User Remark</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                            <tfoot>
                                <tr>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                    <td style="text-align: center;"></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
      


    <div class="modal fade" id="addinvoice_dverror">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title" id="addinvoice_errmsg"></h6>
                </div>
                <div class="modal-footer align-content-center">
                    <button class="btn btn-primary" type="button" id="addinvoice_btnMessage" onclick="return addinvoice_closepopup();">Okay</button>
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
            <span style="color: #fff; font-size: 24px; font-weight: bold; font-style: italic;" id="spntext">Reconciliation is in process. Please wait</span>
            <span style="color: #fff; font-size: 48px; font-weight: bold; font-style: italic; animation: animate 1s linear infinite;">&nbsp;. . . .</span>
        </div>
    </div>
</asp:Content>
