export interface SelectedOption {
  optionName: string;
  optionValue: string;
}
export interface CartItem {
  productId: string,
  quantity: number,
  selectedOption: SelectedOption[]
}

