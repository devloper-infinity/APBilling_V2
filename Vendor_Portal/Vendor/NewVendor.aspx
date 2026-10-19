<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="NewVendor.aspx.cs" Inherits="Vendor_Portal.Vendor.NewVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .uploadCard{

border:none;

border-radius:15px;

}

.uploadHeader{

background:linear-gradient(90deg,#5dade2,#2d8cf0);

color:white;

padding:20px;

}

.upload-area{

border:3px dashed #b5d8ff;

border-radius:15px;

padding:40px;

text-align:center;

background:#fafcff;

transition:.3s;

cursor:pointer;

}

.upload-area:hover{

background:#eef7ff;

border-color:#2d8cf0;

}

.pdfIcon{

font-size:60px;

color:#dc3545;

margin-bottom:15px;

}

.excelIcon{

font-size:60px;

color:#28a745;

margin-bottom:15px;

}

.card{

border-radius:15px;

}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mt-3">

    <div class="card uploadCard shadow">

        <div class="card-header uploadHeader">

            <h4>

                <i class="fas fa-file-upload"></i>

                Vendor Invoice Upload

            </h4>

            <small>Upload PDF Invoice and Vendor Excel</small>

        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-6">

                    <label>Company</label>

                    <asp:DropDownList ID="ddlCompany"

                        runat="server"

                        CssClass="form-control"></asp:DropDownList>

                </div>

                <div class="col-md-6">

                    <label>Vendor</label>

                    <asp:DropDownList ID="ddlVendor"

                        runat="server"

                        CssClass="form-control"></asp:DropDownList>

                </div>

            </div>

        </div>

    </div>

</div>
    <div class="card mt-4 shadow">

    <div class="card-header bg-primary text-white">

        <b>

            <i class="fas fa-file-pdf"></i>

            Upload Invoice PDF

        </b>

    </div>

    <div class="card-body">

        <div class="upload-area">

            <i class="fas fa-file-pdf pdfIcon"></i>

            <h5>Drag & Drop Invoice PDF</h5>

            <p>or</p>

            <asp:FileUpload ID="fuInvoice"

                runat="server"

                CssClass="form-control" />

        </div>

        <center>

            <asp:Button

                ID="btnScan"

                runat="server"

                Text="Scan Invoice"

                CssClass="btn btn-primary btn-lg mt-3"/>

        </center>

    </div>

</div>
    <div class="card mt-4 shadow">

<div class="card-header bg-info text-white">

Invoice Information

</div>

<div class="card-body">

<div class="row">

<div class="col-md-3">

<label>Invoice No</label>

<asp:TextBox ID="txtInvoiceNo"

runat="server"

CssClass="form-control"></asp:TextBox>

</div>

<div class="col-md-3">

<label>Invoice Date</label>

<asp:TextBox ID="txtInvoiceDate"

runat="server"

CssClass="form-control"></asp:TextBox>

</div>

<div class="col-md-3">

<label>Due Date</label>

<asp:TextBox ID="txtDueDate"

runat="server"

CssClass="form-control"></asp:TextBox>

</div>

<div class="col-md-3">

<label>Amount</label>

<asp:TextBox ID="txtAmount"

runat="server"

CssClass="form-control"></asp:TextBox>

</div>

</div>

</div>

</div>
    <div class="card mt-4 shadow">

<div class="card-header bg-success text-white">

Vendor Excel

</div>

<div class="card-body">

<div class="upload-area">

<i class="fas fa-file-excel excelIcon"></i>

<h5>Drag & Drop Excel File</h5>

<p>or</p>

<asp:FileUpload

ID="fuExcel"

runat="server"

CssClass="form-control"/>

</div>

<center>

<asp:Button

ID="btnValidate"

runat="server"

Text="Validate"

CssClass="btn btn-warning"/>

<asp:Button

ID="btnImport"

runat="server"

Text="Import"

CssClass="btn btn-success"/>

</center>

</div>

</div>

</asp:Content>
