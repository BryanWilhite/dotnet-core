import { Injectable } from '@angular/core';

import { HttpClient, HttpResponse } from '@angular/common/http';

import { Store, StoreConfig } from '@datorama/akita';
import { Observable } from 'rxjs';

export interface PhoneNumber {
  area: number;
  prefix: number;
  line: number;
}

export interface ReactiveFormModel {
  email: string;
  password: string;
  phones: PhoneNumber[];
  age: number;
}

const initialState = {} as ReactiveFormModel;

@Injectable({
  providedIn: 'root'
})
@StoreConfig({ name: 'reactive-form' })
export class ReactiveFormStore extends Store<ReactiveFormModel> {
  constructor() {
    super(initialState);
  }
}

@Injectable({
  providedIn: 'root'
})
export class FormsDataService {
  constructor(
    private reactiveFormStore: ReactiveFormStore,
    public client: HttpClient) { }

  getStateOfStore(): ReactiveFormModel {
    return this.reactiveFormStore.getValue();
  }

  updateBackingStore(formData: Partial<ReactiveFormModel>): void {
    console.log('updateBackingStore', { formData });
    this.reactiveFormStore.update(formData);
  }
}
