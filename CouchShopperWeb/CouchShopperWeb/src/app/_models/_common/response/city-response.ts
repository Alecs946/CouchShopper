export interface City {
  id?: string;
  countryId: string;
  name: string;
}

export interface CityList {
  totalEntities: number;
  offset: number;
  cities: City[];
}
