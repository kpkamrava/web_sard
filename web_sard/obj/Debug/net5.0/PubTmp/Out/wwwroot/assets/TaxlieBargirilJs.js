/// <reference path="js/jquery.min.js" />
/// <reference path="js/vue.js" />



var app = new Vue({
    el: '#app',
    data: {
        Id: "",
        fkcontract: "",
        KindCotractType: "ASardKhane",
        IsEntry: true,
        NeedLocation:true,
        ListRows: [],
        ListInjurys: [],
        ListProducts: [],
        ListPackings: [],
        nimpalets:[]

    },
    methods: {
        ischeckInjury(arr,idd)
        {
            
            return jQuery.inArray(idd, arr) != -1;
        }
    },
    computed: {
        total: function () {
            if (!this.ListRows) {
                return 0;
            }

            return this.ListRows.reduce(function (total, value) {
                return total + Number(value.Count);
            }, 0);
        }
    }
}); 
 
app.Id = $("#Id").val(); 
 
app.IsEntry = $("#IsEntry").val();
app.KindCotractType = $("#KindCotractType").val();
 
app.fkcontract = $("#fkcontract").val();
readListrows();


function saveListrows(bbb) {
    if ($(bbb).find(".spinner-border").length == 0) {
        $(bbb).append("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>");

    }  var p = $(bbb).parent(".card-body");

    //  Guid idp, Guid id, string CodeLocation, long Count, Guid FkPacking, Guid FkProduct FkInjurys      Txt

    var _id = "&id=" + p.find("#Id").val();
   var _CodeLocation = "&L1=" + p.find("#L1").val();
    _CodeLocation += "&L2=" + p.find("#L2").val();
    _CodeLocation += "&L3=" + p.find("#L3").val();
    var _Count = "&Count=" + p.find("#Count").val();
    var _FkPacking = "&FkPacking=" + p.find("#FkPacking").val();
    var _FkProduct = "&FkProduct=" + p.find("#FkProduct").val();
    var _Txt = "&Txt=" + p.find("#Txt").val();

    var _IsNimPalet = "&IsNimPalet=" + p.find("#IsNimPalet").prop('checked');
    
    var fkcontract = "&fkcontract=" + app.fkcontract;

    //if (app.NeedLocation==false) {
    //    _CodeLocation = "";
    //}

    if (app.IsProduct1Packing0==0) {
        _IsNimPalet = "";
        _FkProduct = "";
    }  


    var _FkInjurys = "";
    p.find("#FkInjurys:checked").each(function ( ) {
       
             _FkInjurys += "&FkInjurys=" + $(this).val();
         

    }); 
    var t = _id + fkcontract + _CodeLocation + _Count + _FkPacking + _FkProduct + _FkInjurys + _Txt + _IsNimPalet;
    $.get("/Yard/AddlistRow" +
        "?idp=" + app.Id +
        t

        , function (data, status) {
            var model = $.parseJSON(data);
            if (model.ok == true) {
                readListrows();

            } else {

               alert(model.txt);
            }
             $(bbb).find(".spinner-border").remove();

        });

}

function removeListrows(bbb) {

    if (confirm("آیا میخواهید حذف شود؟")) {
        if ($(bbb).find(".spinner-border").length == 0) {
            $(bbb).append("<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>");

        }

        var p = $(bbb).parent(".card-body");

        var _id = p.find("#Id").val();


        $.get("/Yard/removelistRow" +
            "/" + _id


            , function (data, status) {
                var model = $.parseJSON(data);
                if (model.ok = true) {
                    readListrows();

                }
                alert(model.txt);
                $(bbb).find(".spinner-border").remove();

            });
    }
  

}

function readListrows() {

    $.get("/Yard/getlistRows/" + app.Id + "?fkcontract=" + app.fkcontract, function (data, status) {
        var model = $.parseJSON(data);//rows packings products injurys
        app.ListRows = model.rows; 
        app.ListProducts = model.products;
        app.ListPackings = model.packings;
        app.ListInjurys = model.injurys;
        app.nimpalets = model.nimpalets;
        
      });
 
}

   



function collapsebody(b) {
    $(b).parent().parent().parent().children(".card-body").slideToggle();  
}