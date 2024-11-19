export * from './inscription.service';
import { InscriptionService } from './inscription.service';
export * from './inscription.serviceInterface';
export * from './user.service';
import { UserService } from './user.service';
export * from './user.serviceInterface';
export const APIS = [InscriptionService, UserService];
