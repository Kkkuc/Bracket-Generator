<template>
  <div class="bracket-match" :class="{ 'is-final': isFinal }">
    <BracketTeam/>

    <!-- Łącznik -->
    <div v-if="!isFinal" class="line-connector">
      <div class="line-horizontal-short"></div>
      <div class="line-vertical"></div>
      <div class="line-horizontal-long" v-if="isTopInPair"></div>
    </div>
  </div>
</template>

<script setup>
import {computed, getCurrentInstance} from 'vue';
import BracketTeam from './BracketTeam.vue';

defineProps({
  isFinal: {type: Boolean, default: false}
});

// Sprawdzamy czy mecz jest górnym (nieparzystym) czy dolnym w parze
const instance = getCurrentInstance();
const isTopInPair = computed(() => {
  return (instance?.vnode.key % 2) === 0;
});
</script>

<style scoped>
.bracket-match {
  position: relative;
  display: flex;
  align-items: center;
  margin: 20px 0;
}

.bracket-round {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  margin-right: 60px; /* Odstęp między rundami */
}

/* Pozioma linia z kafelka */
.bracket-match::after {
  content: '';
  position: absolute;
  right: -30px; /* 50% z 60px */
  top: 50%;
  width: 30px;
  height: 2px;
  background: #000;
}

/* Pionowa linia łącząca mecz 1 i 2 */
.bracket-match:nth-child(odd)::before {
  content: '';
  position: absolute;
  right: -30px;
  top: 50%;
  height: calc(100% + 40px); /* Pokrywa dystans do dolnego meczu */
  width: 2px;
  background: #000;
}

/* Poziomy "odcinek wychodzący" ze środka pionowej linii do następnej rundy */
.bracket-match:nth-child(odd) .bracket-team::after {
  content: '';
  position: absolute;
  right: -60px;
  /* Ustawia się w połowie wysokości między nieparzystym a parzystym meczem */
  top: calc(100% + 20px);
  width: 30px;
  height: 2px;
  background: #000;
}

.bracket-match.is-final::after,
.bracket-match.is-final::before {
  display: none;
}
</style>