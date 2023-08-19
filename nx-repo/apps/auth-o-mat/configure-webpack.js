/*
 * Modify the webpack config by exporting an Object or Function.
 *
 * If the value is an Object, it will be merged into the final
 * config using `webpack-merge`.
 *
 * If the value is a function, it will receive the resolved config
 * as the argument. The function can either mutate the config and
 * return nothing, OR return a cloned or merged version of the config.
 *
 * https://cli.vuejs.org/config/#configurewebpack
 */


const { withModuleFederation } = require("@nrwl/react/module-federation");
const baseConfig = require("./module-federation.config");

/**
 * @type {import("@nrwl/devkit").ModuleFederationConfig}
 **/
const defaultConfig = {
  ...baseConfig
};

module.exports = withModuleFederation(defaultConfig);
