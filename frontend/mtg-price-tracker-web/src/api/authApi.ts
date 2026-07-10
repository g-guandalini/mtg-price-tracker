import { api } from './api'

export interface LoginRequest {
    username: string
    password: string
}

export interface LoginResponse {
    token: string
}

export async function login(
    request: LoginRequest
): Promise<LoginResponse> {

    const response = await api.post(
        '/auth/login',
        request
    )

    return response.data
}

export interface RegisterRequest {
    username: string
    email: string
    password: string
}

export async function register(
    request: RegisterRequest
) {

    await api.post(
        "/auth/register",
        request
    )

}