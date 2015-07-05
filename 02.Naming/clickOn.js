function clickONTheButton(event, arguments) {
    var curetWindow = window,
        curentBrowser = curetWindow.navigator.appCodeName,
        isSelected = curentBrowser == "Mozilla";
    if (isSelected) {
        alert("Yes");
    } else {
        alert("No");
    }
}