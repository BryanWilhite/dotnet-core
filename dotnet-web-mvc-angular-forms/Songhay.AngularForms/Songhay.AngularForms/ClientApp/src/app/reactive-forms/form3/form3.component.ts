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
  serverMessage = '[nothing from the server 😑]';
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

    this.fields = this.reactiveFormService.getFormlyConfig('form3');

    const state = this.reactiveFormService.getStateOfStore();
    this.model = {
      password: state?.password,
      age: state?.age,
    };

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

    const sub = this.reactiveFormService.saveReactiveFormModel().subscribe(() => {

      const state = this.reactiveFormService.getStateOfStore();

      console.log('saveReactiveFormModel', { model: state });

      this.serverMessage = state.serverData;

    });

    this.subscriptions.push(sub);

    console.log('next', { model: this.reactiveFormService.getStateOfStore() });
  }

  previous(): void {
    this.router.navigate(['wizard/step-2-of-3']);
  }
}
