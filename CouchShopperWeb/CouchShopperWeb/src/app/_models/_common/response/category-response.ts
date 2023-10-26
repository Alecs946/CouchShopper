export interface Category {
  id?: string;
  name: string;
  iconName: string;
}
export interface CategoryList {
  totalEntities: number;
  offset: number;
  categories: Category[];
}
