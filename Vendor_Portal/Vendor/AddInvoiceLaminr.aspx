<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="AddInvoiceLaminr.aspx.cs" Inherits="Vendor_Portal.Vendor.AddInvoiceLaminr" %>

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
    </style>
    <script>

        window.onload = function () {

            document.getElementById('RemoteUWLaminrInvoice').addEventListener('change', getFileNamestewartinvoice);
            document.getElementById('RemoteUWLaminrInvoiceExcel').addEventListener('change', getFileNamestewartexcel);
        }

        const getFileNamestewartinvoice = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWLLaminr").value = files[0].name;

            const fd = new FormData();

            // add all selected files

            fd.append(event.target.name, file, "file_RemoteUWLLaminr" + file.name);
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
            document.getElementById("RemoteUWLLaminrInvoice").innerHTML = file.name;
        }
        const getFileNamestewartexcel = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWexcelLLaminr").value = files[0].name;

            const fd = new FormData();

            // add all selected files
            fd.append(event.target.name, file, "file_RemoteUWexcelLLaminr" + file.name);
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
            document.getElementById("RemoteUWInvoiceExcel").innerHTML = file.name;
            document.getElementById("RemoteUWInvoice").innerHTML = file.name;
            //alert(document.getElementById("filep").value);
        }


        $(document).ready(function () {
            addinvoiceLaminr_BindYear();
        });


    </script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {

            const invoiceDateInput = document.getElementById('RemoteUWLaminr_invoicedate');
            const dueDateInput = document.getElementById('cm_DueDateLaminr');

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

        function toggleExcelInput() {
            var checkBox = document.getElementById("invoiceCheck");
            var excelInput = document.getElementById("RemoteUWLaminrInvoiceExcel");

            if (checkBox.checked == true) {
                excelInput.disabled = true;
                excelInput.value = "";
            } else {
                excelInput.disabled = false;
            }
        }

        //$(document).ready(function () {
        //    $('#RemoteUWLaminr').change(function () {
        //        var selectedValue = $(this).val();
        //        $('#laminarCard').toggle($(this).val() === 'Laminr');
        //        $('#ComplianceEaseCard').toggle(selectedValue === 'Compliance');
        //        $('#LoanLogicCard').toggle(selectedValue === 'LoanLogics');

        //    });
        //});


        function refreshPage() {
            window.location.reload();
        }
</script>
    <script>

        function validateERPDownload() {
            var selectElement = document.getElementById("RemoteUWLaminr");
            var selectedValue = selectElement.value;
            var downloadLink = document.getElementById("erpDownloadLink");
            var alertMsg = document.getElementById("erpAlertMsg");
            if (!selectedValue || selectedValue === "") {

                document.getElementById("RemoreUWLaminr_errmsg").innerText = "Please select a Vendor option before downloading the ERP format.";
                $('#RemoreUWLaminr_dverror').modal('show');

                downloadLink.href = "#";
                return false;
            }  
            return true; 
        }


        function changeERPFile(selectObj) {
            var selectedValue = selectObj.value;
            var downloadLink = document.getElementById('erpDownloadLink');

            var excelInputcanopy = document.getElementById("RemoteUWLaminrInvoiceExcel");

            if (selectedValue === "Box" || selectedValue === "BOX" || selectedValue === "Magna") {
                excelInputcanopy.disabled = true;
                exceexcelInputcanopylInput.value = "";
            } else {
                excelInputcanopy.disabled = false;
            }
         
            if (selectedValue === "Compliance") {
                downloadLink.href = "Excel/CE_ERPFormat.xlsx";
            }
            else if (selectedValue === "Laminr") {
                downloadLink.href = "Excel/Laminar-ERP.xlsx";
            }
            else if (selectedValue === "LauraMac") {
                downloadLink.href = "Excel/LM-ERPFormat.xlsx";
            }
            else if (selectedValue === "LoanLogics") {
                downloadLink.href = "Excel/LoanLogicsERPFormat.xlsx";
            }
            else if (selectedValue === "Stewart_CD") {
                downloadLink.href = "Excel/StewartIAERP-Format.xlsx";
            }
            else if (selectedValue === "Stewart_AVM") {
                downloadLink.href = "Excel/StewartIAERP-Format.xlsx";
            }
            else {
                downloadLink.href = "Excel/CE_ERPFormat.xlsx";
            }
        }
    </script>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="file_RemoteUWLLaminr" style="display: none;" />
    <input id="file_RemoteUWexcelLLaminr" style="display: none;" />
      <div class="loading" id="load1">
      <img src="../images/Load_1.gif" width="48" height="48" />
      <div style="font-size: 13px; font-weight: 600; color: #495057; margin-top: 10px;">One moment, please...</div>
  </div>



    <div class="container-fluid pt-3">
        <!-- Input Form Card -->
        <div class="card card-custom">
            <div class="card-header card-header-custom d-flex justify-content-between align-items-center">
                <h5 class="m-0 font-weight-bold"><i class="fas fa-edit mr-1"></i>Add Canopy Invoice Details</h5>
                <div class="col-sm-9 text-right">
                    <div class="form-check form-check-inline">
                        <a id="erpDownloadLink" href="Excel/CE_ERPFormat.xlsx" class="text-white font-weight-bold mr-4" style="text-decoration: underline; font-size: 13px;" download="" onclick="return validateERPDownload();">
                            <i class="fas fa-download mr-1"></i>ERP Sample Format
                          </a>
                        <label class="form-check-label mr-2 text-white font-weight-bold" for="invoiceCheck">Invoice : </label>
                        <input class="form-check-input" type="checkbox" id="invoiceCheck" value="invoice" onclick="toggleExcelInput()">
                    </div>
                </div>
                <button type="button" class="btn btn-link p-0 ml-3 text-white" onclick="refreshPage()" style="font-size: 14px; text-decoration: none;" data-toggle="tooltip" data-placement="bottom" title="Refresh Page">
                    <i class="fas fa-sync-alt"></i>
                </button>


            </div>
            <div class="card-body px-4 py-3">
                <div class="row">
                    <!-- Column 1 -->
                    <div class="col-md-6">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Month: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <select id="addinvoiceLaminr_month" name="addinvoiceLaminr_month" class="form-control" autofocus="autofocus">
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

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice No: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWLaminr_invoiceaNo_canopy" name="RemoteUWLaminr_invoiceaNo_canopy" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Date: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="date" id="RemoteUWLaminr_invoicedate" name="RemoteUWLaminr_invoicedate" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>No Of Loans: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWLaminr_NoOfLoans_canopy" name="RemoteUWLaminr_NoOfLoans_canopy" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice (PDF): <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="file" id="RemoteUWLaminrInvoice" name="RemoteUWInvoice" class="form-control" accept=".pdf" />
                            </div>
                        </div>
                    </div>

                    <!-- Column 2 -->
                    <div class="col-md-6">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Year: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <select id="addinvoiceLaminr_year" name="addinvoiceLaminr_year" class="form-control">
                                    <option value="">Select</option>
                                </select>
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Amount($): <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWLaminr_invoiceamount_canopy" name="RemoteUWLaminr_invoiceamount_canopy" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Due Date: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <input type="date" id="cm_DueDateLaminr" name="cm_DueDateLaminr" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Vendor: <span class="text-danger">*</span></b></label>
                            <div class="col-sm-8">
                                <select id="RemoteUWLaminr" name="RemoteUWLaminr" class="form-control" onchange="changeERPFile(this)">
                                    <option value="">Select</option>
                                    <option value="Compliance">Compliance</option>
                                    <option value="Laminr">Laminr</option>
                                    <option value="LauraMac">Laura Mac</option>
                                    <option value="LoanLogics">Loan Logics</option>
                                    <option value="Stewart_CD">Stewart_CD BPO</option>
                                    <option value="Stewart_AVM">Stewart_AVM</option>
                                    <option value="Magna">Magna 5</option>
                                    <option value="BOX">BOX</option>
                                    
                                </select>
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Excel Data:</b></label>
                            <div class="col-sm-8">
                                <input type="file" id="RemoteUWLaminrInvoiceExcel" name="RemoteUWInvoiceExcel" class="form-control" accept=".xlsx, .xls" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Action Button Row -->
                <div class="row mt-3">
                    <div class="col-12 text-right">
                        <button class="btn-primary-custom" type="button" id="cm_btnImportLaminr" onclick="return RemoteUWLaminr_submit();">
                            <i class="fas fa-file-import mr-1"></i>Import
                   
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Data Table Card Container -->
        <div class="card card-custom mt-4" id="laminarCard" style="display: none;">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <table class="table table-bordered table-striped dataTable w-100 m-0" id="table_RemoteUWLaminr">
                        <thead>
                            <tr>
                                <th style="white-space: nowrap; text-align: center;">Sr. #</th>
                                <th style="white-space: nowrap; text-align: center;">Month</th>
                                <th style="white-space: nowrap; text-align: center;">Year</th>
                                <th style="white-space: nowrap; text-align: center;">BSmrtID</th>
                                <th style="white-space: nowrap; text-align: center;">TimeStamp</th>
                                <th style="white-space: nowrap; text-align: center;">Loan Number</th>
                                <th style="white-space: nowrap; text-align: center;">INVESTOR</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

        <div class="card card-custom mt-4" id="ComplianceEaseCard" style="display: none;">
        <div class="card-body p-0">
            <div class="table-responsive">
                <table class="table table-bordered table-striped dataTable w-100 m-0" id="table_RemoteUWComplianceEaseCard">
                    <thead>
                        <tr>
                            <th style="white-space: nowrap; text-align: center;">Sr. #</th>
                            <th style="white-space: nowrap; text-align: center;">Month</th>
                            <th style="white-space: nowrap; text-align: center;">Year</th>
                        
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>

        <div class="card card-custom mt-4" id="LoanLogicCard" style="display: none;">
    <div class="card-body p-0">
        <div class="table-responsive">
            <table class="table table-bordered table-striped dataTable w-100 m-0" id="table_RemoteUWLoanLogics">
                <thead>
                    <tr>
                         <th style="white-space: nowrap; text-align: center;"">Sr. #</th>
                         <th style="white-space: nowrap; text-align: center;">Month</th>
                         <th style="white-space: nowrap; text-align: center;">Year</th>
                         <th style="white-space: nowrap; text-align: center;">Loan Number</th>
                         <th style="white-space: nowrap; text-align: center;">Project Month</th>
                         <th style="white-space: nowrap; text-align: center;">LM Loan No</th>
                         <th style="white-space: nowrap; text-align: center;">Page Count</th>
                         <th style="white-space: nowrap; text-align: center;">Doc Count</th>
                         <th style="white-space: nowrap; text-align: center;">Delivery Date</th>
                         <th style="white-space: nowrap; text-align: center;">Fee Type</th>
                         <th style="white-space: nowrap; text-align: center;">Quantity</th>
                         <th style="white-space: nowrap; text-align: center;">Amount</th>
                    
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>
</div>






     <div class="modal fade" id="RemoreUWLaminr_dverror" tabindex="-1" role="dialog" aria-hidden="true">
     <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
         <div class="modal-content" style="border-radius: 12px; border: none;">
             <div class="modal-header bg-light" style="border-top-left-radius: 12px; border-top-right-radius: 12px;">
                 <h6 class="modal-title font-weight-bold text-dark" id="RemoreUWLaminr_errmsg"></h6>
             </div>
             <div class="modal-footer justify-content-center border-0">
                 <button class="btn btn-primary-custom btn-sm px-4" type="button" id="RemoreUWLaminr_btnMessage" onclick="return RemoreUWLaminr_closepopup();">Okay</button>
             </div>
         </div>
     </div>
 </div>
</asp:Content>
