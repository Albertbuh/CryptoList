import { getLocation, postLocation } from "./location.js";
import { getCookie, setCookie } from "./cookie.js";
import createMdSection from "./mdParser.js";
import { toFixed } from "./general.js";

async function getCoin(id) {
  const response = await fetch(`/coins/${id}`, {
    method: "GET",
    headers: { Accept: "application/json" },
  });
  if (response.ok) {
    const coinInfo = await response.json();
    fillCoin(coinInfo);
  } else {
    console.log("Error in getting response");
  }
}

async function fillCoin(info) {
  let container = document.querySelector(".coin-container");
  container.querySelector("img").setAttribute("src", info.logoUrl);
  document.getElementById("name").textContent = info.name;
  document
    .getElementById("usd")
    .insertAdjacentText("beforeend", `USD: ${toFixed(info.usdPrice)}`);
  document
    .getElementById("eur")
    .insertAdjacentText("beforeend", `EUR: ${toFixed(info.eurPrice)}`);

  let currency = getCookie("currency") ?? "JPY";
  document
    .getElementById("usr")
    .insertAdjacentText(
      "beforeend",
      `${currency}: ${toFixed(info.userCountryPrice)}`
    );

  let section = createMdSection(info.description);
  section.insertAdjacentHTML(
    "beforeend",
    `<p>More information: <a href='${info.websiteUrl}'>${info.websiteUrl}</a></p>`
  );
  container.append(section);
}

if (!getCookie("currency")) {
  getLocation().then((data) => {
    setCookie("currency", data.country.currency, { "max-age": 3600 * 24 });
    postLocation(data);
  });
}
const params = new Proxy(new URLSearchParams(window.location.search), {
  get: (searchParams, prop) => searchParams.get(prop),
});
getCoin(params.id);

window.addEventListener("scroll", () => {
  let offset =
    (window.pageYOffset / document.documentElement.scrollHeight) * 100;
  let btnUp = document.getElementById("btn-up");
  if (offset < 35) {
    btnUp.style.opacity = 0;
    btnUp.style.visibility = "hidden";
  } else {
    btnUp.style.opacity = 1;
    btnUp.style.visibility = "visible";
  }
});
