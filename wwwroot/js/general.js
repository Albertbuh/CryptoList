export function toFixed(value) {
  if (value - 1 > 0.000000001) return value.toFixed(2);
  let copyValue = value;
  let f = 2;
  while (value < 1) {
    value *= 10;
    f++;
  }
  return copyValue.toFixed(f);
}

export function insert(elem, value, position = "beforeend") {
  elem.innerHTML = "";
  elem.insertAdjacentHTML(position, value);
  return elem;
}
