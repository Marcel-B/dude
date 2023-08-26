<template>
  <div class="card flex justify-content-between">
    <V-Card>
      <template #title>Sensoren</template>
      <template #content>
        <div class="flex justify-content-around">
          <V-Dropdown
            placeholder="Gerät"
            optionLabel="name"
            :options="devices"
            v-model="selectedDevice"
          ></V-Dropdown>
          <V-Dropdown
            class="w-full md:w-14rem"
            placeholder="Sensor"
            optionLabel="name"
            :options="sensors"
            v-model="selectedSensor"
          ></V-Dropdown>
        </div>
      </template>
    </V-Card>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { client } from 'client';

const selectedDevice = ref();
const selectedSensor = ref();
const devices = ref();
const sensors = ref();

client.get<{ id: string; name: string }[]>('/api/devices/').then((r) => {
  devices.value = r;
});

watch(selectedDevice, (device) => {
  if (device) {
    client
      .get<{
        id: string;
        name: string;
        sensoren: { id: string; name: string }[];
      }>(`/api/device/${selectedDevice.value.id}/`)
      .then((r) => {
        sensors.value = r.sensoren;
      });
  }
});
</script>

}

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
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
