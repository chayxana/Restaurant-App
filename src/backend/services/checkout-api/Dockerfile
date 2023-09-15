FROM oven/bun

WORKDIR /usr/src/app

COPY package.json bun.lockb ./

RUN bun install

COPY . .

ENTRYPOINT [ "bun", "start" ]