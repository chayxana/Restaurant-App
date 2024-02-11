import http, { get } from "k6/http";
import { check, sleep } from "k6";
import faker from "k6/x/faker";

export const options = {
  vus: 100,
  duration: "30s",
  thresholds: {
    http_req_failed: ["rate<0.01"],
    http_req_duration: ["p(99)<600"],
  },
};

const baseUrl = "http://localhost";
const cartUrl = baseUrl + "/shoppingcart";
const catalogUrl = baseUrl + "/catalog";
const checkoutUrl = baseUrl + "/checkout";

export default function () {
  const getFoods = http.get(catalogUrl + "/items/all");
  check(getFoods, {
    "get catalog items status was 200": (r) => r.status == 200,
  });

  if (getFoods.status != 200) {
    return;
  }

  const foods = getFoods.json();
  const food = foods[Math.floor(Math.random() * foods.length)];

  const user_id = faker.uuid();
  // create new cart
  const createCart = http.post(
    cartUrl + "/api/v1/cart",
    JSON.stringify({
      items: [
        {
          img: food.image,
          item_id: food.id,
          product_description: food.description,
          product_name: food.name,
          quantity: faker.number(1, 40),
          unit_price: food.price,
        },
      ],
      user_id,
    })
  );

  check(createCart, {
    "create new cart was 200": (r) => r.status == 200,
  });

  // checkout
  const newCart = createCart.json();
  const creditCard = faker.creditCard();
  const cardExp = creditCard.exp.split("/");

  const address = faker.address();

  const checkoutBody = JSON.stringify({
    address: {
      city: address.city,
      country: address.country,
      state: address.state,
      street_address: address.address,
      zip_code: Number(address.zip),
    },
    credit_card: {
      name_on_card: faker.name(),
      credit_card_cvv: Number(creditCard.cvv),
      credit_card_expiration_month: Number(cardExp[0]),
      credit_card_expiration_year: Number(cardExp[1]),
      credit_card_number: "4111111111111111",
    },
    user_id,
    email: faker.email(),
    user_currency: faker.currency().short,
    cart_id: newCart.id,
  });

  const checkout = http.post(checkoutUrl + "/api/v1/checkout", checkoutBody, {
    headers: { "Content-Type": "application/json" },
  });
  check(checkout, {
    "checkout was 200": (r) => r.status == 200,
  });

  console.log(checkout.json());

  sleep(1);
}
