import { h, createApp } from 'vue';
import singleSpaVue from 'single-spa-vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/lara-light-indigo/theme.css';
import 'primeicons/primeicons.css';
import TabView from 'primevue/tabview';
import TabPanel from 'primevue/tabpanel';
import Dropdown from 'primevue/dropdown';
import Card from 'primevue/card';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Chart from 'primevue/chart';
import 'leaflet/dist/leaflet.css';

const vueLifecycles = singleSpaVue({
  createApp,
  appOptions: {
    render() {
      return h(App, {
        // single-spa props are available on the "this" object. Forward them to your component as needed.
        // https://single-spa.js.org/docs/building-applications#lifecycle-props
        // if you uncomment these, remember to add matching prop definitions for them in your App.vue file.
        name: this.name,
        foo: 'Hallo Welt',
      });
    },
  },
  handleInstance(app) {
    const pinia = createPinia();
    app.use(PrimeVue);
    app.use(pinia);
    // eslint-disable-next-line vue/multi-word-component-names
    app.component('Dropdown', Dropdown);
    // eslint-disable-next-line vue/multi-word-component-names
    app.component('Card', Card);
    app.component('TabView', TabView);
    app.component('TabPanel', TabPanel);
    app.component('DataTable', DataTable);
    // eslint-disable-next-line vue/multi-word-component-names
    app.component('Chart', Chart);
    // eslint-disable-next-line vue/multi-word-component-names
    app.component('Column', Column);
  },
  replaceMode: false,
});

export const bootstrap = vueLifecycles.bootstrap;
export const mount = vueLifecycles.mount;
export const unmount = vueLifecycles.unmount;
