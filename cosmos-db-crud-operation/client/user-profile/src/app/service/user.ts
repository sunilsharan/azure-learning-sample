export interface IAddress {
    addressLine1: string;
    addressLine2?: any;
    city: string;
    state: string;
    postalcode: string;
}

export interface IUser {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    address: IAddress;
    dob: Date;
}
