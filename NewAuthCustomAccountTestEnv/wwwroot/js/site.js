// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const loaderContainer = document.getElementById('loading-div-background');
window.addEventListener('load', () => {
    loaderContainer.style.display = 'none';
});

const wrapper = document.getElementById('navbarwrapper');

wrapper.addEventListener('click', (event) => {
    const isButton = event.target.nodeName === 'BUTTON';
    if (!isButton) {
        return;
    }

    loaderContainer.style.display = 'block';
})