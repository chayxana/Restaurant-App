import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Food } from 'app/models/food'
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { BaseService } from "app/services/base.service";

@Injectable()
export class FoodService extends BaseService {

  constructor(private http: Http) {
    super();
    this.baseUrl = "http://localhost:4200/api/foods/"
  }

  getAll(): Observable<Food[]> {
    return null;
  }

  get(id: string): Observable<Food> {
    return null;
  }

  update(food: Food) {
    return null;
  }

  create(food: Food) {
    return null;
  }
}
