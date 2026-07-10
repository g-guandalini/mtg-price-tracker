<template>

  <div class="list">

    <CardItem
      v-for="card in cards"
      :key="card.scryfallId"
      :card="card"
       @monitor="emit('monitor', $event)"
    />

  </div>

</template>


<script setup lang="ts">
import CardItem from './CardItem.vue'
import type { Card } from '../models/Card'

defineProps<{
  cards: Card[]
}>()

const emit = defineEmits<{
    (
        e:'monitor',
        payload:any
    ):void

}>()

</script>


<style scoped>

.list {

  display:grid;

  grid-template-columns:
    repeat(auto-fill, 250px);

  justify-content:center;

  gap:25px;
}

</style>