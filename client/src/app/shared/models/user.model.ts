export interface User {
  firstName: string;
  lastName: string;
}

export interface UserCreatedDto {
  firstName: string;
  lastName: string;
}

export interface UserCreatedResponse {
  message: string;
  data: UserCreatedDto;
}
