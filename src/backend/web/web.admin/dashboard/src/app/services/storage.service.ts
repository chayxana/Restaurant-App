import { Injectable } from '@angular/core';

const storage = localStorage;
@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  setItem(key: string, value: string) {
    storage.setItem(key, value);
  }

  getItem(key: string): string {
    return storage.getItem(key);
  }
}
