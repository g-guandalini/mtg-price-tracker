import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
    state: () => ({
        token: "",
        username: ""
    }),

    getters: {
        isAuthenticated(state) {
            return state.token.length > 0;
        }
    },

    actions: {
        initialize() {
            const token = localStorage.getItem("token");
            if (!token)
                return;
            this.token = token;
            this.loadUserFromToken();
        },

        login(token: string) {
            this.token = token;
            localStorage.setItem("token", token);
            this.loadUserFromToken();
        },

        logout() {
            this.token = "";
            this.username = "";
            localStorage.removeItem("token");
        },

        loadUserFromToken() {
            try {
                const parts = this.token.split(".");
                if (parts.length < 2)
                    return;
                const payload = JSON.parse(
                    (parts && parts[1]) ? atob(parts[1]): ""
                );
                this.username =
                    payload.username ?? "";
            }
            catch {
                this.logout();
            }
        }
    }
});