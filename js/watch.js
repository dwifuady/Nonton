
export function setFullHeightPlayer(numTry) {
    var maxTry = 5;
    if (numTry > maxTry){
        return;
    }
    setTimeout(function () {
        if (document.getElementsByClassName('plyr')[0] !== null &&
            document.getElementsByClassName('plyr')[0] != undefined) {
            document.getElementsByClassName('plyr')[0].classList.add('h-screen');
        } else {
            setFullHeightPlayer(numTry++);
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

export function setScreenOrientation(orientation) {
    screen.orientation.lock(orientation);
}

export function generateSubtitleUrl(data) {
    // Create the URL
    var vttData = srt2webvtt(data);
    const file = new File([vttData], "filename", { type: 'text/vtt' });
    const exportUrl = URL.createObjectURL(file);
    return exportUrl;
    
    //URL.revokeObjectURL(exportUrl);
}


function srt2webvtt(data) {
    // remove dos newlines
    var srt = data.replace(/\r+/g, '');
    // trim white space start and end
    srt = srt.replace(/^\s+|\s+$/g, '');
    // get cues
    var cuelist = srt.split('\n\n');
    var result = "";
    if (cuelist.length > 0) {
        result += "WEBVTT\n\n";
        for (var i = 0; i < cuelist.length; i = i + 1) {
            result += convertSrtCue(cuelist[i]);
        }
    }
    return result;
}
function convertSrtCue(caption) {
    // remove all html tags for security reasons
    //srt = srt.replace(/<[a-zA-Z\/][^>]*>/g, '');
    var cue = "";
    try {
        var s = caption.split(/\n/);
        // concatenate muilt-line string separated in array into one
        while (s.length > 3) {
            for (var i = 3; i < s.length; i++) {
                s[2] += "\n" + s[i]
            }
            s.splice(3, s.length - 3);
        }
        var line = 0;
        // detect identifier
        if (!s[0].match(/\d+:\d+:\d+/) && s[1].match(/\d+:\d+:\d+/)) {
            cue += s[0].match(/\w+/) + "\n";
            line += 1;
        }
        // get time strings
        if (s[line].match(/\d+:\d+:\d+/)) {
            // convert time string
            var m = s[1].match(/(\d+):(\d+):(\d+)(?:,(\d+))?\s*--?>\s*(\d+):(\d+):(\d+)(?:,(\d+))?/);
            if (m) {
                cue += m[1] + ":" + m[2] + ":" + m[3] + "." + m[4] + " --> "
                    + m[5] + ":" + m[6] + ":" + m[7] + "." + m[8] + "\n";
                line += 1;
            } else {
                // Unrecognized timestring
                return "";
            }
        } else {
            // file format error or comment lines
            return "";
        }
        // get cue text
        if (s[line]) {
            cue += s[line] + "\n\n";
        }
    } catch (e) {
        console.log("convertSrtCue Error :" + e.message);
    } 
    return cue;
}