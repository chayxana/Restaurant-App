import OrderCompletion from './component';

export default function Page({ searchParams }: { searchParams?: { [key: string]: string } }) {
  console.log(searchParams);
  return <OrderCompletion />;
}
