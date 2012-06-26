$(function() {
    $('#example').dataTable({
        
         "sAjaxSource": "../Home/Data",
            "aoColumns": [
            {"mDataProp": "pid" },
            {"mDataProp": "n" },
            {"mDataProp": "pn" },
            {"mDataProp": "mf" },
            {"mDataProp": "fgf" },
            { "mDataProp": "c" }]

    });
});