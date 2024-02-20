import { signalStore, withState } from '@ngrx/signals';
import { Group } from '../models/group';

type GroupState = {
	length: number,
    groups: Group[]
};

const initialState: GroupState = {
	length: 0,
    groups: []
};

export const GroupStore = signalStore({ providedIn: 'root' }, withState(initialState));
