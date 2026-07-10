import axios from 'axios'
import type { Card } from '../models/Card'
import { api } from './api'

export async function searchCards(
  name: string
): Promise<Card[]> {


  const response = await api.get(
    '/cards/search',
    {
      params: {
        name
      }
    }
  )


  return response.data

}