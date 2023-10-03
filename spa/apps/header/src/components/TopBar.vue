<template>
  <div class="card relative z-2">
    <V-Menubar :model="items"></V-Menubar>
  </div>
</template>

<script setup>
import {ref, defineProps} from 'vue';
import * as singleSpa from 'single-spa';

const customProps = defineProps(['foo']);

const items = ref([]);
if (customProps.foo) {
  items.value.push(
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
  );
  items.value.push({
    label: 'Logout',
    icon: 'pi pi-fw pi-user',
    command: () => {
      window.location = "/logout";
    }
  });
} else {
  items.value.push({
    label: 'Login',
    icon: 'pi pi-fw pi-user',
    command: () => {
      window.location = "/login";
    }
  });
}
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
