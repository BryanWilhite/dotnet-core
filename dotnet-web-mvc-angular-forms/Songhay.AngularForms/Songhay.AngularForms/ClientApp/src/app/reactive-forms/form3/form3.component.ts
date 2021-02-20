import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';

import { FormlyFieldConfig } from '@ngx-formly/core';

import { FormsDataService, ReactiveFormModel } from '../state/forms-data.service';

@Component({
  selector: 'app-form3',
  templateUrl: './form3.component.html',
  styleUrls: ['./form3.component.css']
})
export class Form3Component implements OnInit, OnDestroy {
  componentForm = new FormGroup({});

  fields: FormlyFieldConfig[];
  model: Partial<ReactiveFormModel>;
  success = false;

  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private reactiveFormService: FormsDataService
  ) {}

  ngOnDestroy(): void {
    for (const sub of this.subscriptions) {
      sub.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.fields = [
      {
        key: 'password',
        type: 'input',
        templateOptions: {
          label: 'Password',
          minLength: 8,
          pattern: '^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$',
          required: true,
          type: 'password',
        },
      },
      {
        key: 'age',
        type: 'input',
        templateOptions: {
          label: 'Age',
          type: 'number',
          min: 18,
          minLength: 2,
          max: 65,
          required: true,
        }
      }
    ];

    const state = this.reactiveFormService.getStateOfStore();
    this.model = {
      password: state?.password,
      age: state?.age,
    }

    const sub = this.componentForm.valueChanges.subscribe(change => {
      if (!this.componentForm.valid) {
        return;
      }

      this.reactiveFormService.updateBackingStore(change);
    });

    this.subscriptions.push(sub);
  }

  next() {
    this.success = true;

    console.log('next', { model: this.reactiveFormService.getStateOfStore() });
  }

  previous(): void {
    this.router.navigate(['wizard/step-2-of-3']);
  }
}
