<template>
  <div class="bracket-match" :class="{ 'is-final': isFinal }">
    <div v-if="!isFirstRound" class="incoming-stub"></div>

    <BracketTeam/>

    <!-- Łącznik -->
    <div v-if="!isFinal" class="outgoing-connector" :class="isTopInPair ? 'top-match' : 'bottom-match'">
      
      <div class="line-horizontal"></div>
      
      <div class="line-vertical"></div>
      
    </div>
  </div>
</template>

<script setup>
import {computed} from 'vue';
import BracketTeam from './BracketTeam.vue';

const props = defineProps({
  matchIndex: {
    type: Number,
    required: true
  },
  isFirstRound: {
    type: Boolean,
    default: false
  },
  isFinal: {
    type: Boolean,
    default: false
  }
});

const isTopInPair = computed(() => props.matchIndex % 2 === 0);
</script>

<style scoped>
.bracket-match {
  position: relative;
  display: flex;
  align-items: center;
  margin: var(--vertical-gap, 20px) 0;
}

.incoming-stub {
  position: absolute;
  left: calc(-1 * var(--round-gap, 60px) / 2); /* -x/2 */
  width: calc(var(--round-gap, 60px) / 2);       /* x/2 */
  top: 50%;
  height: 2px;
  background-color: #000;
  transform: translateY(-50%);
}

/* Łącznik wyjściowy L (z prawej) = x / 2 */
.outgoing-connector {
  position: absolute;
  right: calc(-1 * var(--round-gap, 60px) / 2); /* -x/2 */
  width: calc(var(--round-gap, 60px) / 2);        /* x/2 */
  pointer-events: none;
}

.outgoing-connector .line-horizontal {
  position: absolute;
  top: 50%;
  left: 0;
  width: 100%; /* Wypełnia całą szerokość łącznika (czyli x/2) */
  height: 2px;
  background-color: #000;
  transform: translateY(-50%);
}

.outgoing-connector .line-vertical {
  position: absolute;
  right: 0; /* Pionowa kreska staje dokładnie na końcu poziomej */
  width: 2px;
  background-color: #000;
}


.outgoing-connector.top-match {
  top: 50%;
  height: calc(50% + var(--vertical-gap, 20px));
}

.outgoing-connector.top-match .line-vertical {
  top: 0;
  height: 100%;
}

.outgoing-connector.bottom-match {
  bottom: 50%;
  height: calc(50% + var(--vertical-gap, 20px));
}

.outgoing-connector.bottom-match .line-vertical {
  bottom: 0;
  height: 100%;
}

.bracket-match.is-winner .outgoing-connector .line-horizontal,
.bracket-match.is-winner .outgoing-connector .line-vertical,
.bracket-match.is-winner .incoming-stub {
  background-color: #4caf50;
  height: 4px;
}
.bracket-match.is-winner .outgoing-connector .line-vertical {
  width: 4px;
}

</style>