﻿(function () {
    const bottomLenience = 200;
    const debounceTimeout = 100;
    const processChange = debounce(() => AlertMe());
    const container = "#recipes-list";
    let index = 1;

    function AlertMe() {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - bottomLenience) {
            console.log(index);
            const url = `/Recipes/Recipes/Page/${index++}`;

            $.get(url, function (data) {
                $(container).append(data);
            });
        }
    }

    function debounce(func, timeout = debounceTimeout) {
        let timer;
        return (...args) => {
            clearTimeout(timer);
            timer = setTimeout(() => { func.apply(this, args); }, timeout);
        };
    }

    $(window).scroll(function () {
        processChange();
    });
})();
