﻿$(document).ready(function () {
    var max_fields = 10;
    var wrapper = $(".input_fields_wrap");
    var add_button = $(".add_field_button");

    var i = 1;
    var ingredientNames = function (request, response) {
        $.ajax({
            url: "/admin/recipes/autocomplete/",
            type: "POST",
            dataType: "json",
            data: { prefix: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.Name, value: item.Name };
                }))
            }
        })
    };

    $(add_button).click(function (e) {
        e.preventDefault();
        var items = "";
        var url = "/admin/recipes/getfoodcategories";
        $.getJSON(url, function (data) {
            $.each(data, function (index, item) {
                items += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            });
            $("#foodCategories" + i).html(items);
        });

        if (i < max_fields) {
            i++;
            $(wrapper).append('<div><input id="ingredientName' + i + '" type="text" name="ingredientNames" placeholder="Ingredient">' +
                '<input id="quantity' + i + '" type="number" name="ingredientQuantities" placeholder="Quantity">' +
                '<input id="price' + i + '" type="number" name="ingredientPrices" placeholder="Price">' +
                '<select id="foodCategories' + i + '" name="foodCategories"></select>' +
                '<a href="#" class="remove_field">Remove</a></div>');

            $(wrapper).find('input[type=text]:last').autocomplete({
                source: ingredientNames
            });

            //SendData($("ingredientName" + i), $("quantity" + i), $("price" + i), $("foodCategories" + i));

        }

        $(wrapper).on("click", ".remove_field", function (e) {
            e.preventDefault(); $(this).parent('div').remove();
            i--;
        });

        $("input[name^='ingredientNames']").autocomplete({
            source: ingredientNames
        });
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