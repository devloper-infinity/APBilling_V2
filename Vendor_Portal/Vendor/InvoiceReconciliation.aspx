<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="InvoiceReconciliation.aspx.cs" Inherits="Vendor_Portal.Vendor.InvoiceReconciliation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .content-header .callout-info {
            border-left-color: #0056b3 !important;
            background-color: #e6f0fa !important;
            color: #2d7ecf !important;
        }

        .card {
            border-top: 3px solid #0056b3 !important;
            box-shadow: 0 0 1px rgba(0,0,0,.125), 0 1px 3px rgba(0,0,0,.2);
            margin-bottom: 1rem;
        }

        .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
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

        .card-body {
            padding: 0.5rem !important;
        }

        .card-header.p-0.pt-1 {
            margin-bottom: 5px !important;
        }

        .tab-content {
            padding-top: 5px !important;
        }

        .tab-pane {
            margin-top: 0 !important;
        }

        .table thead th {
            background-color: #f1f5f9 !important;
            color: #003366 !important;
            border-bottom: 2px solid #0056b3 !important;
        }

        .card-body,
        .tab-content,
        .tab-pane {
            padding: 0px !important;
            margin: 0px !important;
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

        .col-lg-12 .card {
            max-width: 100% !important;
            width: 100% !important;
        }

        #reconcile_invrec_IPS,
        #invrec_canopy {
            width: 100% !important;
            display: table !important;
        }

        .tab-pane div[style*="overflow: auto"] {
            width: 100% !important;
        }

        #reconcile_invrec_IPS thead, #invrec_canopy thead {
            border-radius: 8px !important;
            overflow: hidden;
        }
            #reconcile_invrec_IPS thead th, #invrec_canopy thead th {
                font-weight: bold !important;
                font-size: 13px;
                padding: 12px 15px !important;
                min-width: 50px;
            }

        #reconcile_invrec_IPS th, #invrec_canopy th {
            background-color: #f4f6f9;
            padding: 12px 15px !important;
            vertical-align: middle;
        }

            #reconcile_invrec_IPS th:first-child, #invrec_canopy th:first-child {
                border-top-left-radius: 8px !important;
                border-bottom-left-radius: 8px !important;
            }

            #reconcile_invrec_IPS th:last-child, #invrec_canopy th:last-child {
                border-top-right-radius: 8px !important;
                border-bottom-right-radius: 8px !important;
            }

 /*===============================
        Page Header
================================*/
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
            CheckLoginID_ForRights()
            BindIPSGridForReconcile();
            BindCanopyGrid();
        });
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var url = window.location.href;

            var panelIPS = document.getElementById('custom-tabs-one-home_infinity');
            var panelCanopy = document.getElementById('custom-tabs-one-profile_canopy');
            var headerText = document.getElementById('cardHeaderText');

            if (url.includes('type=Canopy')) {
                if (headerText) {
                    headerText.innerText = "Canopy Reconcile Invoice";
                }

                if (panelIPS) {
                    panelIPS.classList.remove('show', 'active');
                    panelIPS.style.display = 'none';
                }
                if (panelCanopy) {
                    panelCanopy.classList.add('show', 'active');
                    panelCanopy.style.display = 'block';
                }
            }
            else if (url.includes('type=IPS')) {
                if (headerText) {
                    headerText.innerText = "Infinity IPS Reconcile Invoice";
                }

                if (panelCanopy) {
                    panelCanopy.classList.remove('show', 'active');
                    panelCanopy.style.display = 'none';
                }
                if (panelIPS) {
                    panelIPS.classList.add('show', 'active');
                    panelIPS.style.display = 'block';
                }
            }
        };
</script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>

    <div class="col-lg-15 mt-3 mb-2">
        <div class="card card-custom"  style="margin-bottom: 0px !important; min-height: 60px;">
            <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
                <h5 class="m-0 font-weight-bold">
                    <i class="fas fa-copy mr-1"></i><span id="cardHeaderText">Infinity IPS Reconcile Invoice</span>
                </h5>
            </div>
        </div>
    </div>

    <div class="col-lg-15">


        <div class="card card-tabs">


            <div class="card-body" style="padding: 24px !important;">
                <div class="tab-content" id="custom-tabs-one-tabContent_addinvocie">
                    <div class="tab-pane fade show active" id="custom-tabs-one-home_infinity" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_infinity">
                        <div style="width: 100%; overflow: auto;">
                            <table class="table table-striped table-hover" id="reconcile_invrec_IPS">
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
                    <div class="tab-pane fade" id="custom-tabs-one-profile_canopy" role="tabpanel" aria-labelledby="custom-tabs-one-profile-tab_canopy">
                        <div style="width: 100%; overflow: auto;">
                            <table class="table table-striped table-hover" id="invrec_canopy">
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
<div id="invoiceModal" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.5); z-index:9999; justify-content:center; align-items:center;">
    <div style="background:#fff; width:90%; height:95%; border-radius:8px; display:flex; flex-direction:column; overflow:hidden; box-shadow:0px 5px 15px rgba(0,0,0,0.3);">
        <div style="padding:5px; background:#f1f1f1; text-align:right; border-bottom:1px solid #ddd;">
            <button onclick="closeInvoiceModal()" style="padding:5px 15px; background:#ff4d4d; color:#fff; border:none; border-radius:4px; cursor:pointer; font-weight:bold;">X</button>
        </div>
        <iframe id="invoiceFrame" src="" style="width:100%; height:100%; border:none;"></iframe>
    </div>
</div>


</asp:Content>
