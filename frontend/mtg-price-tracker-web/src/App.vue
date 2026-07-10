<template>

  <div class="app">

    <header>
      <h1>
        MTG Price Tracker
      </h1>

      <p>
        Acompanhe preços das suas cartas de Magic
      </p>
    </header>


    <RouterView />

  </div>

</template>


<script setup lang="ts">

import SearchView from './views/SearchView.vue'

</script>


<style scoped>

.app {
  min-height: 100vh;
  padding: 40px;
  box-sizing: border-box;
}

header {
  text-align:center;
  margin-bottom:40px;
}

h1 {
  font-size:36px;
}

p {
  color:#666;
}

main {
  width: 100%;
  max-width: 1200px;
  margin: 0 auto;
}

* {
  box-sizing: border-box;
}

body {
  margin: 0;
}

</style>