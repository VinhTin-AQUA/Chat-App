import { signalStore, withState } from '@ngrx/signals';
import { User } from '../models/user';

const initialState: User = {
	email: '',
	fullName: '',
	avatarUrl: '',
};

export const UserStore = signalStore({ providedIn: 'root' }, withState(initialState));
