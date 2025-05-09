export default function Loading() {
  return (
    <div className="fixed inset-0 z-50 bg-white opacity-70 flex items-center justify-center">
      <div className="flex flex-col items-center gap-4">
        <div className="w-12 h-12 border-4 border-indigo-500 border-t-transparent rounded-full animate-spin"></div>
        <p className="text-indigo-600 font-semibold text-lg">Carregando...</p>
      </div>
    </div>
  );
}
