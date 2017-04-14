import { RouterOutlet } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "episode-master-detail", authRequired: true },
            { path: "/tab/:tabIndex", name: "episode-master-detail", authRequired: true },
            { path: "/episode/edit/:episodeId/tab/:tabIndex", name: "episode-master-detail", authRequired: true },
            { path: "/episode/edit/:episodeId", name: "episode-master-detail", authRequired: true },
            { path: "/register", name: "account-register" },
            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }
        ] as any);

        this.use(new AuthorizedRouteMiddleware());

        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);