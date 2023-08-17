import {toFixed} from "../toFixed.js";
import {getLocation, postLocation} from "../location.js";
import {getCookie, setCookie} from "../cookie.js";
import { createTableBody, createTableHeader } from "./createTable.js";
import { isMobile } from "./mobileCheck.js";
import { HighValueToVolume } from "./Elements/volumeElement.js";

function setGetUrl() {
  let url = "";
  if (isMobile()) url = "/crypt/mobile";
  else url = "/crypt";
  console.log(url);
  return url;
}


async function GetToplist(url) {
  const response = await fetch(url, {
    method: "GET",
    headers: { Accept: "application/json" },
  });
  if (response.ok) {
    const toplist = await response.json();
    return toplist;
  }
}      

function insert(elem, value, position = "beforeend") {
  elem.innerHTML = "";
  elem.insertAdjacentHTML(position, value);
  return elem;
}

function checkValue(oldValue, newValue) {
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

function checkTable(collection) {
  let rows = document.querySelector("tbody").rows;
  let rowsLength = rows.length;
  let rowIndex = 0;
  collection.forEach((item) => {
    let id = rows[rowIndex].querySelector(".id").textContent;
    if (item.id != id) {
      rows[rowIndex].replaceWith(createTableRow(item));
      console.log(`Row ${id} recreated to ${item.id}`);
    } else {
      checkPriceCell(rows[rowIndex].querySelector(".price"), item.price);
      checkVolumeCell(rows[rowIndex].cells[2], item.direct);
      checkVolumeCell(rows[rowIndex].cells[3], item.total);
      checkVolumeCell(rows[rowIndex].cells[4], item.topTier);
      checkVolumeCell(rows[rowIndex].cells[5], item.marketCap);
      checkChangeCell(rows[rowIndex].querySelector(".change"), item.change);
    }

    rowIndex++;
  });
}

function checkVolumeCell(cell, volume) {
  let old = cell.textContent;
  let newValue = HighValueToVolume(volume);
  if (old != newValue) {
    if (volume < 0.00000001) cell = insert(cell, "-");
    else cell = insert(cell, newValue);
  }
}

function checkChangeCell(cell, chg) {
  let oldPcnt = cell.textContent.match(/[+-]?\d+(\.\d+)?/g)[0];
  let newPcnt = chg.toFixed(2);
  let isEqual = checkValue(oldPcnt, newPcnt);
  if(isEqual != 0) {
    cell = insert(cell, `&percnt; ${newPcnt}`);
    cell.classList.remove("lower-color", "higher-color");
    if(newPcnt > 0) cell.classList.add("higher-color");
    else if(newPcnt < 0) cell.classList.add("lower-color");
  }
}



function checkPriceCell(cell, price) {
  let oldPrice = cell.textContent.match(/[+-]?\d+(\.\d+)?/g)[0];
  let newPrice = toFixed(price);
  let isEqual = checkValue(oldPrice, newPrice);
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

async function start() {
  createTableHeader(
    "Coin",
    "Price",
    "Direct Vol",
    "Total Vol",
    "Top Tier Vol",
    "Market Cap",
    "&percnt;  Chg"
  );
  let toplist = await GetToplist(setGetUrl());
  createTableBody(toplist);

  // while(true)
  // {
  //   let result = await GetToplist(setGetUrl());
  //   checkTable(result);
  // }
  setInterval(async () => {
    let result = await GetToplist(setGetUrl());
    checkTable(result);
  }, 3500);
}

if(!getCookie("currency")) {
    getLocation().then((data) => {
      setCookie("currency", data.country.currency, {'max-age': 3600 * 24});
      postLocation(data);
    })
}   
start();