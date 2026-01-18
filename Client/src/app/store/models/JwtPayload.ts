export interface JwtPayload {
  nameid: string;          // ClaimTypes.NameIdentifier
  email: string;
  firstName: string;
  lastName: string;
  role?: string | string[];
  exp: number;
  iss: string;
  aud: string;
}

