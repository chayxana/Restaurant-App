import { Component, OnInit } from '@angular/core';
import { FoodService } from "app/services/food.service";
import { Food } from "app/models/food";
import { ContentUrl } from "app/shared/constants";

@Component({
  selector: 'app-foods-list',
  templateUrl: './list.component.html'
})
export class FoodListComponent implements OnInit {
  contentUrl = ContentUrl;
  foods: Food[]
  isLoading: boolean;

  constructor(private foodService: FoodService) { }

  ngOnInit() {
    this.isLoading = true;
    this.foodService.getAll().subscribe(foods => {
      foods.forEach(f => {
        f.picture = ContentUrl + f.picture;
      });
      this.foods = foods;
      this.isLoading = false;
    });
  }
}
