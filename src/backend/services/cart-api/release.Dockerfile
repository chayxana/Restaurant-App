FROM alpine:latest
RUN apk --no-cache add ca-certificates

ARG sourceFilePath=./cart-api

COPY ${sourceFilePath} ./cart-api
CMD ["./cart-api"] 
LABEL Name=cart-api Version=0.0.1
EXPOSE 5200
ENV PORT 5200