<template>
  <div class="bracket-container">
    <BracketRound
        v-for="(matches, index) in rounds"
        :key="index"
        :match-count="matches"
        :round-index="index"
        :is-final="index === rounds.length - 1"
        :is-first-round="index === 0"
    />
  </div>
</template>

<script setup>
import { computed } from 'vue';
import BracketRound from './BracketRound.vue';

const props = defineProps({
  teamCount: {
    type: Number,
    default: 8
  }
});

const rounds = computed(() => {
  const result = [];
  let matches = props.teamCount / 2;

  while (matches >= 1) {
    result.push(matches);
    matches /= 2;
  }

  return result;
});
</script>

<style scoped>
.bracket-container {
  display: flex;
  flex-direction: row;
  padding: 40px;
  background-color: #ffffff;
  overflow-x: auto;
}
</style>