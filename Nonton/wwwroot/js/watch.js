
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
        const player = new Plyr('#player', { autoplay: true });

        window.player = player;

        player.on('enterfullscreen', event => {
            screen.orientation.lock('landscape');
        });

        player.on('exitfullscreen', event => {
            screen.orientation.lock('portrait');
        });
    }, 1000);
}

export function downloadSubtitle(filename, data) {

    // Create the URL
    const file = new File([data], filename, { type: 'text/vtt' });
    const exportUrl = URL.createObjectURL(file);
    window.open(exportUrl, "_self");
    // Create the <a> element and click on it
    //const a = document.createElement("a");
    //document.body.appendChild(a);
    //a.href = exportUrl;
    //a.download = filename;
    //a.target = "_self";
    //a.click();

    // We don't need to keep the url, let's release the memory
    // On Safari it seems you need to comment this line... (please let me know if you know why)
    URL.revokeObjectURL(exportUrl);
    //a.remove();
}