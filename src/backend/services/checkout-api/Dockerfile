FROM node:20

WORKDIR /usr/src/app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm run build

ENTRYPOINT [ "node", "dist/index.js" ]