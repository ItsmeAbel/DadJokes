// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//typewriter row by row
    document.addEventListener("DOMContentLoaded", function () {
    const lines = document.querySelectorAll(".typewriter");
    let lineIndex = 0;

    function typeLine() {
        if (lineIndex >= lines.length) return;

    const el = lines[lineIndex];
    const fullText = el.textContent.trim();
    el.textContent = "";
    el.style.visibility = "visible"; //reveal only when typing starts

    let i = 0;

    function typeChar() {
            if (i < fullText.length) {
        el.textContent += fullText.charAt(i);
    i++;
    setTimeout(typeChar, 50); // typing speed
            } else {
        el.style.borderRight = "none";
    lineIndex++;
    setTimeout(typeLine, 500);
            }
        }

    typeChar();
    }

    typeLine();
});



