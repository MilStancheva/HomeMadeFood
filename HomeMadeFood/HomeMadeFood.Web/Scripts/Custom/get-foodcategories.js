$(function () {
    var items = "";
    var url = "/admin/recipes/getfoodcategories";
    $.getJSON(url, function (data) {

        $.each(data, function (index, item) {
            items += "<option value='" + item.Id + "'>" + item.Name + "</option>";
        });
        $("#foodCategories").html(items);
    });

});