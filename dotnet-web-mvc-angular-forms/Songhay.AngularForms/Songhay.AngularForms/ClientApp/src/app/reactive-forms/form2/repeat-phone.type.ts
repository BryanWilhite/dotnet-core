import { Component } from '@angular/core';

import { FieldArrayType } from '@ngx-formly/core';

@Component({
  selector: 'app-repeat-phone',
  template: `
  <h4>Phones</h4>
  <div *ngFor="let field of field.fieldGroup; let i = index;" class="container">
    <div class="row justify-content-between">
      <formly-field class="col-8" [field]="field"></formly-field>

      <button class="btn btn-secondary col-sm-2" type="button" (click)="remove(i)">Delete</button>
    </div>
  </div>

  <button class="btn btn-secondary" type="button" (click)="add()">{{ to.addText }}</button>
  `,
})
export class RepeatPhoneTypeComponent extends FieldArrayType {}
