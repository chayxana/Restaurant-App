package com.jurabek.restaurant.order.api.config;

import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import javax.servlet.ServletContext;

import com.google.common.base.Predicates;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import springfox.documentation.builders.*;
import springfox.documentation.schema.ModelRef;
import springfox.documentation.service.*;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spi.service.contexts.SecurityContext;
import springfox.documentation.spring.web.paths.RelativePathProvider;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.swagger.web.SecurityConfiguration;
import springfox.documentation.swagger.web.SecurityConfigurationBuilder;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

import static java.util.Collections.singletonList;

@Configuration
@EnableSwagger2
public class SwaggerConfig {
    @Value("${IDENTITY_URL_PUB:http://localhost:5100}")
    private String identityUrl;

    @Value("${BASE_PATH:}")
    private String basePath;

    private final ServletContext servletContext;

    @Autowired
    public SwaggerConfig(ServletContext servletContext) {
        this.servletContext = servletContext;
    }

    @Bean
    public Docket api() {

        return new Docket(DocumentationType.SWAGGER_2)
                .select().apis(RequestHandlerSelectors.basePackage("com.jurabek.restaurant.order.api"))
                .paths(Predicates.not(PathSelectors.regex("/error"))).build()
                .pathProvider(new RelativePathProvider(this.servletContext) {
                    @Override
                    public String getApplicationBasePath() {
                        return basePath;
                    }
                })
                .securitySchemes(singletonList(securityScheme()))
                .securityContexts(singletonList(securityContext()));
    }

    @Bean
    public SecurityConfiguration security() {
        return SecurityConfigurationBuilder.builder().clientId("order-api-swagger-ui").scopeSeparator(" ")
                .useBasicAuthenticationWithAccessCodeGrant(true).build();
    }

    private SecurityScheme securityScheme() {
        GrantType grantType = new ImplicitGrantBuilder()
            .loginEndpoint(new LoginEndpoint(identityUrl + "/connect/authorize")).build();

        SecurityScheme oauth = new OAuthBuilder().name("oauth2").grantTypes(singletonList(grantType))
                .scopes(Arrays.asList(scopes())).build();

        return oauth;
    }

    private AuthorizationScope[] scopes() {
        AuthorizationScope[] scopes = { new AuthorizationScope("order-api", "Restaurant Order API") };
        return scopes;
    }

    private SecurityContext securityContext() {
        return SecurityContext.builder()
                .securityReferences(Arrays.asList(new SecurityReference("oauth2", scopes())))
                .forPaths(PathSelectors.regex("/api/v1/orders*")).build();
    }

}