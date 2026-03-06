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
======= Swagger.Bootstrap V2.0 by N-Tech ======================================= */

(function () {
    let loginWindow = null;

    function addLoginButton() {
        const container = document.querySelector(".auth-wrapper");
        if (!container) return;

        if (container.querySelector(".swagger-login-btn")) return;

        const btn = document.createElement("button");
        btn.innerText = "Login";
        btn.className = "authorize btn swagger-login-btn";
        btn.style.marginRight = ".5rem";
        btn.onclick = openLoginWindow;

        const authorizeBtn = container.firstChild;

        if (authorizeBtn) {
            container.insertBefore(btn, authorizeBtn);
        } else {
            container.appendChild(btn);
        }
    }

    function addToolsLoginButton(container) {
        const btn = document.createElement("button");
        btn.innerText = "Login";
        btn.className = "nav-link";
        btn.onclick = openLoginWindow;

        container.appendChild(btn);
    }

    function openLoginWindow() {
        const loginUrl = `/swaggerbootstrap/login.html`;

        const w = 500, h = 600;
        const left = window.screenX + (window.outerWidth - w) / 2;
        const top = window.screenY + (window.outerHeight - h) / 2;

        loginWindow = window.open(loginUrl, "Login", `width=${w},height=${h},left=${left},top=${top}`);
    }

    window.addEventListener("message", async (event) => {
        if (event.data && event.data.type === "login-complete") {

            const token = event.data.payload;
            if (token) {
                window.ui.preauthorizeApiKey("Bearer", token);
            }

            if (loginWindow && !loginWindow.closed) {
                loginWindow.close();
            }
            loginWindow = null;
        }
    });

    document.addEventListener("DOMContentLoaded", async () => {
        const authWrapperObserver = new MutationObserver(() => {
            const container = document.querySelector(".auth-wrapper");
            if (container) {
                addLoginButton();
                authWrapperObserver.disconnect();
            }
        });

        authWrapperObserver.observe(document.body, { childList: true, subtree: true });
    });

    document.addEventListener("toolsCardReady", (e) => {
        const navCardBody = e.detail.navCardBody;
        addToolsLoginButton(navCardBody);
    });
})();