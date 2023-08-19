// @ts-check

/**
 * @type {import('@nrwl/devkit').ModuleFederationConfig}
 **/
const moduleFederationConfig = {
  name: 'auth-o-mat',
  exposes: {
    './Module': './src/app.ts',
  },
};

module.exports = moduleFederationConfig;
