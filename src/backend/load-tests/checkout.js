import http from "k6/http";
import { sleep } from "k6";

export const options = {
  vus: 300,
  duration: "30s",
  thresholds: {
    http_req_duration: ["p(99)<300"],
  },
};

const baseUrl = "http://localhost:8080";
const catalogItems = baseUrl + "/cart/api/v1/items/all";

export default function () {
  const customer_id = "dfdee7b6-04d3-4d77-89a9-6542a4f2f31a";
  const foods = http.get(baseUrl + "/catalog/items/all").json();
  const food = foods[Math.floor(Math.random() * foods.length)];

  http.post(
    catalogItems,
    JSON.stringify({
      customer_id,
      items: [
        {
          food_id: food.id,
          food_name: food.name,
          old_unit_price: 0,
          picture: food.image,
          quantity: Math.floor(Math.random() * 20),
          unit_price: food.price,
        },
      ],
    })
  );

  sleep(1);

  http.post(baseUrl + "");
}
