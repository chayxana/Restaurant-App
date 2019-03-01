
# Stage 1
FROM node:8.11.2-alpine as node

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

# Stage 2
FROM nginx:1.13.12-alpine

COPY --from=node /usr/src/app/dist/angular-docker /usr/share/nginx/html

COPY ./nginx.conf /etc/nginx/conf.d/default.conf