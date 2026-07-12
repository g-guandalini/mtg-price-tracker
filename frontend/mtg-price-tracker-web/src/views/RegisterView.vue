<template>
<main>
    <h2>Cadastro</h2>
    <form @submit.prevent="handleRegister">
        <input
            v-model="username"
            placeholder="Usuário"
        />

        <input
            v-model="email"
            placeholder="E-mail"
            type="email"
        />

        <input
            v-model="password"
            type="password"
            placeholder="Senha"
        />

        <input
            v-model="confirmPassword"
            type="password"
            placeholder="Confirmar senha"
        />

        <button type="submit">
            Criar conta
        </button>
    </form>
    <p v-if="message">

        {{ message }}

    </p>
    <button
        class="secondary"
        @click="router.push('/login')"
    >
        Voltar para Login
    </button>
</main>

</template>

<script setup lang="ts">

import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { register } from '../api/authApi'
import axios from "axios"

const router = useRouter()

const username = ref("")
const email = ref("")
const password = ref("")
const confirmPassword = ref("")
const message = ref("")

async function handleRegister() {
    message.value = ""
    if (password.value.length < 6) {
        message.value =
            "A senha deve possuir no mínimo 6 caracteres."
        return
    }

    if (password.value !== confirmPassword.value) {
        message.value =
            "As senhas não coincidem."
        return
    }

    try {
        await register({
            username: username.value,
            email: email.value,
            password: password.value
        })
        router.push("/login?registered=true")
    }
    catch (error) {
        message.value = (error as Error).message
    }
}

</script>

<style scoped>

main{
    width:400px;
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

.secondary{
    margin-top:20px;
}

p{
    color:red;
}

</style>