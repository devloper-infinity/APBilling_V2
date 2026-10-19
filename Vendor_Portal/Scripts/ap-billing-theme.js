(function () {
  "use strict";

  function markActiveMenu() {
    var path = window.location.pathname.toLowerCase();
    document.querySelectorAll(".main-header a[href]").forEach(function (link) {
      var href = (link.getAttribute("href") || "").split("?")[0].toLowerCase();
      if (href && href !== "#" && path.endsWith(href.replace(/^\.\.\//, "").replace(/^\.\//, ""))) {
        link.classList.add("active");
      }
    });
  }

  function makeTablesResponsive() {
    document.querySelectorAll(".card-body > table.table").forEach(function (table) {
      if (table.parentElement && !table.parentElement.classList.contains("table-responsive")) {
        var wrapper = document.createElement("div");
        wrapper.className = "table-responsive";
        table.parentNode.insertBefore(wrapper, table);
        wrapper.appendChild(table);
      }
    });
  }

  function standardizePageHeader() {
    var scope = document.querySelector(".content-wrapper") || document;
    var header = scope.querySelector(".card-header-custom") || scope.querySelector(".content-header .callout");
    if (!header) { return; }
    header.classList.add("ap-page-header");
    var title = header.querySelector("h1, h2, h3, h4, h5, h6");
    if (title) {
      title.classList.add("ap-page-title");
      var icon = title.querySelector(".fa-copy");
      if (icon) { icon.className = "fas fa-file-invoice ap-page-title-icon"; }
    }
  }

  function standardizeLoaders() {
    var loaders = document.querySelectorAll("#load1.loading, #divLoader.loader-overlay, #divLoader.table-loader");
    loaders.forEach(function (loader) {
      loader.classList.add("ap-loading-overlay");
      loader.setAttribute("role", "status");
      loader.setAttribute("aria-live", "polite");
      loader.setAttribute("aria-label", "Loading");
      loader.innerHTML = '<div class="ap-loader-panel"><span class="ap-spinner" aria-hidden="true"></span><strong>Please wait...</strong><small>Your request is being processed.</small></div>';
    });
  }

  document.addEventListener("DOMContentLoaded", function () {
    markActiveMenu();
    makeTablesResponsive();
    standardizePageHeader();
    standardizeLoaders();
  });
}());
