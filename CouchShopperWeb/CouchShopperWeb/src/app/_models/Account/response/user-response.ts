export interface ActivationForm {
  activationCode:string
}
export interface LoginResponse {
  username: string;
  token: string;
  role:string
}

export interface UserIsActiveResponse {
  isActive: boolean
}

export interface UserResponse {
  username: string
  fullName: string
  email: string
  country: string
  city: string
  address: string
  phone: string
  zipCode: string
}
