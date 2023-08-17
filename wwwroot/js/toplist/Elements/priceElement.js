import {toFixed} from "../../toFixed.js";
export default function createPriceElement(price) {
  let span = document.createElement("span");
  span.classList.add("price");

  span.insertAdjacentHTML("beforeend", `&dollar; ${toFixed(price)}`);
  return span;
}