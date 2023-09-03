<template>
  <TopBar :foo="authToken" msg="Welcome to Your Vue.js App"/>
  <button @click="() => console.log('====', authToken)">Click</button>
</template>

<script>
import TopBar from "@/components/TopBar.vue";

export default {
  name: 'App',
  props: ['authToken'],
  components: {
    TopBar,
  }
}

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 0;
}
</style>
