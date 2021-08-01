(function () {
    const bottomLenience = 100;
    const debounceTimeout = 150;
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
