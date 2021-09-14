
export enum AddOrUpdate {
  Add,
  Update
}
export class DialogResult<T>
{
  isOk!: boolean;
  data!: T;
}
export class AddOrUpdateReq<T>
{
  Sign!: AddOrUpdate;
  Data!: T;
}

export class User {
  Id!: string;
  Login!: string;
  Role!: string;
  FirstName: string | undefined;
  LastName: string | undefined;
  MiddleName: string | undefined;
  PassUI!: string;
}

export class WorkStation {
  Id: string;
  Number: number;
  State: WorkStationState;
  Ip: String;
  Info: string;
  InfoInstance: WorkStationInfo;
  BusyDateTime: Date;
}

export enum WorkStationState {
  Wait,
  Busy,
  Off
}
export class WorkStationInfo {
  Ram: Loaded[];
  Cpu: Loaded[];
}
export class Loaded {
  Time: string;
  Usage: string;
}
