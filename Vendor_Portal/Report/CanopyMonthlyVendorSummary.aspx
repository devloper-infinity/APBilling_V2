<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="CanopyMonthlyVendorSummary.aspx.cs" Inherits="Vendor_Portal.Report.CanopyMonthlyVendorSummary" %>

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

      

        label:not(.form-check-label):not(.custom-file-label) {
            font-weight: normal !important;
            border: none !important;
        }

        div.dt-buttons {
            position: static;
            padding-left: 50px;
            float: left;
        }

    

     

        .dtfc-fixed-left {
            left: 0px !important;
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
            padding: 12px 15px !important;
            min-width: 50px;
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
        

.card {
    border-top: 3px solid #0056b3 !important;
    box-shadow: 0 0 1px rgba(0,0,0,.125), 0 1px 3px rgba(0,0,0,.2);
    margin-bottom: 1rem;
}

.card-body::after {
    content: "";
    clear: both;
    display: table;
}

.card-header.p-0.pt-1 {
    margin-bottom: 5px !important;
}
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
    </style>
    <script>
        window.onload = function () {
            document.getElementById('dashboard_attachment_upload').addEventListener('change', getFileName);
        }
        const getFileName = (event) => {
            const files = event.target.files;
            var file = files[0];
            const fd = new FormData();

            // add all selected files
            fd.append(event.target.name, file, file.name);
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
        }
        $(document).ready(function () {
            const urlParams = new URLSearchParams(window.location.search);
            const user = urlParams.get('user');
            if (user == 'IPS') {

                dashboard_arBind_displayERP_IPS();
            }   
            else {

                dashboard_arBind_displayERP();
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="loading" id="load1">
        <img src="../images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>

               <div class="col-lg-15 mt-3 mb-2">
    <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
        <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
            <div>
                <h5 class="m-0 font-weight-bold">
                    <i class="fas fa-copy"></i> Monthly Vendor Report
            </h5>          
            </div>   
        </div>
    </div>
</div>
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="card card-tabs">
                    <div class="card-header p-0 pt-1">
                        <ul class="nav nav-tabs" id="custom-tabs-one-tab_bpd_tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="custom-tabs-one-home-tab_company" data-toggle="pill" href="#custom-tabs-one-home_company" role="tab" aria-controls="custom-tabs-one-home_company" aria-selected="true"><b>Summary</b></a>
                            </li>
                        </ul>
                    </div>
                    <div class="card-body">
                        <div class="tab-content" id="custom-tabs-one-tabContent_addinvocie">
                            <div class="tab-pane fade show active" id="custom-tabs-one-home_company" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_company">
                                <table class="table table-bordered" id="dashboard_ar" style="width: 100%;">
                                </table>
                            </div>
                            <div class="tab-pane fade" id="custom-tabs-one-profile_sales" role="tabpanel" aria-labelledby="custom-tabs-one-profile-tab_sales">
                                <label id="newlabl"></label>
                                <button id="dashboard_btnrefreshgrid" name="dashboard_btnrefreshgrid" class="btn btn-primary" style="float: left; position: relative; z-index: 1000;" onclick="return dashboard_arBind_DifferenceQuickbook_BillNo();">Refresh Data</button>
                                <table class="table table-bordered" id="dashboard_ardifference" style="width: 100%;">
                                </table>
                            </div>
                            <div class="tab-pane fade" id="custom-tabs-one-profile_deal" role="tabpanel" aria-labelledby="custom-tabs-one-profile_deal">
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="waitingpanel" tabindex="-1" data-bs-backdrop="static" aria-hidden="true">
        <div class="modal-dialog text-center">
            <img src="../Images/Load.gif" />
            <br />
            <span style="color: #fff; font-size: 24px; font-weight: bold; font-style: italic;" id="spntext">System is updating details. Please wait</span>
            <span style="color: #fff; font-size: 48px; font-weight: bold; font-style: italic; animation: animate 1s linear infinite;">&nbsp;. . . .</span>
        </div>
    </div>
    <%--Add Reconciliation Remark--%>
    <div class="modal fade" id="dashboard_updateremark">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add Reconciliation Remark</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table">
                        <tr>
                            <td><b>Client:</b></td>
                            <td>
                                <label id="dashboard_projectid" class="form-control" style="width: 300px; display: none;"></label>
                                <label id="dashboard_monthyear" class="form-control" style="width: 300px; display: none;"></label>
                                <label id="dashboard_projectno" class="form-control" style="width: 300px;"></label>
                            </td>
                            <td><b>Process:</b></td>
                            <td>
                                <label id="dashboard_process" class="form-control" style="width: 300px;"></label>
                            </td>
                        </tr>
                        <tr>

                            <td><b>Remark:</b></td>
                            <td colspan="3">
                                <textarea id="dashboard_remark" name="dashboard_remark" class="form-control" style="width: 300px;"></textarea>
                            </td>

                        </tr>
                    </table>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary" type="button" id="dashboard_remark_btnsubmit" onclick="return dashboard_remark_submit();">Submit</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--Upload Excel Attachment--%>
    <div class="modal fade" id="dashboard_uploadattachment">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Upload Excel Attachment</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table">
                        <tr>
                            <td><b>Client:</b></td>
                            <td>
                                <label id="dashboard_projectid_upload" class="form-control" style="width: 300px; display: none;"></label>
                                <label id="dashboard_monthyear_upload" class="form-control" style="width: 300px; display: none;"></label>
                                <label id="dashboard_projectno_upload" class="form-control" style="width: 300px;"></label>
                            </td>
                            <td><b>Process:</b></td>
                            <td>
                                <label id="dashboard_process_upload" class="form-control" style="width: 300px;"></label>
                            </td>
                        </tr>
                        <tr>

                            <td><b>Attachment:</b></td>
                            <td>
                                <input type="file" id="dashboard_attachment_upload" class="form-control" style="width: 300px;" />
                            </td>
                            <td></td>
                            <td></td>

                        </tr>
                    </table>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary" type="button" id="dashboard_upload_btnsubmit" onclick="return dashboard_upload_submit();">Submit</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="dashboard_RecRemark_dverror">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title" id="dashboard_RecRemark_errmsg"></h6>
                </div>
                <div class="modal-footer align-content-center">
                    <button class="btn btn-primary" type="button" id="dashboard_RecRemark_btnMessage" onclick="return dashboard_RecRemark_MessageRedirect();">Okay</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
