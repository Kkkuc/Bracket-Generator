<template>
  <div
      class="bracket-match"
      :class="{ 
      'is-final': isFinal, 
      'is-first-round': isFirstRound,
      'top-match': isTopInPair, 
      'bottom-match': !isTopInPair 
    }"
  >
    <BracketTeam />
  </div>
</template>

<script setup>
import { computed } from 'vue';
import BracketTeam from './BracketTeam.vue';

const props = defineProps({
  matchIndex: { type: Number, required: true },
  roundIndex: { type: Number, default: 0 },
  isFirstRound: { type: Boolean, default: false },
  isFinal: { type: Boolean, default: false },
  baseGap: { type: Number, default: 20 } // opcjonalny prop na bazowy margines
});

const isTopInPair = computed(() => props.matchIndex % 2 === 0);

// Mnożnik rundy: 1, 2, 4, 8...
const scaleFactor = computed(() => Math.pow(2, props.roundIndex));
</script>

<style scoped>
.bracket-match {
  position: relative;
  display: flex;
  align-items: center;
  margin: calc(v-bind(baseGap + 'px') * v-bind(scaleFactor)) 0;
}

.bracket-match:not(.is-first-round)::before {
  content: '';
  position: absolute;
  left: calc(-1 * var(--round-gap, 60px) / 2);
  width: calc(var(--round-gap, 60px) / 2);
  top: 50%;
  height: 2px;
  background-color: #000;
  transform: translateY(-50%);
}

.bracket-match:not(.is-final)::after {
  content: '';
  position: absolute;
  right: calc(-1 * var(--round-gap, 60px) / 2);
  width: calc(var(--round-gap, 60px) / 2);
  height: calc((50% + v-bind(baseGap + 'px')) * v-bind(scaleFactor));

  pointer-events: none;
  box-sizing: border-box;
}

.bracket-match.top-match:not(.is-final)::after {
  top: 50%;
  border-top: 2px solid #000;
  border-right: 2px solid #000;
}

.bracket-match.bottom-match:not(.is-final)::after {
  bottom: 50%;
  border-bottom: 2px solid #000;
  border-right: 2px solid #000;
}
</style>