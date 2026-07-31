<template>
  <div class="bracket-round">
    <BracketMatch
        v-for="(_, index) in matchCount"
        :key="index"
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
  isFinal: {
    type: Boolean,
    default: false
  }
});
</script>

<style scoped>
.bracket-round {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  margin-right: 50px;
}
</style>