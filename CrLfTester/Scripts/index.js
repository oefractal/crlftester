$(function () {
  $("#testButton").on("click", function () {
    var url = $("#results").data("request-url");
    var encodedFolderName = encodeURIComponent($("#folderInput").val());
    $("#results").load(url + "?folderName=" + encodedFolderName);
  });
});