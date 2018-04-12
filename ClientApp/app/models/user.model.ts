
export interface IUser {
    id: string,
    userName: string,
    dateRegistered: Date,
    contact: IContact
}

interface IContact {
    firstName: string,
    lastName?: string,
    email: string,
    phone?: string
}