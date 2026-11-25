
var salesInvoiceData = [];
function BindGrid_saleInvoiceApproval() {
    $('#load1').show();
    var invrec_html_sales = '';

    $.ajax({
        url: "InvoiceApprovalSales.aspx/GetInfinity_SalesInvoiceDetails_Approval",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d || "[]");
            salesInvoiceData = dataArray;
            $.each(dataArray, function (index, value) {
                invrec_html_sales += '<tr>';
                invrec_html_sales += '<td><div class="btn-group">';
                invrec_html_sales += '<div type="button" data-toggle="dropdown" aria-expanded="false" style="cursor:pointer;"><i style="color: dodgerblue; font-size:14px;" class="uil fs-0 me-2 uil-cog"></i></div>';
                invrec_html_sales += '<div class="dropdown-menu" role="menu">';
                invrec_html_sales += '<a class="dropdown-item" href="#!" onclick="SalesApproveInvoice(\'' + blankForNull(value.InvoiceId) + '\',' + index + ',1);"><span style="color: forestgreen;"><i class="uil fs-0 me-2 uil-pen"></i></span>&nbsp;&nbsp;Approve</a>';
                invrec_html_sales += '</div></div></td>';

                invrec_html_sales += '<td style="text-wrap: nowrap; text-align:center;">' + (index + 1) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap; display:none;">' + blankForNull(value.InvoiceId) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.Month) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.Year) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceType) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceNo) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceDate) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.BillTo) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.Balance) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.Delay) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.DueDate) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.VendorName) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.ToCompany) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceCategory) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.InvoiceAmount) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.PaymentTerms) + '</td>';
                invrec_html_sales += '<td style="text-wrap: nowrap;">' + blankForNull(value.PaymentFromCompany) + '</td>';
                invrec_html_sales += '</tr>';
            });

            if ($.fn.dataTable.isDataTable('#invoiceapprovesales_saleapproved')) {
                $('#invoiceapprovesales_saleapproved').DataTable().destroy();
            }

            $('#invoiceapprovesales_saleapproved tbody').html(invrec_html_sales);

            $('#invoiceapprovesales_saleapproved').DataTable({
                dom: 'lftip',
                destroy: true,
                scrollX: false,
                paging: true,
                autoWidth: true,
                ordering: true,
                order: [[2, 'desc']], 
                processing: true,
                select: {
                    style: 'single'
                },
                initComplete: function () {
                    $('#invoiceapprovesales_saleapproved').wrap('<div class="dataTables_scroll" />');
                    $('#load1').hide();
                }
            });
        },
        error: function (error) {
            $('#load1').hide();
            console.error(error);
            alert('Error: ' + (error.responseText || error.statusText));
        }
    });

    return false;
}

function SalesApproveInvoice(InvoiceId, index) {
    var item = salesInvoiceData[index];
    if (!item) {
        console.error("Row data not found for index:", index);
        return;
    }

    var company = blankForNull(item.ToCompany || item.PaymentFromCompany || item.BillTo);
    var invoiceType = blankForNull(item.InvoiceType);
    var invoiceNo = blankForNull(item.InvoiceNo);
    var invoiceDate = blankForNull(item.InvoiceDate);
    var invoiceAmount = parseFloat(item.InvoiceAmount) || 0;

    document.getElementById("lblsalescompany").innerHTML = company;
    document.getElementById("lblsalesInvoiceType").innerHTML = invoiceType;
    document.getElementById("lblsalesInvoiceNo").innerHTML = invoiceNo;
    document.getElementById("lblsalesInvoiceDate").innerHTML = invoiceDate;
    document.getElementById("lblsalesInvoiceAmout").innerHTML = item.InvoiceAmount;

    document.getElementById("salesApproval_Status").value = "";

    document.getElementById("salesApproval_PaybleToVendor").value = invoiceAmount;
    document.getElementById("salesApproval_remark").value = "";

    window.popUp_InvID = InvoiceId;
    window.popUp_Company = company;
    window.popUp_InvType = invoiceType;

    $('#InvoiceApproval').modal('show');
}

function salesInvoiceApproval() {
    var remark = document.getElementById("salesApproval_remark").value.trim();
    if (remark === "") {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Remarks',
            text: 'Approve remarks should not be blank.',
            confirmButtonText: 'OK'
        });
        return false;
    }

    var ApStatus = document.getElementById("salesApproval_Status");
    var Status = ApStatus.options[ApStatus.selectedIndex].value;
    if (Status === "") {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Status',
            text: 'Status should not be blank.',
            confirmButtonText: 'OK'
        });
        return false;
    }

    var Approval_PaybleToVendor = document.getElementById("salesApproval_PaybleToVendor").value;

    $('#InvoiceApproval').modal('hide');
    $('#waitingpanel').modal('show');
    document.getElementById("spntext").innerHTML = "In process please wait.";

    PageMethods.SalesApprovalInvoice(
        window.popUp_InvID,
        remark,
        window.popUp_Company,
        window.popUp_InvType,
        Status,
        Approval_PaybleToVendor,
        OnsalesSuccessRemove,
        OnsalesErrorRemove
    );
    return false;
}

function OnsalesSuccessRemove(result) {
    $('#waitingpanel').modal('hide');

    if (result > 0) {
        window.popUp_InvID = 0;
        window.popUp_Company = '';
        window.popUp_InvType = '';
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: 'Invoice remark has been added successfully and email has been sent.',
            confirmButtonText: 'OK'
        }).then(() => {
            location.reload();
        });

        return false;
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Error occurred while approving Invoice. Please contact administrator.',
            confirmButtonText: 'OK'
        }).then(() => {
            location.reload();
        });

        return false;
    }
}

function OnsalesErrorRemove(error) {
    $('#waitingpanel').modal('hide');
    console.error(error);
    alert("Error: " + (error.get_message ? error.get_message() : "Unable to process approval."));
    return false;
}