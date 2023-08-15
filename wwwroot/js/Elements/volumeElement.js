function createVolumeElement(name, value) {
  let span = document.createElement("span");
  span.classList.add("volume");

  let result = "";
  if (value < 0.00000001) result = "-";
  else result = `&dollar; ${HighValueToVolume(value)}`;

  span.insertAdjacentHTML("beforeend", result);
  return span;
}

function HighValueToVolume(value) {
  const thousand = 1000;
  const million = thousand * 1000;
  const billion = million * 1000;
  let size = "";

  if (value > billion) {
    value /= billion;
    size = "B";
  } else if (value > million) {
    value /= million;
    size = "M";
  } else if (value > thousand) {
    value /= thousand;
    size = "K";
  }
  return `${value.toFixed(2)} ${size}`;
}