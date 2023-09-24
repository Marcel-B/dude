const {defineConfig} = require('@vue/cli-service')
module.exports = defineConfig({
    transpileDependencies: true,
    configureWebpack: {
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: 'ts-loader',
                    exclude: /node_modules/,
                },
                {
                    test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                    use: [{
                        loader: 'url-loader',
                        options: {
                            limit: 8192,  // Dateien kleiner als 8 KB werden als Data URIs eingefügt
                            name: '[name].[ext]',
                            outputPath: 'fonts/',  // Wenn die Datei größer ist, wird sie in den fonts/-Ordner ausgegeben
                            publicPath: '/fonts/',  // Gibt den öffentlichen Pfad für die Fonts an
                            esModule: false,  // Verhindert Probleme mit neueren Versionen von Webpack
                        }
                    }]
                }
            ],
        },
        output: {
            libraryTarget: 'system',
            filename: 'b-velop-header.js',
        },
    },
})
