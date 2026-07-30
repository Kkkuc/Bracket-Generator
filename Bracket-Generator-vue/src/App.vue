<!--
<script setup>
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <HelloWorld />
  <div style="font-family: sans-serif; padding: 2rem; text-align: center;">
    <h1>Bracket Generator 🏆</h1>
    <p>Aplikacja Vue 3 połączona z .NET API działa!</p>
  </div>
  alabama
</template>
-->

<script setup>
import { ref, onMounted } from 'vue';

const forecasts = ref([]);
const loading = ref(true);

onMounted(async () => {
  try {
    const response = await fetch('/weatherforecast');
    forecasts.value = await response.json();
  } catch (error) {
    console.error('Błąd podczas pobierania z API:', error);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <main style="font-family: sans-serif; padding: 2rem;">
    <h1>Bracket Generator 🏆</h1>

    <h2>Prognoza z .NET API:</h2>
    <div v-if="loading">Ładowanie danych...</div>
    <ul v-else>
      <li v-for="(item, index) in forecasts" :key="index">
        <strong>{{ item.date }}</strong>: {{ item.temperatureC }}°C ({{ item.summary }})
      </li>
    </ul>
  </main>
</template>