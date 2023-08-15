function createTableHeader(...params) {
  let thead = document.createElement("thead");
  let tr = document.createElement("tr");
  thead.append(tr);
  for (let p of params) {
    tr.insertAdjacentHTML("beforeend", `<th>${p}</th>`);
  }
  document.querySelector("table").append(thead);
}

function createTableBody(collection) {
  let tbody = document.createElement("tbody");

  collection.forEach((item) => {
    tbody.append(createTableRow(item));
  });
  document.querySelector("table").append(tbody);
}

function createTableRow(coin) {
  let tr = document.createElement("tr");
  let tdCoin = document.createElement("td");
  tdCoin.classList.add("td-coin");
  tdCoin.append(createCoinElement(coin.id, coin.name, coin.iconUrl));
  tr.append(tdCoin);

  tdPrice = document.createElement("td");
  tdPrice.append(createPriceElement(coin.price));
  tr.append(tdPrice);

  let volumes = [coin.direct, coin.total, coin.topTier, coin.marketCap];
  for (let volume of volumes) {
    let tdVolume = document.createElement("td");
    tdVolume.append(createVolumeElement(`${volume}`, volume));
    tr.append(tdVolume);
  }

  let tdChange = document.createElement("td");
  tdChange.append(createChangeElement(coin.change));
  tr.append(tdChange);

  return tr;
}
