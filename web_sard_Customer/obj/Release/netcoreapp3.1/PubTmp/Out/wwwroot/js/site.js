// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function renderAj(doc) {
    $(doc).find('a[isAJ="true"]').each(function (index, a) {

        $(a).click(function (e) {
            var bb = $("#AJ");
            bb.html("<div class='loader'></div>");

            var ur = $(a).attr("href");
            var va = $(a).html();
            $.get($(a).attr("href"), function (data, status) {
                if (status != "success") {
                    document.URL = $(a).attr("href");
                } else {
                    bb.html(data);
                    renderAj(bb)
                    changeurl(ur, va);
                }

               });

             

            e.preventDefault();
            return false;
        });

    });
}

function changeurl(url, title) {
    var new_url =   url;
    
    window.history.pushState(null, null, new_url);
    document.title = title;
}

$(document).ready(function () { renderAj(document); });