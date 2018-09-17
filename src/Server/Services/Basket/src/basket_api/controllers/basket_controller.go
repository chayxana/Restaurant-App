package controllers

import (
	"basket_api/models"
	"net/http"

	"basket_api/repositories"

	"github.com/gin-gonic/gin"
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
// @Router /basket [post]
func (bc *BasketController) Create(c *gin.Context) {

	var entity models.CustomerBasket
	c.BindJSON(&entity)

	err := bc.BasketRepository.Update(&entity)

	if err != nil {
		models.NewError(c, http.StatusBadRequest, err)
		return
	}

	result, err := bc.BasketRepository.GetBasket(entity.CustomerID.String())

	if err != nil {
		models.NewError(c, http.StatusBadRequest, err)
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
// @Router /basket/{id} [get]
func (bc *BasketController) Get(c *gin.Context) {
	id := c.Param("id")

	result, err := bc.BasketRepository.GetBasket(id)

	if err != nil {
		models.NewError(c, http.StatusBadRequest, err)
	}

	c.JSON(http.StatusOK, result)
}
