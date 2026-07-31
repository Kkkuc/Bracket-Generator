<template>
  <div
      class="bracket-match"
      :class="{ 
      'is-final': isFinal, 
      'is-first-round': isFirstRound,
      'top-match': isTopInPair, 
      'bottom-match': !isTopInPair 
    }">

    <BracketTeam/>
    
  </div>
</template>

<script setup>
import {computed} from 'vue';
import BracketTeam from './BracketTeam.vue';

const props = defineProps({
  matchIndex: {type: Number, required: true},
  isFirstRound: {type: Boolean, default: false},
  isFinal: {type: Boolean, default: false}
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
  height: calc(50% + var(--vertical-gap, 20px));
  pointer-events: none;
}

/* Dla meczu na górze: linia na dole i z prawej */
.bracket-match.top-match:not(.is-final)::after {
  top: 50%;
  border-top: 2px solid #000;    /* Pozioma kreska */
  border-right: 2px solid #000;  /* Pionowa kreska w dół */
}

/* Dla meczu na dole: linia na górze i z prawej */
.bracket-match.bottom-match:not(.is-final)::after {
  bottom: 50%;
  border-bottom: 2px solid #000; /* Pozioma kreska */
  border-right: 2px solid #000;  /* Pionowa kreska w górę */
}

</style>