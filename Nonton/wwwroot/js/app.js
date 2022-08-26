forceHorizontalScroll = function(id) {
    const element = document.getElementById(id);

    element.addEventListener('wheel', (event) => {
        event.preventDefault();

        element.scrollBy({
            left: event.deltaY < 0 ? -30 : 30
        });
    });
}