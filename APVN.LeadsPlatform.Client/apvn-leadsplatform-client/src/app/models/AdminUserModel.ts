export class AdminUserModel {
	public userId: number;
	public userName: string;
	public password: string;
    public confirmPassword: string;
	public displayName: string;
	public avatar: string;
	public description: string;
	public email: string;
	public mobile: string;
	public tokenKey: string;
	public createdDate: string;
	public createdBy: string;
	public groups: string;
    public roles: string;
    public isAdminGroup: boolean;
	public status: number;
    public allowEdit: boolean;
    public allowDelete: boolean;
}

export class UserLoginModel {
    public username: string;
    public password: string;
    public remember: boolean;
    public returnUrl: string;
	public otpPrivateKey: string;
}
