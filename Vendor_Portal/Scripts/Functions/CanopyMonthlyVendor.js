function dashboard_arBind_displayERP() {
    $('#load1').show();
    var columns = [];
    var columnlink = [];
    var preheader = '';
    var headers = [];
    var returnstring = '';
    var htmlheader = '';
    var htmlrow = '';
    $.ajax({
        url: "CanopyMonthlyVendorSummary.aspx/GetDataMonthlyVendorData_Canopy",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//
            var firstData = dataArray[0];
            var rowData = dataArray[1];
            var ColumnNames = Object.keys(firstData);
            var rowValues = Object.keys(rowData);
            var groupindex = 0
            htmlheader = '<thead><tr>';
            var thead = document.createElement("thead");
            var trh = document.createElement("tr");
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    var tbh = document.createElement("th");
                    tbh.rowSpan = 2;
                    tbh.style.fontWeight = "bold!important";
                    tbh.style.verticalAlign = "middle";
                    tbh.id = i + "_ClientLabel";
                    tbh.style.left = "0px!important";
                    //tbh.appendChild(label);
                    tbh.innerHTML = ColumnNames[i];
                    trh.appendChild(tbh);

                    //thead.appendChild(trh);
                    //htmlheader = htmlheader + '<th style="left: 0px!important;">Client</th>';
                }
                else if (i > 1) {
                    if (i % 3 == 0) {
                        var tbh = document.createElement("th");
                        tbh.colSpan = 3;
                        tbh.style.textAlign = "center";
                        tbh.innerHTML = ColumnNames[i].replace("-LoanCount", "").replace("-Amount", "");
                        trh.appendChild(tbh);
                        //thead.appendChild(trh);
                        //htmlheader = htmlheader + '<th colspan=2 style="padding-left:20px;left: 0px!important; text-align:center;">' + ColumnNames[i].replace("-LoanCount", "").replace("-Amount", "") + '</th>';
                        i = i + 1;
                    }
                    //else
                    //    htmlheader = htmlheader + '<th style="padding-left:20px;left: 0px!important; text-align:center;">' + ColumnNames[i] + '</th>';
                }
                else {
                    var tbh = document.createElement("th");
                    //tbh.style.display = 'none';
                    tbh.innerHTML = ColumnNames[i];
                    //htmlheader = htmlheader + '<th style="display:none;">' + ColumnNames[i] + '</th>';
                    trh.appendChild(tbh);
                    //thead.appendChild(trh);
                }
            }
            thead.appendChild(trh);
            $('#dashboard_ar').append(thead);
            var trh1 = document.createElement("tr");
            //htmlheader = htmlheader + '</tr><tr>';
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    //var tbr = document.createElement("th");
                    //tbr.innerHTML = ColumnNames[i];
                    //tbr.style.textAlign = "center";
                    //trh1.appendChild(tbr);
                    ////htmlheader = htmlheader + '<th style="left: 0px!important; ">' + ColumnNames[i] + '</th>';
                }
                else if (i > 1) {
                    // if (i % 3 != 0) {
                    
                    if (groupindex == 0) {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'Count';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                        groupindex++;
                        

                    }
                    else if (groupindex == 1) {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'US $';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                        groupindex++;

                    }
                    else {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'Avg Rate';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                        groupindex++;
                        if (groupindex == 3) {
                            groupindex = 0;
                        }
                    }


                    //}
                    //else {

                    //}
                }
                else {
                    var tbr = document.createElement("th");
                    //tbr.style.display = 'none';
                    tbr.innerHTML = ColumnNames[i];

                    //htmlheader = htmlheader + '<th style="display:none;">' + ColumnNames[i] + '</th>';
                    trh1.appendChild(tbr);
                }
            }
            thead.appendChild(trh1);
            $('#dashboard_ar').append(thead);
            var tbody = document.createElement("tbody");
            //$('#dashboard_ar').html("");
            //htmlheader = htmlheader + '</tr></thead>';
            //htmlrow = '<tbody>';
            $.each(dataArray, function (index, item) {
                var tr1 = document.createElement("tr");
                //htmlrow = htmlrow + '<tr>';
                for (var i = 1; i < ColumnNames.length; i++) {
                    var targetColumnName = ColumnNames[i];
                    if (i == 1 || i == 2) {
                        var td = document.createElement("td");
                        //var label = document.createElement("label");
                        //label.id = i + "_" + item[targetColumnName].replace(" ", "") + "ClientLabel";
                        td.innerHTML = item[targetColumnName];
                        //if (ColumnNames[i] == "Process")
                        //    label.style.width = "180px";
                        //else
                        //    label.style.width = "250px";
                        ///*label.style.width = "250px";*/
                        //td.appendChild(label);
                        tr1.appendChild(td);
                    }
                    else if (i > 1) {
                        //if (i % 2 == 0) {
                        var td = document.createElement("td");
                        td.style.textAlign = "center";
                        //if (targetColumnName].includes("Amount")) {
                        //    td.innerHTML = "" + formattedAmount.toLocaleString("en-US", { style: "currency", currency: "USD" });
                        //}
                        //else
                        td.innerHTML = item[targetColumnName];
                        //td.innerHTML = '<input type="text" id="loancount_' + targetColumnName + '_' + index + '" onchange="return getValue(this,' + index + ');" style="width:70px;" value="' + blankForNull(item[targetColumnName]) + '"></input>';
                        tr1.appendChild(td);
                        //}
                        //else {
                        //    var td = document.createElement("td");
                        //    td.style.textAlign = "center";
                        //    td.innerHTML = '<input type="text" id="loancount_' + targetColumnName + '_' + index + '" onchange="return getValue(this,' + index + ');" style="width:50px;" value="' + blankForNull(item[targetColumnName]) + '"></input>';
                        //    tr1.appendChild(td);
                        //}
                    }
                    else {
                        var td = document.createElement("td");
                        //td.style.display = 'none';
                        td.innerHTML = item[targetColumnName];
                        tr1.appendChild(td);
                    }
                }
                tbody.appendChild(tr1);
                $('#dashboard_ar').append(tbody);
            });


            //Footer
            var tfoot = document.createElement("tfoot");
            var trf1 = document.createElement("tr");
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    var tbr = document.createElement("th");
                    tbr.style.textAlign = "center";
                    trf1.appendChild(tbr);
                }
                else if (i > 1) {
                    var tbr = document.createElement("th");
                    tbr.style.textAlign = "center";
                    trf1.appendChild(tbr);
                }
                else {
                    var tbr = document.createElement("th");
                    // tbr.style.display = 'none';
                    trf1.appendChild(tbr);
                }
            }
            tfoot.appendChild(trf1);
            $('#dashboard_ar').append(tfoot);

            $('#dashboard_ar').DataTable({
                dom: 'Bft',
                destroy: true,
                orderCellsTop: true,
                fixedColumns: {
                    leftColumns: 2,
                },
                scrollCollapse: false,
                /*  scrollY: '400px',*/
                scrollX: true,
                "paging": false,
                "autoWidth": true,
                select: true,
                "ordering": false,
                filter: true,
                'select': {
                    'style': 'single'
                },
                "serverSide": false,

                initComplete: function () {
                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5',
                        text: 'Export to Excel',
                        filename: 'Monthwise Summary',
                        exportOptions: {
                            columns: ':visible',
                            modifier: { header: false }
                        },
                        customize: function (xlsx) {
                            const sheetDoc = xlsx.xl.worksheets['sheet1.xml'];
                            const worksheet = sheetDoc.documentElement;
                            const sheetData = worksheet.getElementsByTagName('sheetData')[0];
                            const $worksheet = $(worksheet);


                            // Remove all existing rows (clean slate)
                            $worksheet.find('row').remove();

                            // Remove existing mergeCells if any
                            $worksheet.find('mergeCells').remove();

                            function colLetter(n) {
                                let s = '', t;
                                while (n > 0) {
                                    t = (n - 1) % 26;
                                    s = String.fromCharCode(65 + t) + s;
                                    n = Math.floor((n - 1) / 26);
                                }
                                return s;
                            }

                            const occupied = {};
                            const mergeRanges = [];

                            const stylesDoc = xlsx.xl['styles.xml'];
                            const fonts = stylesDoc.getElementsByTagName('fonts')[0];
                            const borders = stylesDoc.getElementsByTagName('borders')[0];
                            const cellXfs = stylesDoc.getElementsByTagName('cellXfs')[0];

                            // --- Create bold font for header
                            const headerFont = stylesDoc.createElement('font');
                            const bold = stylesDoc.createElement('b');
                            const color = stylesDoc.createElement('color');
                            color.setAttribute('rgb', 'FF000000'); // black
                            headerFont.appendChild(bold);
                            headerFont.appendChild(color);
                            fonts.appendChild(headerFont);
                            const headerFontId = fonts.childNodes.length - 1;
                            fonts.setAttribute('count', fonts.childNodes.length.toString());

                            // --- Create border for all cells
                            const border = stylesDoc.createElement('border');
                            ['left', 'right', 'top', 'bottom'].forEach(side => {
                                const sideElem = stylesDoc.createElement(side);
                                sideElem.setAttribute('style', 'thin');
                                const colorElem = stylesDoc.createElement('color');
                                colorElem.setAttribute('auto', '1');
                                sideElem.appendChild(colorElem);
                                border.appendChild(sideElem);
                            });

                            const headerStyle = stylesDoc.createElement('xf');
                            headerStyle.setAttribute('numFmtId', '0');
                            headerStyle.setAttribute('fontId', headerFontId.toString());
                            headerStyle.setAttribute('fillId', '0');
                            headerStyle.setAttribute('xfId', '0');
                            headerStyle.setAttribute('applyFont', '1');
                            headerStyle.setAttribute('applyBorder', '1');
                            headerStyle.setAttribute('applyAlignment', '1');

                            // Add alignment child
                            const alignment = stylesDoc.createElement('alignment');
                            alignment.setAttribute('horizontal', 'center');
                            alignment.setAttribute('vertical', 'center');
                            alignment.setAttribute('wrapText', '1');
                            headerStyle.appendChild(alignment);

                            cellXfs.appendChild(headerStyle);

                            // Append the new xf
                            //cellXfs.appendChild(headerAlignXf);
                            const headerStyleIndex = cellXfs.childNodes.length - 1;
                            cellXfs.setAttribute('count', cellXfs.childNodes.length.toString());
                            // Start rowIndex at 1 to insert headers at the top
                            let rowIndex = 1;

                            // 1. Add header rows from thead
                            $('#dashboard_ar thead tr').each(function () {
                                let colIndex = 1;
                                const trElm = sheetDoc.createElement('row');
                                trElm.setAttribute('r', rowIndex);

                                $(this).children('th, td').each(function () {
                                    const $cell = $(this);
                                    const colspan = parseInt($cell.attr('colspan')) || 1;
                                    const rowspan = parseInt($cell.attr('rowspan')) || 1;

                                    while (occupied[rowIndex + '-' + colIndex]) {
                                        colIndex++;
                                    }

                                    const startCol = colIndex;
                                    const endCol = colIndex + colspan - 1;
                                    const endRow = rowIndex + rowspan - 1;

                                    const cellRef = colLetter(startCol) + rowIndex;
                                    const c = sheetDoc.createElement('c');
                                    c.setAttribute('r', cellRef);
                                    c.setAttribute('t', 'str');
                                    c.setAttribute('s', headerStyleIndex.toString());

                                    const v = sheetDoc.createElement('v');
                                    let cellText = $cell.text().trim().replace(/\s+/g, ' ');
                                    if (!cellText) cellText = ' ';
                                    v.textContent = cellText;
                                    c.appendChild(v);
                                    trElm.appendChild(c);

                                    for (let rr = rowIndex; rr <= endRow; rr++) {
                                        for (let cc = startCol; cc <= endCol; cc++) {
                                            occupied[rr + '-' + cc] = true;
                                        }
                                    }

                                    if (colspan > 1 || rowspan > 1) {
                                        mergeRanges.push(colLetter(startCol) + rowIndex + ':' + colLetter(endCol) + endRow);
                                    }

                                    colIndex += colspan;
                                });

                                sheetData.appendChild(trElm);
                                rowIndex++;
                            });

                            const dataStyle = stylesDoc.createElement('xf');
                            dataStyle.setAttribute('numFmtId', '0');
                            dataStyle.setAttribute('fontId', '0'); // default font
                            dataStyle.setAttribute('fillId', '0');
                            dataStyle.setAttribute('xfId', '0');
                            dataStyle.setAttribute('applyAlignment', '1');

                            const dataAlignment = stylesDoc.createElement('alignment');
                            dataAlignment.setAttribute('horizontal', 'center');
                            dataAlignment.setAttribute('vertical', 'center');
                            dataAlignment.setAttribute('wrapText', '1');
                            dataStyle.appendChild(dataAlignment);

                            cellXfs.appendChild(dataStyle);
                            const centerStyleIndex = cellXfs.childNodes.length - 1;
                            cellXfs.setAttribute('count', cellXfs.childNodes.length.toString());

                            // 2. Add data rows from tbody
                            $('#dashboard_ar tbody tr').each(function () {
                                let colIndex = 1;
                                const trElm = sheetDoc.createElement('row');
                                trElm.setAttribute('r', rowIndex);

                                $(this).children('td').each(function () {
                                    const $cell = $(this);
                                    const colspan = parseInt($cell.attr('colspan')) || 1;
                                    const rowspan = parseInt($cell.attr('rowspan')) || 1;

                                    while (occupied[rowIndex + '-' + colIndex]) {
                                        colIndex++;
                                    }

                                    const startCol = colIndex;
                                    const endCol = colIndex + colspan - 1;
                                    const endRow = rowIndex + rowspan - 1;

                                    const cellRef = colLetter(startCol) + rowIndex;
                                    const c = sheetDoc.createElement('c');
                                    c.setAttribute('r', cellRef);
                                    c.setAttribute('t', 'str');
                                    if (colIndex > 2)
                                        c.setAttribute('s', centerStyleIndex.toString());

                                    const v = sheetDoc.createElement('v');
                                    let cellText = $cell.text().trim().replace(/\s+/g, ' ');
                                    if (!cellText) cellText = ' ';
                                    v.textContent = cellText;
                                    c.appendChild(v);
                                    trElm.appendChild(c);

                                    for (let rr = rowIndex; rr <= endRow; rr++) {
                                        for (let cc = startCol; cc <= endCol; cc++) {
                                            occupied[rr + '-' + cc] = true;
                                        }
                                    }

                                    if (colspan > 1 || rowspan > 1) {
                                        mergeRanges.push(colLetter(startCol) + rowIndex + ':' + colLetter(endCol) + endRow);
                                    }

                                    colIndex += colspan;
                                });

                                sheetData.appendChild(trElm);
                                rowIndex++;
                            });

                            // Add mergeCells element if any merges needed
                            if (mergeRanges.length > 0) {
                                const mergeCells = sheetDoc.createElement('mergeCells');
                                mergeCells.setAttribute('count', mergeRanges.length);

                                mergeRanges.forEach(range => {
                                    const mergeCell = sheetDoc.createElement('mergeCell');
                                    mergeCell.setAttribute('ref', range);
                                    mergeCells.appendChild(mergeCell);
                                });

                                if (sheetData.nextSibling) {
                                    worksheet.insertBefore(mergeCells, sheetData.nextSibling);
                                } else {
                                    worksheet.appendChild(mergeCells);
                                }
                            }
                        }




                    },
                ],
                footerCallback: function (row, data, start, end, display) {
                    let api = this.api();

                    // Remove the formatting to get integer data for summation
                    let intVal = function (i) {
                        return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
                    };
                    for (var i = 2; i < api.columns().count(); i++) {

                        

                        var totalLoan = api.column(i, { page: 'current' }).data().reduce((a, b) => intVal(a) + intVal(b), 0);
                        var totalAmt = api.column(i, { page: 'current' }).data().reduce((a, b) => intVal(a) + intVal(b), 0);

                        api.column(i).footer().innerHTML = Number(totalLoan).toFixed(2);
                        api.column(i).footer().innerHTML = Number(totalAmt).toFixed(2);

                        var pos = (i - 2) % 3;

                        if (pos === 0) { // COUNT
                            document.getElementById("lbl_" + i).innerHTML =
                                "Count (" + new Intl.NumberFormat().format(totalLoan) + ")";
                        }
                        else if (pos === 1) { // AMOUNT
                            document.getElementById("lbl_" + i).innerHTML =
                                "Amount ($ " + new Intl.NumberFormat().format(totalAmt.toFixed(2)) + ")";
                        }
                        //else { // BALANCE
                        //    document.getElementById("lbl_" + i).innerHTML =
                        //        "AvgRate ($ " + new Intl.NumberFormat().format(totalLoan.toFixed(2)) + ")";
                        //}

                        //if (i % 3 == 0)
                        //    document.getElementById("lbl_" + (i)).innerHTML = "Count (" + new Intl.NumberFormat().format(Number(totalLoan)) + ")";
                        //else
                        //    document.getElementById("lbl_" + (i)).innerHTML = "US $ (" + new Intl.NumberFormat().format(Number(totalAmt).toFixed(2)) + ")";

                        //document.getElementById("lbl_" + (i)).innerHTML = "(" + Number(totalLoan).toFixed(2) + ")";
                        //document.getElementById("lbl_" + (i)).innerHTML = "(" + Number(totalAmt).toFixed(2) + ")";


                    }
                }


            });

        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    return false;
}

