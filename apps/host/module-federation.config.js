// @ts-check

/**
 * @type {import("@nrwl/devkit").ModuleFederationConfig}
 **/
const moduleFederationConfig = {
  name: "host",
  remotes: ["stunden", "pbi-o-mat"]
  //["stunden", "//eu2.contabostorage.com/293d582641ac4dc1a6fc0d39b43574ee:stunden/"],
  //["pbi-o-mat", "//eu2.contabostorage.com/293d582641ac4dc1a6fc0d39b43574ee:pbi-o-mat/"]]
};

module.exports = moduleFederationConfig;
