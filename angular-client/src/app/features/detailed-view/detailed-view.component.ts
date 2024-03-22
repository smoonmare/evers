import { Component, OnInit } from '@angular/core';
import { Machine } from '../../shared/models/machine.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detailed-view',
  standalone: true,
  imports: [],
  templateUrl: './detailed-view.component.html',
  styleUrl: './detailed-view.component.scss'
})
export class DetailedViewComponent implements OnInit {
  item: Machine | undefined;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
      const itemId = this.route.snapshot.params['id'];
  }
}
