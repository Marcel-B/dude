module.exports = {
  displayName: 'auth-o-mat',
  preset: '../../jest.preset.js',
  transform: {
    '^.+.vue$': '@vue/vue2-jest',
    '.+.(css|styl|less|sass|scss|svg|png|jpg|ttf|woff|woff2)$':
      'jest-transform-stub',
    '^.+.tsx?$': 'ts-jest',
  },
  moduleFileExtensions: ['ts', 'tsx', 'vue', 'js', 'json'],
  coverageDirectory: '../../coverage/apps/auth-o-mat',
  snapshotSerializers: ['jest-serializer-vue'],
  globals: {
    'ts-jest': {
      tsconfig: 'apps/auth-o-mat/tsconfig.spec.json',
    },
    'vue-jest': {
      tsConfig: 'apps/auth-o-mat/tsconfig.spec.json',
    },
  },
};
