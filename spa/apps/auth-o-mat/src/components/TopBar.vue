<template>
  <div class="card relative z-2">
    <V-Menubar :model="items"></V-Menubar>
  </div>
</template>

<script setup>
import {ref} from 'vue';
import * as singleSpa from 'single-spa';
import {login} from 'auth';

const items = ref([
  {
    label: 'Chart',
    icon: 'pi pi-fw pi-chart-line',
    command: () => singleSpa.navigateToUrl('/state'),
  },
  {
    label: 'Sensoren',
    icon: 'pi pi-fw pi-microphone',
    command: () => singleSpa.navigateToUrl('/content'),
  },
  {
    label: 'Zeitraum',
    icon: 'pi pi-fw pi-calendar',
  },
  {
    label: 'Tabelle',
    icon: 'pi pi-fw pi-table',
    command: () => login().catch(console.error)
  },
  {
    label: 'Login',
    icon: 'pi pi-fw pi-user',
  }
]);

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>
