import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { Form1Component } from './reactive-forms/form1/form1.component';
import { Form2Component } from './reactive-forms/form2/form2.component';
import { RepeatPhoneTypeComponent } from './reactive-forms/form2/repeat-phone.type';
import { Form3Component } from './reactive-forms/form3/form3.component';
import { FormlyFieldConfig, FormlyModule } from '@ngx-formly/core';
import { FormlyBootstrapModule } from '@ngx-formly/bootstrap';
import { filterNil } from '@datorama/akita';

export function getMinLengthValidationMessage(err: any, field: FormlyFieldConfig) {
  return `Should have at least ${field.templateOptions.minLength} characters`;
}

export function getMaxLengthValidationMessage(err: any, field: FormlyFieldConfig) {
  return `This value should be less than ${field.templateOptions.maxLength} characters`;
}

export function getMinValidationMessage(err: any, field: FormlyFieldConfig) {
  return `This value should be more than ${field.templateOptions.min}`;
}

export function getMaxValidationMessage(err: any, field: FormlyFieldConfig) {
  return `This value should be less than ${field.templateOptions.max}`;
}

export function getRequiredMessage(err: any, field: FormlyFieldConfig) {
  const key = field.templateOptions.label ??
    field.templateOptions.placeholder ??
    field.templateOptions.description ??
    field.key;
  return `${key} is required`;
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    Form1Component,
    Form2Component,
    RepeatPhoneTypeComponent,
    Form3Component
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'wizard', component: Form1Component },
      { path: 'wizard/step-2-of-3', component: Form2Component },
      { path: 'wizard/step-3-of-3', component: Form3Component },
    ],
      { relativeLinkResolution: 'legacy' }),
    FormlyModule.forRoot({
      extras: { lazyRender: true },
      types: [
        { name: 'repeat-phone', component: RepeatPhoneTypeComponent },
      ],
      validationMessages: [
        { name: 'required', message: getRequiredMessage },
        { name: 'minlength', message: getMinLengthValidationMessage },
        { name: 'maxlength', message: getMinLengthValidationMessage },
        { name: 'min', message: getMinValidationMessage },
        { name: 'max', message: getMaxValidationMessage },
      ]
    }),
    FormlyBootstrapModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
