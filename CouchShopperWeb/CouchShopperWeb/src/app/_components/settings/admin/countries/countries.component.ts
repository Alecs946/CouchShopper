import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { number } from 'echarts';
import { count } from 'rxjs';
import { City } from '../../../../_models/_common/request/city-request';
import { Country } from '../../../../_models/_common/request/country-request';
import { CommonService } from '../../../../_services/common.service';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.css']
})
export class CountriesComponent {
  countryForm !: FormGroup;
  cityForm !: FormGroup;
  showCountryForm: boolean = false;
  showCityForm: boolean = false;
  countryFormSubmitted: boolean = false;
  cityFormSubmitted: boolean = false;
  selectedCountryForm: FormGroup;
  selectedCountry: Country | null = null;
  countries: Country[] = [];
  numberOfCountries = 0;
  countryCurrentPage = 1;
  showCitiesForCountryId: string = "";
  cities: City[] | null = null;
  selectedCity: City | null = null;
  newCity: string = '';
  numberOfCities = 0;
  cityCurrentPage = 1;
  itemsPerPage = 5;
  maxSizePagination = 5;

  constructor(private formBuilder: FormBuilder, private commonService: CommonService) {
    this.countryForm = this.formBuilder.group({
      id: [],
      name: ['', Validators.required],
      destinationCharge: ['', Validators.required],
      saverDestinationCharge: ['', Validators.required],
      baseNumberOfDays: [null, Validators.required],
    })

    this.cityForm = this.formBuilder.group({
      newCity: ['', Validators.required],
    })
    
  }


  ngOnInit(): void {
    this.getCountries();
  }

  getCountries(): void {
    this.commonService.getCountries(this.countryCurrentPage)
      .subscribe(
        {
          next: (response => {
            this.numberOfCountries = response.totalEntities;
            this.countries = response.countries;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  addCountry(): void {
    if (this.countryForm.valid) {
      this.countryFormSubmitted = false;
      this.commonService.addCountry(this.countryForm.value).subscribe(
        {
          next: (() => {
            this.countryForm.reset();
            this.getCountries()
            this.showCountryForm = false;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
    else {
      this.countryFormSubmitted = true
    }
  }

  deleteCountry(countryId: string): void {
    this.commonService.deleteCountry(countryId)
      .subscribe(
        {
          next: (response => {
            this.getCountries()
            this.cities = null;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  editCountry(country: Country): void {
    this.selectedCountry = country;
  }

  saveCountry(country: Country): void {
    this.commonService.updateCountry(country).subscribe(
      {
        next: (response => {
          this.getCountries()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      }
    );
    this.selectedCountry = null;
  }

  cancelEditCountry(): void {
    this.selectedCountry = null;
  }

  countryPageChanged(event: any): void {
    this.countryCurrentPage = event.page;
    this.getCountries();
  }

  getCitiesByCountry(countryId: string): void {
    this.showCitiesForCountryId = countryId;
    this.commonService.getCitiesByCountry(this.cityCurrentPage, countryId)
      .subscribe(
        {
          next: (response => {
            this.numberOfCities = response.totalEntities;
            this.cities = response.cities;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }


  addCity(): void {
    if (this.cityForm.valid) {
      this.cityFormSubmitted = false
      const city: City = { name: this.cityForm.get('newCity').value, countryId: this.showCitiesForCountryId }
      this.commonService.addCity(city).subscribe(
        {
          next: (response => {
            this.getCitiesByCountry(this.showCitiesForCountryId)
            this.cityForm.reset();
            this.showCityForm = false;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
    else {
      this.cityFormSubmitted = true
    }
  }

  deleteCity(cityId: string) {
    this.commonService.deleteCity(cityId)
      .subscribe(
        {
          next: (response => {
            this.getCitiesByCountry(this.showCitiesForCountryId);
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  editCity(city: City): void {
    this.selectedCity = city;
  }

  saveCity(city: City): void {
    if (city.name.trim() !== "") {
      this.commonService.updateCity(city).subscribe(
        {
          next: (response => {
            this.getCitiesByCountry(this.showCitiesForCountryId)
            this.selectedCity = null;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
  }

  cancelEditCity(): void {
    this.selectedCity = null;
  }

  cityPageChanged(event: any): void {
    this.countryCurrentPage = event.page;
    this.getCitiesByCountry(this.showCitiesForCountryId);
  }

}
