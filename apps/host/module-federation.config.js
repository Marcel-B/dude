// @ts-check

/**
 * @type {import('@nrwl/devkit').ModuleFederationConfig}
 **/
const moduleFederationConfig = {
  name: 'host',
  remotes: ['shop', 'carto', 'about', 'stunden'],
};

module.exports = moduleFederationConfig;
