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

document.getElementById('add-attachment').addEventListener('click', function () {
    // Create a new input element for file upload
    var newAttachmentInput = document.createElement('input');
    newAttachmentInput.type = 'file';
    newAttachmentInput.name = 'Attachments';
    newAttachmentInput.classList.add('form-control');
    newAttachmentInput.classList.add('mt-2'); // Add some margin for spacing

    // Add the new input to the attachments container
    document.getElementById('attachments-container').appendChild(newAttachmentInput);
});