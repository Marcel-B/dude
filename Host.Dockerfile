FROM nginx
COPY  ./dist/apps/host/** /usr/share/nginx/html

