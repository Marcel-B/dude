<script setup lang="ts">
import ChartView from '@/components/ChartView.vue';
import { ref, watch } from 'vue';

import { useAppStore } from '@/stores/appStore';

const deviceStore = useAppStore();

const sensors = ref();
const selectedDevice = ref();
const selectedSensor = ref();

watch(selectedDevice, async (newValue) => {
  sensors.value = deviceStore.sensorsByDeviceId(newValue.id);
});

watch(selectedSensor, async (newValue) => {
  deviceStore.setSelectedSensor(newValue.id);
});
</script>

<template>
  <Card>
    <template #title>
      <div style="display: flex; justify-content: space-between">
        <div>Messungen</div>
        <div>
          <Dropdown
            v-model="selectedDevice"
            option-label="name"
            style="width: 20rem"
            :options="deviceStore.deviceSelectItems"
          />
          <Dropdown
            v-model="selectedSensor"
            style="width: 20rem; margin-left: 1rem; margin-right: 1rem"
            option-label="name"
            :options="sensors"
          />
        </div>
      </div>
    </template>
    <template #content>
      <div class="card">
        <ChartView />
      </div>
    </template>
  </Card>
</template>

<style scoped></style>
