
function getWindowSize() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
}

// Opcional: Para re-centrar al redimensionar
window.addEventListener('resize', () => {
    if (window.dotNetHelper) {
        window.dotNetHelper.invokeMethodAsync('RecalcularPosicion');
    }
});