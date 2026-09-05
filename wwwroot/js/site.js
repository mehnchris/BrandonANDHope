document.querySelectorAll('a[href^="#"]').forEach((link) => {
  link.addEventListener("click", () => {
    const navigation = document.querySelector(".navbar-collapse.show");
    if (!navigation || !window.bootstrap) {
      return;
    }

    window.bootstrap.Collapse.getOrCreateInstance(navigation).hide();
  });
});
