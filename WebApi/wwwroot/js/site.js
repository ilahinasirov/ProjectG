$(document).ready(function (e) {
    let notification = $(".notification-container");

    if (notification !== null && notification !== undefined) {
        notification.animate({ right: "15px" }, 500, function () {
            setTimeout(function () {
                notification.animate({ right: "-500px" }, 500);
            }, 2000);
        });
    }
});