import { h, createApp } from 'vue';
import singleSpaVue from 'single-spa-vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/lara-light-indigo/theme.css';
import 'primeicons/primeicons.css';
import Dropdown from 'primevue/dropdown';
import Card from 'primevue/card';

const vueLifecycles = singleSpaVue({
  createApp,
  appOptions: {
    render() {
      return h(App, {
        // single-spa props are available on the "this" object. Forward them to your component as needed.
        // https://single-spa.js.org/docs/building-applications#lifecycle-props
        // if you uncomment these, remember to add matching prop definitions for them in your App.vue file.
        name: this.name,
        foo: 'Hallo',
      });
    },
  },
  handleInstance(app) {
    const pinia = createPinia();
    app.use(PrimeVue);
    app.use(pinia);
    app.component('V-Dropdown', Dropdown);
    app.component('V-Card', Card);
  },
  replaceMode: false,
});

export const bootstrap = vueLifecycles.bootstrap;
export const mount = vueLifecycles.mount;
export const unmount = vueLifecycles.unmount;
