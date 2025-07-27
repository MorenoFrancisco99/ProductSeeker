import { getEmptyProduct } from "../DataTemplates/datatemplates";

/**
 *  Takes and event.target from a form and return a JSON of product
 * @param {EventTarget} rawForm event.target field of a submited form
 */
export function ParseProductFormToJson(rawForm) {
  try {
    const formData = new FormData(rawForm);
    //Maybe unnecesary but the alernatives are more verbose
    const raw = Object.fromEntries(formData.entries());

    const productVar = getEmptyProduct();

    productVar.Brand = raw.productBrand;
    productVar.Name = raw.productName;
    productVar.Size = raw.productAmount;
    productVar.UnitType = raw.unitType;
    productVar.SubUnitAmout = isAgglomerate ? raw.subUnitAmount : null;
    productVar.SubUnitSize = isAgglomerate ? raw.subUnitSize : null;
    productVar.SubUnitType = isAgglomerate ? raw.subUnitType : null;
    productVar.SubUnitMode = isAgglomerate ? raw.subUnitMode : null;
    productVar.Const = raw.productCost;
    productVar.ExtraInfo = raw.ExtraInfo;
    productVar.productImg = rawForm.productImg.files[0] || null;

    console.log(productVar)
    return productVar;

  } catch (err) {
    throw console.error(err);
  }

}
