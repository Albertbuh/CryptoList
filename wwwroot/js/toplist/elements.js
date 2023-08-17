import { toFixed, insert } from "../general.js";

function compareValues(oldValue, newValue) {
  let delta = newValue - oldValue;
  let result;
  if (Math.abs(delta) < 0.01)
    //item.price === price
    result = 0;
  else if (delta < 0) {
    //item.price < price
    result = -1;
  } else if (delta > 0) {
    //item.price > price
    result = 1;
  }
  return result;
}  

function createChangeElement(chg) {
  let span = document.createElement("span");
  span.classList.add("change");
  if (chg < 0) span.classList.add("lower-color");
  else if (chg > 0) span.classList.add("higher-color");
  span.insertAdjacentHTML("beforeend", `&percnt; ${chg.toFixed(2)}`);
  return span;
}

function checkChangeCell(cell, chg) {
  let oldPcnt = cell.textContent.match(/[+-]?\d+(\.\d+)?/g)[0];
  let newPcnt = chg.toFixed(2);
  let isEqual = compareValues(oldPcnt, newPcnt);
  if(isEqual != 0) {
    cell = insert(cell, `&percnt; ${newPcnt}`);
    cell.classList.remove("lower-color", "higher-color");
    if(newPcnt > 0) cell.classList.add("higher-color");
    else if(newPcnt < 0) cell.classList.add("lower-color");
  }
}

function createCoinElement(id, name, logoUrl) {
  let div = document.createElement("div");
  div.classList.add("coin");

  let img = document.createElement("img");
  img.setAttribute("src", logoUrl);

  let right = document.createElement("div");
  right.classList.add("right");
  right.insertAdjacentHTML(
    "beforeend",
    `<p>${name}</p><span class="id">${id}</span>`
  );

  div.prepend(img);
  div.append(right);
  div.onclick = () => (window.location.href = `/coin?id=${id}`);
  return div;
}

function createPriceElement(price) {
  let span = document.createElement("span");
  span.classList.add("price");

  span.insertAdjacentHTML("beforeend", `&dollar; ${toFixed(price)}`);
  return span;
}

function checkPriceCell(cell, price) {
  let oldPrice = cell.textContent.match(/[+-]?\d+(\.\d+)?/g)[0];
  let newPrice = toFixed(price);
  let isEqual = compareValues(oldPrice, newPrice);
  if (isEqual != 0) {
    if (isEqual < 0) {
      cell.classList.add("lower-color");
    } else if (isEqual > 0) {
      cell.classList.add("higher-color");
    }
    setTimeout(() => {
      cell.classList.remove("higher-color", "lower-color");
    }, 1500);
    cell = insert(cell, `&dollar; ${newPrice}`);
  }
}

function createVolumeElement(name, value) {
  let span = document.createElement("span");
  span.classList.add("volume");

  let result = "";
  if (value < 0.00000001) result = "-";
  else result = `&dollar; ${HighValueToVolume(value)}`;

  span.insertAdjacentHTML("beforeend", result);
  return span;
}

function checkVolumeCell(cell, volume) {
  let old = cell.textContent;
  let newValue = HighValueToVolume(volume);
  if (old != newValue) {
    if (volume < 0.00000001) cell = insert(cell, "-");
    else cell = insert(cell, newValue);
  }
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

export {
  createChangeElement,
  createCoinElement,
  createPriceElement,
  createVolumeElement,
  checkChangeCell,
  checkPriceCell,
  checkVolumeCell,
};
