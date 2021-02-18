import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl
} from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';

import { FormsDataService } from '../state/forms-data.service';

@Component({
  selector: 'app-form1',
  templateUrl: './form1.component.html',
  styleUrls: ['./form1.component.css']
})
export class Form1Component implements OnInit , OnDestroy {
  componentForm: FormGroup;
  success = false;

  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private reactiveFormService: FormsDataService
  ) {}

  ngOnDestroy(): void {
    for (const sub of this.subscriptions) {
      sub.unsubscribe();
    }
  }

  ngOnInit() {
    const state = this.reactiveFormService.getStateOfStore();

    this.componentForm = this.fb.group({
      email: [state?.email, [Validators.required, Validators.email]]
    });

    const sub = this.componentForm.valueChanges.subscribe(change => {
      if (!this.componentForm.valid) {
        return;
      }

      this.reactiveFormService.updateBackingStore(change);
    });

    this.subscriptions.push(sub);
  }

  get email(): AbstractControl {
    return this.componentForm.get('email');
  }

  next() {
    this.success = true;

    console.log({ model: this.reactiveFormService.getStateOfStore() });

    this.router.navigate(['/step-2-of-3']);
  }
}
