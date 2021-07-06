// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    function addChecked(element) {
        element.classList.add('checked');
    }

    function removeChecked(element) {
        element.classList.remove('checked');
    }

    const ratingParent = document.getElementsByClassName('rating-parent')[0];
    const ratingElements = ratingParent.getElementsByTagName('label');

    $(ratingParent).click(ev => {
        return;
        const target = ev.target;
        if (target.nodeName !== 'LABEL') return;

        const previousSiblings = $(target).prevAll('label');
        const nextSiblings = $(target).nextAll('label');

        addChecked(target);

        for (const sibling of previousSiblings) {
            addChecked(sibling);
        }

        for (const sibling of nextSiblings) {
            removeChecked(sibling);
        }
    });
})();
