const {defineConfig} = require('@vue/cli-service');
const webpack = require('webpack');

module.exports = defineConfig({
    transpileDependencies: true,
    devServer: {
        port: 8080,
    },
    configureWebpack: {
        output: {
            libraryTarget: 'system',
            filename: 'b-velop-header.js',
        },
        plugins: [
            new webpack.optimize.LimitChunkCountPlugin({
                maxChunks: 1,
            }),
        ],
    },
});
