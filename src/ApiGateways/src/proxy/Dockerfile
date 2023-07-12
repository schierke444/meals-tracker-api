FROM nginx:stable-alpine

RUN mkdir /etc/nginx/templates

# Copy Main Conf
COPY src/ApiGateways/src/proxy/nginx.conf /etc/nginx/nginx.conf

# Copy Upstream and Main Server Route conf
# COPY src/ApiGateways/src/nginx/upstream.conf.template /etc/nginx/templates
COPY src/ApiGateways/src/proxy/server.conf.template /etc/nginx/templates

STOPSIGNAL SIGTERM

RUN ["rm", "-rf", "/etc/nginx/conf.d/default.conf"]

CMD ["nginx", "-g", "daemon off;"]