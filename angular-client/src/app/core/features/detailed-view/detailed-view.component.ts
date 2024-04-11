import { Component, OnInit } from '@angular/core';
import { Machine } from '../../../shared/models/machine.model';
import { ActivatedRoute } from '@angular/router';
import { MachineService } from '../../../shared/services/machine.service';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-detailed-view',
  standalone: true,
  imports: [
    MatCardModule
  ],
  templateUrl: './detailed-view.component.html',
  styleUrl: './detailed-view.component.scss'
})
export class DetailedViewComponent implements OnInit {
  machine: Machine | undefined;

  constructor(
    private route: ActivatedRoute,
    private machineService: MachineService
  ) { }

  ngOnInit(): void {
      const id = this.route.snapshot.params['id'];
      if (id) {
        this.machineService.getMachineById(id).subscribe(machine => {
          this.machine = machine;
        });
      }
  }
}
