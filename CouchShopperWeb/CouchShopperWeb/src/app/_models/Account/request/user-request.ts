export interface LoginRequest {
  username: string;
  password: string;
}

export interface UserActivationRequest {
  username: string;
  code: string;
}

export interface UserAddRequest {
  username: string;
  email: string;
  name: string;
  password: string;
  isSeller: boolean;
}
