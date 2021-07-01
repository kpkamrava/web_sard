

$(function () {
   

});


$(document).ready(function () {

     


    $(".searchinput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".table tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
        $(".colSearch").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $(".searchinputM").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".modal-body").find(".table tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
        $(".modal-body").find(".colSearch").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


    $(".zoom").each(function (index, e) {
        $(e).replaceWith("<a href='" + $(e).attr("src") + "' target='blank'>" + $(e)[0].outerHTML + "</a>");

    });



  
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });

    if ($(window).width() > 767) {
        $(".menu-content").find("a.dropdown-item").each(function (index, e) {

            var aa = $(e);
            //     if (aa.attr("href").includes(window.location.pathname)) {
            if (window.location.href.includes(aa.attr("href"))) {
                $(e).addClass("active")
                $(e).parent().parent("ul").click();
            }
        });
    }


    startBascul("#valuebaskul");
    $("#azerobaskul").click(function () {

        $.get("/Ajax/SetZeroBascul", function (data, status) {
        })

    });

    $(document).find("table").each(function (index, item) {
        if ($(item).parent().parent().hasClass("hide") == false)
            if ($(item).find("tr").length > 20) {

                var marginTop = 0; // Add margin if the page has a top nav-bar
                var $thead = $(item).find('thead').first();
                var offset = $thead.offset().top - marginTop;
                var lastPos = 0;

                $(document).on('scroll', function () {
                    console.clear();
                    console.log("offset- " + offset);
                    console.log("scrollTop- " + $(document).scrollTop());

                    if ($(document).scrollTop() > offset) {
                        if (lastPos === 0) {
                            // Add a class for styling
                            $thead.addClass('floating-header');
                        }

                        lastPos = $(document).scrollTop() - offset;
                        $thead.css('transform', 'translateY(' + (lastPos) + 'px)');
                    }
                    else if (lastPos !== 0) {
                        lastPos = 0;
                        $thead.removeClass('floating-header');
                        $thead.css('transform', 'translateY(' + 0 + 'px)');
                    }
                });
            }

    });  


    renderAjax(document); 

});
$(document).ready(function () {
    
     $(".loaderholder").hide();
$(".loaderholder").remove();



   
});




function printTable(item) {
    var str = $(item).parents("table")[0];
    
    var pageTitle = 'چاپ جدول',
        stylesheet = '/libs/bootstrap/scss/bootstrap.css',
        win = window.open('', 'Print', 'width=700,height=600');




    var strtag = $($.parseHTML(str.outerHTML));
    strtag.find("input").remove();
    strtag.find("a").remove();
    strtag.find("img").remove();

   
    win.document.write('<html><head><title>' + pageTitle + '</title>' +
        '<link rel="stylesheet" href="' + stylesheet + '">' +
        '</head><body dir="rtl">' + strtag.prop('outerHTML') + '</body></html>');


     win.document.close();
     setTimeout(function () { win.print();win.close(); },1000)
      
}


function ExcelTable(item) {
    var htmls = "";

    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
      
        return s.replace(/{(\w+)}/g, function (m, p) {
         return c[p];
        })
    };


    var str = $(item).parents("table")[0].outerHTML;

    var strtag = $($.parseHTML(str));
    $(strtag).find("input").remove();
    $(strtag).find("a").remove();
    $(strtag).find("img").remove();
  


    htmls = strtag.html();
 
    var ctx = {
        worksheet: 'Worksheet',
        table: htmls
    }


    var link = document.createElement("a");
    link.download = "export.xls";
    link.href = uri + base64(format(template, ctx));
    link.click();
}

 

$('#examplesignModal').on('show.bs.modal', function (e) {
    openFullscreen();
});
$('#examplesignModal').on('hide.bs.modal', function (e) {
    if (fs == 'false') {
        closeFullscreen();
    }
   
});



function sortTable(th,n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table =$(th);
    switching = true;
    //Set the sorting direction to ascending:
    dir = "asc";
    /*Make a loop that will continue until
    no switching has been done:*/

    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1); i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            /*check if the two rows should switch place,
            based on the direction, asc or desc:*/
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            //Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {
            /*If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again.*/
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}





function openFullscreen() {
    var elem = document.documentElement;
    if (elem.requestFullscreen) {
        elem.requestFullscreen();
    } else if (elem.mozRequestFullScreen) { /* Firefox */
        elem.mozRequestFullScreen();
    } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
        elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) { /* IE/Edge */
        elem.msRequestFullscreen();
    }
}


