import { Food } from './food';
import { MatProgressButtonOptions } from 'mat-progress-buttons';
import * as uuid from 'uuid';

export function getFoodInstance(): Food {
  return <Food>{
    id: uuid.v4(),
    name: '',
    description: '',
    price: null,
    category: null,
    categoryId: null,
    pictures: [],
    deletedPictures: [],
    currency: null
  };
}

export function getProgressButtonOptions(text: string = null): MatProgressButtonOptions {
  return {
    active: false,
    text: text || 'Save',
    spinnerSize: 19,
    raised: true,
    stroked: false,
    flat: false,
    fab: false,
    buttonColor: 'primary',
    spinnerColor: 'accent',
    fullWidth: false,
    disabled: false,
    mode: 'indeterminate',
  };
}
