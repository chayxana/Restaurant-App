package com.jurabek.restaurant.gateway.restaurantgateway.api;

import java.util.ArrayList;
import java.util.List;

import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import springfox.documentation.swagger.web.SwaggerResource;
import springfox.documentation.swagger.web.SwaggerResourcesProvider;

@Component
@Primary
@EnableAutoConfiguration
public class DocumentationController implements SwaggerResourcesProvider {

	@Override
	public List<SwaggerResource> get() {
		List<SwaggerResource> resources = new ArrayList<>();
		resources.add(swaggerResource("basket-api", "/basket/swagger/doc.json", "2.0"));
		resources.add(swaggerResource("order-api", "/order/v2/api-docs", "2.0"));
		resources.add(swaggerResource("menu-api", "/menu/swagger/v1/swagger.json", "2.0"));
		return resources;
	}

	private SwaggerResource swaggerResource(String name, String location, String version) {
		SwaggerResource swaggerResource = new SwaggerResource();
		swaggerResource.setName(name);
		swaggerResource.setUrl(location);
		swaggerResource.setSwaggerVersion(version);
		return swaggerResource;
	}

}