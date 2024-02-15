import { signalStore, withState } from '@ngrx/signals';

type LoadingState = {
	isLoading: boolean;
	isLoggingIn: boolean
};

const initialState: LoadingState = {
	isLoading: false,
	isLoggingIn: false
};

export const LoadingStore = signalStore({ providedIn: 'root' }, withState(initialState));
