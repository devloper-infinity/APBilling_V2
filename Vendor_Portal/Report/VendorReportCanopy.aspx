<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="VendorReportCanopy.aspx.cs" Inherits="Vendor_Portal.Report.VendorReportCanopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --primary-color: #1e3a8a;
            --primary-light: #3b82f6;
            --secondary-color: #f8fafc;
            --text-main: #334155;
            --border-color: #e2e8f0;
        }

        body {
            background-color: #f1f5f9;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }


        .loading {
            display: none;
            position: fixed;
            top: 350px;
            left: 50%;
            margin-top: -96px;
            margin-left: -96px;
            opacity: .85;
            border-radius: 25px;
            width: 192px;
            height: 192px;
            z-index: 99999;
        }

        label:not(.form-check-label):not(.custom-file-label) {
            font-weight: 600 !important;
            color: #475569;
            border: none !important;
            font-size: 13px;
            margin-bottom: 6px;
        }

        div.dt-buttons {
            position: static;
            padding-left: 15px;
            float: left;
        }


        .card {
            border-radius: 12px;
            border: 1px solid var(--border-color);
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.05), 0 2px 4px -1px rgba(0, 0, 0, 0.03);
            margin-bottom: 20px;
            background: #fff;
        }

        .card-body h2 {
            font-size: 30px;
            font-weight: 700;
        }

        .card-body h6 {
            font-size: 15px;
        }

        .main-navigation-tabs {
            background: #ffffff;
            border-radius: 12px;
            padding: 8px;
            display: inline-flex;
            border: 1px solid var(--border-color);
            box-shadow: 0 2px 4px rgba(0,0,0,0.02);
        }

            .main-navigation-tabs .nav-link {
                border-radius: 8px;
                color: #64748b;
                font-weight: 600;
                padding: 10px 24px;
                transition: all .2s ease-in-out;
            }

                .main-navigation-tabs .nav-link.active {
                    background: #1e3a8a;
                    color: #ffffff;
                    box-shadow: 0 2px 4px rgba(30,58,138,0.2);
                }

                .main-navigation-tabs .nav-link:hover:not(.active) {
                    color: #1e3a8a;
                    background: #f1f5f9;
                }

        .custom-report-tabs {
            background: #f1f5f9;
            border-radius: 10px;
            padding: 6px;
            display: inline-flex;
            border: 1px solid var(--border-color);
        }

            .custom-report-tabs .nav-link {
                border-radius: 8px;
                color: #64748b;
                font-weight: 600;
                padding: 8px 24px;
                transition: all .2s ease-in-out;
            }

                .custom-report-tabs .nav-link.active {
                    background: #ffffff;
                    color: #1e3a8a;
                    box-shadow: 0 2px 4px rgba(0,0,0,0.08);
                }

                .custom-report-tabs .nav-link:hover {
                    color: #1e3a8a;
                }



        .dataTables_filter {
            float: right;
            margin-bottom: 15px;
        }

            .dataTables_filter input {
                border: 1px solid #cbd5e1;
                border-radius: 6px;
                padding: 5px 10px;
                outline: none;
            }

        .dataTables_length {
            float: left;
            margin-bottom: 15px;
        }

            .dataTables_length select {
                border: 1px solid #cbd5e1;
                border-radius: 6px;
                padding: 4px 24px 4px 8px !important;
                height: auto !important;
                line-height: normal !important;
                vertical-align: middle !important;
                display: inline-block !important;
            }

            .dataTables_length label {
                display: inline-flex !important;
                align-items: center !important;
                gap: 5px;
                font-weight: 600 !important;
                color: #475569;
            }

        .dt-buttons {
            margin-bottom: 15px;
        }

        .card-header {
            background: #ffffff !important;
            border-bottom: 1px solid var(--border-color);
            border-top-left-radius: 12px !important;
            border-top-right-radius: 12px !important;
            padding: 15px 20px;
        }

            .card-header.bg-primary {
                background: linear-gradient(135deg, #007bff, #0056b3) !important;
                color: #fff !important;
            }

            .card-header h5 {
                margin: 0;
                font-size: 16px;
                font-weight: 600;
            }

        .bg-primary {
            background: linear-gradient(135deg, #1e3a8a, #2563eb) !important;
        }

        .bg-success {
            background: linear-gradient(135deg, #16a34a, #22c55e) !important;
        }

        table.dataTable {
            width: 100% !important;
            table-layout: auto !important;
        }

        .dataTables_scrollHeadInner, .dataTables_scrollHeadInner table, .dataTables_scrollBody table {
            width: 100% !important;
        }

        table.dataTable th, table.dataTable td {
            white-space: nowrap;
            vertical-align: middle;
        }

        #tblSummary thead th {
            border-bottom: 2px solid #1e3a8a !important;
        }

        #tblDetails thead th {
            border-bottom: 2px solid #1e3a8a !important;
        }

        .form-control {
            border-radius: 6px;
            border: 1px solid #cbd5e1;
            font-size: 13px;
            padding: 7px 12px;
            height: calc(1.5em + 0.75rem + 2px);
            color: var(--text-main);
        }

            .form-control:focus {
                border-color: var(--primary-light);
                box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.15);
            }

        .select2-container .select2-selection--single {
            height: 38px !important;
            border: 1px solid #cbd5e1 !important;
            border-radius: 6px !important;
            padding-top: 4px;
        }

        .btn {
            border-radius: 6px;
            font-size: 13px;
            font-weight: 600;
            padding: 8px 16px;
            box-shadow: 0 1px 2px rgba(0,0,0,0.05);
            transition: all 0.2s;
        }

        .btn-info {
            background-color: #0284c7;
            border-color: #0284c7;
            color: #fff;
        }

            .btn-info:hover {
                background-color: #0369a1;
                border-color: #0369a1;
                color: #fff;
            }

        .btn-success {
            background-color: #16a34a;
            border-color: #16a34a;
        }

            .btn-success:hover {
                background-color: #15803d;
                border-color: #15803d;
            }

        .btn-secondary {
            background-color: #64748b;
            border-color: #64748b;
        }

            .btn-secondary:hover {
                background-color: #475569;
                border-color: #475569;
            }

        .btn-light {
            background-color: #f8fafc;
            border: 1px solid #cbd5e1;
            color: #334155;
        }

            .btn-light:hover {
                background-color: #e2e8f0;
                color: #0f172a;
            }

        .content-header {
            padding: 15px 5px;
        }
    </style>

    <style>
        .card {
            border-top: 3px solid #0056b3 !important;
            box-shadow: 0 0 1px rgba(0,0,0,.125), 0 1px 3px rgba(0,0,0,.2);
            margin-bottom: 1rem;
        }


        div.dt-buttons {
            position: static;
            padding-left: 50px;
            float: left;
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
            padding: 8px 10px !important;
            min-width: 30px;
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

        .btn-primary-custom {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            border-radius: 8px;
            padding: 9px 28px;
            font-weight: 600;
            color: white;
            box-shadow: 0 4px 12px rgba(0, 86, 179, 0.3);
            transition: all 0.3s ease;
        }

            .btn-primary-custom:hover {
                background: linear-gradient(135deg, #0056b3, #003366);
                color: white;
                transform: translateY(-1px);
                box-shadow: 0 6px 16px rgba(0, 86, 179, 0.4);
            }

        .filter-container {
            background: #f0f7ff;
            border-radius: 10px;
            padding: 15px 10px;
            border: 1px solid #d0e1fd;
        }
    </style>

    <script>
        $(document).ready(function () {
            if ($.fn.select2) {
                $('.select2').select2({ width: '100%' });
            }

            LoadDashboard("All");

            $("#ddlDashboardCompany").change(function () {
                LoadDashboard($(this).val());
            });

            BindGrid_InvoiceDetails();

            $("#<%= btnExcel.ClientID %>").on("click", function () {
                $("#<%= hdnVendor.ClientID %>").val($("#ddlVendor").val());
                $("#<%= hdnYear.ClientID %>").val($("#ddlYear").val());
                $("#<%= hdnCompany.ClientID %>").val($("#dllcompany").val());

            });
        });

        function BindGrid_InvoiceApproval(company) {
            console.log("Binding invoice approval for: " + company);
        }

        function LoadDashboard(company) {
            BindGrid_InvoiceApproval(company);
        }

        function LoadSummary() {
            var Vendor = $("#ddlVendor").val();
            var Year = $("#ddlYear").val();
            var Company = $("#dllcompany").val();

            $.ajax({
                type: "POST",
                url: "VendorReportCanopy.aspx/GetSummary",
                data: JSON.stringify({ Vendor: Vendor, Year: Year, Company: Company }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);

                    if ($.fn.DataTable.isDataTable('#tblSummary')) {
                        $('#tblSummary').DataTable().clear().destroy();
                        $('#tblSummary').empty(); 
                    }
                    BindSummaryGrid(data);
                },
                error: function (xhr) {
                    // alert(xhr.responseText);
                }
            });
        }

        function LoadDetails() {
            var Vendor = $("#ddlVendor").val();
            var Year = $("#ddlYear").val();
            var Company = $("#dllcompany").val();
            $.ajax({
                type: "POST",
                url: "VendorReportCanopy.aspx/GetDetails",
                data: JSON.stringify({ Vendor: Vendor, Year: Year, Company: Company }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if ($.fn.DataTable.isDataTable('#tblDetails')) {
                        $('#tblDetails').DataTable().clear().destroy();
                        $('#tblDetails').empty();
                    }
                    BindDetailGrid(data);
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    // alert(error);
                }
            });
        }
        var hasLoanLogicData = false;
        function LoadLoanLogic() {
            var Vendor = $("#ddlVendor").val();
            var Year = $("#ddlYear").val();
            var Company = $("#dllcompany").val();
            $.ajax({
                type: "POST",
                url: "VendorReportCanopy.aspx/GetLoadLoanLogic",
                data: JSON.stringify({ Vendor: Vendor, Year: Year, Company: Company }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if (data && data.length > 0) {
                        hasLoanLogicData = true;
                    } else {
                        hasLoanLogicData = false;
                    }
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    hasLoanLogicData = false;
                    // alert(error);
                }
            });
        }


        function BindGrid_InvoiceDetails() {
            $("#divLoader").hide();
            $("#tblInvoice").show();

            $.ajax({
                type: "POST",
                url: "VendorReportCanopy.aspx/GetInvoiceDetails",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);
                    if ($.fn.DataTable.isDataTable("#tblInvoice")) {
                        $("#tblInvoice").DataTable().destroy();
                        $("#tblInvoice").empty();
                    }

                    var columns = [];
                    if (data.length > 0) {
                        $.each(Object.keys(data[0]), function (i, key) {
                            columns.push({ title: key, data: key });
                        });
                    }

                    $("#tblInvoice").DataTable({
                        data: data,
                        columns: columns,
                        responsive: true,
                        scrollX: true,
                        ordering: false,
                        pageLength: 10,
                        dom: '<"custom-table-header"lB>frtip',
                        buttons: [
                            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel mr-1"></i> Excel', className: 'btn btn-success' }
                        ]
                    });
                }
            });
        }
    </script>
    <script type="text/javascript">
        function CheckDataBeforeExport() {
            var summaryRows = $.fn.DataTable.isDataTable('#tblSummary') ? $('#tblSummary').DataTable().rows().count() : 0;
            var detailsRows = $.fn.DataTable.isDataTable('#tblDetails') ? $('#tblDetails').DataTable().rows().count() : 0;

            if (summaryRows === 0 && detailsRows === 0 && !hasLoanLogicData) {
                document.getElementById("Tracking_errmsg").innerText = "No data to export";
                $('#Tracking_dverror').modal('show');
                return false;
            }

            return true;
        }
        function Tracking_closepopup() {
            $('#Tracking_dverror').modal('hide');
            return false;
        }


    </script>
    <script>
        $(document).ready(function () {
            $('#dllcompany').on('change', function () {
                var comp = $(this).val();
                var v = $('#ddlVendor');

                v.empty().append('<option value="">Select Vendor</option>');

                if (comp === 'IPS') {
                    v.append('<option value="Scienna">Scienna</option>');
                    v.append('<option value="Compliance">Compliance</option>');
                    v.append('<option value="Smart Hire">Smart Hire</option>');
                    v.append('<option value="True Resources">True Resources</option>');
                    v.append('<option value="Pacer">Pacer</option>');
                    v.append('<option value="OCR">OCR</option>');
                    v.append('<option value="Magna 5">Magna 5</option>');
                    v.append('<option value="LoanLogics">LoanLogics</option>');
                    v.append('<option value="LauraMac">LauraMac</option>');
                    v.append('<option value="KEB">KEB</option>');
                    v.append('<option value="KCB">KCB</option>');
                    v.append('<option value="BOX">BOX</option>');
                    v.append('<option value="Attorney">Attorney</option>');
                    v.append('<option value="LynnHott">LynnHott</option>');
                    v.append('<option value="LucyBeltran">LucyBeltran</option>');
                    v.append('<option value="CoreyDaise">CoreyDaise</option>');
                }
                else if (comp === 'Canopy') {
                    v.append('<option value="Compliance">Compliance</option>');
                    v.append('<option value="Laminr">Laminr</option>');
                    v.append('<option value="LauraMac">LauraMac</option>');
                    v.append('<option value="LoanLogics">LoanLogics</option>');
                    v.append('<option value="Magna 5">Magna 5</option>');
                    v.append('<option value="SeldenLindeke">SeldenLindeke</option>');
                    v.append('<option value="Stewart_AVM">Stewart_AVM</option>');
                    v.append('<option value="Stewart_CD BPO">Stewart_CD BPO</option>');
                    v.append('<option value="Stewart_IA">Stewart_IA</option>');
                }

                v.trigger('change.select2');
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divLoader" class="loader-overlay" style="display: none;">
        <div class="loader-box">
            <img src="../images/Load_1.gif" />
            <h5>Loading Invoice Summary and Details...</h5>
            <p>Please wait while data is being loaded.</p>
        </div>
    </div>

    <asp:HiddenField ID="hdnVendor" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
        <asp:HiddenField ID="hdnCompany" runat="server" />

    <div class="content-header">

        <!-- Unified Card Container for Tabs and Content -->
        <div class="card shadow-sm border-0">
            <div class="card-header bg-white py-3">
                <!-- Main Top Level Navigation Tabs inside the Card Header -->
                <ul class="nav nav-pills main-navigation-tabs mb-0">
                    <li class="nav-item">
                        <a class="nav-link active" data-toggle="tab" href="#tabInvoiceDetails">
                            <i class="fas fa-file-invoice mr-2"></i>Invoice Details Report
                        </a>
                    </li>
                    <li class="nav-item ml-2">
                        <a class="nav-link" data-toggle="tab" href="#tabReportFilterPanel">
                            <i class="fas fa-filter mr-2"></i>Vendor Reports & Filter Panel
                        </a>
                    </li>
                </ul>
            </div>

            <div class="card-body">
                <div class="tab-content">

                    <div class="tab-pane fade show active" id="tabInvoiceDetails">
                        <div class="card shadow-sm mb-3">
                            <div class="card-header bg-primary text-white d-flex align-items-center py-2">
                                <h5 class="mb-0 text-white">
                                    <i class="fas fa-file-invoice me-2"></i>Invoice Details 
                                 </h5>
                                <button type="button" class="btn btn-light btn-sm fw-semibold ml-auto" onclick="BindGrid_InvoiceDetails();">
                                    <i class="fas fa-sync-alt me-1"></i>
                                </button>
                            </div>
                        </div>

                        <div class="table-responsive">
                            <table id="tblInvoice" class="table table-striped table-hover mb-0" style="width: 100%"></table>
                        </div>
                    </div>

                    <!-- TAB 2: Filter Panel + Summary & Details Reports -->
                    <div class="tab-pane fade" id="tabReportFilterPanel">
                        <!-- Filter Section Card -->
                        <div class="card shadow-sm border-0 mb-4">
                            <div class="card-header bg-primary text-white">
                                <h5 class="mb-0 text-white"><i class="fas fa-filter mr-2"></i>Report Filter Panel</h5>
                            </div>

                            <div class="card-body bg-white">
                                <div class="row align-items-end g-3">

                                                                        <div class="col-md-2 mb-3">
    <label class="font-weight-bold">Company</label>
    <select id="dllcompany" name="dllcompany" class="form-control select2" style="width: 100%;">
        <option value="">Select Company</option>
        <option value="IPS">Infinity</option>
        <option value="Canopy">Canopy</option>

    </select>
</div>



                                    <!-- Vendor -->
                                    <div class="col-md-3 mb-3">
                                        <label class="font-weight-bold">Vendor</label>
                                        <select id="ddlVendor" name="ddlVendor" class="form-control select2" style="width: 100%;">
                                            <option value="">Select Vendor</option>
                                            <option value="Compliance">Compliance</option>
                                            <option value="Laminr">Laminr</option>
                                            <option value="LauraMac">LauraMac</option>
                                            <option value="LoanLogics">LoanLogics</option>
                                            <option value="Magna 5">Magna 5</option>
                                            <option value="Stewart_AVM">Stewart_AVM</option>
                                            <option value="Stewart_CD BPO">Stewart_CD BPO</option>
                                             <option value="Smart Hire">Smart Hire</option>
                                        </select>
                                    </div>

                                    <!-- Year -->
                                    <div class="col-md-2 mb-3">
                                        <label class="font-weight-bold">Year</label>
                                        <select id="ddlYear" name="ddlYear" class="form-control select2" style="width: 100%;">
                                            <option value="">Select Year</option>
                                            <option value="2025">2025</option>
                                            <option value="2026">2026</option>
                                            <option value="2027">2027</option>
                                            <option value="2028">2028</option>
                                            <option value="2029">2029</option>
                                            <option value="2030">2030</option>
                                        </select>
                                    </div>

                                    <!-- Show Button -->
                                    <div class="col-md-2 mb-3">
                                        <button type="button" id="btnShow" class="btn btn-info btn-block w-100" onclick="LoadSummary(); LoadDetails(); LoadLoanLogic();">
                                            <i class="fa fa-search mr-1"></i>Show Report
                                        </button>
                                    </div>

                                    <!-- Export -->
                                    <div class="col-md-2 mb-3">
                                        <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-block w-100" Text="Export Excel" OnClientClick="return CheckDataBeforeExport();" OnClick="btnExcel_Click" />
                                    </div>

                                    <!-- Reset -->
                                    <div class="col-md-2 mb-3">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Summary & Details Sub-Tabs Section -->
                        <div class="card shadow-sm border-0">
                            <div class="card-body">
                                <div class="mb-3">
                                    <ul class="nav nav-pills custom-report-tabs">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#Summary"><i class="fas fa-chart-bar mr-1"></i>Summary</a>
                                        </li>
                                        <li class="nav-item ml-2">
                                            <a class="nav-link" data-toggle="tab" href="#Details"><i class="fas fa-list mr-1"></i>Details</a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="tab-content pt-2">
                                    <div class="tab-pane fade show active" id="Summary">
                                        <div class="table-responsive">
                                            <table id="tblSummary" class="table table-bordered table-hover reportTable" style="width: 100%"></table>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="Details">
                                        <div class="table-responsive">
                                            <table id="tblDetails" class="table table-bordered table-hover reportTable" style="width: 100%"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>

    <div class="modal fade" id="Tracking_dverror" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
            <div class="modal-content" style="border-radius: 12px; border: none;">
                <div class="modal-header bg-light" style="border-top-left-radius: 12px; border-top-right-radius: 12px;">
                    <h6 class="modal-title font-weight-bold text-dark" id="Tracking_errmsg"></h6>
                </div>
                <div class="modal-footer justify-content-center border-0">
                    <button class="btn btn-primary-custom btn-sm px-4" type="button" id="Tracking_btnMessage" onclick="return Tracking_closepopup();">Okay</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
