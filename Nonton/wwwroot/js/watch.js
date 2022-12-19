
export function setFullHeightPlayer() {
    setTimeout(function () {
        if (document.getElementsByClassName('plyr')[0] !== null && document.getElementsByClassName('plyr')[0] != undefined) {
            document.getElementsByClassName('plyr')[0].classList.add('h-screen');
        }
    }, 1000);
}

export function loadPlyrAssets() {
    const url = "https://cdn.plyr.io/3.6.12/plyr.css";
    var plyrCssLink = document.querySelectorAll(`link[href="${url}"]`);
    if (plyrCssLink === null || plyrCssLink.length <= 0) {
        var element = document.createElement("link");
        element.setAttribute("rel", "stylesheet");
        element.setAttribute("type", "text/css");
        element.setAttribute("href", url);
        document.getElementsByTagName("head")[0].appendChild(element);
    }
}

export function initPlyr() {
    setTimeout(function () {
        const player = new Plyr('#player');
    }, 1000);
}