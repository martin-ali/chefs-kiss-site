function setupConfirmationDialog(parentElement, eventTargetClass) {

    document.querySelector(parentElement)
        .addEventListener('click', ev => {
                const isCorrectElement = ev.target.classList.contains(eventTargetClass);
                if (isCorrectElement) {

                    const isConfirmed = confirm('Are you sure?');
                    if (isConfirmed === false) {
                        ev.preventDefault();
                    }
                }
            },
            false);
}
