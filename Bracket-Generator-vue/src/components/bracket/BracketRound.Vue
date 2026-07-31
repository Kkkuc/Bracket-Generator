<template>
  <div class="bracket-round">
    <BracketMatch
        v-for="(_, index) in matchCount"
        :key="index"
        :match-index="index"
        :round-index="roundIndex"
        :is-first-round="isFirstRound"
        :is-final="isFinal"
    />
  </div>
</template>

<script setup>
import BracketMatch from './BracketMatch.vue';

defineProps({
  matchCount: {
    type: Number,
    required: true
  },
  roundIndex: {
    type: Number,
    default: 0
  },
  isFirstRound: {
    type: Boolean,
    default: false
  },
  isFinal: {
    type: Boolean,
    default: false
  },
  gap: {
    type: Number,
    default: 60
  }
});
</script>

<style scoped>
.bracket-round {
  --round-gap: v-bind(gap + 'px');

  display: flex;
  flex-direction: column;
  justify-content: space-around;
  margin-right: var(--round-gap);
}
</style>