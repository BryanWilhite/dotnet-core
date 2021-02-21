import { Subscription } from 'rxjs/internal/Subscription';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup} from '@angular/forms';
import { Router } from '@angular/router';

import { FormlyFieldConfig } from '@ngx-formly/core';

import { FormsDataService, ReactiveFormModel } from '../state/forms-data.service';

@Component({
  selector: 'app-form2',
  templateUrl: './form2.component.html',
  styleUrls: ['./form2.component.css']
})
export class Form2Component implements OnInit, OnDestroy {
  componentForm = new FormGroup({});

  fields: FormlyFieldConfig[];
  model: Partial<ReactiveFormModel>;
  success = false;

  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private reactiveFormService: FormsDataService
  ) { }

  ngOnDestroy(): void {
    for (const sub of this.subscriptions) {
      sub.unsubscribe();
    }
  }

  ngOnInit() {

    this.fields = [
      {
        key: 'phones',
        type: 'repeat-phone',
        templateOptions: {
          addText: 'Add Phone Number',
        },
        fieldArray: {
          fieldGroupClassName: 'phones row',
          fieldGroup: [
            {
              key: 'area',
              type: 'input',
              className: 'col-sm-3',
              templateOptions: {
                maxLength: 3,
                placeholder: 'area',
                required: true
              }
            },
            {
              key: 'prefix',
              type: 'input',
              className: 'col-sm-3',
              templateOptions: {
                maxLength: 3,
                placeholder: 'prefix',
                required: true
              }
            },
            {
              key: 'line',
              type: 'input',
              className: 'col-sm-6',
              templateOptions: {
                maxLength: 4,
                placeholder: 'line',
                required: true
              }
            },
          ]
        }
      }
    ];

    const state = this.reactiveFormService.getStateOfStore();
    this.model = { phones: state?.phones };

    const sub = this.componentForm.valueChanges.subscribe(change => {
      if (!this.componentForm.valid) {
        return;
      }

      this.reactiveFormService.updateBackingStore(change);
    });

    this.subscriptions.push(sub);
  }

  next(): void {
    this.success = true;

    console.log('next', { model: this.reactiveFormService.getStateOfStore() });

    this.router.navigate(['wizard/step-3-of-3']);
  }

  previous(): void {
    this.router.navigate(['wizard']);
  }
}
