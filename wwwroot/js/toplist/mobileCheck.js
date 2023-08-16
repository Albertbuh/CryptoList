function isMobile() {
  const regex =
    /Mobi|Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i;
  return regex.test(navigator.userAgent);
}
function setGetUrl() {
  let url = "";
  if (isMobile()) url = "/crypt/mobile";
  else url = "/crypt";
  console.log(url);
  return url;
}