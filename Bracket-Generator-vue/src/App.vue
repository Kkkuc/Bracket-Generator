<script setup>
import { ref, onMounted } from 'vue';

// Adres Twojego API w .NET
const API_URL = '/api/Team';

// Stany (reaktywne zmienne)
const teams = ref([]);
const loading = ref(true);
const errorMessage = ref('');

// Formularz (używany zarówno do dodawania, jak i edycji)
const form = ref({
  id: 0,
  name: ''
});
const isEditing = ref(false);

// --- 1. GET: Pobieranie wszystkich drużyn ---
const fetchTeams = async () => {
  loading.value = true;
  errorMessage.value = '';
  try {
    const res = await fetch(API_URL);
    if (!res.ok) throw new Error('Błąd podczas pobierania danych');
    teams.value = await res.json();
  } catch (err) {
    errorMessage.value = err.message;
  } finally {
    loading.value = false;
  }
};

// --- 2. POST / PUT: Zapisywanie (Dodawanie lub Edycja) ---
const saveTeam = async () => {
  if (!form.value.name.trim()) return;

  try {
    if (isEditing.value) {
      // PUT: api/Team/{id}
      const res = await fetch(`${API_URL}/${form.value.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(form.value)
      });
      if (!res.ok) throw new Error('Nie udało się zaktualizować drużyny');
    } else {
      // POST: api/Team
      const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: form.value.name })
      });
      if (!res.ok) throw new Error('Nie udało się dodać drużyny');
    }

    resetForm();
    await fetchTeams(); // Odświeżenie listy po zapisie
  } catch (err) {
    errorMessage.value = err.message;
  }
};

// --- 3. Wypełnienie formularza do Edycji ---
const editTeam = (team) => {
  form.value = { ...team };
  isEditing.value = true;
};

// --- 4. DELETE: Usuwanie drużyny ---
const deleteTeam = async (id) => {
  if (!confirm('Na pewno chcesz usunąć tę drużynę?')) return;

  try {
    const res = await fetch(`${API_URL}/${id}`, {
      method: 'DELETE'
    });
    if (!res.ok) throw new Error('Nie udało się usunąć drużyny');

    await fetchTeams();
  } catch (err) {
    errorMessage.value = err.message;
  }
};

// Reset formularza
const resetForm = () => {
  form.value = { id: 0, name: '' };
  isEditing.value = false;
};

// Pobieramy dane zaraz po zamontowaniu komponentu
onMounted(() => {
  fetchTeams();
});
</script>

<template>
  <div class="crud-container">
    <h2>Zarządzanie Drużynami</h2>

    <!-- Komunikat o błędzie -->
    <div v-if="errorMessage" class="error-msg">
      {{ errorMessage }}
    </div>

    <!-- Formularz dodawania / edycji -->
    <form @submit.prevent="saveTeam" class="team-form">
      <h3>{{ isEditing ? 'Edytuj drużynę' : 'Dodaj nową drużynę' }}</h3>
      <div class="form-group">
        <input
            v-model="form.name"
            type="text"
            placeholder="Nazwa drużyny..."
            required
        />
        <button type="submit" class="btn btn-primary">
          {{ isEditing ? 'Zapisz zmiany' : 'Dodaj' }}
        </button>
        <button v-if="isEditing" type="button" @click="resetForm" class="btn btn-secondary">
          Anuluj
        </button>
      </div>
    </form>

    <hr />

    <!-- Tabela / Lista drużyn -->
    <div v-if="loading">Ładowanie drużyn...</div>

    <div v-else-if="teams.length === 0">Brak drużyn w bazie. Dodaj pierwszą!</div>

    <table v-else class="teams-table">
      <thead>
      <tr>
        <th>ID</th>
        <th>Nazwa</th>
        <th>Akcje</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="team in teams" :key="team.id">
        <td>{{ team.id }}</td>
        <td>{{ team.name }}</td>
        <td>
          <button @click="editTeam(team)" class="btn btn-edit">Edytuj</button>
          <button @click="deleteTeam(team.id)" class="btn btn-delete">Usuń</button>
        </td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.crud-container {
  max-width: 600px;
  margin: 20px auto;
  font-family: sans-serif;
}

.team-form {
  margin-bottom: 20px;
  background: #f4f4f4;
  padding: 15px;
  border-radius: 6px;
}

.form-group {
  display: flex;
  gap: 10px;
}

input {
  flex: 1;
  padding: 8px;
  font-size: 14px;
}

.btn {
  padding: 8px 12px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.btn-primary { background: #007bff; color: white; }
.btn-secondary { background: #6c757d; color: white; }
.btn-edit { background: #ffc107; color: black; margin-right: 5px; }
.btn-delete { background: #dc3545; color: white; }

.teams-table {
  width: 100%;
  border-collapse: collapse;
}

.teams-table th, .teams-table td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

.error-msg {
  color: red;
  margin-bottom: 10px;
}
</style>

<!--
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
  alabama
</template>

-->