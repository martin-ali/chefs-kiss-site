window.addEventListener('load', addIngredientEventListener);

function addIngredientEventListener() {

    const container = '.ingredients-container';
    const button = '.add-ingredient';
    let index = 0;

    function addNewIngredientForm() {

        const url = `/Recipes/Ingredients/IngredientAddForm/${index++}`;

        $.get(url, function (data) {
            $(container).append(data);
        });
    }

    $(button).on('click', addNewIngredientForm);
}
