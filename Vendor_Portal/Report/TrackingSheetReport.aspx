<%@ Page Title="" Language="C#" MasterPageFile="~/Report/MIS.Master" AutoEventWireup="true" CodeBehind="TrackingSheetReport.aspx.cs" Inherits="Vendor_Portal.Report.TrackingSheetReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
 

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
                 .custom-table-header {
         display: flex;
         align-items: center;
         gap: 15px;
         margin-bottom: 15px;
         float: left;
     }
                  .form-control, .form-select {
     border-radius: 8px;
     border: 1px solid #93c5fd;
     padding: 8px 12px;
     font-size: 0.9rem;
     transition: all 0.2s ease-in-out;
 }

 .form-control:focus, .form-select:focus {
     border-color: #0056b3;
     box-shadow: 0 0 0 3px rgba(0, 86, 179, 0.15);
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
        function loadTableData() {
            var selectedCategory = $('#trackingCategory').val();

            if (!selectedCategory || selectedCategory === "") {
                document.getElementById("Tracking_errmsg").innerText = "Please select Category Type.";
                $('#Tracking_dverror').modal('show');
                return false;
            }

            var fromDate = $('#trackingFromDate').val();
            var toDate = $('#trackingToDate').val();
            if (!fromDate || fromDate === "") {
                document.getElementById("Tracking_errmsg").innerText = "Please select From Date.";
                $('#Tracking_dverror').modal('show');
                return false;
            }

            if (!toDate || toDate === "") {
                document.getElementById("Tracking_errmsg").innerText = "Please select To Date.";
                $('#Tracking_dverror').modal('show');
                return false;
            }

            var start = new Date(fromDate);
            var end = new Date(toDate);

            if (end < start) {
                document.getElementById("Tracking_errmsg").innerText = "To Date cannot be before From Date.";
                $('#Tracking_dverror').modal('show');
                return false;
            }

            $('#load1').show();


            $.ajax({
                type: "POST",
                url: "TrackingSheetReport.aspx/GetTrackingSheetCreditData",
                data: JSON.stringify({
                    category: selectedCategory,
                    fromDate: fromDate,
                    toDate: toDate
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);

                    if ($.fn.DataTable.isDataTable('#trackingsheet_IPS')) {
                        $('#trackingsheet_IPS').DataTable().clear().destroy();
                    }

                    $('#trackingsheet_IPS thead').empty();
                    $('#trackingsheet_IPS tbody').empty();

                    if (data.length > 0) {
                        var columns = [];
                        var headers = Object.keys(data[0]);

                        var headerHtml = '<tr>';
                        headers.forEach(function (colName) {
                            headerHtml += '<th style="min-width: 100px;">' + colName + '</th>';
                            columns.push({
                                data: colName,
                                createdCell: function (td, cellData, rowData, row, col) {
                                    $(td).css('min-width', '100px');
                                    $(td).css('white-space', 'nowrap'); 
                                }
                            });
                        });
                        headerHtml += '</tr>';
                        $('#trackingsheet_IPS thead').html(headerHtml);

                        $('#trackingsheet_IPS').DataTable({
                            data: data,
                            columns: columns,
                            scrollX: true,
                            autoWidth: false,
                            ordering: false,
                            pageLength: 50,
                            dom: '<"custom-table-header"lB>frtip',
                            buttons: [
                                {
                                    extend: 'excelHtml5',
                                    className: 'buttons-excel',
                                    filename: 'TrackingSheet_Report',
                                    title: 'Tracking Sheet Report',
                                }
                            ]
                        });
                    } else {
                        $('#trackingsheet_IPS thead').html('<tr><th></th></tr>');
                    }
                },
                error: function (xhr, status, error) {
                    console.log("Error: " + error);
                },
                complete: function () {
                    $('#load1').hide();
                }
            });
        }

        $(document).ready(function () {
            $('#trackingbtnShow').click(function () {
                loadTableData();
            });
        });

        function Tracking_closepopup() {
            $('#Tracking_dverror').modal('hide');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Loader HTML -->
    <div class="loading" id="load1">
        <img src="/images/Load_1.gif" />
        <div style="font-size: 12px; font-weight: bold;">One moment, please . . . .</div>
    </div>
    <div class="col-lg-15 mt-3 mb-2">
        <div class="card card-custom" style="margin-bottom: 0px !important; min-height: 60px;">
            <div class="card-header card-header-custom d-flex justify-content-between align-items-center" style="margin-bottom: 0px !important; min-height: 60px;">
                <div>
                    <h5 class="m-0 font-weight-bold">
                        <i class="fas fa-copy"></i> Tracking Sheet Report
            </h5>
                </div>
            </div>
        </div>
    </div>

      <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="filter-container mb-4">
                    <table class="table mb-0 border-0" style="background: transparent;">
                        <tr class="align-middle border-0">
                            <!-- Category Select -->
                            <td style="width: 320px;" class="border-0">
                                <div class="row align-items-center">
                                    <label for="trackingCategory" class="col-sm-4 col-form-label mb-0 text-right">
                                        <b>Category:</b>
                                    </label>
                                    <div class="col-sm-8">
                                        <select id="trackingCategory" name="trackingCategory" class="form-control form-select">
                                            <option value="">-- Select --</option>
                                            <option value="Credit">Credit</option>
                                            <option value="Servicing">Servicing</option>
                                        </select>
                                    </div>
                                </div>
                            </td>

                            <!-- From Date -->
                            <td style="width: 90px; text-align: right; padding-right: 8px;" class="border-0">
                                <label for="trackingFromDate" class="mb-0"><b>From:</b></label>
                            </td>
                            <td style="width: 180px;" class="border-0">
                                <input type="date" class="form-control" id="trackingFromDate" name="FromDate" max="<%= DateTime.Now.ToString("yyyy-MM-dd") %>" />
                            </td>

                            <!-- To Date -->
                            <td style="width: 80px; text-align: right; padding-right: 8px;" class="border-0">
                                <label for="trackingToDate" class="mb-0"><b>To:</b></label>
                            </td>
                            <td style="width: 180px;" class="border-0">
                                <input type="date" class="form-control" id="trackingToDate" name="ToDate" max="<%= DateTime.Now.ToString("yyyy-MM-dd") %>" />
                            </td>

                            <!-- Show Button -->
                            <td style="padding-left: 25px;" class="border-0">
                                <button class="btn btn-primary-custom" type="button" id="trackingbtnShow">
                                    <i class="fas fa-search mr-1"></i> Search
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane fade show active" id="trackingsheettable" role="tabpanel" aria-labelledby="custom-tabs-one-home-tab_infinity">
                    <div style="width: 100%; overflow: auto;">
                        <table class="table table-striped table-hover" id="trackingsheet_IPS" style="width: 100%;">
                            <thead>
                                <tr>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- Modal Popup -->
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
