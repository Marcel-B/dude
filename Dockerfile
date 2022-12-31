FROM node:16

WORKDIR /usr/src/app
RUN mkdir db
COPY ./dist/apps/api/ .
RUN npm i -G express cors sqlite3

EXPOSE 3333

CMD [ "node", "main.js" ]


