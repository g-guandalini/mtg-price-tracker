<template>

<main>
    <h2>Login</h2>
    <form @submit.prevent="handleLogin">
        <input
            v-model="username"
            placeholder="Usuário"
        />
        <input
            v-model="password"
            type="password"
            placeholder="Senha"
        />
        <button>
            Entrar
        </button>
    </form>
    <p class="register-text">
        Não possui uma conta?
    </p>

    <button
        class="secondary"
        @click="router.push('/register')"
    >
        Cadastre-se
    </button>
    <p v-if="message">
        {{ message }}
    </p>
</main>

</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { login } from '../api/authApi'
import { useAuthStore } from '../stores/authStore'

const router = useRouter()
const route = useRoute()
const username = ref('')
const password = ref('')
const message = ref('')
const auth = useAuthStore()

async function handleLogin() {

    try {
        const response = await login({
            username: username.value,
            password: password.value
        })

        auth.login(response.token)
        router.push('/')
    }
    catch {
        message.value = 'Usuário ou senha inválidos.'
    }

}

if(route.query.registered){

    message.value =
        "Cadastro realizado com sucesso. Faça login."

}
</script>

<style scoped>
main{
    width:350px;
    margin:80px auto;
}

form{
    display:flex;
    flex-direction:column;
    gap:15px;
}

input{
    padding:10px;
}

button{
    padding:12px;
}

</style>