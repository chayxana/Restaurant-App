import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Food } from 'app/models/food'
import { BaseService } from "app/services/base.service";
import { HttpRequest } from '@angular/common/http';
import { NgForm } from "@angular/forms"

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ApiUrl } from "app/shared/constants";

@Injectable()
export class FoodService extends BaseService<Food> {


  constructor(http: Http) {
    super(http);
  }

  baseUrl(id?: string): string {
    var entityId = "";
    if (id)
      entityId = "/" + id;
    return ApiUrl + "foods" + entityId;
  }

  createFood(food: Food, file: File): any {
    return new HttpRequest("POST", this.baseUrl(), file, {
      reportProgress: true,
    });
  }

  uploadImage(picture: File, foodId: string) {
    return new Promise((resolve, reject) => {
      let xhr: XMLHttpRequest = new XMLHttpRequest();
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
          if (xhr.status === 200) {
            resolve(true);
          } else {
            reject(false);
          }
        }
      };

      xhr.open('POST', this.baseUrl() + "/UploadFile", true);

      let formData = new FormData();
      formData.append("file", picture, picture.name);
      formData.append("foodId", foodId);
      xhr.send(formData);
    });
  }
}
