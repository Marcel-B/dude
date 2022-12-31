FROM node:16

WORKDIR /usr/src/app
RUN mkdir db
COPY ./dist/apps/api/ .
RUN npm i -G express cors sqlite3

EXPOSE 3333

CMD [ "node", "main.js" ]

# docker buildx build --platform linux/amd64 --push -t millegalb/devit-api:0.0.1 .
#docker run -d --name devit-api -p 3333:3333 millegalb/devit-api
