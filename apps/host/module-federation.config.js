// @ts-check

/**
 * @type {import("@nrwl/devkit").ModuleFederationConfig}
 **/
const moduleFederationConfig = {
  name: "host",
  remotes: ["stunden", "pbi"]
};

module.exports = moduleFederationConfig;
