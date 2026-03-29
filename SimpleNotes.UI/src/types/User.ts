export interface Address {
  streetNo: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  age: number;
  email: string;
  address?: Address;
}
