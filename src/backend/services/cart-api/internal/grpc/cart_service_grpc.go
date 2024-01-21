package grpc

import (
	context "context"

	"github.com/jurabek/cart-api/internal/models"
	pbv1 "github.com/jurabek/cart-api/pb/v1"
)

var _ pbv1.CartServiceServer = (*cartGrpcService)(nil)

type CartGetter interface {
	Get(ctx context.Context, customerID string) (*models.Cart, error)
}

type cartGrpcService struct {
	getter CartGetter
}

func NewCartGrpcService(cartGetter CartGetter) pbv1.CartServiceServer {
	return &cartGrpcService{
		getter: cartGetter,
	}
}

func mapBasketToCartResponse(basket *models.Cart) *pbv1.GetCustomerCartResponse {
	var cartItems []*pbv1.CartItem
	for _, basketItem := range *basket.LineItems {
		cartItems = append(cartItems, &pbv1.CartItem{
			ItemId:   int64(basketItem.ItemID),
			Price:    basketItem.UnitPrice,
			Quantity: int64(basketItem.Quantity),
		})
	}

	return &pbv1.GetCustomerCartResponse{
		CustomerId: basket.ID.String(),
		Items:      cartItems,
	}
}

// GetCustomerCart implements v1.CartServiceServer
func (s *cartGrpcService) GetCustomerCart(
	ctx context.Context,
	req *pbv1.GetCustomerCartRequest,
) (*pbv1.GetCustomerCartResponse, error) {
	customerBasket, err := s.getter.Get(ctx, req.GetCustomerId())
	if err != nil {
		return nil, err
	}
	return mapBasketToCartResponse(customerBasket), nil
}
