async function getLocation() {
  const response = await fetch(
    "https://api.geoapify.com/v1/ipinfo?&apiKey=89932f2b5f2c4614acab4f99e84377b8"
  )
    // .then((response) => response.json())
    // .then((data) => {
    //   return data; //postLocation(data);
    // });
  return await response.json();
}

async function getCurrency() {
  return await getLocation().country.currency;
}

async function postLocation(data) {
  const response = await fetch("/country", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json; charset=utf-8",
    },
    body: JSON.stringify(data),
  });
  if (response.ok) {
    console.log(await response.text());
  } else {
    console.log("ERROR");
  }
}

export {getLocation, postLocation, getCurrency}