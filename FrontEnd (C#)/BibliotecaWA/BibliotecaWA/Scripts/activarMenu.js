document.addEventListener("DOMContentLoaded", function () {
    const currentPage = window.location.pathname.split("/").pop().toLowerCase();

    document.querySelectorAll(".mynav a").forEach(link => {
        const href = link.getAttribute("href")?.toLowerCase();
        if (href && currentPage === href) {
            link.classList.add("active");
        }
    });
});