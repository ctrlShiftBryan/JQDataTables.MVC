    var dataTable;
    var canSort;
    var test;
    var asInitVals = new Array();

    $(function () {

        // aoColumns[0].bVisible = false;
        initDT();
        function initDT() {

            dataTable = $('#example').dataTable({
                "sPaginationType": "full_numbers",
                "bJQueryUI": true,
                "bDestroy": true,
                "bProcessing": false,
                "bServerSide": true,
                "sServerMethod": "POST",

                "sAjaxSource": "../Home/Data",

                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {


                    var id = aData["pid"];

                    canSort = id !== undefined;
                    if (canSort)
                        $(nRow).attr("id", id);


                    var settings = this.fnSettings();
                    var str = settings.oPreviousSearch.sSearch;
                    var regex = new RegExp(str + "(?!([^<]+)?>)", 'i');
                    //for (i in aData) {
                    //    if (!!aData[i]) {  // not sure what this is for.. 
                    //        aData[i] = aData[i].replace( regex, function(matched) { return "<span class='filterMatches'>"+matched+"</span>";} );
                    //    }
                    //              }

                    $('td', nRow).each(
                    function (i, v) {


                        var html = $(v).html();

                        html = html.replace(regex, function (matched) { return "<span class='filterMatches'>" + matched + "</span>"; });

                        $(v).html(html);

                    }
                );

                    return nRow;
                },


                "aoColumns": aoColumns

            });
        }

        $('#order').click(function () {
            dataTable.rowReordering({
                sURL: "../Home/Sort",
                sRequestType: "POST"
            });

            $(this).hide();
            $('#order-done').show();


            $("#div-single input").each(function (i, v) {
                v.className = "search_init";
                v.value = asInitVals[$("tfoot input").index(v)];

            });
            $('#example_filter input, .search_init').attr('readonly', 'readonly');

            dataTable.fnFilterClear();
        });

        $('#order-done').click(function () {
            $('#example_filter input, .search_init').removeAttr('readonly');
            $("tbody", dataTable).sortable("destroy");
            initDT();
            $(this).hide();
            $('#order').show();


        });

        $("tfoot input[type=text]").keyup(function () {
            /* Filter on the column (the index) of this element */
            if (!($(this).is('[readonly]'))) {

                var column = $(this).attr('data-column-index');
                var isRange = $(this).hasClass('range');

                var value = "";
                if (isRange) {


                    var start = $('input[type="text"]:eq(0)', $(this).parent()).val();
                    var end = $('input[type="text"]:eq(1)', $(this).parent()).val()

                    start = start == 'Start' ? '' : start;
                    end = end == 'End' ? '' : end;


                    value =
                        start + '|' + end
                            ;
                    value = value == '|' ? '' : value;
                    
                } else {
                    value = this.value;
                }
                dataTable.fnFilter(value, column);
            }
        });


        $("tfoot input[type=radio]").click(function () {
            var single = $(this).val() == 'single';
            var other = single ? 'range' : 'single';
            var otherDiv = $(this).attr('name').replace('rb', other);

            $('input[type=text]', $('#' + otherDiv)).attr(
                'readonly', 'readonly'
            );

            $('input[type=text]', $(this).parent()).removeAttr('readonly');


        });

        /*
        * Support functions to provide a little bit of 'user friendlyness' to the textboxes in 
        * the footer
        */
        $("tfoot input[type=text]").each(function (i) {
            asInitVals[i] = this.value;
        });

        $("tfoot input[type=text]").focus(function () {
            if (!($(this).is('[readonly]'))) {

                if ($(this).hasClass("search_init")) {
                    $(this).removeClass("search_init");
                    this.value = "";
                }
            }
            else {
                $(this).blur();
            }
        });

        $("tfoot input[type=text]").blur(function (i) {


            if (!($(this).is('[readonly]')))
                if (this.value == "") {
                    $(this).addClass("search_init");
                    this.value = asInitVals[$("tfoot input[type=text]").index(this)];
                }
        });


    });
