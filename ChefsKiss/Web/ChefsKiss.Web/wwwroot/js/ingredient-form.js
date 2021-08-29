(function () {
    function addIngredientEventListener() {

        const container = '#ingredients-container';
        const button = '#add-ingredient';
        let index = $('#ingredients-count').val();

        function addNewIngredientForm() {

            const url = `/Ingredients/IngredientAddForm/${index++}`;

            $.get(url, function (data) {
                $(container).append(data);
            });
        }

        function RemoveIngredient(ev) {

            if (ev.target.classList.contains('remove-ingredient')) {
                const deleteButton = ev.target;
                deleteButton.parentNode.parentNode.removeChild(deleteButton.parentNode);
            }
        }

        $(button).on('click', addNewIngredientForm);
        $(container).on('click', RemoveIngredient);
    }

    window.addEventListener('load', addIngredientEventListener);
})();
