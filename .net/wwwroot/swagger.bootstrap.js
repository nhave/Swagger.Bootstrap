/* ================================================================================
    ███████╗██╗    ██╗ █████╗  ██████╗  ██████╗ ███████╗██████╗                 
    ██╔════╝██║    ██║██╔══██╗██╔════╝ ██╔════╝ ██╔════╝██╔══██╗                
    ███████╗██║ █╗ ██║███████║██║  ███╗██║  ███╗█████╗  ██████╔╝                
    ╚════██║██║███╗██║██╔══██║██║   ██║██║   ██║██╔══╝  ██╔══██╗                
    ███████║╚███╔███╔╝██║  ██║╚██████╔╝╚██████╔╝███████╗██║  ██║██╗             
    ╚══════╝ ╚══╝╚══╝ ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝             
                                                                            
    ██████╗  ██████╗  ██████╗ ████████╗███████╗████████╗██████╗  █████╗ ██████╗ 
    ██╔══██╗██╔═══██╗██╔═══██╗╚══██╔══╝██╔════╝╚══██╔══╝██╔══██╗██╔══██╗██╔══██╗
    ██████╔╝██║   ██║██║   ██║   ██║   ███████╗   ██║   ██████╔╝███████║██████╔╝
    ██╔══██╗██║   ██║██║   ██║   ██║   ╚════██║   ██║   ██╔══██╗██╔══██║██╔═══╝ 
    ██████╔╝╚██████╔╝╚██████╔╝   ██║   ███████║   ██║   ██║  ██║██║  ██║██║     
    ╚═════╝  ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝      
======= Swagger.Bootstrap V1.1 by N-Tech ======================================= */

(function () {
    let currentTheme = "system";

    function changeTheme(theme) {
        localStorage.setItem("colorTheme", theme.toLowerCase());
        updateTheme();
        currentTheme = theme.toLowerCase();
    }

    function updateTheme() {
        const themeSelectIcon = document.getElementById("themeSelectIcon");
        const hasIcon = themeSelectIcon != null;
        if (hasIcon) {
            themeSelectIcon.classList.remove("bi-circle-half", "bi-sun-fill", "bi-moon-stars-fill", "bi-lightbulb-off-fill");
        }

        const themeSelectLight = document.getElementById("themeSelectLight");
        const themeSelectDark = document.getElementById("themeSelectDark");
        const themeSelectAuto = document.getElementById("themeSelectSystem");
        const themeSelectOled = document.getElementById("themeSelectOled");

        if (hasIcon) {
            themeSelectLight.classList.remove("active");
            themeSelectDark.classList.remove("active");
            themeSelectAuto.classList.remove("active");
            themeSelectOled.classList.remove("active");
        }

        const theme = localStorage.getItem("colorTheme");

        if (theme == "light") {
            document.querySelector("html").setAttribute("data-bs-theme", "light")
            if (hasIcon) {
                themeSelectIcon.classList.add("bi-sun-fill");
                themeSelectLight.classList.add("active");
            }
            currentTheme = "light";
        }
        else if (theme == "dark") {
            document.querySelector("html").setAttribute("data-bs-theme", "dark")
            if (hasIcon) {
                themeSelectIcon.classList.add("bi-moon-stars-fill");
                themeSelectDark.classList.add("active");
            }
            currentTheme = "dark";
        }
        else if (theme == "oled") {
            document.querySelector("html").setAttribute("data-bs-theme", "oled")
            if (hasIcon) {
                themeSelectIcon.classList.add("bi-lightbulb-off-fill");
                themeSelectOled.classList.add("active");
            }
            currentTheme = "oled";
        }
        else {
            document.querySelector("html").setAttribute("data-bs-theme",
                window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light")
            if (hasIcon) {
                themeSelectIcon.classList.add("bi-circle-half");
                themeSelectAuto.classList.add("active");
            }
            currentTheme = "system";
        }
    }

    function addThemeButton() {
        const topbar = document.querySelector('.topbar-wrapper');
        if (!topbar) return;

        const dropdown = document.createElement("div");
        dropdown.classList.add("bs-btn-group", "dropdown", "nav-item");
        topbar.appendChild(dropdown);

        const dropToggleBtn = document.createElement("button");
        dropToggleBtn.classList.add("nav-link", "dropdown-toggle");
        dropToggleBtn.type = "button";

        dropToggleBtn.addEventListener("click", () => {
            dropdownMenu.classList.add("show");
        });
        dropdown.appendChild(dropToggleBtn);

        const icon = document.createElement("i");
        icon.id = "themeSelectIcon";
        icon.classList.add("bi");
        dropToggleBtn.appendChild(icon);

        const dropdownMenu = document.createElement("ul");
        dropdownMenu.classList.add("dropdown-menu", "dropdown-menu-end");
        dropdown.appendChild(dropdownMenu);

        const themes = [["Light", "bi-sun-fill"], ["Dark", "bi-moon-stars-fill"], ["Oled", "bi-lightbulb-off-fill"], ["System", "bi-circle-half"]];
        for (i in themes) {
            const theme = themes[i][0];
            const bi = themes[i][1];

            if (currentTheme == theme.toLowerCase()) icon.classList.add(bi);

            const li = document.createElement("li");
            dropdownMenu.appendChild(li);

            const themeBtn = document.createElement("button");
            themeBtn.classList.add("dropdown-item", "d-flex", "align-items-center");
            if (currentTheme == theme.toLowerCase()) {
                themeBtn.classList.add("active");
            }
            themeBtn.id = `themeSelect${theme}`;
            themeBtn.onclick = () => {
                changeTheme(theme)
            };
            li.appendChild(themeBtn);

            const themeIcon = document.createElement("i");
            themeIcon.classList.add("bi", bi, "me-2");
            themeBtn.appendChild(themeIcon);
            themeBtn.append(`${theme}`);

            const themeCheck = document.createElement("i");
            themeCheck.classList.add("bi", "bi-check", "ms-auto", "dropdown-check");
            themeBtn.appendChild(themeCheck);

            document.body.addEventListener("click", (e) => {
                const btn = e.target.closest(".dropdown-toggle");
                if (!btn) {
                    dropdownMenu.classList.remove("show");
                }
            });
        }
    }

    updateTheme();

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', updateTheme);

    document.addEventListener("DOMContentLoaded", async () => {
        const observer = new MutationObserver(() => {
            const container = document.querySelector(".topbar-wrapper");
            if (container) {
                addThemeButton();
                observer.disconnect();
            }
        });

        observer.observe(document.body, { childList: true, subtree: true });
    });
})();
