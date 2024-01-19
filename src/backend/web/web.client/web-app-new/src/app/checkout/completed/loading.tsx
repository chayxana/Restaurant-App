import { ArrowPathIcon } from '@heroicons/react/24/solid';
export default function Loading() {
  return (
    <div className="mx-auto mt-10 max-w-md  rounded-lg border  bg-white p-6 shadow-lg">
      <div className="text-center">
        <ArrowPathIcon className="mx-auto h-12 w-12 text-slate-700" />
        <h3 className="my-2 text-lg font-semibold">Submitting the order...</h3>
      </div>
      <div className="flex animate-pulse space-x-4">
        <div className="h-10 w-10 rounded-full bg-slate-700"></div>
        <div className="flex-1 space-y-6 py-1">
          <div className="h-2 rounded bg-slate-700"></div>
          <div className="space-y-3">
            <div className="grid grid-cols-3 gap-4">
              <div className="col-span-2 h-2 rounded bg-slate-700"></div>
              <div className="col-span-1 h-2 rounded bg-slate-700"></div>
            </div>
            <div className="h-2 rounded bg-slate-700"></div>
          </div>
        </div>
      </div>
    </div>
  );
}
