// wwwroot/js/download.js
function downloadFileFromBlazor(fileName, contentType, content) {
    const link = document.createElement("a");
    link.download = fileName;
    link.href = `data:${contentType};base64,${content}`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}