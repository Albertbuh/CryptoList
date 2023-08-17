export default function createChangeElement(chg) {
  let span = document.createElement("span");
  span.classList.add("change");
  if (chg < 0) span.classList.add("lower-color");
  else if (chg > 0) span.classList.add("higher-color");
  span.insertAdjacentHTML("beforeend", `&percnt; ${chg.toFixed(2)}`);
  return span;
}