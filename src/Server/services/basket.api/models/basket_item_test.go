package models

import (
	"testing"

	"github.com/google/uuid"
	"gopkg.in/go-playground/assert.v1"
)

func BasketItemGetterSetterTest(t *testing.T) {
	itemID := uuid.New()
	foodID := uuid.New()

	item := BasketItem{
		ID:           itemID,
		FoodID:       foodID,
		UnitPrice:    20,
		OldUnitPrice: 20,
		Quantity:     1,
		Picture:      "picture1",
		FoodName:     "Name1",
	}

	assert.Equal(t, itemID, item.ID)
	assert.Equal(t, foodID, item.FoodID)
	assert.Equal(t, 20, item.UnitPrice)
	assert.Equal(t, 20, item.OldUnitPrice)
	assert.Equal(t, 1, item.Quantity)
	assert.Equal(t, "picture1", item.Picture)
	assert.Equal(t, "Name1", item.FoodName)
}
