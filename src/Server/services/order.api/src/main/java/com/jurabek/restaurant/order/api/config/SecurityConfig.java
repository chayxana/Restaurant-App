package com.jurabek.restaurant.order.api.config;

import com.auth0.jwk.*;
import com.auth0.spring.security.api.JwtAuthenticationProvider;
import com.auth0.spring.security.api.JwtWebSecurityConfigurer;
import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.common.collect.Lists;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;
import java.util.Map;
import java.util.concurrent.TimeUnit;

@Configuration
@EnableWebSecurity
public class SecurityConfig extends WebSecurityConfigurerAdapter {

    @Value(value = "${jwt.apiAudience}")
    private String apiAudience;

    @Value("${IDENTITY_URL_PUB:http://localhost:5100}")
    private String identityUrl;

    @Override
    protected void configure(HttpSecurity http) throws Exception {
        JwtWebSecurityConfigurer
                .forRS256(apiAudience, "issuer")
                .configure(http);
        final JwkProvider jwkProvider = new JWKProviderBuilderCustom().build();
        JwtWebSecurityConfigurer.forRS256(apiAudience, null,
                new JwtAuthenticationProvider(jwkProvider, null, apiAudience))
                .configure(http)
                .authorizeRequests()
                .antMatchers(HttpMethod.GET, "/").permitAll()
                .antMatchers(HttpMethod.GET, "/api/v1/orders/getAll").hasRole("ADMIN")
                .antMatchers(HttpMethod.GET, "/api/v1/orders/**")
                .authenticated();
    }

    class JWKProviderBuilderCustom {
        private final int expiresIn;
        private final TimeUnit expiresUnit;
        private final int cacheSize;

        JWKProviderBuilderCustom() {
            this.expiresIn = 10;
            this.expiresUnit = TimeUnit.HOURS;
            this.cacheSize = 5;
        }

        JwkProvider build() {
            JwkProvider urlProvider = null;
            try {
                urlProvider = new UrlJwkProviderOverride(
                        new URL(identityUrl + "/.well-known/openid-configuration/jwks"));
            } catch (MalformedURLException ignored) {
            }
            urlProvider = new GuavaCachedJwkProvider(urlProvider, cacheSize, expiresIn, expiresUnit);
            return urlProvider;
        }
    }

    static class UrlJwkProviderOverride implements JwkProvider {
        final URL url;

        UrlJwkProviderOverride(URL url) {
            this.url = url;
        }

        private Map<String, Object> getJwks() throws SigningKeyNotFoundException {
            try {
                final URLConnection urlConnection = this.url.openConnection();
                urlConnection.setRequestProperty("Accept", "application/json");
                final InputStream inputStream = urlConnection.getInputStream();
                final JsonFactory factory = new JsonFactory();
                try (final JsonParser parser = factory.createParser(inputStream)) {
                    final TypeReference<Map<String, Object>> typeReference = new TypeReference<Map<String, Object>>() {
                    };
                    return new ObjectMapper().reader().readValue(parser, typeReference);
                }
            } catch (IOException e) {
                throw new SigningKeyNotFoundException("Cannot obtain jwks from url " + url.toString(), e);
            }
        }

        List<Jwk> getAll() throws SigningKeyNotFoundException {
            List<Jwk> jwks = Lists.newArrayList();
            final List<Map<String, Object>> keys = (List<Map<String, Object>>) getJwks().get("keys");

            if (keys == null || keys.isEmpty()) {
                throw new SigningKeyNotFoundException("No keys found in " + url.toString(), null);
            }

            try {
                for (Map<String, Object> values : keys) {
                    jwks.add(Jwk.fromValues(values));
                }
            } catch (IllegalArgumentException e) {
                throw new SigningKeyNotFoundException("Failed to parse jwk from json", e);
            }
            return jwks;
        }

        @Override
        public Jwk get(String keyId) throws JwkException {
            final List<Jwk> jwks = getAll();
            if (keyId == null && jwks.size() == 1) {
                return jwks.get(0);
            }
            if (keyId != null) {
                for (Jwk jwk : jwks) {
                    if (keyId.equals(jwk.getId())) {
                        return jwk;
                    }
                }
            }
            throw new SigningKeyNotFoundException("No key found in " + url.toString() + " with kid " + keyId, null);
        }
    }
}
