<template>
  <div class="bracket-round">
    <BracketMatch
        v-for="(_, index) in matchCount"
        :key="index"
        :match-index="index"
        :is-first-round="isFirstRound"
        :is-final="isFinal"
    />
  </div>
</template>

<script setup>
import { computed } from 'vue';
import BracketMatch from './BracketMatch.vue';

const props = defineProps({
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
  },
  baseVerticalGap: {
    type: Number,
    default: 20
  }
});

const computedVerticalGap = computed(() => {
  return props.baseVerticalGap * Math.pow(2, props.roundIndex);
});
</script>

<style scoped>
.bracket-round {
  --round-gap: v-bind(gap + 'px');
  --vertical-gap: v-bind(computedVerticalGap + 'px');

  display: flex;
  flex-direction: column;
  justify-content: space-around;
  margin-right: var(--round-gap);
}
</style>