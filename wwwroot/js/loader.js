(function () {
    // Cria o loader visual
    const loader = document.createElement("div");
    loader.id = "pageLoader";
    loader.style.position = "fixed";
    loader.style.top = 0;
    loader.style.left = 0;
    loader.style.width = "100vw";
    loader.style.height = "100vh";
    loader.style.backgroundColor = "rgba(59, 7, 100, 0.95)";
    loader.style.zIndex = 9999;
    loader.style.display = "flex";
    loader.style.alignItems = "center";
    loader.style.justifyContent = "center";
    loader.innerHTML = `
        <div style="width:60px;height:60px;border:6px solid white;border-top:6px solid transparent;
                    border-radius:50%;animation:spin 1s linear infinite;"></div>
        <style>
            @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
            }
        </style>
    `;

    // Insere no body imediatamente
    document.addEventListener("DOMContentLoaded", () => {
        document.body.appendChild(loader);

        // Esconde após a página carregar
        window.requestAnimationFrame(() => {
            window.setTimeout(() => {
                loader.style.display = "none";
            }, 300); // pequeno delay para suavidade
        });

        // Exibe novamente ao clicar em links internos
        document.querySelectorAll("a[href]:not([target]):not([download])").forEach(link => {
            const href = link.getAttribute("href");
            if (href && !href.startsWith("#") && !href.startsWith("javascript:")) {
                link.addEventListener("click", function () {
                    loader.style.display = "flex";
                });
            }
        });
    });
})();
