// Adding real-time title preview functionality
document.addEventListener("DOMContentLoaded", function () {
    var titleInput = document.getElementById('title');
    var titlePreview = document.getElementById('form-title-preview');

    titleInput.addEventListener('input', function () {
        var currentTitle = titleInput.value;
        if (currentTitle.length > 0) {
            titlePreview.innerText = "Ny innmelding - " + currentTitle;
        } else {
            titlePreview.innerText = "Ny innmelding";
        }
    });
});