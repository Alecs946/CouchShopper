import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { SpinnerService } from '../../_services/spinner.service';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.css']
})
export class LoadingSpinnerComponent implements OnInit {
  showSpinner: boolean = false;

  constructor(private spinnerService: SpinnerService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.spinnerService.spinnerStatus.subscribe(status => {
      this.showSpinner = status;
      this.cdr.detectChanges();
    });
  }
}
