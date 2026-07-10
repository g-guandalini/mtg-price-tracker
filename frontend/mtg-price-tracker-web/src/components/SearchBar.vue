<template>
  <div>
    <form @submit.prevent="search">
      <input
        v-model="searchText"
        placeholder="Digite o nome da carta"
      />

      <button @click="search">
        Pesquisar
      </button>
    </form>
  </div>
</template>


<script setup lang="ts">

import { ref } from 'vue'


const searchText = ref('')


const emit = defineEmits<{
  search: [value: string]
}>()


function search() {

  emit('search', searchText.value)

}

</script>

<style scoped>

div {
  width:100%;
  display:flex;
  justify-content:center;
  gap:10px;
  margin-bottom:40px;
}

input {

  width:400px;

  padding:12px 16px;

  border-radius:8px;

  border:1px solid #ccc;

  font-size:16px;

}


button {

  padding:12px 25px;

  border:none;

  border-radius:8px;

  cursor:pointer;

  font-size:16px;

}

</style>