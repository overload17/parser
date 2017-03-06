window.addEventListener("load",
  function() {
    $("#btnPage").click();
  });

$(function () {
  $("body")
      .on("select.uk.pagination",
          "[data-uk-pagination]",
          function (e, pageIndex) {
            $("#pageNumber").val(pageIndex);
            $("#btnPage").click();
          });
});

function buildPages(count, current) {
  UIkit.pagination($("#pages"), { items: count, currentPage: current });
}

function editProduct(id) {
  getProduct(id);
  $("#productID").val(id);
  $("#productID").text(id);

  var prices = getPrices(id);

  var arr = $.map(prices, function (el) { return el.Price });
  arr.unshift("Цена");

  var modal = UIkit.modal("#gridGlobal");
  modal.bgclose = false;
  modal.center = true;
  modal.show();
  var chart = c3.generate({
    bindto: "#chart",
    data: {
      columns: [
        arr
      ]
    }
  });
}

var tempData;

function getProduct(id) {
  $("#productID").val(id);
  $("#productID").text(id);
  var url = $("#gridGlobal").data("feedsapi") + "feed/" + id;
  emptyEditor();
  $.get(url,
    function(data) {

      data = JSON.parse(data);
      tempData = data;

      fillEditor(tempData);
    },
    "json");
}

function getPrices(id) {
  var url = $("#gridGlobal").data("feedsapi") + "feed/price/" + id;
  var xmlHttp = new XMLHttpRequest();
  xmlHttp.open("GET", url, false);
  xmlHttp.send(null);
  var res = xmlHttp.responseText;
  var result = JSON.parse(JSON.parse(res));
  return result;
}

function fillEditor(data) {
  $("#TitleProduct").val(data[0].Title);
  $("#LinkProduct").val(data[0].Link);
  $("#imageBase64").val(data[0].ImageBase64 == "" ? "" : "data:image/png;base64," + data[0].ImageBase64);
  $("#ImageProduct").attr("src",data[0].ImageBase64 == "" ? defaultImage : "data:image/png;base64," + data[0].ImageBase64);
  $("#DescriptionProduct").val(data[0].Description);
}

function emptyEditor() {
  $("#TitleProduct").val("");
  $("#LinkProduct").val("");
  $("#DescriptionProduct").val("");
  $("#productID").val("");
  $("#imageBase64").val("");
}