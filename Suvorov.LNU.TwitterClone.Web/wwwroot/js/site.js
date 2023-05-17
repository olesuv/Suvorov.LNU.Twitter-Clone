function scrollToPost() {
  var postContent = document.getElementById("postTextContent");
  postContent.scrollIntoView({ behavior: "smooth", block: "start" });
}
