export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  personName: string;
  gender: string;
}

export interface AuthResponse {
  userID: string;
  email: string;
  personName: string;
  gender: string;
  token: string | null;
  refreshToken: string | null;
  success: boolean;
}
