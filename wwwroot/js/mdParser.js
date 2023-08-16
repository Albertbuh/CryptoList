let getHeader = (text, ind = 2) => {
  let h = document.createElement(`h${ind}`);
  h.insertAdjacentHTML("beforeend", text);
  return h;
};
let getParagraph = (text) => {
  let p = document.createElement(`p`);
  text = text.replace(/\\n/, "<br>");
  text = text.replace(/\*\s/, "&bull; ");
  let links = text.matchAll(/\[.*\]\s*\(.*\)/g);
  for(let link of links)
  {
    text = text.replace(link, getLinkAsString(link));
  }

  p.insertAdjacentHTML("beforeend", text);
  return p;
}
let getLinkAsString = (mdLink) => {
  // let a = document.createElement("a");
  let str = String(mdLink);
  let value = str.match(/\[.*\]/)[0];
  value = value.replace("[", "").replace("]", "");
  let src = str.match(/\(.*\)/)[0];
  src = src.replace("(", "").replace(")", "");
  // a.setAttribute("href", src);
  // a.insertAdjacentHTML("beforeend", value);
  return ` <a href=${src}>${value.trim()}</a>`;
};

function createMdSection(text)
{
  let section = document.createElement("section");
  section.classList.add("md-text");
  let textParts = text.split("\n\n");
  for(let part of textParts) {
    let el;
    if(part.startsWith("##")) section.append(getHeader(part.slice(2)));
    else section.append(getParagraph(part));
  }
  return section;
}