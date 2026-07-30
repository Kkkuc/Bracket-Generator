import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5095', // <-- TUTAJ wpisz port, na którym działa Twój .NET!
        changeOrigin: true,
        secure: false // Wyłącza sprawdzanie certyfikatu SSL na localhost
      }
    }
  }
})