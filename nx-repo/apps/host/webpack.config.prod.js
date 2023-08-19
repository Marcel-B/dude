// @ts-check

const { withModuleFederation } = require("@nrwl/react/module-federation");
const baseConfig = require("./module-federation.config");

/**
 * @type {import("@nrwl/devkit").ModuleFederationConfig}
 **/
const prodConfig = {
  ...baseConfig,
  /*
   * Remote overrides for production.
   * Each entry is a pair of an unique name and the URL where it is deployed.
   *
   * e.g.
   * remotes: [
   *   ['app1', '//app1.example.com'],
   *   ['app2', '//app2.example.com'],
   * ]
   *
   * You can also use a full path to the remoteEntry.js file if desired.
   *
   * remotes: [
   *   ['app1', '//example.com/path/to/app1/remoteEntry.js'],
   *   ['app2', '//example.com/path/to/app2/remoteEntry.js'],
   * ]
   */
  remotes: [
    ["stunden", "https://eu2.contabostorage.com/293d582641ac4dc1a6fc0d39b43574ee:stunden/"],
    ["pbi-o-mat", "https://eu2.contabostorage.com/293d582641ac4dc1a6fc0d39b43574ee:pbi-o-mat/"]
  ]
};

module.exports = withModuleFederation(prodConfig);
