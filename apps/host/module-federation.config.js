// @ts-check

/**
 * @type {import('@nrwl/devkit').ModuleFederationConfig}
 **/
const moduleFederationConfig = {
  name: 'host',
  remotes: ['shop', 'carto', 'about'],
};

module.exports = moduleFederationConfig;
