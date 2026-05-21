export interface User {
  firstName: string;
  lastName: string;
}

export interface UserResponseDto {
  firstName: string;
  lastName: string;
}

export interface UserCreatedResponse {
  message: string;
  data: UserResponseDto;
}
