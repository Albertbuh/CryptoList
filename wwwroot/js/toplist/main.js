import {getLocation, postLocation} from "../location.js";
import {getCookie, setCookie} from "../cookie.js";
import { createTableBody, createTableHeader, createTableRow } from "./createTable.js";
import { isMobile } from "../mobile.js";
import { checkChangeCell, checkPriceCell, checkVolumeCell } from "./elements.js";

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

function checkTable(collection) {
  let rows = document.querySelector("tbody").rows;
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