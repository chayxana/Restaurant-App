import { ShoppingCartIcon } from "@heroicons/react/24/solid";

export default function OpenCart({
  className,
  quantity,
}: {
  className?: string;
  quantity?: number;
}) {
  return (
    <div className="relative flex h-11 w-11 items-center justify-center rounded-md border border-neutral-200 text-black transition-colors">
      <ShoppingCartIcon className={className} />
      {quantity ? (
        <div className="absolute right-0 top-0 -mr-2 -mt-2 h-4 w-4 rounded bg-orange-500 text-[11px] font-medium text-white text-center">
          {quantity}
        </div>
      ) : null}
    </div>
  );
}
