export enum ErrorCode {
	
}

export class ErrorDto {
	public readonly code: ErrorCode;
	public readonly message: string;

	public constructor(code: ErrorCode, message: string) {
		this.code = code;
		this.message = message;
	}

	public static fromJson(errorObject: any) {
		return new ErrorDto(errorObject.code, errorObject.message);
	}

	public getMessage(): string {
		return "Unknown Error occurred. Please try again later.";
	}
}