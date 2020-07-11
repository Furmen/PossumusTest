// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function addJob() {
    var countJobs = $("#Jobs tbody").find("tr").length;
    $.get("/Candidate/GetPartialJob", { "countJobs": countJobs })
        .done(function (response) {
            $("#Jobs tbody").append(response);
        });
}

function deleteRow(btnDeleteRow) {
    var tr = $(btnDeleteRow).closest("tr");
    $(tr).remove();
    rebuildIndex();
}

function rebuildIndex() {
    $(".partialJobs").each(function (i, itemJob) {
        $(itemJob).find("[name^='Jobs[']").each(function () {
            $(this).attr("name", "Jobs[" + i + "]." + $(this).attr("id"));
        });
    });
}