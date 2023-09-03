import {h, createApp} from 'vue';
import singleSpaVue from 'single-spa-vue';
import PrimeVue from 'primevue/config';
import Button from 'primevue/button';
import "primevue/resources/themes/lara-light-indigo/theme.css";
import "primeicons/primeicons.css";
import Menubar from 'primevue/menubar';
import App from './App.vue';


const vueLifecycles = singleSpaVue({
    createApp,
    appOptions: {
        render() {
            const vNode = h(App, {
                authToken: this.authToken,
            });
            return vNode;
        },
    },
    handleInstance(app) {
        app.use(PrimeVue);
        app.component('V-Button', Button);
        app.component('V-Menubar', Menubar);
    }
});

export const bootstrap = vueLifecycles.bootstrap;
export const mount = vueLifecycles.mount;
export const unmount = vueLifecycles.unmount;
