import { IUser } from "./user.model";

export interface ISaveVehicle {
    id: number,
    name: string,
    isRegistered: boolean,
    modelId: number,
    makeId: number,
    identityId: string,
    features: number[]
}

export interface KeyValuePair { 
    id: number,
    name: string,
}
export interface IVehicle {
    id: number,
    name: string,
    model: KeyValuePair,
    make: KeyValuePair,
    user: IUser,
    isRegistered: boolean,
    features: KeyValuePair[],
    lastUpdate: string,
    orders?: any
  }