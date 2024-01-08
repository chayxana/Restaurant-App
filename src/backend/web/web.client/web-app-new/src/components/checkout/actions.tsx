import { creditCardSchema } from '@/lib/types/checkout';

export async function checkoutServer(prevState: any, formData: FormData) {
  console.log('checkoutServer');
  console.log(formData);

  // payment data
  const creditCardInfoValidated = creditCardSchema.safeParse({
    credit_card_cvv: formData.get('cardCVC'),
    credit_card_number: formData.get('cardNumber'),
    name_on_card: formData.get('cardName')
  });

  if (!creditCardInfoValidated.success) {
    return {
      errors: creditCardInfoValidated.error.flatten().fieldErrors
    };
  }
}