function closeFullscreen() {
     if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
    } else if (document.msExitFullscreen) {
        document.msExitFullscreen();
    }
}

var fs =  false ;
function FullscreenToggle() {
    if (fs== true ) {
        fs = false ;
        localStorage['Fullscreen'] = fs;
        closeFullscreen();
    } else {
        fs = true ; 
        localStorage['Fullscreen'] = fs;
        openFullscreen();
    }
    launchFullScreen(document.documentElement);
}


function renderAjax(doc) {

    $(doc).find("tr.hide").each(function (index, ele) {
        $(ele).hide();
        $(ele).prev().click(function () {
            $(ele).fadeToggle();
        });
        $(ele).prev().css('cursor', 'pointer');
    }); 

    $(doc).find(".table").addClass("table-striped");
    $(doc).find(".table").parent().addClass("table-responsive");

    $(doc).find(".table tbody").addClass("shadow");
    $(doc).find(".table tfoot").addClass("shadow");
    $(doc).find(".table").addClass("shadow");

     
    $(doc).find(".datepicker").each(function (index, a) {
        $(a).persianDatepicker({ theme: 'latoja' });

    })

    $(doc).find("a[isModal='true']").each(function (index, a) {

        $(a).click(function (e) {

            var bodymodal = $("#ajaxModal").find(".modal-body");
            bodymodal.html("<div class='loader'></div>");
            bodymodal.load($(a).attr("href") );

            $("#ajaxModal").modal("show");




            e.preventDefault();
            return false;
        });

    });

    $(doc).find("a[isAJ='true']").each(function (index, a) {

        $(a).click(function (e) {

            var tag = $($(a).attr("AJ")) ;
            tag.html("<div class='loader'></div>");
            tag.load($(a).attr("href"));
              
            e.preventDefault();
            return false;
        });

    });

    $(doc).find("select").each(function (index, a) {
        $(a).attr("data-live-search", "true");
        $(a).selectpicker();
    })

    $(doc).find(".table").each(function (index, el) {

        var z = $(el).find("caption");
        var str = "<a class='btn fa fa-print'onclick='printTable(this)'></a><a class='btn fa fa-file-excel'onclick='ExcelTable(this)'></a>";

        if (z.length==0) {
            $(el).append("<caption>" + str + "</caption>");
        } else {
            z.append(str);

        }
        });

 
  

     

}
function startBascul(idI, idButton, idInput) {
    SetBasculagain();
    if (idButton != null) {
    $(idInput).addClass("active") 

    }
    function SetBasculagain() {
        getBascul();
        setTimeout(function () {

            SetBasculagain();
        }, 1000);

    } ;
    var i = 1;
    function getBascul() {
      
        $.get("/Ajax/GetLastBascul", function (data, status) {
            var model = $.parseJSON(data);
            //debugger;
            if (idButton != null) {
                if ($(idInput).hasClass("active") && ($(idInput).prop('readonly') == true) && ($(idInput).hasClass("noactive")==false)) {
                 
                if (model.WeightBaculIsAllow) {
                    $(idInput).val(model.WeightBacul);

                } else {
                    $(idInput).val("0");
                }
                $(idInput).change();
            }
     
            }
             $(idI).html((model.WeightBacul ?? "")/* + "" + (i++)*/);

             

        });
        
    }
    /*if ($(idButton).is(":readonly") == false)*/
    if (idButton != null) {
        $(idButton).click(function () {

            $(idInput).val($(idI).html());
            $(idInput).removeClass("active");
            $(idInput).change();
        }
        );
    }

}
 

function Contractbutton(FkC, vc, fkportageAdd, kind) {
    var fkcontractid = $(FkC).val(); 
   
    var ur = "/Contract/ViewContractAjax/" + fkcontractid;
    if (kind != null) {
        ur = ur + "?kind=" + kind;
    }
    if (fkportageAdd != null) {
  //var fkportageidAdd = $(FkCadd).val();
        ur += "?fkportageAdd=" + fkportageAdd;
    }
    $(vc).html("");
    $.get(ur, function (data, status) {
        $(vc).html(data);
    });

}




function getCssSelector(el) {
    names = [];
    do {
        index = 0;
        var cursorElement = el;
        while (cursorElement !== null) {
            ++index;
            cursorElement = cursorElement.previousElementSibling;
        };
        names.unshift(el.tagName + ":nth-child(" + index + ")");
        el = el.parentElement;
    } while (el !== null);

    return names.join(" > ");
}