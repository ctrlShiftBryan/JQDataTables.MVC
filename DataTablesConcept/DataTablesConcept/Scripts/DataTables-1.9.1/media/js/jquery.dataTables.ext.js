//jQuery.fn.dataTableExt.aTypes.unshift(
//    function (sData) {
//        var sValidChars = "0123456789-,";
//        var Char;
//        var bDecimal = false;

//        /* Check the numeric part */
//        for (i = 0; i < sData.length; i++) {
//            Char = sData.charAt(i);
//            if (sValidChars.indexOf(Char) == -1) {
//                return null;
//            }

//            /* Only allowed one decimal place... */
//            if (Char == ",") {
//                if (bDecimal) {
//                    return null;
//                }
//                bDecimal = true;
//            }
//        }

//        return 'numeric-comma';
//    }
//);

//    jQuery.fn.dataTableExt.oSort['numeric-comma-asc'] = function (a, b) {

//        try {
//            a = a.toString();
//            b = b.toString();
//            var x = (a == "-") ? 0 : a.replace(/,/, ".");
//            var y = (b == "-") ? 0 : b.replace(/,/, ".");
//            x = parseFloat(x);
//            y = parseFloat(y);
//            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
//        } catch (e) {
//            console.log(a, b);
//            return 0;
//        }

//    };

//jQuery.fn.dataTableExt.oSort['numeric-comma-desc'] = function (a, b) {
//    var x = (a == "-") ? 0 : a.replace(/,/, ".");
//    var y = (b == "-") ? 0 : b.replace(/,/, ".");
//    x = parseFloat(x);
//    y = parseFloat(y);
//    return ((x < y) ? 1 : ((x > y) ? -1 : 0));
//};