forceHorizontalScroll = function(id) {
    const element = document.getElementById(id);
    if (element !== null) {
        element.addEventListener('wheel',
            (event) => {
                event.preventDefault();

                element.scrollBy({
                    left: event.deltaY < 0 ? -30 : 30
                });
            });
    };
}

setFullHeightPlayer = function () {
    setTimeout(function() {
        document.getElementsByClassName('plyr')[0].classList.add('h-screen');
    }, 100);
}