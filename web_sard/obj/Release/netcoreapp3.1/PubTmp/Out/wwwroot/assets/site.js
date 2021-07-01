
$(document).ready(function () {

    $(".table").addClass("table-striped");
    $(".table").parent().addClass("table-responsive");
    $(".table").parent().addClass("table-responsive-sm");


    $(".table thead").addClass("table-dark");
    $(".table tbody").addClass("shadow");
    $(".table tfoot").addClass("shadow");

    $(".searchinput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".table tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
        $(".colSearch").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


 
    $(".zoom").each(function (index, e) {
        $(e).replaceWith("<a href='" + $(e).attr("src") + "' target='blank'>" + $(e)[0].outerHTML + "</a>");

    });



    $(".datepicker").each(function (index, a) {
        $(a).persianDatepicker({ theme: 'latoja' });
       
    })
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


    renderAjax(document);


});
$(document).ready(function () {
    
     $(".loaderholder").hide();
$(".loaderholder").remove();



  
});


$('#examplesignModal').on('show.bs.modal', function (e) {
    openFullscreen();
});
$('#examplesignModal').on('hide.bs.modal', function (e) {
    closeFullscreen();

});
var elem = document.documentElement;
function openFullscreen() {
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

function renderAjax(doc) {
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

    $(doc).find("select").each(function (index, a) {
        $(a).attr("data-live-search", "true");
        $(a).selectpicker();
    })

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