function dashboard_arBind_displayERP_IPS() {
    $('#load1').show();
    var columns = [];
    var columnlink = [];
    var preheader = '';
    var headers = [];
    var returnstring = '';
    var htmlheader = '';
    var htmlrow = '';
    $.ajax({
        url: "CanopyMonthlyVendorSummary.aspx/GetDataMonthlyVendorData_IPS",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//
            var firstData = dataArray[0];
            var rowData = dataArray[1];
            var ColumnNames = Object.keys(firstData);
            var rowValues = Object.keys(rowData);
            var groupindex = 0
            htmlheader = '<thead><tr>';
            var thead = document.createElement("thead");
            var trh = document.createElement("tr");
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    var tbh = document.createElement("th");
                    tbh.rowSpan = 2;
                    tbh.style.fontWeight = "bold!important";
                    tbh.style.verticalAlign = "middle";
                    tbh.id = i + "_ClientLabel";
                    tbh.style.left = "0px!important";
                    //tbh.appendChild(label);
                    tbh.innerHTML = ColumnNames[i];
                    trh.appendChild(tbh);

                    //thead.appendChild(trh);
                    //htmlheader = htmlheader + '<th style="left: 0px!important;">Client</th>';
                }
                else if (i > 1) {
                    if (i % 3 == 0) {
                        var tbh = document.createElement("th");
                        tbh.colSpan = 3;
                        tbh.style.textAlign = "center";
                        tbh.innerHTML = ColumnNames[i].replace("-LoanCount", "").replace("-Amount", "");
                        trh.appendChild(tbh);
                        //thead.appendChild(trh);
                        //htmlheader = htmlheader + '<th colspan=2 style="padding-left:20px;left: 0px!important; text-align:center;">' + ColumnNames[i].replace("-LoanCount", "").replace("-Amount", "") + '</th>';
                        i = i + 1;
                    }
                    //else
                    //    htmlheader = htmlheader + '<th style="padding-left:20px;left: 0px!important; text-align:center;">' + ColumnNames[i] + '</th>';
                }
                else {
                    var tbh = document.createElement("th");
                    //tbh.style.display = 'none';
                    tbh.innerHTML = ColumnNames[i];
                    //htmlheader = htmlheader + '<th style="display:none;">' + ColumnNames[i] + '</th>';
                    trh.appendChild(tbh);
                    //thead.appendChild(trh);
                }
            }
            thead.appendChild(trh);
            $('#dashboard_ar').append(thead);
            var trh1 = document.createElement("tr");
            //htmlheader = htmlheader + '</tr><tr>';
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    //var tbr = document.createElement("th");
                    //tbr.innerHTML = ColumnNames[i];
                    //tbr.style.textAlign = "center";
                    //trh1.appendChild(tbr);
                    ////htmlheader = htmlheader + '<th style="left: 0px!important; ">' + ColumnNames[i] + '</th>';
                }
                else if (i > 1) {
                    // if (i % 3 != 0) {

                    if (groupindex == 0) {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'Count';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                        groupindex++;


                    }
                    else if (groupindex == 1) {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'US $';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                        groupindex++;

                    }
                    else {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'Avg Rate';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                        groupindex++;
                        if (groupindex == 3) {
                            groupindex = 0;
                        }
                    }


                    //}
                    //else {

                    //}
                }
                else {
                    var tbr = document.createElement("th");
                    //tbr.style.display = 'none';
                    tbr.innerHTML = ColumnNames[i];

                    //htmlheader = htmlheader + '<th style="display:none;">' + ColumnNames[i] + '</th>';
                    trh1.appendChild(tbr);
                }
            }
            thead.appendChild(trh1);
            $('#dashboard_ar').append(thead);
            var tbody = document.createElement("tbody");
            //$('#dashboard_ar').html("");
            //htmlheader = htmlheader + '</tr></thead>';
            //htmlrow = '<tbody>';
            $.each(dataArray, function (index, item) {
                var tr1 = document.createElement("tr");
                //htmlrow = htmlrow + '<tr>';
                for (var i = 1; i < ColumnNames.length; i++) {
                    var targetColumnName = ColumnNames[i];
                    if (i == 1 || i == 2) {
                        var td = document.createElement("td");
                        //var label = document.createElement("label");
                        //label.id = i + "_" + item[targetColumnName].replace(" ", "") + "ClientLabel";
                        td.innerHTML = item[targetColumnName];
                        //if (ColumnNames[i] == "Process")
                        //    label.style.width = "180px";
                        //else
                        //    label.style.width = "250px";
                        ///*label.style.width = "250px";*/
                        //td.appendChild(label);
                        tr1.appendChild(td);
                    }
                    else if (i > 1) {
                        //if (i % 2 == 0) {
                        var td = document.createElement("td");
                        td.style.textAlign = "center";
                        //if (targetColumnName].includes("Amount")) {
                        //    td.innerHTML = "" + formattedAmount.toLocaleString("en-US", { style: "currency", currency: "USD" });
                        //}
                        //else
                        td.innerHTML = item[targetColumnName];
                        //td.innerHTML = '<input type="text" id="loancount_' + targetColumnName + '_' + index + '" onchange="return getValue(this,' + index + ');" style="width:70px;" value="' + blankForNull(item[targetColumnName]) + '"></input>';
                        tr1.appendChild(td);
                        //}
                        //else {
                        //    var td = document.createElement("td");
                        //    td.style.textAlign = "center";
                        //    td.innerHTML = '<input type="text" id="loancount_' + targetColumnName + '_' + index + '" onchange="return getValue(this,' + index + ');" style="width:50px;" value="' + blankForNull(item[targetColumnName]) + '"></input>';
                        //    tr1.appendChild(td);
                        //}
                    }
                    else {
                        var td = document.createElement("td");
                        //td.style.display = 'none';
                        td.innerHTML = item[targetColumnName];
                        tr1.appendChild(td);
                    }
                }
                tbody.appendChild(tr1);
                $('#dashboard_ar').append(tbody);
            });


            //Footer
            var tfoot = document.createElement("tfoot");
            var trf1 = document.createElement("tr");
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    var tbr = document.createElement("th");
                    tbr.style.textAlign = "center";
                    trf1.appendChild(tbr);
                }
                else if (i > 1) {
                    var tbr = document.createElement("th");
                    tbr.style.textAlign = "center";
                    trf1.appendChild(tbr);
                }
                else {
                    var tbr = document.createElement("th");
                    // tbr.style.display = 'none';
                    trf1.appendChild(tbr);
                }
            }
            tfoot.appendChild(trf1);
            $('#dashboard_ar').append(tfoot);

            $('#dashboard_ar').DataTable({
                dom: 'Bft',
                destroy: true,
                orderCellsTop: true,
                fixedColumns: {
                    leftColumns: 2,
                },
                scrollCollapse: false,
                /*  scrollY: '400px',*/
                scrollX: true,
                "paging": false,
                "autoWidth": true,
                select: true,
                "ordering": false,
                filter: true,
                'select': {
                    'style': 'single'
                },
                "serverSide": false,

                initComplete: function () {
                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5',
                        text: 'Export to Excel',
                        filename: 'Monthwise Summary',
                        exportOptions: {
                            columns: ':visible',
                            modifier: { header: false }
                        },
                        customize: function (xlsx) {
                            const sheetDoc = xlsx.xl.worksheets['sheet1.xml'];
                            const worksheet = sheetDoc.documentElement;
                            const sheetData = worksheet.getElementsByTagName('sheetData')[0];
                            const $worksheet = $(worksheet);


                            // Remove all existing rows (clean slate)
                            $worksheet.find('row').remove();

                            // Remove existing mergeCells if any
                            $worksheet.find('mergeCells').remove();

                            function colLetter(n) {
                                let s = '', t;
                                while (n > 0) {
                                    t = (n - 1) % 26;
                                    s = String.fromCharCode(65 + t) + s;
                                    n = Math.floor((n - 1) / 26);
                                }
                                return s;
                            }

                            const occupied = {};
                            const mergeRanges = [];

                            const stylesDoc = xlsx.xl['styles.xml'];
                            const fonts = stylesDoc.getElementsByTagName('fonts')[0];
                            const borders = stylesDoc.getElementsByTagName('borders')[0];
                            const cellXfs = stylesDoc.getElementsByTagName('cellXfs')[0];

                            // --- Create bold font for header
                            const headerFont = stylesDoc.createElement('font');
                            const bold = stylesDoc.createElement('b');
                            const color = stylesDoc.createElement('color');
                            color.setAttribute('rgb', 'FF000000'); // black
                            headerFont.appendChild(bold);
                            headerFont.appendChild(color);
                            fonts.appendChild(headerFont);
                            const headerFontId = fonts.childNodes.length - 1;
                            fonts.setAttribute('count', fonts.childNodes.length.toString());

                            // --- Create border for all cells
                            const border = stylesDoc.createElement('border');
                            ['left', 'right', 'top', 'bottom'].forEach(side => {
                                const sideElem = stylesDoc.createElement(side);
                                sideElem.setAttribute('style', 'thin');
                                const colorElem = stylesDoc.createElement('color');
                                colorElem.setAttribute('auto', '1');
                                sideElem.appendChild(colorElem);
                                border.appendChild(sideElem);
                            });

                            const headerStyle = stylesDoc.createElement('xf');
                            headerStyle.setAttribute('numFmtId', '0');
                            headerStyle.setAttribute('fontId', headerFontId.toString());
                            headerStyle.setAttribute('fillId', '0');
                            headerStyle.setAttribute('xfId', '0');
                            headerStyle.setAttribute('applyFont', '1');
                            headerStyle.setAttribute('applyBorder', '1');
                            headerStyle.setAttribute('applyAlignment', '1');

                            // Add alignment child
                            const alignment = stylesDoc.createElement('alignment');
                            alignment.setAttribute('horizontal', 'center');
                            alignment.setAttribute('vertical', 'center');
                            alignment.setAttribute('wrapText', '1');
                            headerStyle.appendChild(alignment);

                            cellXfs.appendChild(headerStyle);

                            // Append the new xf
                            //cellXfs.appendChild(headerAlignXf);
                            const headerStyleIndex = cellXfs.childNodes.length - 1;
                            cellXfs.setAttribute('count', cellXfs.childNodes.length.toString());
                            // Start rowIndex at 1 to insert headers at the top
                            let rowIndex = 1;

                            // 1. Add header rows from thead
                            $('#dashboard_ar thead tr').each(function () {
                                let colIndex = 1;
                                const trElm = sheetDoc.createElement('row');
                                trElm.setAttribute('r', rowIndex);

                                $(this).children('th, td').each(function () {
                                    const $cell = $(this);
                                    const colspan = parseInt($cell.attr('colspan')) || 1;
                                    const rowspan = parseInt($cell.attr('rowspan')) || 1;

                                    while (occupied[rowIndex + '-' + colIndex]) {
                                        colIndex++;
                                    }

                                    const startCol = colIndex;
                                    const endCol = colIndex + colspan - 1;
                                    const endRow = rowIndex + rowspan - 1;

                                    const cellRef = colLetter(startCol) + rowIndex;
                                    const c = sheetDoc.createElement('c');
                                    c.setAttribute('r', cellRef);
                                    c.setAttribute('t', 'str');
                                    c.setAttribute('s', headerStyleIndex.toString());

                                    const v = sheetDoc.createElement('v');
                                    let cellText = $cell.text().trim().replace(/\s+/g, ' ');
                                    if (!cellText) cellText = ' ';
                                    v.textContent = cellText;
                                    c.appendChild(v);
                                    trElm.appendChild(c);

                                    for (let rr = rowIndex; rr <= endRow; rr++) {
                                        for (let cc = startCol; cc <= endCol; cc++) {
                                            occupied[rr + '-' + cc] = true;
                                        }
                                    }

                                    if (colspan > 1 || rowspan > 1) {
                                        mergeRanges.push(colLetter(startCol) + rowIndex + ':' + colLetter(endCol) + endRow);
                                    }

                                    colIndex += colspan;
                                });

                                sheetData.appendChild(trElm);
                                rowIndex++;
                            });

                            const dataStyle = stylesDoc.createElement('xf');
                            dataStyle.setAttribute('numFmtId', '0');
                            dataStyle.setAttribute('fontId', '0'); // default font
                            dataStyle.setAttribute('fillId', '0');
                            dataStyle.setAttribute('xfId', '0');
                            dataStyle.setAttribute('applyAlignment', '1');

                            const dataAlignment = stylesDoc.createElement('alignment');
                            dataAlignment.setAttribute('horizontal', 'center');
                            dataAlignment.setAttribute('vertical', 'center');
                            dataAlignment.setAttribute('wrapText', '1');
                            dataStyle.appendChild(dataAlignment);

                            cellXfs.appendChild(dataStyle);
                            const centerStyleIndex = cellXfs.childNodes.length - 1;
                            cellXfs.setAttribute('count', cellXfs.childNodes.length.toString());

                            // 2. Add data rows from tbody
                            $('#dashboard_ar tbody tr').each(function () {
                                let colIndex = 1;
                                const trElm = sheetDoc.createElement('row');
                                trElm.setAttribute('r', rowIndex);

                                $(this).children('td').each(function () {
                                    const $cell = $(this);
                                    const colspan = parseInt($cell.attr('colspan')) || 1;
                                    const rowspan = parseInt($cell.attr('rowspan')) || 1;

                                    while (occupied[rowIndex + '-' + colIndex]) {
                                        colIndex++;
                                    }

                                    const startCol = colIndex;
                                    const endCol = colIndex + colspan - 1;
                                    const endRow = rowIndex + rowspan - 1;

                                    const cellRef = colLetter(startCol) + rowIndex;
                                    const c = sheetDoc.createElement('c');
                                    c.setAttribute('r', cellRef);
                                    c.setAttribute('t', 'str');
                                    if (colIndex > 2)
                                        c.setAttribute('s', centerStyleIndex.toString());

                                    const v = sheetDoc.createElement('v');
                                    let cellText = $cell.text().trim().replace(/\s+/g, ' ');
                                    if (!cellText) cellText = ' ';
                                    v.textContent = cellText;
                                    c.appendChild(v);
                                    trElm.appendChild(c);

                                    for (let rr = rowIndex; rr <= endRow; rr++) {
                                        for (let cc = startCol; cc <= endCol; cc++) {
                                            occupied[rr + '-' + cc] = true;
                                        }
                                    }

                                    if (colspan > 1 || rowspan > 1) {
                                        mergeRanges.push(colLetter(startCol) + rowIndex + ':' + colLetter(endCol) + endRow);
                                    }

                                    colIndex += colspan;
                                });

                                sheetData.appendChild(trElm);
                                rowIndex++;
                            });

                            // Add mergeCells element if any merges needed
                            if (mergeRanges.length > 0) {
                                const mergeCells = sheetDoc.createElement('mergeCells');
                                mergeCells.setAttribute('count', mergeRanges.length);

                                mergeRanges.forEach(range => {
                                    const mergeCell = sheetDoc.createElement('mergeCell');
                                    mergeCell.setAttribute('ref', range);
                                    mergeCells.appendChild(mergeCell);
                                });

                                if (sheetData.nextSibling) {
                                    worksheet.insertBefore(mergeCells, sheetData.nextSibling);
                                } else {
                                    worksheet.appendChild(mergeCells);
                                }
                            }
                        }




                    },
                ],
                footerCallback: function (row, data, start, end, display) {
                    let api = this.api();

                    // Remove the formatting to get integer data for summation
                    let intVal = function (i) {
                        return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
                    };
                    for (var i = 2; i < api.columns().count(); i++) {



                        var totalLoan = api.column(i, { page: 'current' }).data().reduce((a, b) => intVal(a) + intVal(b), 0);
                        var totalAmt = api.column(i, { page: 'current' }).data().reduce((a, b) => intVal(a) + intVal(b), 0);

                        api.column(i).footer().innerHTML = Number(totalLoan).toFixed(2);
                        api.column(i).footer().innerHTML = Number(totalAmt).toFixed(2);

                        var pos = (i - 2) % 3;

                        if (pos === 0) { // COUNT
                            document.getElementById("lbl_" + i).innerHTML =
                                "Count (" + new Intl.NumberFormat().format(totalLoan) + ")";
                        }
                        else if (pos === 1) { // AMOUNT
                            document.getElementById("lbl_" + i).innerHTML =
                                "Amount ($ " + new Intl.NumberFormat().format(totalAmt.toFixed(2)) + ")";
                        }
                        //else { // BALANCE
                        //    document.getElementById("lbl_" + i).innerHTML =
                        //        "AvgRate ($ " + new Intl.NumberFormat().format(totalLoan.toFixed(2)) + ")";
                        //}

                        //if (i % 3 == 0)
                        //    document.getElementById("lbl_" + (i)).innerHTML = "Count (" + new Intl.NumberFormat().format(Number(totalLoan)) + ")";
                        //else
                        //    document.getElementById("lbl_" + (i)).innerHTML = "US $ (" + new Intl.NumberFormat().format(Number(totalAmt).toFixed(2)) + ")";

                        //document.getElementById("lbl_" + (i)).innerHTML = "(" + Number(totalLoan).toFixed(2) + ")";
                        //document.getElementById("lbl_" + (i)).innerHTML = "(" + Number(totalAmt).toFixed(2) + ")";


                    }
                }


            });

        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    return false;
}


