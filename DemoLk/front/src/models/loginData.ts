export class LoginRequest {
  constructor(login: string, pass: string) {
    this.Login = login;
    this.Password = pass;
  }
  Login: string;
  Password: string;
}

export interface LoginData {
  Token: string;
  Role: string;
  User: string;
  RefreshToken: string;
}

export interface FullNameData {
  FirstName: string | undefined;
  LastName: string | undefined;
  MiddleName: string | undefined;
}

export class RefreshRequest {
  Refresh!: string | null;
}


