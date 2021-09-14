export interface APIResponse {
  IsOk: boolean;
  Code: number;
  Message: string;
}
export interface DataResponse<TData> {
  IsOk: boolean;
  Code: number;
  Message: string;
  Data: TData;
}
