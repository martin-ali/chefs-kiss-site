function ConfigureInfiniteScroll(url, startingPage, parameters) {
    const bottomLenience = 200;
    const debounceTimeout = 100;
    const processChange = debounce(() => AlertMe());
    const container = "#recipes-list";
    let page = startingPage;

    function AlertMe() {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - bottomLenience) {
            console.log(page);
            const url = `/${url}/${page}`;

            $.get(url, parameters)
                .done(function (data) {
                    if (data.trim() != 0) {
                        $(container).append(data);
                        page++;
                    }
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
};
