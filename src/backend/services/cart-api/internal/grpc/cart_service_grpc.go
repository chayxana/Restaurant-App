package grpc

import (
	context "context"

	"github.com/jurabek/cart-api/internal/models"
	pbv1 "github.com/jurabek/cart-api/pb/v1"
)

var _ pbv1.CartServiceServer = (*cartGrpcService)(nil)

type CartGetter interface {
	Get(ctx context.Context, cartID string) (*models.Cart, error)
}

type cartGrpcService struct {
	getter CartGetter
}

func NewCartGrpcService(cartGetter CartGetter) pbv1.CartServiceServer {
	return &cartGrpcService{
		getter: cartGetter,
	}
}

func mapBasketToCartResponse(cart *models.Cart) *pbv1.GetCartResponse {
	var cartItems []*pbv1.CartItem
	for _, basketItem := range cart.LineItems {
		cartItems = append(cartItems, &pbv1.CartItem{
			ItemId:   int64(basketItem.ItemID),
			Price:    basketItem.UnitPrice,
			Quantity: int64(basketItem.Quantity),
		})
	}

	return &pbv1.GetCartResponse{
		CartId: cart.ID.String(),
		Items:  cartItems,
	}
}

// GetCustomerCart implements v1.CartServiceServer
func (s *cartGrpcService) GetCart(
	ctx context.Context,
	req *pbv1.GetCartRequest,
) (*pbv1.GetCartResponse, error) {
	customerBasket, err := s.getter.Get(ctx, req.GetCartId())
	if err != nil {
		return nil, err
	}
	return mapBasketToCartResponse(customerBasket), nil
}
