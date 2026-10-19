<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="AddInvoiceKEB.aspx.cs" Inherits="Vendor_Portal.Vendor.AddInvoiceKEB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* --- Loading Overlay --- */
        /* --- Loading Overlay (Fixed Center Overlay) --- */
.loading {
    display: none;
    position: fixed !important;
    top: 50% !important;
    left: 50% !important;
    transform: translate(-50%, -50%) !important;
    -webkit-transform: translate(-50%, -50%) !important;
    -ms-transform: translate(-50%, -50%) !important;
    background-color: rgba(255, 255, 255, 0.95);
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
    border: 1px solid #e3e6f0;
    border-radius: 12px;
    width: 180px;
    height: 180px;
    z-index: 999999 !important;
    text-align: center;
    padding-top: 35px;
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
    </style>
    <script>

        window.onload = function () {

            document.getElementById('RemoteUWKEBInvoice').addEventListener('change', getFileNamestewartinvoice);
            document.getElementById('RemoteUWKEBInvoiceExcel').addEventListener('change', getFileNamestewartexcel);
        }

        const getFileNamestewartinvoice = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWKEB").value = files[0].name;

            const fd = new FormData();

            // add all selected files

            fd.append(event.target.name, file, "file_RemoteUWKEB" + file.name);
            // create the request

            const xhr = new XMLHttpRequest();

            xhr.onload = () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    // we done!
                }
            };
            var url = window.location.href;
            // path to server would be where you'd normally post the form to
            xhr.open('POST', url, true);
            xhr.send(fd);
            document.getElementById("RemoteUWKEBInvoice").innerHTML = file.name;
        }
        const getFileNamestewartexcel = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWexcelKEB").value = files[0].name;

            const fd = new FormData();

            // add all selected files
            fd.append(event.target.name, file, "file_RemoteUWexcelKEB" + file.name);
            // create the request
            const xhr = new XMLHttpRequest();

            xhr.onload = () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    // we done!
                }
            };
            var url = window.location.href;
            // path to server would be where you'd normally post the form to
            xhr.open('POST', url, true);
            xhr.send(fd);
            //document.getElementById("dropzonecanopyexcel").classList.add("dz-max-files-reached");
            //document.getElementById("conentdivcanopyexcel").style.display = '';
            var excelLabel = document.getElementById("RemoteUWInvoiceExcel");
            if (excelLabel) {
                excelLabel.innerHTML = file.name;
            }
            //alert(document.getElementById("filep").value);
        }


        $(document).ready(function () {

            addinvoiceKEB_BindYear();

        });


        document.addEventListener('DOMContentLoaded', function () {

            const invoiceDateInput = document.getElementById('RemoteUWKEB_invoicedate');
            const dueDateInput = document.getElementById('cm_DueDateKEB');

            if (invoiceDateInput && dueDateInput) {

                invoiceDateInput.addEventListener('change', function () {
                    const selectedDateStr = this.value;

                    if (selectedDateStr) {
                        dueDateInput.min = selectedDateStr;

                        const invoiceDate = new Date(selectedDateStr);
                        invoiceDate.setDate(invoiceDate.getDate() + 30);

                        const year = invoiceDate.getFullYear();
                        const month = String(invoiceDate.getMonth() + 1).padStart(2, '0');
                        const day = String(invoiceDate.getDate()).padStart(2, '0');
                        const maxDateStr = `${year}-${month}-${day}`;

                        dueDateInput.max = maxDateStr;

                        if (dueDateInput.value) {
                            if (dueDateInput.value < selectedDateStr || dueDateInput.value > maxDateStr) {
                                dueDateInput.value = '';
                            }
                        }
                    } else {
                        dueDateInput.removeAttribute('min');
                        dueDateInput.removeAttribute('max');
                    }
                });

            }
        });




        function refreshPage() {
            window.location.reload();
        }

        function toggleExcelInput() {
            var checkBox = document.getElementById("invoiceCheck");
            var excelInput = document.getElementById("RemoteUWKEBInvoiceExcel");

            if (checkBox.checked == true) {
                excelInput.disabled = true;
                excelInput.value = "";
            } else {
                excelInput.disabled = false;
            }
        }
    </script>
        <script>

            function validateERPDownload() {
                var selectElement = document.getElementById("RemoteUWKEB");
                var selectedValue = selectElement.value;
                var downloadLink = document.getElementById("keberpDownloadLink");
                var alertMsg = document.getElementById("erpAlertMsg");
                if (!selectedValue || selectedValue === "") {

                    document.getElementById("RemoreUWKEB_errmsg").innerText = "Please select a Vendor option before downloading the ERP format.";
                    $('#RemoreUWKEB_dverror').modal('show');

                    downloadLink.href = "#";
                    return false;
                }
                if (!downloadLink.href || downloadLink.href === "#" || downloadLink.href.endsWith("#")) {
                    document.getElementById("RemoreUWKEB_errmsg").innerText = "File not found for the selected vendor!";
                    $('#RemoreUWKEB_dverror').modal('show');
                    return false;
                }
                return true;
            }


            function changeERPFile(selectObj) {
                var selectedValue = selectObj.value;
                var downloadLink = document.getElementById('keberpDownloadLink');
                var excelInput = document.getElementById("RemoteUWKEBInvoiceExcel");
                if (selectedValue === "Magno" || selectedValue === "Magna") {
                    excelInput.disabled = true;
                    excelInput.value = "";
                } else {
                    excelInput.disabled = false;
                }
                if (selectedValue === "Compliance") {
                    downloadLink.href = "Excel/CE_ERPFormat.xlsx";
                }  
                else if (selectedValue === "LauraMac") {
                    downloadLink.href = "Excel/LM-ERPFormat.xlsx";
                }
                else if (selectedValue === "LoanLogics") {
                    downloadLink.href = "Excel/LoanLogicsERPFormat.xlsx";
                }
                else if (selectedValue === "KCB") {
                    downloadLink.href = "Excel/KCB-ERPFormat.xlsx";
                }
                else if (selectedValue === "KEB") {
                    downloadLink.href = "Excel/KEB-ERPFormat.xlsx";
                }
                else if (selectedValue === "Pacer") {
                    downloadLink.href = "Excel/PACER-ERP-Format.xlsx";
                }
                else if (selectedValue === "Attorney") {
                    downloadLink.href = "Excel/Attorney.xlsx";
                }
                else if (selectedValue === "CoreyDaise") {
                    
                }
                else if (selectedValue === "LucyBeltran") {
                }
                else if (selectedValue === "LynnHott") {
                }
                else if (selectedValue === "TrueResources") {
                }
                else if (selectedValue === "SmartHire") {
                }
            }
        </script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="file_RemoteUWKEB" style="display: none;" />
    <input id="file_RemoteUWexcelKEB" style="display: none;" />

    <!-- Loading Spinner -->
    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" width="48" height="48" />
        <div style="font-size: 13px; font-weight: 600; color: #495057; margin-top: 10px;">One moment, please...</div>
    </div>

    <div class="container-fluid pt-3">
        <!-- Input Form Card (5 Fields per Column Layout) -->
        <div class="card card-custom">


 <div class="card-header card-header-custom d-flex justify-content-between align-items-center">
     <h5 class="m-0 font-weight-bold"><i class="fas fa-edit mr-1"></i>Add IPS Invoice Details</h5>
                <div class="col-sm-9 text-right">
                      <div class="form-check form-check-inline">
                    <a id="keberpDownloadLink" href="#" class="text-white font-weight-bold mr-4" style="text-decoration: underline; font-size: 13px;" download="" onclick="return validateERPDownload();">
                        <i class="fas fa-download mr-1"></i>ERP Sample Format
                </a>
                           <label class="form-check-label mr-3 text-white font-weight-bold" for="invoiceCheck" style="font-size: 14px;">Invoice : </label>
 <input class="form-check-input" type="checkbox" id="invoiceCheck" value="invoice" onclick="toggleExcelInput()">
                </div>
                    </div>
                <button type="button" class="btn btn-link p-0 ml-3 text-white" onclick="refreshPage()" style="font-size: 14px; text-decoration: none;" data-toggle="tooltip" data-placement="bottom" title="Refresh Page">
                    <i class="fas fa-sync-alt"></i>
                </button>
            </div>




            <div class="card-body px-4 py-3">
                <div class="row">
                    <!-- Column 1 (5 Fields) -->
                    <div class="col-md-6">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Month:</b></label>
                            <div class="col-sm-8">
                                <select id="addinvoiceKEB_month" name="addinvoiceKEB_month" class="form-control">
                                    <option value="">Select Month</option>
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
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice No: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWKEB_invoiceaNo_canopy" name="RemoteUWKEB_invoiceaNo_canopy" class="form-control" placeholder="Enter Invoice No" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Date: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="date" id="RemoteUWKEB_invoicedate" name="RemoteUWKEB_invoicedate" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>No Of Hours/Loans: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWKEB_NoOfLoans_canopy" name="RemoteUWKEB_NoOfLoans_canopy" class="form-control" placeholder="Enter hours or loans"  />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice PDF: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="file" id="RemoteUWKEBInvoice" name="RemoteUWInvoice" class="form-control" accept=".pdf" />
                            </div>
                        </div>

                    </div>

                    <!-- Column 2 (5 Fields) -->
                    <div class="col-md-6">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Year:</b></label>
                            <div class="col-sm-8">
                                <select id="addinvoiceKEB_year" name="addinvoiceKEB_year" class="form-control">
                                    <option value="">Select Year</option>
                                </select>
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Amount ($): <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWKEB_invoiceamount_canopy" name="RemoteUWKEB_invoiceamount_canopy" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Due Date: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="date" id="cm_DueDateKEB" name="cm_DueDateKEB" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Vendor: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <select id="RemoteUWKEB" name="RemoteUWKEB" class="form-control" onchange="changeERPFile(this)">
                                    <option value="">Select</option>
                                    <option value="CoreyDaise">Corey Daise</option>
                                    <option value="LucyBeltran">Lucy Beltran</option>
                                    <option value="LynnHott">Lynn Hott</option>
                                    <option value="TrueResources">True Resources</option>
                                    <option value="SmartHire">Smart Hire</option>
                                    <option value="Compliance">Compliance</option>
                                    <option value="LauraMac">Laura Mac</option>
                                    <option value="LoanLogics">Loan Logics</option>
                                    <option value="KCB">KCB</option>
                                    <option value="KEB">KEB</option>
                                    <option value="Pacer">Pacer</option>
                                    <option value="Magna">Magna 5</option>
                                                                        <option value="Attorney">Attorney</option>





                                </select>
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Excel File:</b></label>
                            <div class="col-sm-8">
                                <input type="file" id="RemoteUWKEBInvoiceExcel" name="RemoteUWInvoiceExcel" class="form-control" accept=".xlsx, .xls" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Hidden Date Fields (Maintained for logic) -->
                <div class="row" style="display: none;">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label><b>From Date:</b></label>
                            <input type="date" id="RWKEBFromDate" name="RWKEBFromDate" class="form-control" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label><b>To Date:</b></label>
                            <input type="date" id="RWKEBToDate" name="RWKEBToDate" class="form-control" />
                        </div>
                    </div>
                </div>

                <!-- Import Button Row -->
                <div class="row mt-2">
                    <div class="col-12 text-right">
                        <button class="btn btn-primary-custom px-5" type="button" id="cm_btnImportKEB" onclick="return RemoteUWKEB_submit();">
                            <i class="fas fa-upload mr-1"></i>Import
                       
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Data Grid Table Card -->
        <div class="card card-custom">
            <div class="card-body p-3" style="display: none;">
                <div class="table-responsive" >
                    <table class="table table-striped table-bordered" id="table_RemoteUWKEB" style="width: 100%;">
                        <thead>
                            <tr>
                                <th class="text-center">Sr. #</th>
                                <th class="text-center">Month</th>
                                <th class="text-center">Year</th>
                                <th class="text-center">Date</th>
                                <th class="text-center">Start Time</th>
                                <th class="text-center">End Time</th>
                                <th class="text-center">Break Time</th>
                                <th class="text-center">Total Time</th>
                                <th class="text-center">Client No</th>
                                <th class="text-center">Deal No</th>
                                <th class="text-center">Task Name</th>
                                <th class="text-center">Target</th>
                                <th class="text-center">Loans Reviewed</th>
                                <th class="text-center">Loans Correction</th>
                                <th class="text-center">No Errors Files</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Popup -->
    <div class="modal fade" id="RemoreUWKEB_dverror" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
            <div class="modal-content" style="border-radius: 12px; border: none;">
                <div class="modal-header bg-light" style="border-top-left-radius: 12px; border-top-right-radius: 12px;">
                    <h6 class="modal-title font-weight-bold text-dark" id="RemoreUWKEB_errmsg"></h6>
                </div>
                <div class="modal-footer justify-content-center border-0">
                    <button class="btn btn-primary-custom btn-sm px-4" type="button" id="RemoreUWKEB_btnMessage" onclick="return RemoreUWKEB_closepopup();">Okay</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
