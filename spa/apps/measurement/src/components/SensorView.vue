<template>
  <div class="card flex justify-content-between">
    <Card>
      <template #title>Geräte</template>
      <template #content>
        <DataTable :value="devices" tableStyle="min-width: 50rem">
          <Column field="id" header="ID"></Column>
          <Column field="name" header="Name"></Column>
          <Column field="sen" header="Sensor(en) (Einheit)"></Column>
        </DataTable>
      </template>
    </Card>
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { useSensorStore } from '@/stores/sensorStore';

const { devices } = storeToRefs(useSensorStore());
</script>

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
