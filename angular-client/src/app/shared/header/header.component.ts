import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    FormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @ViewChild('searchbar') searchbar!: ElementRef;
  searchText = '';
  toggleSearch: boolean = false;

  openSearch(): void {
    this.toggleSearch = true;
    this.searchbar.nativeElement.focus();
  }

  closeSearch() {
    this.searchText = '';
    this.toggleSearch = false;
  }
}
