import { addressSchema, creditCardSchema } from '@/lib/types/checkout';
import { ZodIssue } from 'zod';

export async function checkoutServer(prevState: any, formData: FormData): Promise<ZodIssue[]> {
  // payment data
  const expiration =
    formData.get('cardExpiration')?.toString()?.split('/') || 'invalid expiration date';
  const creditCardInfoValidated = creditCardSchema.safeParse({
    name_on_card: formData.get('nameOnCard'),
    credit_card_number: formData.get('cardNumber'),
    credit_card_cvv: formData.get('cardCVC'),
    credit_card_expiration_month: expiration[0],
    credit_card_expiration_year: expiration[1]
  });

  if (!creditCardInfoValidated.success) {
    return creditCardInfoValidated.error.issues;
  }

  const addresDetailsValidated = addressSchema.safeParse({
    street_address: formData.get('streetAddress'),
    city: formData.get('city'),
    state: formData.get('state'),
    zip_code: formData.get('postalCode'),
    country: ''
  });

  if (!addresDetailsValidated.success) {
    return addresDetailsValidated.error.issues;
  }

  return [];
}
