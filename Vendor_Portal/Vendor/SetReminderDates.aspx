<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/Vendor.Master" AutoEventWireup="true" CodeBehind="SetReminderDates.aspx.cs" Inherits="Vendor_Portal.Vendor.SetReminderDates" %>

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

        /*.form-control {
            font-size: 11px !important;
        }*/
    </style>

    <script>
        $(document).ready(function () {

            BindSrd_Grid();

        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>

    <div class="content-header">
        <div class="container">
            <div class="row mb-2 callout callout-info">
                <div class="col-sm-6">
                    <h6 class="m-0"><i class="fas fa-copy"></i>&nbsp;&nbsp;<b>Set Reminder Date</b></h6>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>


    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div>
                    <table class="table">
                        <tr>
                            <td><b>Company :</b></td>
                            <td>
                                <select id="srd_Company" name="srd_Company" class="form-control" style="width: 250px;">
                                    <option value="">Select</option>
                                    <option value="IPS">IPS</option>
                                    <option value="Canopy">Canopy</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Vendor :</b></td>
                            <td>
                                <select id="srd_Vendor" name="srd_Vendor" class="form-control" style="width: 250px;">
                                    <option value="">Select</option>
                                    <option value="670">670</option>
                                    <option value="Abstractor">Abstractor</option>
                                    <option value="Attorney">Attorney</option>
                                    <option value="Compliance">Compliance</option>
                                    <option value="KCB">KCB</option>
                                    <option value="LauraMac">Laura Mac</option>
                                    <option value="Magna5">Magna 5</option>
                                    <option value="RemoteUW">Remote UW</option>
                                    <option value="Scienna">Scienna</option>
                                    <option value="Stewart">Stewart</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Day :</b></td>
                            <td>
                                <%--<input type="date" id="srd_date" name="srd_date" class="form-control" style="width: 250px;" />--%>
                                <select id="srd_Day" name="srd_Day" class="form-control" style="width: 250px;">
                                    <option value="">Select</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                    <option value="9">9</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                    <option value="24">24</option>
                                    <option value="25">25</option>
                                    <option value="26">26</option>
                                    <option value="27">27</option>
                                    <option value="28">28</option>
                                    <option value="29">29</option>
                                    <option value="30">30</option>
                                    <option value="31">31</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <button id="srd_btn" name="srd_btn" class="btn btn-primary" onclick="return srd_btnsubmit();">Submit</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table class="table" id="table_srd_records" style="width: 100%;">
                        <thead>
                            <tr>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Actions</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap; text-align: center;">Sr. #</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap; display: none;">ReminderDateID</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Company</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Vendor</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Day</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Added By</th>
                                <th class="sort border-top ps-3" style="text-wrap: nowrap;">Added Datetime</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="srd_UpdatePopUpDate">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Set New Reminder Day</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table table-responsive">
                        <tr>
                            <td style="width: 100px;"><b>Company</b> :</td>
                            <td style="width: 150px;">
                                <label id="srd_lblCompany" name="srd_lblCompany" class="form-control" style="display: inline;"></label>
                            </td>
                            <td ><b>Vendor : </b></td>
                            <td style="width: 150px;">
                                <label id="srd_lblVendor" name="srd_lblVendor" class="form-control" style="display: inline;"></label>
                            </td>
                            <td><b>Previous Day : </b></td>
                            <td>
                                <label id="srd_lblPDay" name="srd_lblPDay" class="form-control" style="display: inline;"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>New Day :</b>
                            </td>
                            <td colspan="4">
                                <select id="srd_UpdateDay" name="srd_UpdateDay" class="form-control" style="width: 250px;">
                                    <option value="">Select</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                    <option value="9">9</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                    <option value="13">13</option>
                                    <option value="14">14</option>
                                    <option value="15">15</option>
                                    <option value="16">16</option>
                                    <option value="17">17</option>
                                    <option value="18">18</option>
                                    <option value="19">19</option>
                                    <option value="20">20</option>
                                    <option value="21">21</option>
                                    <option value="22">22</option>
                                    <option value="23">23</option>
                                    <option value="24">24</option>
                                    <option value="25">25</option>
                                    <option value="26">26</option>
                                    <option value="27">27</option>
                                    <option value="28">28</option>
                                    <option value="29">29</option>
                                    <option value="30">30</option>
                                    <option value="31">31</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary" type="button" id="srd_update" onclick="return srd_updateDate();">Update</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>

</asp:Content>
