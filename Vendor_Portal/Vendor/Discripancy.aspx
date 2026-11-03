<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="Discripancy.aspx.cs" Inherits="Vendor_Portal.Vendor.Discripancy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* --- Loading Overlay --- */
        .loading {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: rgba(255, 255, 255, 0.95);
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
            border: 1px solid #e3e6f0;
            border-radius: 12px;
            width: 180px;
            height: 180px;
            z-index: 99999;
            text-align: center;
            padding-top: 30px;
        }

        /* --- DataTable & Label Customizations --- */
        .dataTables_length, .dataTables_info {
            float: left !important;
        }

        label:not(.form-check-label):not(.custom-file-label) {
            font-size: 13px;
            font-weight: 600 !important;
            border: none !important;
            color: #495057;
        }

        div.dt-buttons {
            position: static;
            padding-left: 15px;
            float: left;
        }

        .buttons-excel, .buttons-html5 {
            color: #fff;
            box-shadow: none;
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: 0;
            font-weight: 600;
            border-radius: 6px;
            padding: 6px 15px;
            margin: 0px 10px;
            transition: all 0.3s ease;
        }

            .buttons-excel:hover, .buttons-html5:hover {
                background: linear-gradient(135deg, #0056b3, #004085);
                color: #fff;
            }

        /* --- Table Enhancements --- */
        .table.dataTable th {
            background: linear-gradient(135deg, #f8f9fa, #e9ecef) !important;
            color: #212529;
            border-bottom: 2px solid #dee2e6;
            font-weight: 700;
        }

        .table.dataTable tr td {
            background: none !important;
            background-color: #fff !important;
            vertical-align: middle;
        }

        #table_RemoteUWLaminr th,
        #table_RemoteUWLaminr td {
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
        }

        /* --- Form Card Styling --- */
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

        .form-control {
            border-radius: 6px;
            border: 1px solid #d1d3e2;
            padding: 6px 12px;
            height: calc(1.5em + .75rem + 2px);
            width: 100%;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }

        input[type="file"].form-control {
            line-height: 17px;
            padding: 4px 12px;
        }

        .form-control:focus {
            border-color: #80bdff;
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
            background-color: #fff;
        }

        .btn-primary-custom {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            border-radius: 6px;
            padding: 8px 25px;
            font-weight: 600;
            color: white;
            transition: all 0.3s ease;
        }

            .btn-primary-custom:hover {
                background: linear-gradient(135deg, #0056b3, #004085);
                color: white;
                box-shadow: 0 4px 12px rgba(0, 123, 255, 0.3);
            }

        .form-group-item {
            margin-bottom: 12px;
        }

        label, .col-form-label {
            font-size: 13px !important;
            font-weight: 700 !important; /* Force bold styling */
            color: #000000 !important; /* Force solid black color */
            border: none !important;
        }

        #discrepancyDetailModal .modal-dialog {
            max-width: 1100px;
        }

        #discrepancyDetailModal .modal-content {
            border: 0;
            border-radius: 14px;
            overflow: hidden;
            box-shadow: 0 18px 55px rgba(26, 47, 78, .22);
        }

        #discrepancyDetailModal .modal-header {
            position: relative;
            display: flex;
            align-items: center;
            gap: 18px;
            color: #fff;
            background: linear-gradient(135deg, #123f68, #1769aa);
            border-bottom: 0;
            padding: 14px 48px 14px 24px;
        }

        #discrepancyDetailModal .discrepancy-modal-heading {
            flex: 0 0 180px;
        }

        #discrepancyDetailModal .modal-title {
            color: #fff !important;
            font-size: 18px;
            letter-spacing: 0;
        }

        #discrepancyDetailModal #discrepancyDetailSubtitle {
            display: inline-block;
            margin-top: 2px;
            color: rgba(255, 255, 255, .72) !important;
            font-size: 12px;
        }

        #discrepancyDetailModal .discrepancy-header-info {
            flex: 1 1 auto;
            min-width: 0;
            margin: 0;
            padding: 0;
            background: transparent;
            border: 0;
        }

            #discrepancyDetailModal .discrepancy-header-info .row {
                display: grid;
                grid-template-columns: repeat(3, minmax(0, 1fr));
                gap: 8px;
                margin: 0;
            }

            #discrepancyDetailModal .discrepancy-header-info .discrepancy-info-item {
                width: auto;
                max-width: none;
                min-height: 54px;
                padding: 8px 12px;
                background: rgba(255, 255, 255, .1);
                border: 1px solid rgba(255, 255, 255, .18);
                border-radius: 9px;
            }

            #discrepancyDetailModal .discrepancy-header-info .discrepancy-info-label {
                margin-bottom: 3px;
                color: rgba(255, 255, 255, .72);
                font-size: 10px;
                letter-spacing: .07em;
            }

            #discrepancyDetailModal .discrepancy-header-info .discrepancy-info-value {
                color: #fff;
                font-size: 14px;
                font-weight: 600;
            }

        #discrepancyDetailModal .modal-header .close {
            position: absolute;
            top: 12px;
            right: 16px;
            color: #fff;
            opacity: .85;
            text-shadow: none;
        }

        #discrepancyDetailModal .modal-body {
            padding: 24px;
        }

        .discrepancy-info {
            background: #f5f8fc;
            border: 1px solid #e5edf5;
            border-radius: 10px;
            padding: 16px 18px;
            margin-bottom: 20px;
        }

        .discrepancy-info-label {
            display: block;
            color: #718096;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: .06em;
            margin-bottom: 4px;
            text-transform: uppercase;
        }

        .discrepancy-info-value {
            display: block;
            color: #24364b;
            font-size: 15px;
            font-weight: 600;
            overflow-wrap: anywhere;
        }

        .discrepancy-count-link {
            min-width: 42px;
            border: 0;
            border-radius: 14px;
            background: #eaf3ff;
            color: #1769aa;
            font-weight: 700;
            padding: 3px 10px;
            cursor: pointer;
        }

            .discrepancy-count-link:hover,
            .discrepancy-count-link:focus {
                background: #1769aa;
                color: #fff;
                outline: none;
            }

        #discrepancyDetailTable th,
        #discrepancyDetailTable td {
            white-space: nowrap;
            vertical-align: middle;
        }

        #discrepancyDetailModal .dataTables_scrollHeadInner table,
        #discrepancyDetailModal .dataTables_scrollBody table {
            width: 1250px !important;
            min-width: 1250px !important;
        }

        #discrepancyDetailModal .dataTables_scrollBody {
            overflow-x: auto !important;
            overflow-y: auto !important;
        }

        #discrepancyDetailTable .discrepancy-user-remark {
            width: 280px;
            min-width: 280px;
            height: 48px;
            min-height: 48px;
            resize: vertical;
        }

        @media (max-width: 991.98px) {
            #discrepancyDetailModal .modal-header {
                display: block;
                padding: 16px 44px 16px 16px;
            }

            #discrepancyDetailModal .discrepancy-modal-heading {
                margin-bottom: 12px;
            }
        }

        @media (max-width: 575.98px) {
            #discrepancyDetailModal .modal-body {
                padding: 16px;
            }

            #discrepancyDetailModal .discrepancy-header-info .row {
                grid-template-columns: 1fr;
            }

            .discrepancy-info {
                padding: 13px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" width="48" height="48" />
        <div style="font-size: 13px; font-weight: 600; color: #495057; margin-top: 10px;">One moment, please...</div>
    </div>

    <div class="container-fluid pt-3">
        <!-- Input Form Card -->
        <div class="card card-custom">
            <div class="card-header card-header-custom d-flex justify-content-between align-items-center">
                <h5 class="m-0 font-weight-bold"><i class="fas fa-edit mr-1"></i>Discripancy</h5>
            </div>
            <div class="card-body px-4 py-3">
                <%-- <div class="row">
                    <!-- Column 1 -->
                    <div class="col-md-4">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Month: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <select id="disc_month" name="disc_month" class="form-control" autofocus="autofocus">
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
                            </div>

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Year: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <select id="disc_year" name="disc_year" class="form-control" autofocus="autofocus"></select>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <button class="btn-primary-custom" type="button" id="disc_show" onclick="return showInvoice();">
                            <i class="fas fa-file-invoice mr-1"></i>Get Invoice Details
                        </button>
                    </div>
                </div>
                --%>
                <!-- Data Table Card Container -->
                <div class="card card-custom mt-4" id="laminarCard" style="display: none;">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <table class="table table-bordered table-striped dataTable w-100 m-0" id="table_RemoteUWLaminr">
                                <thead>
                                    <tr>
                                        <th style="white-space: nowrap; text-align: center;">Sr. #</th>
                                        <th style="white-space: nowrap; text-align: center;">Invoice ID</th>
                                        <th style="white-space: nowrap; text-align: center;">TableName</th>
                                        <th style="white-space: nowrap; text-align: center;">Invoice Type</th>
                                        <th style="white-space: nowrap; text-align: center;">Invoice No</th>
                                        <th style="white-space: nowrap; text-align: center;">Invoice Date</th>
                                        <th style="white-space: nowrap; text-align: center;">Bill To</th>
                                        <th style="white-space: nowrap; text-align: center;">Total Records</th>
                                        <th style="white-space: nowrap; text-align: center;">Match With System</th>
                                        <th style="white-space: nowrap; text-align: center;">Not Match With System</th>
                                        <th style="white-space: nowrap; text-align: center;">Duplicate</th>
                                        <th style="white-space: nowrap; text-align: center;">Pending Client Billing</th>
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

    <div class="modal fade" id="discrepancyDetailModal" tabindex="-1" role="dialog" aria-labelledby="discrepancyDetailTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="discrepancy-modal-heading">
                        <h5 class="modal-title font-weight-bold mb-1" id="discrepancyDetailTitle">Invoice details</h5>
                        <small id="discrepancyDetailSubtitle" class="text-white-50">Selected invoice summary</small>
                    </div>
                    <div class="discrepancy-info discrepancy-header-info">
                        <div class="row">
                            <div class="discrepancy-info-item">
                                <span class="discrepancy-info-label">Invoice Type</span>
                                <span class="discrepancy-info-value" id="detailInvoiceType">—</span>
                            </div>
                            <div class="discrepancy-info-item">
                                <span class="discrepancy-info-label">Invoice No</span>
                                <span class="discrepancy-info-value" id="detailInvoiceNo">—</span>
                            </div>
                            <div class="discrepancy-info-item">
                                <span class="discrepancy-info-label">Billed To</span>
                                <span class="discrepancy-info-value" id="detailBilledTo">—</span>
                            </div>
                        </div>
                    </div>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover w-100" id="discrepancyDetailTable">
                            <thead>
                                <tr></tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer py-2">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
