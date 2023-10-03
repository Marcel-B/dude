<template>
  <div style="margin: 2rem">
    <TabView>
      <TabPanel header="Geräte">
        <SensorView />
      </TabPanel>
      <TabPanel header="Messungen">
        <MessungenView />
      </TabPanel>
      <TabPanel header="Karte">
        <MapView />
      </TabPanel>
    </TabView>
  </div>
</template>

<script setup lang="ts">
import SensorView from '@/components/SensorView.vue';
import MessungenView from '@/components/MessungenView.vue';
import MapView from '@/components/MapView.vue';
import { onMounted } from 'vue';
import { userLoggedIn } from 'auth';
import * as singleSpa from 'single-spa';
import { useAppStore } from '@/stores/appStore';

onMounted(async () => {
  const is = await userLoggedIn();
  if (!is) {
    singleSpa.navigateToUrl('/');
  } else {
    const store = useAppStore();
    try {
      await store.fill();
    } catch (e) {
      console.error(e);
    }
  }
});
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
