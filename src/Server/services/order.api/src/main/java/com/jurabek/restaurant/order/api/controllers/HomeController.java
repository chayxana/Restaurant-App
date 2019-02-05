package com.jurabek.restaurant.order.api.controllers;

import java.io.IOException;

import javax.servlet.http.HttpServletResponse;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/")
public class HomeController {
	@RequestMapping(value = "", method = RequestMethod.GET)
	public void index(HttpServletResponse httpResponse) throws IOException {
		httpResponse.sendRedirect("/swagger-ui.html");
	}
}