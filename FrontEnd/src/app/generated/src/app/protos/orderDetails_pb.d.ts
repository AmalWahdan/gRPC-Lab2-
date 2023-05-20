// package: 
// file: src/app/protos/orderDetails.proto

import * as jspb from "google-protobuf";

export class OrderRequest extends jspb.Message {
  getUserid(): string;
  setUserid(value: string): void;

  getProductid(): string;
  setProductid(value: string): void;

  getQuantity(): number;
  setQuantity(value: number): void;

  getAmount(): number;
  setAmount(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): OrderRequest.AsObject;
  static toObject(includeInstance: boolean, msg: OrderRequest): OrderRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: OrderRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): OrderRequest;
  static deserializeBinaryFromReader(message: OrderRequest, reader: jspb.BinaryReader): OrderRequest;
}

export namespace OrderRequest {
  export type AsObject = {
    userid: string,
    productid: string,
    quantity: number,
    amount: number,
  }
}

export class OrderResponse extends jspb.Message {
  getDone(): boolean;
  setDone(value: boolean): void;

  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): OrderResponse.AsObject;
  static toObject(includeInstance: boolean, msg: OrderResponse): OrderResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: OrderResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): OrderResponse;
  static deserializeBinaryFromReader(message: OrderResponse, reader: jspb.BinaryReader): OrderResponse;
}

export namespace OrderResponse {
  export type AsObject = {
    done: boolean,
    message: string,
  }
}

