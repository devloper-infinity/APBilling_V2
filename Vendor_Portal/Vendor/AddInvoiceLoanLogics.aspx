<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="AddInvoiceLoanLogics.aspx.cs" Inherits="Vendor_Portal.Vendor.AddInvoiceLoanLogics" %>
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
    </style>
    <script>

        window.onload = function () {
            
            document.getElementById('RemoteUWLoanLogicsInvoice').addEventListener('change', getFileNamestewartinvoice);
            document.getElementById('RemoteUWLoanLogicsInvoiceExcel').addEventListener('change', getFileNamestewartexcel);
        }

        const getFileNamestewartinvoice = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWLoanLogics").value = files[0].name;

            const fd = new FormData();

            // add all selected files

            fd.append(event.target.name, file, "file_RemoteUWLoanLogics" + file.name);
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
            document.getElementById("RemoteUWLoanLogicsInvoice").innerHTML = file.name;
        }
        const getFileNamestewartexcel = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUWexcelLoanLogics").value = files[0].name;

            const fd = new FormData();

            // add all selected files
            fd.append(event.target.name, file, "file_RemoteUWexcelLoanLogics" + file.name);
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
            addinvoiceLoanLogics_BindYear();

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="file_RemoteUWLoanLogics" style="display: none;" />
    <input id="file_RemoteUWexcelLoanLogics" style="display: none;" />
     <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
    
 <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>LoanLogics</b></h6>
                </div>
            </div>
        </div>
    </div>

     <div class="col-lg-12">
        <div class="card">
            <table class="table">
            <tr>
                    <td><b>Month:</b></td> 
                    <td>
                        <select id="addinvoiceLoanLogics_month" name="addinvoiceLoanLogics_month" class="form-control" style="width: 250px;">
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
                      <select id="addinvoiceLoanLogics_year" name="addinvoiceLoanLogics_year" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                            </select>
                    </td>
                </tr>
            <tr>
                <td><b>Invoice No:</b></td>
                <td>
                     <input type="text" id="RemoteUWLoanLogics_invoiceaNo_canopy" name="RemoteUWLoanLogics_invoiceaNo_canopy" class="form-control" style="width: 250px;" />
                </td>
                 <td><b>Invoice Amount($):</b></td>
                                        <td>
                                            <input type="text" id="RemoteUWLoanLogics_invoiceamount_canopy" name="RemoteUWLoanLogics_invoiceamount_canopy" class="form-control" style="width: 250px;" />
                                        </td>
            </tr>
            <tr>
                 <td><b>Invoice Date:</b></td>
                  <td>
                   <input type="date" id="RemoteUWLoanLogics_invoicedate" name="RemoteUWLoanLogics_invoicedate" class="form-control" style="width: 250px;" />
                   </td>
                 <td><b>Due Date</b></td>
                <td><input type="date" id="cm_DueDateLoanLogics" name="cm_DueDateLoanLogics" style="width: 250px;" /></td>
            </tr>
            <tr>
                <td><b>No Of Loans:</b></td>
                <td>
                     <input type="text" id="RemoteUWLoanLogics_NoOfLoans_canopy" name="RemoteUWLoanLogics_NoOfLoans_canopy" class="form-control" style="width: 250px;" />
                </td>
                <td><b>Remote UW </b></td>
                   <td> <select id="RemoteUWLoanLogics" name="RemoteUWLoanLogics" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                                <option value="LoanLogics">LoanLogics</option>
                                            </select>
                       </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td> <a href="LoanLogics-ERPFormat.xls" style="font-family: Verdana; font-size: 13px; font-weight: bold; color: blue; text-decoration: underline;">ERP Sample Format</a></td>
                <td><b>Company</b></td>
                   <td> <select id="RemoteUWLoanLogicsCompany" name="RemoteUWLoanLogicsCompany" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                                <option value="IPS">IPS</option>
                                                 <option value="Canopy">Canopy</option>
                                            </select>
                       </td>

                </tr>
            <tr style="display:none;">
                <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>From Date:</b></td>
                        <td>
                            <input type="date" id="RWLoanLogicsFromDate" name="RWLoanLogicsFromDate" style="width: 250px;" />
                        </td>

                <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>To Date:</b></td>
                        <td>
                            <input type="date" id="RWLoanLogicsToDate" name="RWLoanLogicsToDate" style="width: 250px;" />
                        </td>
            </tr>
            <tr>
                 <td><b>Invoice:</b></td>
                 <td>
                  <input type="file" id="RemoteUWLoanLogicsInvoice" name="RemoteUWInvoice" class="form-control" style="width: 250px;" />  
                 </td>
                  <td><b>Excel:</b></td>
                   <td>
                   <input type="file" id="RemoteUWLoanLogicsInvoiceExcel" name="RemoteUWInvoiceExcel" class="form-control" style="width: 250px;" />
                 </td>
              </tr> 
            <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <button class="btn btn-primary" type="button" id="cm_btnImportLoanLogics" onclick="return RemoteUWLoanLogics_submit();">Import</button>
                        </td>
                    </tr>
           </table>
        </div>

         <div>
                <table class="table" id="table_RemoteUWLoanLogics" style="width: 100%;">
                    <thead>
                        <tr>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Month</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Year</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Loan Number</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Project Month</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">LM Loan No</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Page Count</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Doc Count</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Delivery Date</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Fee Type</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Quantity</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Amount</th>
                            >
                    </thead>
                    <tbody></tbody>
                </table>
            </div>

     </div>    

     <div class="modal fade" id="RemoreUWLoanLogics_dverror">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title" id="RemoreUWLoanLogics_errmsg"></h6>
                </div>
                <div class="modal-footer align-content-center">
                    <button class="btn btn-primary" type="button" id="RemoreUWLoanLogics_btnMessage" onclick="return RemoreUWLoanLogics_closepopup();">Okay</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

</asp:Content>
