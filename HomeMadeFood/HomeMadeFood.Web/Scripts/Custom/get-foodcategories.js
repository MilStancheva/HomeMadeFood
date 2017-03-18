$(function () {
    var items = "";
    var url = "/admin/recipes/getfoodcategories";
    $.getJSON(url, function (data) {

        $.each(data, function (index, item) {
            items += "<option value='" + item.Id + "'>" + item.Name + "</option>";
        });
        $("#foodCategories").html(items);

        //SendData($("ingredientName" + 1), $("quantity" + 1), $("price" + 1), $("foodCategories"));
    });

    //function SendData(ingredientName, ingredientQuantity, ingredientPrice, foodCategory) {
    //    var token = $("[name='__RequestVerificationToken']").val();
    //    var options = {
    //        url: '@Url.Action("AutoComplete","Recipes")',
    //        type: "post",
    //        data: {
    //            __RequestVerificationToken: token,
    //            name: ingredientName.val(),
    //            quantity: ingredientQuantity.val(),
    //            price: ingredientPrice.val(),
    //            foodCategory: foodCategory.val()
    //        }
    //    };
    //    $.ajax(options);
    //}

});