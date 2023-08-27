<template>
  <div style="margin: 2rem">
    <TabView>
      <TabPanel header="Geräte">
        <SensorView />
      </TabPanel>
      <TabPanel header="Messungen">
        <MessungenView />
      </TabPanel>
    </TabView>
  </div>
</template>

<script setup lang="ts">
import SensorView from '@/components/SensorView.vue';
import MessungenView from '@/components/MessungenView.vue';
import { onMounted } from 'vue';
import { useSensorStore } from '@/stores/sensorStore';

onMounted(async () => {
  const store = useSensorStore();
  await store.fill();
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
