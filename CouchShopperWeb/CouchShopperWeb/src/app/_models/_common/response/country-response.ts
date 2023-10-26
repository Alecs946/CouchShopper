export interface Country {
  id?: string;
  name: string;
  destinationCharge: number;
  saverDestinationCharge: number;
  baseNumberOfDays: number;
}

export interface CountryList {
  totalEntities: number;
  offset: number;
  countries: Country[];
}
