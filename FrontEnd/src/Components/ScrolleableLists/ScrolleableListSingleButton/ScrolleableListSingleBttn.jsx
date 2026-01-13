//tomado de: https://dev.to/forksofpower/autoscrolling-lists-with-react-hooks-10o7
import "./ScrolleableListSingleBttn.css";
import { useState } from "react";
/**
 * Renders a scrollable list of items with a customizable header.
 * Meant for single action items
 * 
 * @param {Array<Object>} items - Array of product objects returned by the API.
 * @param {Array<string>} categories - Array of column headers to display above the list.
 * @param {Array<Function>|null} optionsfunc - Optional callback to trigger a action on item interaction.
 * @returns {JSX.Element} A rendered list component with header and interactive items.
 */
function ScrolleableListSingleBttn({ items =[], categories= [] , optionsfunc}) {
  //optionsfunct va a recibir las opciones de los botones.

  
  return (
    <div class="autoscroll-container">
      <div className="table-head">
        {categories &&
          categories.map((category) => (
            <div className="table-head-item">{`${category}`}</div> 
          ))}
      </div>
      <div className="scroll-list"> 
        {items &&
          items.map((item, index) => (
            <div class="item" onClick={() => optionsfunc(item)}>
              <span>
                  <div class="inner-text">
                    <p key={index}>{`${index + 1}. ${item.Nombre}`}</p>
                  </div>
              </span>
            </div>
          ))}
      </div>
    </div>
  );
}
export default ScrolleableListSingleBttn;
