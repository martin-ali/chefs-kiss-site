// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    const ratingParent = document.getElementsByClassName('rating-parent')[0];
    const ratingElements = ratingParent.getElementsByTagName('label');

    $(ratingParent).click(ev => {
        const target = ev.target;
        if (target.nodeName == 'INPUT') return;
        const previousSiblings = $(target).prevAll('label');

        for (const sibling of previousSiblings) {
            sibling.classList.add('checked');
            console.log(sibling);
        }
    });
})();
