<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="AddInvoiceSales.aspx.cs" Inherits="Vendor_Portal.Vendor.AddInvoiceSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
        .content-wrapper {
   
    padding: 2px 0 40px;
}
    </style>

    <script>
        window.onload = function () {
            document.getElementById('RemoteUWSalesInvoice').addEventListener('change', getFileNamestewartinvoice);
            document.getElementById('RemoteUWSalesInvoiceExcel').addEventListener('change', getFileNamestewartexcel);
        };

        const getFileNamestewartinvoice = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWSales").value = files[0].name;

            const fd = new FormData();
            fd.append(event.target.name, file, "file_RemoteUWSales" + file.name);

            const xhr = new XMLHttpRequest();
            xhr.onload = () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    
                }
            };
            var url = window.location.href;
            xhr.open('POST', url, true);
            xhr.send(fd);
            document.getElementById("RemoteUWSalesInvoice").innerHTML = file.name;
        };

        const getFileNamestewartexcel = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWexcelSales").value = files[0].name;

            const fd = new FormData();
            fd.append(event.target.name, file, "file_RemoteUWexcelSales" + file.name);

            const xhr = new XMLHttpRequest();
            xhr.onload = () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    // we done!
                }
            };
            var url = window.location.href;
            xhr.open('POST', url, true);
            xhr.send(fd);
            document.getElementById("RemoteUWSalesInvoiceExcel").innerHTML = file.name;
        };

        $(document).ready(function () {
           // addinvoiceSales_BindYear();
            GetSalesVendors();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="file_RemoteUWSales" style="display: none;" />
    <input id="file_RemoteUWexcelSales" style="display: none;" />
    <!-- Loading Spinner -->
    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" width="48" height="48" alt="Loading" />
        <div style="font-size: 13px; font-weight: 600; color: #495057; margin-top: 10px;">One moment, please...</div>
    </div>
    <div class="container-fluid pt-3">
        <!-- Input Form Card -->
        <div class="card card-custom">
            <div class="card-header card-header-custom d-flex justify-content-between align-items-center">
                <h5 class="m-0 font-weight-bold"><i class="fas fa-copy mr-1"></i>Sales Invoice</h5>
            </div>
            <div class="card-body px-4 py-3">
                <div class="row">
                    <!-- Column 1 -->
                    <div class="col-md-6">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Vendor Name:</b></label>
                            <div class="col-sm-8">
                                <select class="form-control" id="RemoteUWSales_VendorName" name="RemoteUWSales_VendorName">
                                    <option value="">Select</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>To Company:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="Invoicesales_ToCompany" name="Invoicesales_ToCompany" class="form-control" disabled/>
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice No:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWSales_invoiceaNo_canopy" name="RemoteUWSales_invoiceaNo_canopy" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Service Period From:</b></label>
                            <div class="col-sm-8">
                                <input type="date" id="RemoteUWSales_ServicePeriodFrom" name="RemoteUWSales_invoicedate" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Category:</b></label>
                            <div class="col-sm-8">
                                <select id="saleinvoice_InvoiceCategory" name="saleinvoice_InvoiceCategory" class="form-control">
                                    <option value="">Select</option>
                                                                        <option value="Sales Commission">Sales Commission </option>
                                                                        <option value="Advisory Fee">Advisory Fee </option>
                                                                        <option value="Consulting Fee">Consulting Fee</option>
                                                                        <option value="Other">Other</option>


                                </select>
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Amount($):</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWSales_invoiceamount_canopy" name="RemoteUWSales_invoiceamount_canopy" class="form-control" disabled/>
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center"> 
                            <label class="col-sm-4 col-form-label"><b>Payment Terms:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWSales_PaymentTerms" name="RemoteUWSales_PaymentTerms" class="form-control" disabled />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Bank Account:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWSales_BankAccount" name="RemoteUWSales_BankAccount" class="form-control" disabled />
                            </div>
                        </div>
                    </div>
                    <!-- Column 2 -->
                    <div class="col-md-6">
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Vendor Type:</b></label>
                            <div class="col-sm-8">
                               <input type="text" class="form-control" id="Invoicesales_vendortype" name="Invoicesales_vendortype" disabled />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Email Address:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="Invoicesales_EmailAddress" name="Invoicesales_EmailAddress" class="form-control" disabled/>
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Date:</b></label>
                            <div class="col-sm-8">
                                <input type="date" id="RemoteUWSales_invoicedate" name="RemoteUWSales_invoicedate" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Service Period To:</b></label>
                            <div class="col-sm-8">
                                <input type="date" id="RemoteUWSales_ServicePeriodTo" name="RemoteUWSales_ServicePeriodTo" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Invoice Description:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWSales_InvoiceDescription" name="RemoteUWSales_InvoiceDescription" class="form-control" />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Due Date:</b></label>
                            <div class="col-sm-8">
                                <input type="date" id="cm_DueDateSales" name="cm_DueDateSales" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Payment Method:</b></label>
                            <div class="col-sm-8">
                                                                <input type="text" id="Invoicesales_PaymentMethod" name="Invoicesales_PaymentMethod" class="form-control" disabled/>

                            
                            </div>
                        </div>
                        <div class="form-group form-group-item row align-items-center">
                            <label class="col-sm-4 col-form-label"><b>Payment From Company:</b></label>
                            <div class="col-sm-8">
                                <input type="text" id="RemoteUWSales_PaymentFromCompany" name="RemoteUWSales_PaymentFromCompany" class="form-control" disabled />
                            </div>
                        </div>

                        <div class="form-group form-group-item row align-items-center" style="display: none;">
                            <label class="col-sm-4 col-form-label"><b>Excel:</b></label>
                            <div class="col-sm-8">
                                <input type="file" id="RemoteUWSalesInvoiceExcel" name="RemoteUWInvoiceExcel" class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Hidden Date Fields (Maintained for logic) -->
                <div class="row" style="display: none;">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label><b>From Date:</b></label>
                            <input type="date" id="RWSalesFromDate" name="RWSalesFromDate" class="form-control" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label><b>To Date:</b></label>
                            <input type="date" id="RWSalesToDate" name="RWSalesToDate" class="form-control" />
                        </div>
                    </div>
                </div>

                <!-- Add Button Row -->
                <div class="row mt-2">
                    <div class="col-12 text-right">
                        <button class="btn btn-primary-custom px-5" type="button" id="cm_btnImportSales" onclick="return RemoteUWSales_submit();">Add</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Data Grid Table Card -->
        <div class="card card-custom">
            <div class="card-body p-3" style="display: none;">
                <div class="table-responsive">
                    <table class="table table-striped table-bordered" id="table_RemoteUWSales" style="width: 100%;">
                        <thead>
                            <tr>
                                <th class="text-center">Sr. #</th>
                                <th class="text-center">Month</th>
                                <th class="text-center">Year</th>
                                <th class="text-center">InvoiceType</th>
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
    <div class="modal fade" id="RemoreUWSales_dverror" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
            <div class="modal-content" style="border-radius: 12px; border: none;">
                <div class="modal-header bg-light" style="border-top-left-radius: 12px; border-top-right-radius: 12px;">
                    <h6 class="modal-title font-weight-bold text-dark" id="RemoreUWSales_errmsg"></h6>
                </div>
                <div class="modal-footer justify-content-center border-0">
                    <button class="btn btn-primary-custom btn-sm px-4" type="button" id="RemoreUWSales_btnMessage" onclick="return RemoreUWSales_closepopup();">Okay</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
