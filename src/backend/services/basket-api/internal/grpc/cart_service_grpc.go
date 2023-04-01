package grpc

import (
	context "context"

	"github.com/jurabek/basket.api/internal/models"
	pbv1 "github.com/jurabek/basket.api/pb/v1"
)

var _ pbv1.CartServiceServer = (*cartGrpcService)(nil)

type CartGetter interface {
	Get(ctx context.Context, customerID string) (*models.CustomerBasket, error)
}

type cartGrpcService struct {
	getter CartGetter
}

func NewCartGrpcService(cartGetter CartGetter) pbv1.CartServiceServer {
	return &cartGrpcService{
		getter: cartGetter,
	}
}

func mapBasketToCartResponse(basket *models.CustomerBasket) *pbv1.GetCustomerCartResponse {
	var cartItems []*pbv1.CartItem
	for _, basketItem := range *basket.Items {
		cartItems = append(cartItems, &pbv1.CartItem{
			ItemId:   int64(basketItem.FoodID),
			Price:    basketItem.UnitPrice,
			Quantity: int64(basketItem.Quantity),
		})
	}

	return &pbv1.GetCustomerCartResponse{
		CustomerId: basket.CustomerID.String(),
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
