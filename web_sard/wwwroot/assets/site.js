
$(document).ready(function () {

    $(".table").addClass("table-striped");
    $(".table").addClass("table-responsive");
    $(".table").addClass("table-responsive-sm");


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


    $("select").each(function (index, a) {
        $(a).attr("data-live-search", "true");
        $(a).selectpicker();
    })
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


});
  

function startBascul(idI, idButton, idInput) {
    SetBasculagain();
    $(idInput).addClass("active") 
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
            if ($(idInput).hasClass("active") && ($(idButton).is(":disabled") == false) && ($(idInput).hasClass("noactive")==false)) {
                 
                if (model.WeightBaculIsAllow) {
                    $(idInput).val(model.WeightBacul);

                } else {
                    $(idInput).val("0");
                }
                $(idInput).change();
            }
            $(idI).html((model.WeightBacul ?? "")/* + "" + (i++)*/);

             

        });
        
    }
    if ($(idButton).is(":disabled") == false) {
        $(idButton).click(function () {

            $(idInput).val($(idI).html());
            $(idInput).removeClass("active");
            $(idInput).change();
        }
        );
    }

}




function Contractbutton(FkC, vc, fkportageAdd) {
    var fkcontractid = $(FkC).val(); 
    var ur = "/Contract/ViewContractAjax/" + fkcontractid;
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