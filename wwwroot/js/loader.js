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
    loader.style.transition = "opacity 0.3s ease";
    loader.style.opacity = 1;
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

    // Insere no body e gerencia exibição
    window.addEventListener("pageshow", () => {
        // Adiciona o loader (caso não exista)
        if (!document.getElementById("pageLoader")) {
            document.body.appendChild(loader);
        }

        // Oculta com delay
        window.requestAnimationFrame(() => {
            setTimeout(() => {
                loader.style.opacity = 0;
                setTimeout(() => {
                    loader.style.display = "none";
                }, 300); // tempo do fade-out
            }, 300);
        });

        // Exibe novamente ao clicar em links internos
        document.querySelectorAll("a[href]:not([target]):not([download])").forEach(link => {
            const href = link.getAttribute("href");
            if (href && !href.startsWith("#") && !href.startsWith("javascript:")) {
                link.addEventListener("click", function () {
                    loader.style.display = "flex";
                    loader.style.opacity = 1;
                });
            }
        });
    });
})();
