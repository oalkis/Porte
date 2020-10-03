$('#calculate').on("click", function () {
    $.ajax({
        type: "POST",
        url: "/Home/PartialParts",
        success: function (data) {
            $("#box_partial").replaceWith(data);
            $('#box_partial').removeAttr("hidden");

        },
        error: function (error) {
            // handle error
        },
        contentType: false,
        processData: false,
    });
})