function dashboard_arBind_displayERP_IPS_OLD() {
    $('#load1').show();
    var columns = [];
    var columnlink = [];
    var preheader = '';
    var headers = [];
    var returnstring = '';
    var htmlheader = '';
    var htmlrow = '';
    $.ajax({
        url: "CanopyMonthlyVendorSummary.aspx/GetDataMonthlyVendorData_IPS",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var dataArray = JSON.parse(data.d);//
            var firstData = dataArray[0];
            var rowData = dataArray[1];
            var ColumnNames = Object.keys(firstData);
            var rowValues = Object.keys(rowData);

            htmlheader = '<thead><tr>';
            var thead = document.createElement("thead");
            var trh = document.createElement("tr");
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    var tbh = document.createElement("th");
                    tbh.rowSpan = 2;
                    tbh.style.fontWeight = "bold!important";
                    tbh.style.verticalAlign = "middle";
                    tbh.id = i + "_ClientLabel";
                    tbh.style.left = "0px!important";
                    //tbh.appendChild(label);
                    tbh.innerHTML = ColumnNames[i];
                    trh.appendChild(tbh);

                    //thead.appendChild(trh);
                    //htmlheader = htmlheader + '<th style="left: 0px!important;">Client</th>';
                }
                else if (i > 1) {
                    if (i % 3 == 0) {
                        var tbh = document.createElement("th");
                        tbh.colSpan = 3;
                        tbh.style.textAlign = "center";
                        tbh.innerHTML = ColumnNames[i].replace("-LoanCount", "").replace("-Amount", "");
                        trh.appendChild(tbh);
                        //thead.appendChild(trh);
                        //htmlheader = htmlheader + '<th colspan=2 style="padding-left:20px;left: 0px!important; text-align:center;">' + ColumnNames[i].replace("-LoanCount", "").replace("-Amount", "") + '</th>';
                        i = i + 1;
                    }
                    //else
                    //    htmlheader = htmlheader + '<th style="padding-left:20px;left: 0px!important; text-align:center;">' + ColumnNames[i] + '</th>';
                }
                else {
                    var tbh = document.createElement("th");
                    //tbh.style.display = 'none';
                    tbh.innerHTML = ColumnNames[i];
                    //htmlheader = htmlheader + '<th style="display:none;">' + ColumnNames[i] + '</th>';
                    trh.appendChild(tbh);
                    //thead.appendChild(trh);
                }
            }
            thead.appendChild(trh);
            $('#dashboard_ar').append(thead);
            var trh1 = document.createElement("tr");
            //htmlheader = htmlheader + '</tr><tr>';
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    //var tbr = document.createElement("th");
                    //tbr.innerHTML = ColumnNames[i];
                    //tbr.style.textAlign = "center";
                    //trh1.appendChild(tbr);
                    ////htmlheader = htmlheader + '<th style="left: 0px!important; ">' + ColumnNames[i] + '</th>';
                }
                else if (i > 1) 
                    if (i % 2 != 0) {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'US $';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                    }
                    else {
                        var tbr = document.createElement("th");
                        tbr.style.textAlign = "center";
                        tbr.style.verticalAlign = "middle";
                        tbr.innerHTML = 'Count';
                        tbr.id = "lbl_" + (i - 1);
                        trh1.appendChild(tbr);
                    }
                else {
                    var tbr = document.createElement("th");
                    //tbr.style.display = 'none';
                    tbr.innerHTML = ColumnNames[i];

                    //htmlheader = htmlheader + '<th style="display:none;">' + ColumnNames[i] + '</th>';
                    trh1.appendChild(tbr);
                }
            
            }
            thead.appendChild(trh1);
            $('#dashboard_ar').append(thead);
            var tbody = document.createElement("tbody");
            //$('#dashboard_ar').html("");
            //htmlheader = htmlheader + '</tr></thead>';
            //htmlrow = '<tbody>';
            $.each(dataArray, function (index, item) {
                var tr1 = document.createElement("tr");
                //htmlrow = htmlrow + '<tr>';
                for (var i = 1; i < ColumnNames.length; i++) {
                    var targetColumnName = ColumnNames[i];
                    if (i == 1 || i == 2) {
                        var td = document.createElement("td");
                        //var label = document.createElement("label");
                        //label.id = i + "_" + item[targetColumnName].replace(" ", "") + "ClientLabel";
                        td.innerHTML = item[targetColumnName];
                        //if (ColumnNames[i] == "Process")
                        //    label.style.width = "180px";
                        //else
                        //    label.style.width = "250px";
                        ///*label.style.width = "250px";*/
                        //td.appendChild(label);
                        tr1.appendChild(td);
                    }
                    else if (i > 1) {
                        //if (i % 2 == 0) {
                        var td = document.createElement("td");
                        td.style.textAlign = "center";
                        //if (targetColumnName].includes("Amount")) {
                        //    td.innerHTML = "" + formattedAmount.toLocaleString("en-US", { style: "currency", currency: "USD" });
                        //}
                        //else
                        td.innerHTML = item[targetColumnName];
                        //td.innerHTML = '<input type="text" id="loancount_' + targetColumnName + '_' + index + '" onchange="return getValue(this,' + index + ');" style="width:70px;" value="' + blankForNull(item[targetColumnName]) + '"></input>';
                        tr1.appendChild(td);
                        //}
                        //else {
                        //    var td = document.createElement("td");
                        //    td.style.textAlign = "center";
                        //    td.innerHTML = '<input type="text" id="loancount_' + targetColumnName + '_' + index + '" onchange="return getValue(this,' + index + ');" style="width:50px;" value="' + blankForNull(item[targetColumnName]) + '"></input>';
                        //    tr1.appendChild(td);
                        //}
                    }
                    else {
                        var td = document.createElement("td");
                        //td.style.display = 'none';
                        td.innerHTML = item[targetColumnName];
                        tr1.appendChild(td);
                    }
                }
                tbody.appendChild(tr1);
                $('#dashboard_ar').append(tbody);
            });


            //Footer
            var tfoot = document.createElement("tfoot");
            var trf1 = document.createElement("tr");
            for (var i = 1; i < ColumnNames.length; i++) {
                if (i == 1 || i == 2) {
                    var tbr = document.createElement("th");
                    tbr.style.textAlign = "center";
                    trf1.appendChild(tbr);
                }
                else if (i > 1) {
                    var tbr = document.createElement("th");
                    tbr.style.textAlign = "center";
                    trf1.appendChild(tbr);
                }
                else {
                    var tbr = document.createElement("th");
                    // tbr.style.display = 'none';
                    trf1.appendChild(tbr);
                }
            }
            tfoot.appendChild(trf1);
            $('#dashboard_ar').append(tfoot);

            $('#dashboard_ar').DataTable({
                dom: 'Bft',
                destroy: true,
                orderCellsTop: true,
                fixedColumns: {
                    leftColumns: 2,
                },
                scrollCollapse: false,
                /*  scrollY: '400px',*/
                scrollX: true,
                "paging": false,
                "autoWidth": true,
                select: true,
                "ordering": false,
                filter: true,
                'select': {
                    'style': 'single'
                },
                "serverSide": false,

                initComplete: function () {
                    $('#load1').hide();
                },

                buttons: [
                    {
                        extend: 'excelHtml5',
                        text: 'Export to Excel',
                        filename: 'Monthwise Summary',
                        exportOptions: {
                            columns: ':visible',
                            modifier: { header: false }
                        },
                        customize: function (xlsx) {
                            const sheetDoc = xlsx.xl.worksheets['sheet1.xml'];
                            const worksheet = sheetDoc.documentElement;
                            const sheetData = worksheet.getElementsByTagName('sheetData')[0];
                            const $worksheet = $(worksheet);


                            // Remove all existing rows (clean slate)
                            $worksheet.find('row').remove();

                            // Remove existing mergeCells if any
                            $worksheet.find('mergeCells').remove();

                            function colLetter(n) {
                                let s = '', t;
                                while (n > 0) {
                                    t = (n - 1) % 26;
                                    s = String.fromCharCode(65 + t) + s;
                                    n = Math.floor((n - 1) / 26);
                                }
                                return s;
                            }

                            const occupied = {};
                            const mergeRanges = [];

                            const stylesDoc = xlsx.xl['styles.xml'];
                            const fonts = stylesDoc.getElementsByTagName('fonts')[0];
                            const borders = stylesDoc.getElementsByTagName('borders')[0];
                            const cellXfs = stylesDoc.getElementsByTagName('cellXfs')[0];

                            // --- Create bold font for header
                            const headerFont = stylesDoc.createElement('font');
                            const bold = stylesDoc.createElement('b');
                            const color = stylesDoc.createElement('color');
                            color.setAttribute('rgb', 'FF000000'); // black
                            headerFont.appendChild(bold);
                            headerFont.appendChild(color);
                            fonts.appendChild(headerFont);
                            const headerFontId = fonts.childNodes.length - 1;
                            fonts.setAttribute('count', fonts.childNodes.length.toString());

                            // --- Create border for all cells
                            const border = stylesDoc.createElement('border');
                            ['left', 'right', 'top', 'bottom'].forEach(side => {
                                const sideElem = stylesDoc.createElement(side);
                                sideElem.setAttribute('style', 'thin');
                                const colorElem = stylesDoc.createElement('color');
                                colorElem.setAttribute('auto', '1');
                                sideElem.appendChild(colorElem);
                                border.appendChild(sideElem);
                            });

                            const headerStyle = stylesDoc.createElement('xf');
                            headerStyle.setAttribute('numFmtId', '0');
                            headerStyle.setAttribute('fontId', headerFontId.toString());
                            headerStyle.setAttribute('fillId', '0');
                            headerStyle.setAttribute('xfId', '0');
                            headerStyle.setAttribute('applyFont', '1');
                            headerStyle.setAttribute('applyBorder', '1');
                            headerStyle.setAttribute('applyAlignment', '1');

                            // Add alignment child
                            const alignment = stylesDoc.createElement('alignment');
                            alignment.setAttribute('horizontal', 'center');
                            alignment.setAttribute('vertical', 'center');
                            alignment.setAttribute('wrapText', '1');
                            headerStyle.appendChild(alignment);

                            cellXfs.appendChild(headerStyle);

                            // Append the new xf
                            //cellXfs.appendChild(headerAlignXf);
                            const headerStyleIndex = cellXfs.childNodes.length - 1;
                            cellXfs.setAttribute('count', cellXfs.childNodes.length.toString());
                            // Start rowIndex at 1 to insert headers at the top
                            let rowIndex = 1;

                            // 1. Add header rows from thead
                            $('#dashboard_ar thead tr').each(function () {
                                let colIndex = 1;
                                const trElm = sheetDoc.createElement('row');
                                trElm.setAttribute('r', rowIndex);

                                $(this).children('th, td').each(function () {
                                    const $cell = $(this);
                                    const colspan = parseInt($cell.attr('colspan')) || 1;
                                    const rowspan = parseInt($cell.attr('rowspan')) || 1;

                                    while (occupied[rowIndex + '-' + colIndex]) {
                                        colIndex++;
                                    }

                                    const startCol = colIndex;
                                    const endCol = colIndex + colspan - 1;
                                    const endRow = rowIndex + rowspan - 1;

                                    const cellRef = colLetter(startCol) + rowIndex;
                                    const c = sheetDoc.createElement('c');
                                    c.setAttribute('r', cellRef);
                                    c.setAttribute('t', 'str');
                                    c.setAttribute('s', headerStyleIndex.toString());

                                    const v = sheetDoc.createElement('v');
                                    let cellText = $cell.text().trim().replace(/\s+/g, ' ');
                                    if (!cellText) cellText = ' ';
                                    v.textContent = cellText;
                                    c.appendChild(v);
                                    trElm.appendChild(c);

                                    for (let rr = rowIndex; rr <= endRow; rr++) {
                                        for (let cc = startCol; cc <= endCol; cc++) {
                                            occupied[rr + '-' + cc] = true;
                                        }
                                    }

                                    if (colspan > 1 || rowspan > 1) {
                                        mergeRanges.push(colLetter(startCol) + rowIndex + ':' + colLetter(endCol) + endRow);
                                    }

                                    colIndex += colspan;
                                });

                                sheetData.appendChild(trElm);
                                rowIndex++;
                            });

                            const dataStyle = stylesDoc.createElement('xf');
                            dataStyle.setAttribute('numFmtId', '0');
                            dataStyle.setAttribute('fontId', '0'); // default font
                            dataStyle.setAttribute('fillId', '0');
                            dataStyle.setAttribute('xfId', '0');
                            dataStyle.setAttribute('applyAlignment', '1');

                            const dataAlignment = stylesDoc.createElement('alignment');
                            dataAlignment.setAttribute('horizontal', 'center');
                            dataAlignment.setAttribute('vertical', 'center');
                            dataAlignment.setAttribute('wrapText', '1');
                            dataStyle.appendChild(dataAlignment);

                            cellXfs.appendChild(dataStyle);
                            const centerStyleIndex = cellXfs.childNodes.length - 1;
                            cellXfs.setAttribute('count', cellXfs.childNodes.length.toString());

                            // 2. Add data rows from tbody
                            $('#dashboard_ar tbody tr').each(function () {
                                let colIndex = 1;
                                const trElm = sheetDoc.createElement('row');
                                trElm.setAttribute('r', rowIndex);

                                $(this).children('td').each(function () {
                                    const $cell = $(this);
                                    const colspan = parseInt($cell.attr('colspan')) || 1;
                                    const rowspan = parseInt($cell.attr('rowspan')) || 1;

                                    while (occupied[rowIndex + '-' + colIndex]) {
                                        colIndex++;
                                    }

                                    const startCol = colIndex;
                                    const endCol = colIndex + colspan - 1;
                                    const endRow = rowIndex + rowspan - 1;

                                    const cellRef = colLetter(startCol) + rowIndex;
                                    const c = sheetDoc.createElement('c');
                                    c.setAttribute('r', cellRef);
                                    c.setAttribute('t', 'str');
                                    if (colIndex > 2)
                                        c.setAttribute('s', centerStyleIndex.toString());

                                    const v = sheetDoc.createElement('v');
                                    let cellText = $cell.text().trim().replace(/\s+/g, ' ');
                                    if (!cellText) cellText = ' ';
                                    v.textContent = cellText;
                                    c.appendChild(v);
                                    trElm.appendChild(c);

                                    for (let rr = rowIndex; rr <= endRow; rr++) {
                                        for (let cc = startCol; cc <= endCol; cc++) {
                                            occupied[rr + '-' + cc] = true;
                                        }
                                    }

                                    if (colspan > 1 || rowspan > 1) {
                                        mergeRanges.push(colLetter(startCol) + rowIndex + ':' + colLetter(endCol) + endRow);
                                    }

                                    colIndex += colspan;
                                });

                                sheetData.appendChild(trElm);
                                rowIndex++;
                            });

                            // Add mergeCells element if any merges needed
                            if (mergeRanges.length > 0) {
                                const mergeCells = sheetDoc.createElement('mergeCells');
                                mergeCells.setAttribute('count', mergeRanges.length);

                                mergeRanges.forEach(range => {
                                    const mergeCell = sheetDoc.createElement('mergeCell');
                                    mergeCell.setAttribute('ref', range);
                                    mergeCells.appendChild(mergeCell);
                                });

                                if (sheetData.nextSibling) {
                                    worksheet.insertBefore(mergeCells, sheetData.nextSibling);
                                } else {
                                    worksheet.appendChild(mergeCells);
                                }
                            }
                        }




                    },
                ],
                footerCallback: function (row, data, start, end, display) {
                    let api = this.api();

                    // Remove the formatting to get integer data for summation
                    let intVal = function (i) {
                        return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
                    };
                    for (var i = 2; i < api.columns().count(); i++) {

                        var totalLoan = api.column(i, { page: 'current' }).data().reduce((a, b) => intVal(a) + intVal(b), 0);
                        var totalAmt = api.column(i, { page: 'current' }).data().reduce((a, b) => intVal(a) + intVal(b), 0);

                        api.column(i).footer().innerHTML = Number(totalLoan).toFixed(2);
                        api.column(i).footer().innerHTML = Number(totalAmt).toFixed(2);

                        if (i % 2 == 0)
                            document.getElementById("lbl_" + (i)).innerHTML = "Count (" + new Intl.NumberFormat().format(Number(totalLoan)) + ")";
                        else
                            document.getElementById("lbl_" + (i)).innerHTML = "US $ (" + new Intl.NumberFormat().format(Number(totalAmt).toFixed(2)) + ")";

                        //document.getElementById("lbl_" + (i)).innerHTML = "(" + Number(totalLoan).toFixed(2) + ")";
                        //document.getElementById("lbl_" + (i)).innerHTML = "(" + Number(totalAmt).toFixed(2) + ")";


                    }
                }


            });

        },
        error: function (error) {
            alert('error; ' + eval(error));
            alert('error; ' + error.responseText);
        }
    });
    return false;
}