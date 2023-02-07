// Code generated by swaggo/swag. DO NOT EDIT
package docs

import "github.com/swaggo/swag"

const docTemplate = `{
    "schemes": {{ marshal .Schemes }},
    "swagger": "2.0",
    "info": {
        "description": "{{escape .Description}}",
        "title": "{{.Title}}",
        "termsOfService": "http://swagger.io/terms/",
        "contact": {
            "name": "API Support",
            "url": "http://www.swagger.io/support",
            "email": "support@swagger.io"
        },
        "license": {
            "name": "Apache 2.0",
            "url": "http://www.apache.org/licenses/LICENSE-2.0.html"
        },
        "version": "{{.Version}}"
    },
    "host": "{{.Host}}",
    "basePath": "{{.BasePath}}",
    "paths": {
        "/checkout": {
            "post": {
                "security": [
                    {
                        "OAuth": []
                    }
                ],
                "description": "Start checkout",
                "consumes": [
                    "application/json"
                ],
                "produces": [
                    "application/json"
                ],
                "tags": [
                    "Checkout"
                ],
                "summary": "Starts checkout for the entered card and basket items",
                "parameters": [
                    {
                        "description": "Checkout",
                        "name": "Checkout",
                        "in": "body",
                        "required": true,
                        "schema": {
                            "$ref": "#/definitions/models.Checkout"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": ""
                    },
                    "400": {
                        "description": "Bad Request",
                        "schema": {
                            "$ref": "#/definitions/models.HTTPError"
                        }
                    }
                }
            }
        },
        "/items": {
            "post": {
                "security": [
                    {
                        "OAuth": []
                    }
                ],
                "description": "add by json new CustomerBasket",
                "consumes": [
                    "application/json"
                ],
                "produces": [
                    "application/json"
                ],
                "tags": [
                    "CustomerBasket"
                ],
                "summary": "Add a CustomerBasket",
                "parameters": [
                    {
                        "description": "Add CustomerBasket",
                        "name": "CustomerBasket",
                        "in": "body",
                        "required": true,
                        "schema": {
                            "$ref": "#/definitions/models.CustomerBasket"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK",
                        "schema": {
                            "$ref": "#/definitions/models.CustomerBasket"
                        }
                    },
                    "400": {
                        "description": "Bad Request",
                        "schema": {
                            "$ref": "#/definitions/models.HTTPError"
                        }
                    }
                }
            }
        },
        "/items/{id}": {
            "get": {
                "security": [
                    {
                        "OAuth": []
                    }
                ],
                "description": "Get CustomerBasket by ID",
                "consumes": [
                    "application/json"
                ],
                "produces": [
                    "application/json"
                ],
                "tags": [
                    "CustomerBasket"
                ],
                "summary": "Gets a CustomerBasket",
                "parameters": [
                    {
                        "type": "string",
                        "description": "CustomerBasket ID",
                        "name": "id",
                        "in": "path",
                        "required": true
                    }
                ],
                "responses": {
                    "200": {
                        "description": "OK",
                        "schema": {
                            "$ref": "#/definitions/models.CustomerBasket"
                        }
                    },
                    "400": {
                        "description": "Bad Request",
                        "schema": {
                            "$ref": "#/definitions/models.HTTPError"
                        }
                    }
                }
            },
            "delete": {
                "security": [
                    {
                        "OAuth": []
                    }
                ],
                "description": "Deletes CustomerBasket by ID",
                "consumes": [
                    "application/json"
                ],
                "produces": [
                    "application/json"
                ],
                "tags": [
                    "CustomerBasket"
                ],
                "summary": "Deletes a CustomerBasket",
                "parameters": [
                    {
                        "type": "string",
                        "description": "CustomerBasket ID",
                        "name": "id",
                        "in": "path",
                        "required": true
                    }
                ],
                "responses": {
                    "200": {
                        "description": ""
                    },
                    "400": {
                        "description": "Bad Request",
                        "schema": {
                            "$ref": "#/definitions/models.HTTPError"
                        }
                    }
                }
            }
        }
    },
    "definitions": {
        "models.Address": {
            "type": "object",
            "properties": {
                "city": {
                    "type": "string"
                },
                "country": {
                    "type": "string"
                },
                "state": {
                    "type": "string"
                },
                "street_address": {
                    "type": "string"
                },
                "zip_code": {
                    "type": "integer"
                }
            }
        },
        "models.BasketItem": {
            "type": "object",
            "properties": {
                "foodId": {
                    "type": "string"
                },
                "foodName": {
                    "type": "string"
                },
                "id": {
                    "type": "string"
                },
                "oldUnitPrice": {
                    "type": "number"
                },
                "picture": {
                    "type": "string"
                },
                "quantity": {
                    "type": "integer"
                },
                "unitPrice": {
                    "type": "number"
                }
            }
        },
        "models.Checkout": {
            "type": "object",
            "properties": {
                "address": {
                    "$ref": "#/definitions/models.Address"
                },
                "credit_card": {
                    "$ref": "#/definitions/models.CreditCardInfo"
                },
                "customer_id": {
                    "type": "string"
                },
                "email": {
                    "type": "string"
                },
                "user_currency": {
                    "type": "string"
                }
            }
        },
        "models.CreditCardInfo": {
            "type": "object",
            "properties": {
                "credit_card_cvv": {
                    "type": "integer"
                },
                "credit_card_expiration_month": {
                    "type": "integer"
                },
                "credit_card_expiration_year": {
                    "type": "integer"
                },
                "credit_card_number": {
                    "type": "string"
                }
            }
        },
        "models.CustomerBasket": {
            "type": "object",
            "properties": {
                "customerId": {
                    "type": "string"
                },
                "items": {
                    "type": "array",
                    "items": {
                        "$ref": "#/definitions/models.BasketItem"
                    }
                }
            }
        },
        "models.HTTPError": {
            "type": "object",
            "properties": {
                "code": {
                    "type": "integer",
                    "example": 400
                },
                "message": {
                    "type": "string",
                    "example": "status bad request"
                }
            }
        }
    },
    "securityDefinitions": {
        "OAuth": {
            "type": "oauth2",
            "flow": "implicit",
            "authorizationUrl": "{{.AuthUrl}}",
            "scopes": {
                "basket-api": "\t\t\t\t\t\tAccess to basket-api"
            }
        }
    }
}`

// SwaggerInfo holds exported Swagger Info so clients can modify it
var SwaggerInfo = &swag.Spec{
	Version:          "1.0",
	Host:             "",
	BasePath:         "",
	Schemes:          []string{},
	Title:            "Basket API",
	Description:      "This is a rest api for basket which saves items to redis server",
	InfoInstanceName: "swagger",
	SwaggerTemplate:  docTemplate,
}

func init() {
	swag.Register(SwaggerInfo.InstanceName(), SwaggerInfo)
}