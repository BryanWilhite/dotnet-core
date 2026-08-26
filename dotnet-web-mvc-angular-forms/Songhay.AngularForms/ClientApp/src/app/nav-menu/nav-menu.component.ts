import { Component, VERSION } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  clientFrameworkVersion: string;
  isExpanded = false;

  constructor() {
    this.clientFrameworkVersion = `${VERSION.major}.${VERSION.minor}.${VERSION.patch
      }`;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
