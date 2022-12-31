// @ts-check

const { withModuleFederation } = require("@nrwl/react/module-federation");
const baseConfig = require("./module-federation.config");

/**
 * @type {import("@nrwl/devkit").ModuleFederationConfig}
 **/
const defaultConfig = {
  ...baseConfig,
  rules: [
    {
      test: /\.(png|jpe?g|gif)$/i,
      use: [
        {
          loader: "file-loader"
        }
      ]
    }
  ]
};

module.exports = withModuleFederation(defaultConfig);
