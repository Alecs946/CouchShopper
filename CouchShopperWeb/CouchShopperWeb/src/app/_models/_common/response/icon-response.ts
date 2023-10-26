export interface Icon {
  id?: string;
  name: string;
}
export interface IconList {
  totalEntities: number;
  offset: number;
  icons: Icon[];
}
