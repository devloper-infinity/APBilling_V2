var discrepancySummaryTable = null;
var discrepancyDetailTable = null;
var discrepancyDetailRequestId = 0;

function discrepancyNormalizeKey(key) {
    return String(key).replace(/[^a-z0-9]/gi, "").toLowerCase();
}

function discrepancyValue(row, names) {
    if (!row) return "";
    var keys = Object.keys(row);
    for (var i = 0; i < names.length; i++) {
        var expected = discrepancyNormalizeKey(names[i]);
        for (var j = 0; j < keys.length; j++) {
            if (discrepancyNormalizeKey(keys[j]) === expected) {
                return row[keys[j]] == null ? "" : row[keys[j]];
            }
        }
    }
    return "";
}

function discrepancyEscapeHtml(value) {
    return String(value == null ? "" : value)
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/\"/g, "&quot;")
        .replace(/'/g, "&#39;");
}

function discrepancyCountButton(value, type) {
    var count = value == null || value === "" ? 0 : value;
    var label = type === "not-match" ? "Not Match With System" : type === "pending" ? "Pending Client Billing" : "Duplicate";
    return '<button type="button" class="discrepancy-count-link" data-discrepancy-type="' + type + '" aria-label="View ' + label + ' details">' + discrepancyEscapeHtml(count) + '</button>';
}

function showInvoice() {
    $("#load1").show();

    function finishRequest() {
        $("#load1").hide();
    }

    try {
        if (!window.PageMethods || typeof PageMethods.GetDiscripancyInvoice !== "function") {
            throw new Error("The invoice page method is unavailable.");
        }

        PageMethods.GetDiscripancyInvoice(function (result) {
            try {
                var rows = JSON.parse(result || "[]");
                $("#laminarCard").show();
                discrepancyRenderSummary(rows);
                discrepancySummaryTable.columns.adjust().draw(false);
            } catch (error) {
                alert("Unable to display the invoice details. Please try again.");
            } finally {
                finishRequest();
            }
        }, function () {
            finishRequest();
            alert("Unable to load invoice details. Please try again.");
        });
    } catch (error) {
        finishRequest();
        alert("Unable to start the invoice request. Please refresh the page and try again.");
    }
    return false;
}

function discrepancyRenderSummary(rows) {
    var data = $.map(rows || [], function (row, index) {
        return {
            serial: index + 1,
            invoiceId: discrepancyValue(row, ["InvoiceID", "InvoiceId"]),
            tableName: discrepancyValue(row, ["TableName", "Table Name"]),
            invoiceType: discrepancyValue(row, ["InvoiceType", "Invoice Type"]),
            invoiceNo: discrepancyValue(row, ["InvoiceNo", "Invoice Number", "Invoice No"]),
            invoiceDate: discrepancyValue(row, ["InvoiceDate", "Invoice Date"]),
            billTo: discrepancyValue(row, ["BillTo", "BilledTo", "Bill To", "Company"]),
            totalRecords: discrepancyValue(row, ["TotalRecords", "Total Records"]),
            matchWithLm: discrepancyValue(row, ["MatchWithLM", "Match With LM", "MatchWithSystem"]),
            notMatchWithLm: discrepancyValue(row, ["NotMatchWithLM", "Not Match With LM", "NotMatchWithSystem"]),
            duplicate: discrepancyValue(row, ["Duplicate"]),
            pendingToBilling: discrepancyValue(row, ["PendingClientBilling", "Pending For Billing", "PendingToBilling"]),
            source: row
        };
    });

    if ($.fn.dataTable.isDataTable("#table_RemoteUWLaminr")) {
        $("#table_RemoteUWLaminr").DataTable().clear().destroy();
    }

    discrepancySummaryTable = $("#table_RemoteUWLaminr").DataTable({
        data: data,
        destroy: true,
        paging: true,
        searching: true,
        ordering: false,
        responsive: false,
        scrollX: true,
        scrollXInner: "1500px",
        autoWidth: false,
        pageLength: 10,
        columnDefs: [{ targets: [1, 2], visible: false }],
        language: { emptyTable: "No invoice records found." },
        columns: [
            { data: "serial" },
            { data: "invoiceId", defaultContent: "" },
            { data: "tableName", defaultContent: "" },
            { data: "invoiceType", defaultContent: "" },
            { data: "invoiceNo", defaultContent: "" },
            { data: "invoiceDate", defaultContent: "" },
            { data: "billTo", defaultContent: "" },
            { data: "totalRecords", defaultContent: "" },
            { data: "matchWithLm", defaultContent: "" },
            { data: "notMatchWithLm", render: function (value) { return discrepancyCountButton(value, "not-match"); } },
            { data: "duplicate", render: function (value) { return discrepancyCountButton(value, "duplicate"); } },
            { data: "pendingToBilling", render: function (value) { return discrepancyCountButton(value, "pending"); } }
        ]
    });
}

function bindDiscrepancyDetailTable(columnType, selectedRow) {
    var title;
    var status;
    if (columnType === "duplicate") {
        title = "Duplicate";
        status = "Duplicate";
    } else if (columnType === "not-match") {
        title = "Not Match With System";
        status = "NotMatched";
    } else {
        title = "Pending Client Billing";
        status = "Pending";
    }
    var invoiceId = parseInt(discrepancyValue(selectedRow, ["InvoiceID", "InvoiceId"]), 10) || 0;
    var invoiceType = discrepancyValue(selectedRow, ["InvoiceType", "Invoice Type"]);
    var tableName = discrepancyValue(selectedRow, ["TableName", "Table Name"]);
    var requestId = ++discrepancyDetailRequestId;

    $("#discrepancyDetailTitle").text(title + " details");
    $("#discrepancyDetailSubtitle").text("Selected invoice summary");
    $("#detailInvoiceType").text(discrepancyValue(selectedRow, ["InvoiceType", "Invoice Type"]) || "—");
    $("#detailInvoiceNo").text(discrepancyValue(selectedRow, ["InvoiceNo", "Invoice Number", "Invoice No"]) || "—");
    $("#detailBilledTo").text(discrepancyValue(selectedRow, ["Company", "BillTo", "BilledTo", "Bill To"]) || "—");

    $("#discrepancyDetailTable thead tr").empty();
    $("#discrepancyDetailTable tbody").empty();
    $("#discrepancyDetailTable thead tr").append("<th>Loading details...</th>");
    if ($.fn.dataTable.isDataTable("#discrepancyDetailTable")) {
        $("#discrepancyDetailTable").DataTable().destroy();
    }

    if (!invoiceId) {
        discrepancyRenderDetailRows([], "Unable to identify the selected invoice.");
        return;
    }

    PageMethods.GetLoansForReconcillation(invoiceId, status, invoiceType, tableName, function (result) {
        if (requestId !== discrepancyDetailRequestId) return;
        try {
            discrepancyRenderDetailRows(JSON.parse(result || "[]"), null, {
                invoiceId: invoiceId,
                invoiceType: invoiceType,
                tableName: tableName
            });
        } catch (error) {
            discrepancyRenderDetailRows([], "Unable to read the loan details.");
        }
    }, function () {
        if (requestId === discrepancyDetailRequestId) {
            discrepancyRenderDetailRows([], "Unable to load the loan details.");
        }
    });
}

function discrepancyRenderDetailRows(rows, emptyMessage, invoiceContext) {
    invoiceContext = invoiceContext || {};
    rows = $.map(rows || [], function (row) {
        row.__discrepancyUniqueId = discrepancyValue(row, ["ID", "UniqueID", "UniqueId"]);
        row.__discrepancyInvoiceId = invoiceContext.invoiceId || "";
        row.__discrepancyInvoiceType = invoiceContext.invoiceType || "";
        row.__discrepancyTableName = invoiceContext.tableName || "";
        return row;
    });
    var $header = $("#discrepancyDetailTable thead tr").empty();
    var detailFields = [
        { label: "Loan No", keys: ["LoanNo", "Loan No", "LoanNumber"] },
        { label: "Completed Date", keys: ["CompletedDate", "Completed Date", "DeliveryDate"] },
        { label: "System Remark", keys: ["SysRemark", "SystemRemark", "System Remark"] },
        { label: "Duplicate", keys: ["IsDuplicate"] },
        { label: "Client Billing", keys: ["ClientBilling", "ClientBillingPeriod", "Client Billing Period"] }
    ];
    var columns = detailFields.map(function (field) {
        $header.append($('<th></th>').text(field.label));
        return {
            data: null,
            defaultContent: "",
            render: function (value, type, row) {
                var cellValue = discrepancyValue(row, field.keys);
                if (type !== "display") return cellValue;
                return discrepancyEscapeHtml(cellValue);
            }
        };
    });

    $header.append($('<th></th>').text("User Remark"));
    columns.push({
        data: null,
        orderable: false,
        searchable: false,
        render: function (value, type, row) {
            if (type !== "display") return "";
            return '<textarea class="form-control discrepancy-user-remark" rows="2" maxlength="5000" aria-label="User remark"></textarea>';
        }
    });

    $header.append($('<th></th>').text("Action"));
    columns.push({
        data: null,
        orderable: false,
        searchable: false,
        render: function (value, type) {
            if (type !== "display") return "";
            return '<button type="button" class="btn btn-sm btn-primary discrepancy-update-remark">Update remark</button>';
        }
    });

    $header.append($('<th></th>').text("Unique ID"));
    columns.push({
        data: "__discrepancyUniqueId",
        defaultContent: "",
        orderable: false,
        searchable: false
    });

    if ($.fn.dataTable.isDataTable("#discrepancyDetailTable")) {
        $("#discrepancyDetailTable").DataTable().destroy();
    }
    discrepancyDetailTable = $("#discrepancyDetailTable").DataTable({
        data: rows,
        columns: columns,
        columnDefs: [{ targets: columns.length - 1, visible: false }],
        paging: true,
        searching: true,
        ordering: false,
        responsive: false,
        scrollX: true,
        scrollY: "50vh",
        scrollCollapse: true,
        scrollXInner: "1250px",
        autoWidth: false,
        pageLength: 10,
        lengthChange: true,
        language: {
            emptyTable: emptyMessage || "No loan details were found for this discrepancy."
        }
    });
}

$(document).ready(function () {
    // Keep the shared full-screen loader outside transformed content containers.
    $("#load1").appendTo(document.body);
    showInvoice();

    $(document).on("click", "#table_RemoteUWLaminr .discrepancy-count-link", function () {
        var row = discrepancySummaryTable.row($(this).closest("tr")).data();
        if (!row) return;
        bindDiscrepancyDetailTable($(this).data("discrepancy-type"), row.source);
        $("#discrepancyDetailModal").modal("show");
    });

    $(document).on("click", "#discrepancyDetailTable .discrepancy-update-remark", function () {
        var button = this;
        var tableRow = discrepancyDetailTable.row($(button).closest("tr"));
        var row = tableRow.data();
        var uniqueId = parseInt(row && row.__discrepancyUniqueId, 10) || 0;
        var invoiceId = parseInt(row && row.__discrepancyInvoiceId, 10) || 0;
        var remark = $(button).closest("tr").find(".discrepancy-user-remark").val();

        if (!invoiceId || !uniqueId || !row.__discrepancyInvoiceType || !row.__discrepancyTableName) {
            alert("This loan row is missing invoice details, so its remark cannot be updated.");
            return;
        }
        if (!window.PageMethods || typeof PageMethods.UpdateDiscrepancyRemark !== "function") {
            alert("The remark update page method is not available.");
            return;
        }

        $(button).prop("disabled", true).text("Saving...");
        PageMethods.UpdateDiscrepancyRemark(invoiceId, uniqueId, row.__discrepancyInvoiceType, row.__discrepancyTableName, remark, function (saved) {
            $(button).prop("disabled", false).text("Update remark");
            if (!saved) {
                alert("The remark was not saved. Please check the invoice details and try again.");
                return;
            }

            row.UserRemark = remark;
            tableRow.data(row).invalidate().draw(false);
            alert("Remark updated successfully.");
        }, function () {
            $(button).prop("disabled", false).text("Update remark");
            alert("Unable to update the remark. Please try again.");
        });
    });

    $("#discrepancyDetailModal").on("shown.bs.modal", function () {
        if ($.fn.dataTable.isDataTable("#discrepancyDetailTable")) {
            $("#discrepancyDetailTable").DataTable().columns.adjust();
        }
    });
});
