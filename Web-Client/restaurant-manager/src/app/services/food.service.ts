import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Food } from 'app/models/food'
import { BaseService } from "app/services/base.service";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ApiUrl } from "app/shared/constants";

@Injectable()
export class FoodService extends BaseService<Food> {


  constructor(http: Http) {
    super(http);
  }

  baseUrl(id?: string): string {
    return ApiUrl + "/foods/" + id || "";
  }
}
