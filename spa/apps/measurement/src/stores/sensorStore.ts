import { defineStore } from 'pinia';

export const useSensorStore = defineStore('SensorStore', {
  state: () => {
    return {
      sensors: [],
    };
  },
});
