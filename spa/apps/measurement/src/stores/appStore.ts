import { defineStore } from 'pinia';
import { Device } from '@/models/device';
import { apiClient } from 'client';
import { SelectItem } from '@/models/selectItem';
import { Sensor } from '@/models/sensor';

export interface AppState {
  deviceItems: Device[];
  sensorItems: Sensor[];
  selectedSensor: string | null;
}
export type SensorId = string;
export type DeviceId = string;

export const useAppStore = defineStore('AppStore', {
  state: (): AppState => {
    return {
      deviceItems: [] as Device[],
      sensorItems: [] as Sensor[],
      selectedSensor: null,
    };
  },
  getters: {
    devices: (state: AppState): Device[] => state.deviceItems,
    sensors: (state: AppState): Sensor[] => state.sensorItems,
    deviceSelectItems: (state: AppState): SelectItem[] =>
      state.deviceItems.map(
        (x: Device): SelectItem => ({
          name: x.name,
          id: x.id,
        })
      ),
    sensorSelectItems: (state: AppState): SelectItem[] =>
      state.sensorItems.map(
        (x: Sensor): SelectItem => ({
          name: x.name,
          id: x.id,
        })
      ),
  },
  actions: {
    async fill() {
      const devices = await apiClient.client.get<Device[]>('/api/v1/device');
      for (const device of devices) {
        const deviceDto = await apiClient.client.get<{
          id: string;
          name: string;
          sensoren: {
            name: string;
            unit: string;
          }[];
        }>(`/api/v1/device/${device.id}/`);
        device.sen = deviceDto.sensoren
          .map((x) => `${x.name} (${x.unit})`)
          .join(', ');
      }
      const sensors = await apiClient.client.get<Sensor[]>('/api/v1/sensor');
      this.sensorItems = sensors;
      this.deviceItems = devices;
    },
    setSelectedSensor(sensorId: SensorId): void {
      this.selectedSensor = sensorId;
    },
    sensorsByDeviceId(deviceId: DeviceId): SelectItem[] {
      return this.sensors
        .filter((x: Sensor) => x.deviceId === deviceId)
        .map(
          (x: Sensor): SelectItem => ({
            id: x.id,
            name: `${x.name} (${x.unit})`,
          })
        );
    },
    getItemsById(id: string): SelectItem[] {
      return this.deviceItems
        .filter((x: Device): boolean => x.id === id)
        .map((x: Device): SelectItem => {
          return { id: x.id, name: x.name };
        });
    },
  },
});
