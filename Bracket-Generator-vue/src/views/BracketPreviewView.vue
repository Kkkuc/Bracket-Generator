<template>
  <div class="bracket-preview-view">
    <BracketContainer :team-count="32" />
  </div>
</template>

<script setup>
import BracketContainer from '../components/bracket/BracketContainer.vue';
</script>

<style scoped>
.bracket-preview-view {
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>