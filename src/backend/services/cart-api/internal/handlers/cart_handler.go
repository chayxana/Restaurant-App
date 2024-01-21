package handlers

import (
	"context"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/cart-api/internal/models"
	"github.com/jurabek/cart-api/internal/repositories"
	"github.com/pkg/errors"
)

type GetCreateDeleter interface {
	Get(ctx context.Context, cartID string) (*models.Cart, error)
	Update(ctx context.Context, cart *models.Cart) error
	Delete(ctx context.Context, id string) error
	UpdateItem(ctx context.Context, cartID string, item models.LineItem) error
}

// CartHandler is router initializer for http
type CartHandler struct {
	repository GetCreateDeleter
}

// NewCartHandler creates new instance of BasketController with BasketRepository
func NewCartHandler(r GetCreateDeleter) *CartHandler {
	return &CartHandler{repository: r}
}

func ErrorHandler(f func(c *gin.Context) error) gin.HandlerFunc {
	return func(c *gin.Context) {
		err := f(c)
		if err != nil {
			var httpErr *models.HTTPError
			if errors.As(err, &httpErr) {
				c.JSON(httpErr.Code, httpErr)
				c.Abort()
			}
		}
		c.Next()
	}
}

// Create go doc
//
//	@Summary		Add a CustomerBasket
//	@Description	add by json new CustomerBasket
//	@Tags			CustomerBasket
//	@Accept			json
//	@Produce		json
//	@Param			CustomerBasket	body		models.Cart	true	"Add CustomerBasket"
//	@Success		200				{object}	models.Cart
//	@Failure		400				{object}	models.HTTPError
//	@Failure		404				{object}	models.HTTPError
//	@Failure		500 			{object}	models.HTTPError
//	@Router			/items [post]
func (h *CartHandler) Create(c *gin.Context) error {
	var entity models.Cart
	if err := c.BindJSON(&entity); err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	err := h.repository.Update(c.Request.Context(), &entity)
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	result, err := h.repository.Get(c.Request.Context(), entity.ID.String())
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	c.JSON(http.StatusOK, result)
	return nil
}

// Update line item doc
//
//	@Summary		Update a line item
//	@Description	update by json new line item
//	@Tags			CustomerBasket
//	@Accept			json
//	@Produce		json
//	@Param			id	path		string		true	"Cart ID"
//	@Param			lineItem		body		models.LineItem	true	"Update line item"
//	@Success		200				{object}	models.Cart
//	@Failure		400				{object}	models.HTTPError
//	@Failure		404				{object}	models.HTTPError
//	@Failure		500 			{object}	models.HTTPError

func (h *CartHandler) UpdateItem(c *gin.Context) error {
	cartID := c.Param("id")
	var entity models.LineItem
	if err := c.BindJSON(&entity); err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}
	if err := h.repository.UpdateItem(c.Request.Context(), cartID, entity); err != nil {
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	return nil
}

// Get go doc
//
//	@Summary		Gets a CustomerBasket
//	@Description	Get CustomerBasket by ID
//	@Tags			CustomerBasket
//	@Accept			json
//	@Produce		json
//	@Param			id	path		string	true	"CustomerBasket ID"
//	@Success		200	{object}	models.Cart
//	@Failure		400	{object}	models.HTTPError
//	@Failure		404 {object}	models.HTTPError
//	@Router			/items/{id} [get]
func (h *CartHandler) Get(c *gin.Context) error {
	id := c.Param("id")

	result, err := h.repository.Get(c.Request.Context(), id)
	if err != nil {
		if errors.Is(err, repositories.ErrCartNotFound) {
			return models.NewHTTPError(http.StatusNotFound, errors.Wrap(err, "itemID: "+id))
		}
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	c.JSON(http.StatusOK, result)
	return nil
}

// Delete go doc
//
//	@Summary		Deletes a CustomerBasket
//	@Description	Deletes CustomerBasket by ID
//	@Tags			CustomerBasket
//	@Accept			json
//	@Produce		json
//	@Param			id	path	string	true	"CustomerBasket ID"
//	@Success		200	""
//	@Failure		400	{object}	models.HTTPError
//	@Failure		404	{object}	models.HTTPError
//	@Failure		500	{object}	models.HTTPError
//	@Router			/items/{id} [delete]
func (h *CartHandler) Delete(c *gin.Context) error {
	id := c.Param("id")

	err := h.repository.Delete(c.Request.Context(), id)
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}
	c.Status(http.StatusOK)
	return nil
}
