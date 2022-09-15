// Code goes here

$(document).ready(function () {
    var table = $('#dataTable').DataTable();

    $('#btnExport').on('click', function () {
        $('<table>').append(table.$('tr').clone()).table2excel({
            exclude: ".excludeThisClass",
            name: "Worksheet Name",
            filename: "SomeFile" //do not include extension
        });
    });
})
