function createCoinElement(id, name, logoUrl) {
  let div = document.createElement("div");
  div.classList.add("coin");

  let img = document.createElement("img");
  img.setAttribute("src", logoUrl);

  let right = document.createElement("div");
  right.classList.add("right");
  right.insertAdjacentHTML(
    "beforeend",
    `<p>${name}</p><p class="id">${id}</p>`
  );

  div.prepend(img);
  div.append(right);
  return div;
}
