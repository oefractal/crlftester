$(function () {
  $("#testButton").on("click", function () {
    var url = $("#resultsData").data("request-url");
    var encodedFolderName = encodeURIComponent($("#folderInput").val());
    var encodedFilter = encodeURIComponent($("#fileFilter").val());
    $("#resultsGrid").load(url + "?folderName=" + encodedFolderName + "&filter=" + encodedFilter);
  });
});