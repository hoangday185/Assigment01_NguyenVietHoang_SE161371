// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let currentPage = 1;
const pageSize = 3;

// Define LoadData globally
function LoadData() {

     $.ajax({
                url: window.location.href, // Gửi request đến chính trang này
                type: "GET",
                success: function (data) {
                    let newTags = $(data).find("#tagContainer").html(); // Lấy nội dung mới
                    if (newTags) {
                        $("#tagContainer").html(newTags);
                        console.log("Tag container updated!");
                    } else {
                        console.warn("No #tagContainer found in response.");
                    }
                },
                error: function (error) {
                    console.error("Error reloading tags: ", error);
                }
            });

}

$(() => {

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalR")
        .configureLogging(signalR.LogLevel.Debug)
        .build();

    connection.start()
        .then(() => console.log("SignalR connection established."))
        .catch(err => console.error("SignalR connection failed: ", err));

    connection.on("TagsUpdated", function () {
        console.log("Tags updated, reloading...");
        LoadData();
    });
});
