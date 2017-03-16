$(document).ready(function () {
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
        if (i < max_fields) { 
            i++; 
            $(wrapper).append('<div><input id="ingredientName' + i + '" type="text" name="ingredientNames" placeholder="Ingredient"><input id="quantity' + i + '" type="number" name="ingredientQuantities" placeholder="Quantity"><a href="#" class="remove_field">Remove</a></div>');

            $(wrapper).find('input[type=text]:last').autocomplete({
                source: ingredientNames
            });
        }
    });

    $(wrapper).on("click", ".remove_field", function (e) { 
        e.preventDefault(); $(this).parent('div').remove();
        i--;
    })

    $("input[name^='ingredientNames']").autocomplete({
        source: ingredientNames
    });
});