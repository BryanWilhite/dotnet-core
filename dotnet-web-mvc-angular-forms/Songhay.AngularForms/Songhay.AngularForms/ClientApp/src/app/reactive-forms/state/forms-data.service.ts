import { Observable } from 'rxjs';

import { Inject, Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Store, StoreConfig } from '@datorama/akita';
import { FormlyFieldConfig } from '@ngx-formly/core';

export interface FormlyData {
  componentSet: {
    [index: string]: {
      fields: FormlyFieldConfig[],
      model?: {},
    }
  };

  model: Partial<ReactiveFormModel>;
}

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
  formlyData: FormlyData;

  constructor(
    @Inject('BASE_URL_FOR_API') private apiBaseUri: string,
    private reactiveFormStore: ReactiveFormStore,
    public client: HttpClient) { }

  getFormlyConfig(formId: 'form1' | 'form2' | 'form3'): FormlyFieldConfig[] {
    this.verifyFormlyData();
    const config = this.formlyData.componentSet[formId];

    if (!config) {
      throw new Error('The expected formly fields are not here.');
    }

    return config.fields;
  }

  getStateOfStore(): ReactiveFormModel {
    return this.reactiveFormStore.getValue();
  }

  loadFormlyData(): Observable<{}> {
    const observable = this.client.get(`${this.apiBaseUri}formly`);

    observable.subscribe((data: {}) => {
      if (!data) {
        throw new Error('The expected formly data is not here.');
      }

      this.formlyData = data as FormlyData;
      this.verifyFormlyData();
    });

    return observable;
  }

  updateBackingStore(formData: Partial<ReactiveFormModel>): void {
    console.log('updateBackingStore', { formData });
    this.reactiveFormStore.update(formData);
  }

  private verifyFormlyData(): void {
    if (!this.formlyData) {
      throw new Error('The expected formly domain data is not here.');
    }

    if (!this.formlyData.componentSet) {
      throw new Error('The expected formly domain data is not here.');
    }
  }
}
