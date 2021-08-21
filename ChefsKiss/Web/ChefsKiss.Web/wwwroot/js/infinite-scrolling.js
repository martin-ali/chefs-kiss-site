function ConfigureInfiniteScroll(url, startingPage, parameters) {
    const bottomLenience = 200;
    const debounceTimeout = 100;
    const processChange = debounce(() => Process());
    const container = "#recipes-list";
    let page = startingPage;

    function Process() {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - bottomLenience) {
            console.log(page);
            const route = `${url}/${page}`;

            $.get(route, parameters)
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
