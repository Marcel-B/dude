<template>
  <div class="card relative z-2">
    <V-Menubar :model="items"></V-Menubar>
  </div>
</template>

<script setup>
import {onMounted, ref} from 'vue';
import * as singleSpa from 'single-spa';
import {getUsername, hasUser} from "auth";

const userName = ref();
const items = ref([
  {
    label: 'Nodes',
    icon: 'pi pi-fw pi-chart-line',
    command: () => singleSpa.navigateToUrl('/state'),
  },
  {
    label: 'PBIs',
    icon: 'pi pi-fw pi-list',
    command: () => singleSpa.navigateToUrl('/content'),
  },
  {
    label: 'Zeiten',
    icon: 'pi pi-fw pi-calendar',
    command: () => singleSpa.navigateToUrl('/stunden'),
  },
  {
    label: 'Login',
    icon: 'pi pi-fw pi-user',
    command: () => {
      window.location = "/login";
    }
  }
]);

onMounted(async () => {
  const user = await hasUser();
  if (user) {
    userName.value = await getUsername();
    items.value = [...items.value.filter(x => x.label !== 'Login'), {label: userName.value, icon: 'pi pi-fw- pi-user'}];
  }
})

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
