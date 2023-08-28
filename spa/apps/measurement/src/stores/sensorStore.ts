import { defineStore } from 'pinia';
import { Device } from '@/models/device';
import { apiClient } from 'client';

export const useSensorStore = defineStore('DeviceStore', {
  state: () => {
    return {
      devices: [] as Device[],
    };
  },
  actions: {
    async fill() {
      const data = await apiClient.client.get<Device[]>('/api/v1/device');
      for (const device of data) {
        const deviceDto = await apiClient.client.get<{
          id: string;
          name: string;
          sensoren: { name: string; unit: string }[];
        }>(`/api/v1/device/${device.id}/`);
        device.sen = deviceDto.sensoren
          .map((x) => `${x.name} (${x.unit})`)
          .join(', ');
      }
      this.devices = data;
    },
  },
});
