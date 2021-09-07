(function () {
    function setupPostPreview() {
        const preview = document.getElementById('image-preview');
        const input = document.getElementById('image-upload');

        if (input && preview) {
            input.addEventListener('change', (event) => {
                preview.src = URL.createObjectURL(event.target.files[0]);
            });
        }
    }

    window.addEventListener('load', setupPostPreview)
})();
