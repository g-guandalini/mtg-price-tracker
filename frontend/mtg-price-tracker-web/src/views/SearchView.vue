<template>
<main>
<div class="header">
    <h2>
        Pesquisar cartas
    </h2>
    <div
        v-if="token"
        class="user-info"
    >
        <span>
            Olá, {{ auth.username }}
        </span>
        <button
            @click="logout"
        >
            Logout
        </button>
    </div>
</div>
<SearchBar 
 @search="handleSearch"
/>
<p v-if="message">
  {{ message }}
</p>
<CardList
 v-if="cards.length"
 :cards="cards"
 @monitor="handleMonitor"
/>
</main>

</template>
<script setup lang="ts">
import { ref } from 'vue'
import SearchBar from '../components/SearchBar.vue'
import CardList from '../components/CardList.vue'
import { searchCards } from '../api/cardsApi'
import type { Card } from '../models/Card'
import { useRouter } from 'vue-router'
import type { MonitorCardRequest } from '../models/MonitorCardRequest'
import { createTrackedCard } from '../api/trackedCardsApi'
import { computed } from 'vue'
import { useAuthStore } from '../stores/authStore'

const token = computed(() => localStorage.getItem("token"))

const cards = ref<Card[]>([])
const message = ref('')
const router = useRouter()
const auth = useAuthStore()

async function handleSearch(value:string){

  message.value = ''
  cards.value = []
  cards.value = await searchCards(value)

  if(cards.value.length === 0)
  {
    message.value = `Não há correspondências para "${value}"`
  }

}

async function handleMonitor(
    data: MonitorCardRequest
) {
    const token = auth.isAuthenticated
    if (!token) {
        router.push("/login")
        return
    }
    try {
        await createTrackedCard({
            name: data.card.name,
            imageUrl: data.card.imageUrl,
            scryfallId: data.card.scryfallId,
            currentPrice: data.card.currentPrice,
            quantity: data.quantity

        })
        alert("Carta adicionada ao monitoramento!")
    }
    catch {
        alert("Erro ao monitorar carta.")
    }
}

function logout() {
    auth.logout()
    router.push("/login")
}

</script>

<style scoped>

main {
  width:100%;
  max-width:1200px;
  margin:0 auto;
}

.header{
    display:flex;
    justify-content:space-between;
    align-items:center;
    margin-bottom:25px;
}

.user-info{
    display:flex;
    align-items:center;
    gap:12px;
}

.user-info span{
    font-weight:bold;
}

.user-info button{
    padding:8px 14px;
    border:none;
    border-radius:8px;
    cursor:pointer;
}

</style>