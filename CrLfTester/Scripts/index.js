$(function () {
  $("#testButton").on("click", function () {
    var url = $("#results").data("request-url");
    var encodedFolderName = encodeURIComponent($("#folderInput").val());
    var encodedFilter = encodeURIComponent($("#fileFilter").val());
    $("#results").load(url + "?folderName=" + encodedFolderName + "&filter=" + encodedFilter);
  });
});