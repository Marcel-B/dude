FROM nginx
COPY  ./dist/apps/host/** /usr/share/nginx/html
COPY ./nginx.config /etc/nginx/conf.d/default.conf
