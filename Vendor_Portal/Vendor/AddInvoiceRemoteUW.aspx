<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="AddInvoiceRemoteUW667.aspx.cs" Inherits="Vendor_Portal.Vendor.AddInvoiceRemoteUW667" %>
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
            
            document.getElementById('RemoteUW667Invoice').addEventListener('change', getFileNamestewartinvoice);
            document.getElementById('RemoteUW667InvoiceExcel').addEventListener('change', getFileNamestewartexcel);
        }

        const getFileNamestewartinvoice = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUW667").value = files[0].name;

            const fd = new FormData();

            // add all selected files

            fd.append(event.target.name, file, "file_RemoteUW667" + file.name);
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
            document.getElementById("RemoteUW667Invoice").innerHTML = file.name;
        }
        const getFileNamestewartexcel = (event) => {
            const files = event.target.files;
            var file = files[0];
            document.getElementById("file_RemoteUW667excel").value = files[0].name;

            const fd = new FormData();

            // add all selected files
            fd.append(event.target.name, file, "file_RemoteUW667excel" + file.name);
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
            document.getElementById("RemoteUW667InvoiceExcel").innerHTML = file.name;
            //alert(document.getElementById("filep").value);
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="file_RemoteUW667" style="display: none;" />
    <input id="file_RemoteUW667excel" style="display: none;" />
     <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
    
 <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Remote UW Billing-IPS</b></h6>
                </div>
            </div>
        </div>
    </div>

     <div class="col-lg-12">
        <div class="card">
            <table class="table">
            <tr style="display:none;">
                 <td><b>Invoice Date:</b></td>
                  <td>
                   <input type="date" id="RemoteUW667_invoicedate" name="RemoteUW667_invoicedate" class="form-control" style="width: 250px;" />
                   </td>
                 <td><b>Invoice Amount($):</b></td>
                                        <td>
                                            <input type="text" id="RemoteUW667_invoiceamount_canopy" name="RemoteUW667_invoiceamount_canopy" class="form-control" style="width: 250px;" />
                                        </td>
            </tr>
            <tr style="display:none;">
                <td><b>No Of Loans:</b></td>
                <td>
                     <input type="text" id="RemoteUW667_NoOfLoans_canopy" name="RemoteUW667_NoOfLoans_canopy" class="form-control" style="width: 250px;" />
                </td>
                <td><b>Due Date</b></td>
                <td><input type="date" id="cm_DueDate" name="cm_DueDate" style="width: 250px;" /></td>
            </tr>

            <tr>
                    <td><b>Remote UW </b></td>
                   <td> <select id="RemoteUW667667" name="RemoteUW667" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                                <option value="All">All</option>
                                            </select>
                       </td>
                    <td></td>
                    <td></td>

                </tr>
            <tr>
                <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>From Date:</b></td>
                        <td>
                            <input type="date" id="RW667FromDate" name="RW667FromDate" style="width: 250px;" />
                        </td>

                <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>To Date:</b></td>
                        <td>
                            <input type="date" id="RW667ToDate" name="RW667ToDate" style="width: 250px;" />
                        </td>
            </tr>
            <tr>
                    <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Project No:</b></td>
                    <td>
                        <select id="RW667ProjectNo" name="RW667ProjectNo" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                                <option value="217">667</option>
                                                <option value="70">561</option>
                                            </select>
                    </td>
                    <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Deal No</b></td>
                    <td>
                           <input type="text" id="RemoteUW667_invoiceamount_DealNo" name="RemoteUW667_invoiceamount_DealNo" class="form-control" style="width: 250px;" />
                    </td>
                </tr>
            <tr>
                    <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>Process:</b></td>
                     <td>
                        <select id="RW667Process" name="RW667Process" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                                <option value="SPQA">SPQA</option>
                                                <option value="Review">Review</option>
                                                <option value="QC">QC</option>
                                            </select>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
             <tr style="display:none;">
                 <td><b>Invoice:</b></td>
                 <td>
                  <input type="file" id="RemoteUW667Invoice" name="RemoteUW667Invoice" class="form-control" style="width: 250px;" />  
                 </td>
                  <td><b>Excel:</b></td>
                   <td>
                   <input type="file" id="RemoteUW667InvoiceExcel" name="RemoteUW667InvoiceExcel" class="form-control" style="width: 250px;" />
                 </td>
              </tr> 
               
                <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <button class="btn btn-primary" type="button" id="cm_btnImport" onclick="return RemoteUW667667_submit();">Show</button>
                        </td>
                    </tr>

           </table>
        </div>

         <div>
                <table class="table" id="table_RemoteUW667" style="width: 100%;">
                    <thead>
                        <tr>
                            <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">From Date</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">To Date</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Remote UW</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Loan Number</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Fund</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Completed Date</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Base Rate</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Duplicate</th>
                            <th class="sort border-top" style="text-wrap: nowrap; text-align: center;">Sys Remark</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>

     </div>    

     <div class="modal fade" id="RemoreUW_dverror">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title" id="RemoreUW_errmsg"></h6>
                </div>
                <div class="modal-footer align-content-center">
                    <button class="btn btn-primary" type="button" id="RemoreUW_btnMessage" onclick="return RemoreUW_closepopup();">Okay</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

</asp:Content>
