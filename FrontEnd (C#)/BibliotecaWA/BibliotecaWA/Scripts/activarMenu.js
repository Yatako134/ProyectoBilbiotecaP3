document.addEventListener("DOMContentLoaded", function () {
    const currentPage = window.location.pathname.split("/").pop().toLowerCase();

    document.querySelectorAll(".mynav a").forEach(link => {
        const href = link.getAttribute("href")?.split("?")[0].toLowerCase(); // Ignorar querystrings
        if (href && currentPage.includes(href)) { // Usar includes en lugar de ===
            link.classList.add("active");
        }
    });
});