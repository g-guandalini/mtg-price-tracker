<template>
<div class="card">
  <img
    :src="imageSource"
    :alt="card.name"
    />
  <div class="content">
    <h3>
      {{ card.name }}
    </h3>
    <span v-if="card.currentPrice">
        US$ {{ card.currentPrice }}
    </span>
    <span v-else>
        Preço indisponível
    </span>
    <input
      type="number"
      min="1"
      v-model="quantity"
    />
    <button @click="monitor">
      Monitorar
    </button>
  </div>
</div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import type { Card } from '../models/Card'

const props = defineProps<{
  card: Card
}>()

const emit = defineEmits<{
    (
        e: 'monitor',
        payload: {
            card: Card
            quantity: number
        }
    ): void
}>()

const quantity = ref(1)

const imageSource = computed(() => {
  if(props.card.imageUrl)
  {
    return props.card.imageUrl
  }

  return '/no-image.png'
})

function monitor(){

    emit(
        'monitor',
        {
            card: props.card,
            quantity: quantity.value
        }
    )

}
</script>


<style scoped>
.card {
  width:250px;
  background:white;
  border-radius:12px;
  overflow:hidden;
  box-shadow:0 4px 12px rgba(0,0,0,0.1);
}

img {
  width:100%;
  height:350px;
  object-fit:cover;
}

.content {
  padding:15px;
  text-align:center;
}

h3 {
  font-size:18px;
  min-height:45px;
}

span {
  display:block;
  margin:10px;
  font-size:20px;
  font-weight:bold;
}

button {
  width:100%;
  padding:10px;
  border:none;
  border-radius:8px;
  cursor:pointer;
}

</style>