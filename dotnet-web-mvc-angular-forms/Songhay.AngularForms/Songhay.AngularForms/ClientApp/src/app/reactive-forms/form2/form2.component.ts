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

    this.fields = this.reactiveFormService.getFormlyConfig('form2');

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
