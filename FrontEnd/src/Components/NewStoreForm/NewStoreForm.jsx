import Input from "../Input/Input";
import { useState } from "react";
function NewStoreForm({ isToggleable = false }) {
  const [toggleShow, setToggleShow] = useState(false);

  const items = [
    {
      name: "storeName",
      labelname: "Nombre del negocio",
      id: "storeName",
      placeholder: "El gauchito gil",
      required: true,
    },
    {
      name: "storeType",
      labelname: "Tipo de negocio",
      id: "storeType",
      placeholder: "Carniceria",
      required: true,
    },
    {
      name: "bussinesHours",
      labelname: "Horario de trabajo",
      id: "bussinesHours",
      placeholder: "bussinesHours",
      required: true,
    },
    {
      name: "extraInfo",
      labelname: "Informacion extra",
      id: "extraInfo",
      placeholder: "Tremendas las milanesas",
      required: false,
    },
  ];
  const handleToggle = () => {
    setToggleShow((prev) => !prev);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(e.target);
  };

  const renderForm = () => (
    <form onSubmit={handleSubmit}>
      <div>
        {items.map((item) => (
          <Input key={item.type} {...item} />
        ))}

        <button type="submit">Submit</button>
      </div>
    </form>
  );
  return (
    <div>
      {isToggleable ? (
        <>
          <button onClick={handleToggle}>
            {toggleShow ? "Cerrar formulario" : "Nuevo Negocio"}
          </button>
          {toggleShow && renderForm()}
        </>
      ) : (
        renderForm()
      )}
    </div>
  );
}
export default NewStoreForm;
