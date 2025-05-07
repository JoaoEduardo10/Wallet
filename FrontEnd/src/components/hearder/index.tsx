export const Header = () => {
  return (
    <header className="bg-[linear-gradient(90deg,_rgb(255,1,150)_0%,_rgb(124,51,185)_100%)] text-white flex items-center justify-between p-4">
      <h1 className="text-xl font-bold">Carteira</h1>
      <section className="flex gap-5 items-center">
        <p className="text-lg  italic">
          Transferencias mais rápidas que o pix !
        </p>
      </section>
    </header>
  );
};
