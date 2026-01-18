import {User} from '../store/models/User';
import {JwtPayload} from '../store/models/JwtPayload';

export function mapJwtToUser(
  payload: JwtPayload,
  token: string
): User {
  return {
    id: payload.nameid,
    email: payload.email,
    firstName: payload.firstName,
    lastName: payload.lastName,
    role: Array.isArray(payload.role)
      ? payload.role
      : payload.role
        ? [payload.role]
        : [],
    token
  };
}
