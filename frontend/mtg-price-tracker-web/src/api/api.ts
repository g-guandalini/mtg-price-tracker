import axios from 'axios'
import { useAuthStore } from '../stores/authStore'

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "/api"
})

api.interceptors.request.use(config => {

    const auth = useAuthStore()

    if(auth.token){
        config.headers.Authorization =
            `Bearer ${auth.token}`
    }
    return config

})

api.interceptors.response.use(
    response => response,
    error => {

        if (axios.isAxiosError(error)) {

            const message =
                error.response?.data?.message ??
                "Erro inesperado.";

            return Promise.reject(new Error(message));
        }

        return Promise.reject(
            new Error("Erro inesperado.")
        );
    }
);
