module default {
	scalar type Role extending enum<Normal, Admin>; 

	type Contact{
		required username: str;
		required password: str;
		required contactRole : Role;
		required firstName: str;
		required lastName: str;
		required email: str;
		description: str;
		required birthDate: str;
		required status: bool;
		required title: str
	}
}
