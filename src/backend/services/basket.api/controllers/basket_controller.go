package controllers

import (
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/models"
	"github.com/jurabek/basket.api/repositories"
)

// BasketController is router initializer for http
type BasketController struct {
	BasketRepository repositories.BasketRepository
}

// NewBasketController creates new instance of BasketController with BasketRepository
func NewBasketController(r repositories.BasketRepository) *BasketController {
	return &BasketController{BasketRepository: r}
}

// Create go doc
// @Summary Add a CustomerBasket
// @Description add by json new CustomerBasket
// @Tags CustomerBasket
// @Accept json
// @Produce json
// @Param CustomerBasket body models.CustomerBasket true "Add CustomerBasket"
// @Success 200 {object} models.CustomerBasket
// @Failure 400 {object} models.HTTPError
// @Router /items [post]
// @Security OAuth
func (bc *BasketController) Create(c *gin.Context) {
	var entity models.CustomerBasket
	c.BindJSON(&entity)

	err := bc.BasketRepository.Update(&entity)

	if err != nil {
		httpError := models.NewHTTPError(http.StatusBadRequest, err)
		c.JSON(http.StatusBadRequest, httpError)
		return
	}

	result, err := bc.BasketRepository.GetBasket(entity.CustomerID.String())

	if err != nil {
		httpError := models.NewHTTPError(http.StatusBadRequest, err)
		c.JSON(http.StatusBadRequest, httpError)
		return
	}

	c.JSON(http.StatusOK, result)
}

// Get go doc
// @Summary Gets a CustomerBasket
// @Description Get CustomerBasket by ID
// @Tags CustomerBasket
// @Accept json
// @Produce json
// @Param id path string true "CustomerBasket ID"
// @Success 200 {object} models.CustomerBasket
// @Failure 400 {object} models.HTTPError
// @Router /items/{id} [get]
// @Security OAuth
func (bc *BasketController) Get(c *gin.Context) {
	id := c.Param("id")

	result, err := bc.BasketRepository.GetBasket(id)

	if err != nil {
		httpError := models.NewHTTPError(http.StatusBadRequest, err)
		c.JSON(http.StatusBadRequest, httpError)
		return
	}
	c.JSON(http.StatusOK, result)
}

// Delete go doc
// @Summary Deletes a CustomerBasket
// @Description Deletes CustomerBasket by ID
// @Tags CustomerBasket
// @Accept json
// @Produce json
// @Param id path string true "CustomerBasket ID"
// @Success 200 ""
// @Failure 400 {object} models.HTTPError
// @Router /items/{id} [delete]
// @Security OAuth
func (bc *BasketController) Delete(c *gin.Context) {
	id := c.Param("id")

	err := bc.BasketRepository.Delete(id)

	if err != nil {
		httpError := models.NewHTTPError(http.StatusBadRequest, err)
		c.JSON(http.StatusBadRequest, httpError)
		return
	}
	c.Status(http.StatusOK)
}
