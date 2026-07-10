import { api } from './api'

export interface CreateTrackedCardRequest {
    name: string
    imageUrl: string
    scryfallId: string
    currentPrice: number
    quantity: number
}

export async function createTrackedCard(
    request: CreateTrackedCardRequest
) {

    const response = await api.post(
        '/tracked-cards',
        request
    )

    return response.data
}