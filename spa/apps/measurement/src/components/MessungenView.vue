<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { apiClient } from 'client';
import format from 'date-fns/format';
import startOfDay from 'date-fns/startOfDay';
import endOfDay from 'date-fns/endOfDay';

onMounted(async () => {
  chartData.value = await setChartData();
  chartOptions.value = setChartOptions();
});
const chartData = ref();
const chartOptions = ref();
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

const setChartData = async () => {
  const documentStyle = getComputedStyle(document.documentElement);
  const from = format(
    startOfDay(new Date(2023, 7, 26)),
    'yyyy-MM-dd HH:mm:ssxxx'
  ).replace(' ', 'T');
  const to = format(
    endOfDay(new Date(2023, 7, 26)),
    'yyyy-MM-dd HH:mm:ssxxx'
  ).replace(' ', 'T');

  const urlPara = new URLSearchParams();
  urlPara.append('from', from);
  urlPara.append('to', to);

  const d = await apiClient.client.get<
    { id: string; timestamp: Date; value: number }[]
  >(
    `/api/measurements/581f81e4-51ad-46d4-5a43-08dba423e6d8?${urlPara.toString()}`
  );
  return {
    labels: d.map((x) => format(new Date(x.timestamp), 'HH:mm')),
    datasets: [
      {
        label: 'First Dataset',
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
  <div>
    <Card>
      <template #title>Messungen</template>
      <template #content>
        <div class="card">
          <Chart type="line" :data="chartData" :options="chartOptions" />
        </div>
      </template>
    </Card>
  </div>
</template>

<style scoped></style>
