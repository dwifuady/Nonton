export function initFlickity() {
    var flkty = new Flickity('.main-carousel', {
        wrapAround: true,
        lazyLoad: 1,
        pageDots: false,
        autoPlay: 5000
    });

    flkty.on('dragStart', () => flkty.slider.style.pointerEvents = 'none');
    flkty.on('dragEnd', () => flkty.slider.style.pointerEvents = 'auto');
}

export function initFlickityContent(id) {
    var elem = document.querySelector('#' + id);
    var flkty = new Flickity(elem, {
        wrapAround: true,
        lazyLoad: 1,
        pageDots: false,
        groupCells: true
    });

    flkty.on('dragStart', () => flkty.slider.style.pointerEvents = 'none');
    flkty.on('dragEnd', () => flkty.slider.style.pointerEvents = 'auto');
}