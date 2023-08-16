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
  div.onclick = () => window.location.href = `/coin?id=${id}`;
  return div;
}
