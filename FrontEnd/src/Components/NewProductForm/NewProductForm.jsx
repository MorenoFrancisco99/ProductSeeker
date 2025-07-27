import { useState } from "react";

/**
 * Form for creating a new product
 *
 * @param {Function|null} onSubmitFunc -  Callback function
 * @param {boolean} isToggleable - If true, displays a button to expand the form
 * @returns Raw form data
 */
function NewProductForm({ onSubmitFunc = null, isToggleable = false }) {
  const [toggleShow, setToggleShow] = useState(false);
  const [isChecked, setIsChecked] = useState(true);
  const [unitType, setUnitType] = useState("Unit/s");
  const [ImgPreview, setImgPreview] = useState();

  const handleToggle = () => {
    setToggleShow((prev) => !prev);
  };

  const handleCheckboxChange = () => {
    setIsChecked((prev) => !prev);
  };
  const handleUnitTypeChange = (e) => {
    setUnitType(e.target.value);
    setIsChecked(false);
  };

  const handleImageChange = (e) => {
    const img = e.target.files[0];
    if (!img) return;

    setImgPreview(URL.createObjectURL(img));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    
    onSubmitFunc(e);
  };

  if (!onSubmitFunc) {
    return <p>Missing callback function</p>;
  }
  const renderForm = () => (
    <form onSubmit={handleSubmit}>
      <label htmlFor="productBrand">Marca y Submarca</label>
      <input
        type="text"
        name="productBrand"
        id="productBrand"
        placeholder="Ej. Criollitas Original"
        required
      />
      <br />
      <br />
      <label htmlFor="productName">Nombre del producto</label>
      <input
        type="text"
        name="productName"
        id="productName"
        placeholder="Ej. Galletitas de agua"
        required
      />
      <br />
      <br />
      <label htmlFor="productAmount">Tamaño</label>
      <input
        type="number"
        name="productAmount"
        id="productAmount"
        placeholder="Ej. 3"
        required
      />
      <select
        name="unitType"
        id="unitType"
        value={unitType}
        onChange={handleUnitTypeChange}
      >
        <option value="Unit/s">Unidad/des</option>
        <option value="Ml">Mililitros</option>
        <option value="l">Litros</option>
        <option value="Kg">Kilogramos</option>
        <option value="g">Gramos</option>
      </select>
      {unitType === "Unit/s" && ( //Inicio de seleccion de info de subunidades
        <>
          <label htmlFor="isAgglomerate"> ¿Tiene subunidades?</label>
          <input
            type="checkbox"
            name="isAgglomerate"
            id="isAgglomerate"
            checked={isChecked}
            onChange={handleCheckboxChange}
          />
        </>
      )}
      {unitType === "Unit/s" && isChecked && (
        <>
          <br /> <br />
          <fieldset>
            <label htmlFor="subUnitAmount">Cantidad de subunidades</label>
            <input
              type="number"
              name="subUnitAmount"
              id="subUnitAmount"
              placeholder="Ej. 3"
              required={isChecked === true}
            />
            <br />
            <br />
            <label htmlFor="subUnitSize">Tamaño</label>
            <input
              type="number"
              name="subUnitSize"
              id="subUnitSize"
              placeholder="Ej. 300"
              required={isChecked === true}
            />
            <select name="subUnitType" id="subUnitType">
              <option value="g">Gramos</option>
              <option value="Ml">Mililitros</option>
              <option value="l">Litros</option>
              <option value="Kg">Kilogramos</option>
              <option value="Unit/s">Unidad/des</option>
            </select>
            <div>
              <label htmlFor="C/U">C/U</label>
              <input type="radio" id="C/U" name="subUnitMode" value="C/U" />
              <label htmlFor="TOTAL">Total</label>
              <input
                type="radio"
                id="TOTAL"
                name="subUnitMode"
                value="TOTAL"
                defaultChecked
              />
            </div>
          </fieldset>
        </>
        //Final de seleccion de subunidades
      )}
      <br /> <br />
      <label htmlFor="productCost">Precio</label>
      <input type="number" id="productCost" name="productCost" required />
      <br /> <br />
      <label for="productImg">Elegir Imagen:</label>
      <input
        type="file"
        id="productImg"
        name="productImg"
        accept="image/png, image/jpeg"
        onChange={handleImageChange}
      />
      {ImgPreview && <img src={ImgPreview} alt="IMG"></img>}
      <br /> <br />
      <label htmlFor="ExtraInfo">Informacion Adicional</label>
      <textarea name="ExtraInfo" id="ExtraInfo"></textarea>
      <br /> <br />
      <button type="submit">Submit</button>
      <br /> <br />
    </form>
  );

  return (
    <div>
      {isToggleable ? (
        <>
          <button onClick={handleToggle}>
            {toggleShow ? "Cerrar formulario" : "Nuevo Producto"}
          </button>
          {toggleShow && renderForm()}
        </>
      ) : (
        renderForm()
      )}
    </div>
  );
}

export default NewProductForm;
