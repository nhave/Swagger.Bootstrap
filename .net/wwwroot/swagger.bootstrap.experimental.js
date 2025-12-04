(function () {
    let stickyNav = false;

    function addStickyEventHandler() {
        const navCard = document.getElementById("navCard");
        const toolsCard = document.getElementById("toolsCard");

        const startRect = navCard.getBoundingClientRect();
        const startTop = startRect.top + window.scrollY - 16;

        window.addEventListener("scroll", () => {
            if (window.scrollY >= startTop && !stickyNav) {
                stickyNav = true;
                navCard.classList.add("sticky");
                toolsCard.classList.add("sticky");
            } else if (window.scrollY < startTop && stickyNav) {
                stickyNav = false;
                navCard.classList.remove("sticky");
                toolsCard.classList.remove("sticky");
            }
        });
    }

    function collapseAll() {
        const opblockTags = document.querySelectorAll(".opblock-tag-section");

        opblockTags.forEach(opblockTag => {

            const opblockActions = opblockTag.querySelectorAll(".opblock");

            opblockActions.forEach(opblockAction => {
                if (opblockAction.classList.contains("is-open")) {
                    const control = opblockAction.querySelector(".opblock-summary-control");
                    control.click();
                }
            });

            if (opblockTag.classList.contains("is-open")) {
                const h3 = opblockTag.querySelector("h3");
                h3.click();
            }
        });

        const models = document.querySelectorAll(".models");
        models.forEach(model => {
            if (model.classList.contains("is-open")) {
                const control = model.querySelector(".models-control");
                control.click();
            }
        });
    }

    function addNavigation() {
        const wrapper = document.querySelector(".swagger-ui .swagger-ui > div > .wrapper:not(.information-container)");
        if (!wrapper) return;

        const navCard = document.createElement("div");
        navCard.classList.add("card", "navigation-card", "shadow");
        navCard.id = "navCard"
        wrapper.prepend(navCard);

        const navCardHeader = document.createElement("h1");
        navCardHeader.classList.add("card-header");
        navCardHeader.innerHTML = "Navigation"
        navCard.append(navCardHeader);

        const navCardBody = document.createElement("div");
        navCardBody.classList.add("card-body");
        navCard.append(navCardBody);

        const navTopButton = document.createElement("button");
        navTopButton.classList.add("nav-link");
        navTopButton.innerHTML = "Scroll to top";
        navTopButton.addEventListener("click", () => {
            window.scrollTo(0, 0);
        });
        navCardBody.append(navTopButton);

        //const navDivider = document.createElement("hr");
        //navDivider.classList.add("dropdown-divider");
        //navCardBody.append(navDivider);

        const opblocks = document.querySelectorAll(".opblock-tag-section")

        opblocks.forEach(opblock => {
            const span = opblock.querySelector("h3 span");

            const navButton = document.createElement("button");
            navButton.classList.add("nav-link");
            navButton.innerHTML = span.innerHTML;

            navButton.addEventListener("click", () => {
                opblock.scrollIntoView({
                    behavior: "smooth",
                    block: "start"
                });
            });

            navCardBody.append(navButton);
        });
    }

    function addTools() {
        const wrapper = document.querySelector(".swagger-ui .swagger-ui > div > .wrapper:not(.information-container)");
        if (!wrapper) return;

        const toolsCardWrapper = document.createElement("div");
        toolsCardWrapper.classList.add("tools-card-wrapper");
        wrapper.prepend(toolsCardWrapper);

        const toolsCard = document.createElement("div");
        toolsCard.classList.add("card", "tools-card", "shadow");
        toolsCard.id = "toolsCard"
        toolsCardWrapper.append(toolsCard);

        const navCardHeader = document.createElement("h1");
        navCardHeader.classList.add("card-header");
        navCardHeader.innerHTML = "Tools"
        toolsCard.append(navCardHeader);

        const navCardBody = document.createElement("div");
        navCardBody.classList.add("card-body");
        toolsCard.append(navCardBody);

        const collapseButton = document.createElement("button");
        collapseButton.classList.add("nav-link");
        collapseButton.innerHTML = "Collapse all";
        collapseButton.addEventListener("click", () => {
            collapseAll();
        });
        navCardBody.append(collapseButton);
    }

    document.addEventListener("DOMContentLoaded", async () => {
        const observer = new MutationObserver(() => {
            const wrappers = document.querySelectorAll(".swagger-ui .swagger-ui .wrapper");
            if (wrappers.length > 1) {
                addNavigation();
                addTools();
                addStickyEventHandler();
                observer.disconnect();
            }
        });

        observer.observe(document.body, { childList: true, subtree: true });
    });
})();