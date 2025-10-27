window.startPageTransition = (direction) => {
    const body = document.body;
    body.classList.remove("transition-forward", "transition-backward");
    body.classList.add(direction === "backward" ? "transition-backward" : "transition-forward");
};

window.playPageEntryTransition = () => {
    const body = document.body;
    const direction = sessionStorage.getItem("pageTransitionDirection");
    sessionStorage.removeItem("pageTransitionDirection");

    if (!direction) return;

    body.classList.remove("transition-forward", "transition-backward");
    body.classList.add(direction === "backward" ? "enter-from-left" : "enter-from-right");

    setTimeout(() => {
        body.classList.remove("enter-from-left", "enter-from-right");
    }, 600);
};
