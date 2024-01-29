package handlers

import (
	"context"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/cart-api/internal/models"
	"github.com/jurabek/cart-api/internal/repositories"
	"github.com/pkg/errors"
)

type GetCreateDeleter interface {
	Get(ctx context.Context, cartID string) (*models.Cart, error)
	Update(ctx context.Context, cart *models.Cart) error
	Delete(ctx context.Context, id string) error
	AddItem(ctx context.Context, cartID string, item models.LineItem) error
	UpdateItem(ctx context.Context, cartID string, itemID int, item models.LineItem) error
	DeleteItem(ctx context.Context, cartID string, itemID int) error
}

// CartHandler is router initializer for http
type CartHandler struct {
	repository GetCreateDeleter
}

// NewCartHandler creates new instance of CartHandler with CartRepository
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
//	@Summary		Creates new cart
//	@Description	add by json new Cart
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			cart			body		models.CreateCartReq	true	"Creates new cart"
//	@Success		200				{object}	models.Cart
//	@Failure		400				{object}	models.HTTPError
//	@Failure		404				{object}	models.HTTPError
//	@Failure		500 			{object}	models.HTTPError
//	@Router			/cart 			[post]
func (h *CartHandler) Create(c *gin.Context) error {
	var req models.CreateCartReq
	if err := c.BindJSON(&req); err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	cart := models.MapCreateCartReqToCart(req)
	err := h.repository.Update(c.Request.Context(), cart)
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	result, err := h.repository.Get(c.Request.Context(), cart.ID.String())
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	c.JSON(http.StatusOK, result)
	return nil
}

// Update cart doc
//
//	@Summary		Update cart
//	@Description	update by json cart
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			id	path			string		true	"Cart ID"
//	@Param			update_cart			body		models.CreateCartReq	true "Updates cart"
//	@Success		200					{object}	models.Cart
//	@Failure		400					{object}	models.HTTPError
//	@Failure		404					{object}	models.HTTPError
//	@Failure		500 				{object}	models.HTTPError
//	@Router			/cart/{id}			[put]
func (h *CartHandler) Update(c *gin.Context) error {
	cartID := c.Param("id")
	var updateReq models.UpdateCartReq
	if err := c.BindJSON(&updateReq); err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	cart, err := h.repository.Get(c.Request.Context(), cartID)
	if err != nil {
		if errors.Is(err, repositories.ErrCartNotFound) {
			return models.NewHTTPError(http.StatusNotFound, errors.Wrap(err, "cartID: "+cartID))
		}
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}

	cartForUpdate := models.MapUpdateCartReqToCart(cart, updateReq)
	if err := h.repository.Update(c.Request.Context(), cartForUpdate); err != nil {
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	return nil
}

// Get go doc
//
//	@Summary		Gets a Cart
//	@Description	Get Cart by ID
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			id	path		string	true	"Cart ID"
//	@Success		200	{object}	models.Cart
//	@Failure		400	{object}	models.HTTPError
//	@Failure		404 {object}	models.HTTPError
//	@Router			/cart/{id} 		[get]
func (h *CartHandler) Get(c *gin.Context) error {
	id := c.Param("id")

	result, err := h.repository.Get(c.Request.Context(), id)
	if err != nil {
		if errors.Is(err, repositories.ErrCartNotFound) {
			return models.NewHTTPError(http.StatusNotFound, errors.Wrap(err, "cartID: "+id))
		}
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	c.JSON(http.StatusOK, result)
	return nil
}

// Delete go doc
//
//	@Summary		Deletes a Cart
//	@Description	Deletes Cart by ID
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			id	path	string	true	"Cart ID"
//	@Success		200	""
//	@Failure		400	{object}	models.HTTPError
//	@Failure		404	{object}	models.HTTPError
//	@Failure		500	{object}	models.HTTPError
//	@Router			/cart/{id} 		[delete]
func (h *CartHandler) Delete(c *gin.Context) error {
	id := c.Param("id")

	err := h.repository.Delete(c.Request.Context(), id)
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}
	c.Status(http.StatusOK)
	return nil
}

// Update line item doc
//
//	@Summary		Add a line item
//	@Description	Adds item into cart, if item exists sums the quantity
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			id	path			string		true	"Cart ID"
//	@Param			lineItem			body		models.LineItem	true	"Update line item"
//	@Success		200					{object}	models.Cart
//	@Failure		400					{object}	models.HTTPError
//	@Failure		404					{object}	models.HTTPError
//	@Failure		500 				{object}	models.HTTPError
//	@Router			/cart/{id}/item		[post]
func (h *CartHandler) AddItem(c *gin.Context) error {
	cartID := c.Param("id")
	var entity models.LineItem
	if err := c.BindJSON(&entity); err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}
	if err := h.repository.AddItem(c.Request.Context(), cartID, entity); err != nil {
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	return nil
}

// Update line item doc
//
//	@Summary		Updates a line item
//	@Description	Updates item in the cart,
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			id	path						string		true	"Cart ID"
//	@Param			itemID	path				string		true	"Item ID"
//	@Param			lineItem						body		models.LineItem	true	"Update line item"
//	@Success		200								{object}	models.Cart
//	@Failure		400								{object}	models.HTTPError
//	@Failure		404								{object}	models.HTTPError
//	@Failure		500 							{object}	models.HTTPError
//	@Router			/cart/{id}/item/{itemID}		[put]
func (h *CartHandler) UpdateItem(c *gin.Context) error {
	cartID := c.Param("id")
	itemID := c.Param("itemID")

	itemIDInt, err := strconv.Atoi(itemID)
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	var entity models.LineItem
	if err := c.BindJSON(&entity); err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}
	if err := h.repository.UpdateItem(c.Request.Context(), cartID, itemIDInt, entity); err != nil {
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	return nil
}

// Deletes line item doc
//
//	@Summary		Delete line item
//	@Description	Delete line item by json
//	@Tags			Cart
//	@Accept			json
//	@Produce		json
//	@Param			id		path				string		true	"Cart ID"
//	@Param			itemID	path				string		true	"Item ID"
//	@Success		200							{object}	models.Cart
//	@Failure		400							{object}	models.HTTPError
//	@Failure		404							{object}	models.HTTPError
//	@Failure		500 						{object}	models.HTTPError
//	@Router			/cart/{id}/item/{itemID}	[delete]
func (h *CartHandler) DeleteItem(c *gin.Context) error {
	cartID := c.Param("id")
	itemID := c.Param("itemID")

	itemIDInt, err := strconv.Atoi(itemID)
	if err != nil {
		return models.NewHTTPError(http.StatusBadRequest, err)
	}

	if err := h.repository.DeleteItem(c.Request.Context(), cartID, itemIDInt); err != nil {
		return models.NewHTTPError(http.StatusInternalServerError, err)
	}
	return nil
}
