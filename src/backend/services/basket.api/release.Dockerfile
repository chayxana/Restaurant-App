FROM alpine:latest
RUN apk --no-cache add ca-certificates
COPY ./basket.api ./basket.api
CMD ["./basket.api"] 
LABEL Name=basket.api Version=0.0.1
EXPOSE 5200
ENV PORT 5200