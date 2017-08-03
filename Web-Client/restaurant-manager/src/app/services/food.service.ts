import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Food } from 'app/models/food'
import { BaseService } from "app/services/base.service";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class FoodService extends BaseService<Food> {


  constructor(http: Http) {
    super(http);
  }

  baseUrl(id?: string): string {
    return "http://localhost:4200/api/foods/" + id;
  }
}
