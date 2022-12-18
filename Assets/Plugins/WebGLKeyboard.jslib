mergeInto(LibraryManager.library, {

    OpenInputKeyboard: function (currentValue) {
        document.getElementById("fixInput").value = Pointer_stringify(currentValue);
        document.getElementById("fixInput").focus();
    },
    CloseInputKeyboard: function () {
        document.getElementById("fixInput").blur();
    },
    FixInputOnBlur: function () {
        SendMessage('_WebGLKeyboard', 'LoseFocus');
    },
    FixInputUpdate: function () {
        SendMessage('_WebGLKeyboard', 'ReceiveInputChange', document.getElementById("fixInput").value);
    },
    UpdateKeyboard: function () {
        var returnStr = document.getElementById("fixInput").value
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    },
    IsMobile: function () {
        const toMatch = [
            /Android/i,
            /webOS/i,
            /iPhone/i,
            /iPad/i,
            /iPod/i,
            /BlackBerry/i,
            /Windows Phone/i
        ];

        return toMatch.some((toMatchItem) => {
            return navigator.userAgent.match(toMatchItem);
        });
    }
});