FROM node:alpine

WORKDIR /usr/src/app

COPY ./src/B3.Test-FrontrEnd /usr/src/app

RUN npm install -g @angular/cli

RUN npm install --force

CMD ["ng", "serve", "--host", "0.0.0.0"]
