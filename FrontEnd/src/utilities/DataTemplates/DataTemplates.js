/**
 * @typedef {Object} Product
 * @property {string} Brand - Marca y subtipo del producto
 * @property {string} Name - Nombre general del producto
 * @property {string|number} Size - Cantidad principal
 * @property {string} UnitType - Unidad principal (Kg, L, Unit/s, etc.)
 * @property {?string|number} SubUnitAmout - Cantidad de subunidades (si aplica)
 * @property {?string|number} SubUnitSize - Tamaño de cada subunidad (si aplica)
 * @property {?string} SubUnitType - Unidad de subunidad (g, ml, etc.)
 * @property {?string} SubUnitMode - "C/U" o "TOTAL" (si aplica)
 * @property {string|number} Const - Costo o precio estimado
 * @property {string} ExtraInfo - Información adicional opcional
 * @property {File|null} img - Archivo de imagen cargado (o null)
 */

/**
 * Retorna una estructura vacía estándar de producto
 * @returns {Product}
 */
export function getEmptyProduct() {
  return {
    Brand: "",
    Name: "",
    Size: "",
    UnitType: "",
    SubUnitAmout: null,
    SubUnitSize: null,
    SubUnitType: null,
    SubUnitMode: null,
    Const: "",
    ExtraInfo: "",
    img: null,
  };
}
