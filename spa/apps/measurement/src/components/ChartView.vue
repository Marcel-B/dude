<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { apiClient } from 'client';
import format from 'date-fns/format';
import * as dateFns from 'date-fns';
import { storeToRefs } from 'pinia';
import { userLoggedIn } from 'auth';
import * as singleSpa from 'single-spa';
import { useAppStore } from '@/stores/appStore';

const store = useAppStore();
const { selectedSensor } = storeToRefs(store);
onMounted(async () => {
  const loggedIn = await userLoggedIn();
  if (loggedIn) {
    chartData.value = await setChartData();
    chartOptions.value = setChartOptions();
  } else {
    singleSpa.navigateToUrl('/');
  }
});

const chartData = ref();
const chartOptions = ref();

watch(selectedSensor, async (value: string | null) => {
  if (value === null) {
    return;
  }
  chartData.value = await setChartData(value);
  chartOptions.value = setChartOptions();
});

const setChartOptions = () => {
  const documentStyle = getComputedStyle(document.documentElement);
  const textColor = documentStyle.getPropertyValue('--text-color');
  const textColorSecondary = documentStyle.getPropertyValue(
    '--text-color-secondary'
  );
  const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

  return {
    maintainAspectRatio: false,
    aspectRatio: 0.6,
    plugins: {
      legend: {
        labels: {
          color: textColor,
        },
      },
    },
    scales: {
      x: {
        ticks: {
          color: textColorSecondary,
        },
        grid: {
          color: surfaceBorder,
        },
      },
      y: {
        ticks: {
          color: textColorSecondary,
        },
        grid: {
          color: surfaceBorder,
        },
      },
    },
  };
};

const setChartData = async (
  sensorId = '581f81e4-51ad-46d4-5a43-08dba423e6d8'
) => {
  if (!sensorId) {
    return null;
  }
  const documentStyle = getComputedStyle(document.documentElement);
  const from = format(
    dateFns.addHours(new Date(), -24),
    'yyyy-MM-dd HH:mm:ssxxx'
  ).replace(' ', 'T');
  const to = format(new Date(), 'yyyy-MM-dd HH:mm:ssxxx').replace(' ', 'T');

  const urlPara = new URLSearchParams();
  urlPara.append('from', from);
  urlPara.append('to', to);

  const d = await apiClient.client.get<
    { id: string; timestamp: Date; value: number }[]
  >(`/api/v1/measurement/sensor/${sensorId}?${urlPara.toString()}`);
  return {
    labels: d.map((x) => format(new Date(x.timestamp), 'HH:mm')),
    datasets: [
      {
        label: '',
        data: d.map((x) => x.value),
        fill: false,
        borderColor: documentStyle.getPropertyValue('--blue-500'),
        tension: 0.2,
      },
    ],
  };
};
</script>

<template>
  <Chart
    type="line"
    :data="chartData"
    :options="chartOptions"
    class="h-30rem"
    height="500"
  />
</template>

<style scoped></style>
