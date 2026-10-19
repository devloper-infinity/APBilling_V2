<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="AddInvoiceLauraMacIPS.aspx.cs" Inherits="Vendor_Portal.Vendor.AddInvoiceLauraMacIPS" %>
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
            
            document.getElementById('RemoteUWLauraMacIPSInvoice').addEventListener('change', getFileNamestewartinvoice);
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <input id="file_RemoteUWLauraMacIPS" style="display: none;" />
    <input id="file_RemoteUWexcelLauraMacIPS" style="display: none;" />
     <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
    <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>LauraMac-IPS</b></h6>
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
                        <select id="addinvoiceLauraMacIPS_month" name="addinvoiceLauraMacIPS_month" class="form-control" style="width: 250px;">
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
                      <select id="addinvoiceLauraMacIPS_year" name="addinvoiceLauraMacIPS_year" class="form-control" style="width: 250px;">
                                                <option value="">Select</option>
                                            </select>
                    </td>
                </tr>
            <tr>
                <td><b>Invoice No:</b></td>
                <td>
                     <input type="text" id="RemoteUWLauraMacIPS_invoiceaNo_canopy" name="RemoteUWLauraMacIPS_invoiceaNo_canopy" class="form-control" style="width: 250px;" />
                </td>
                 <td><b>Invoice Amount($):</b></td>
                                        <td>
                                            <input type="text" id="RemoteUWLauraMacIPS_invoiceamount_canopy" name="RemoteUWLauraMacIPS_invoiceamount_canopy" class="form-control" style="width: 250px;" />
                                        </td>
            </tr>
            <tr>
                 <td><b>Invoice Date:</b></td>
                  <td>
                   <input type="date" id="RemoteUWLauraMacIPS_invoicedate" name="RemoteUWLauraMacIPS_invoicedate" class="form-control" style="width: 250px;" />
                   </td>
                 <td><b>Due Date</b></td>
                <td><input type="date" id="cm_DueDateLauraMacIPS" name="cm_DueDateLauraMacIPS" style="width: 250px;" /></td>
            </tr>
            <tr>
                <td><b>No Of Loans:</b></td>
                <td>
                     <input type="text" id="RemoteUWLauraMacIPS_NoOfLoans_canopy" name="RemoteUWLauraMacIPS_NoOfLoans_canopy" class="form-control" style="width: 250px;" />
                </td>
                <td></td>
                <td> <a href="LauraMacIPS-ERPFormat.xls" style="font-family: Verdana; font-size: 13px; font-weight: bold; color: blue; text-decoration: underline;">ERP Sample Format</a></td>
            </tr>
            <tr style="display:none;">
                <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>From Date:</b></td>
                        <td>
                            <input type="date" id="RWLauraMacIPSFromDate" name="RWLauraMacIPSFromDate" style="width: 250px;" />
                        </td>

                <td><i class="fa fa-star" style="font-size: 5px; color: red"></i>&nbsp;<b>To Date:</b></td>
                        <td>
                            <input type="date" id="RWLauraMacIPSToDate" name="RWLauraMacIPSToDate" style="width: 250px;" />
                        </td>
            </tr>
            <tr>
                 <td><b>Invoice:</b></td>
                 <td>
                  <input type="file" id="RemoteUWLauraMacIPSInvoice" name="RemoteUWInvoice" class="form-control" style="width: 250px;" />  
                 </td>
                <td><b>Excel:</b></td>
                   <td>
                   <input type="file" id="RemoteUWLauraMacIPSInvoiceExcel" name="RemoteUWInvoiceExcel" class="form-control" style="width: 250px;" />
                 </td>
                
            <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <button class="btn btn-primary" type="button" id="cm_btnImportLauraMacIPS" onclick="return RemoteUWLauraMacIPS_submit();">Import</button>
                        </td>
                    </tr>
           </table>
        </div>
</asp:Content>
