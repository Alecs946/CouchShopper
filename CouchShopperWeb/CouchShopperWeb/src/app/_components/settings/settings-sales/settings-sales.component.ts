import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import * as echarts from 'echarts';
import { UserSalesProducts } from '../../../_models/_settings/response/settings-response';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-sales',
  templateUrl: './settings-sales.component.html',
  styleUrls: ['./settings-sales.component.css']
})
export class SettingsSalesComponent implements AfterViewInit {
  @ViewChild('chart', { static: true }) chartElementRef!: ElementRef;
  selectedPeriod: number = 1
  typeData: string[]
  seriesData: number[]
  productDetails: UserSalesProducts[]
  constructor(private settingService: SettingsService) { }
  chartInstance: echarts.ECharts;
  ngOnInit(): void {
    this.getSalesInfo()
  }

  ngAfterViewInit(): void {
    const chartDom = this.chartElementRef.nativeElement;
    if (!chartDom) {
      return;
    }
    this.chartInstance = echarts.init(chartDom);;
    this.setChartOptions(this.typeData, this.seriesData)
  }

  onSelectPeriod(): void {
    this.getSalesInfo()
  }
  setChartOptions(typeData, seriesData) {
    const option = {
      xAxis: {
        type: 'category',
        data: typeData,
      },
      yAxis: {
        type: 'value',
      },
      series: [{
        data: seriesData,
        type: 'line',
        smooth: true
      }],
    };
    this.chartInstance.setOption(option)
  }


  getSalesInfo() {
    this.settingService.getSalesInfo(this.selectedPeriod).subscribe({
      next: (response => {
        this.typeData = response.salesDetails.map(x => {
          return x.date
        })
        this.seriesData = response.salesDetails.map(x => {
          return x.revenue
        })
        this.setChartOptions(this.typeData, this.seriesData)
        this.productDetails = response.salesProducts
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }
}
