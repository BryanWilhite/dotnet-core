import { Injectable } from '@angular/core';

import { Store, StoreConfig } from '@datorama/akita';

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
  constructor(private reactiveFormStore: ReactiveFormStore) {}

  getStateOfStore(): ReactiveFormModel {
    return this.reactiveFormStore.getValue();
  }

  updateBackingStore(formData: Partial<ReactiveFormModel>): void {
    console.log({ formData });
    this.reactiveFormStore.update(formData);
  }
}
