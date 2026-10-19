(function (window, document) {
  "use strict";
  var api = window.APBilling = window.APBilling || {};
  function showMessage(message) { var box = document.getElementById("dvError"); if (!box) return; box.className = "alert alert-danger"; box.textContent = message; box.style.display = "block"; }
  api.validateLogin = function () { var user = document.getElementById("login_username"), password = document.getElementById("login_password"); if (!user || !user.value.trim()) { showMessage("Please enter your username."); user.focus(); return false; } if (!password || !password.value) { showMessage("Please enter your password."); password.focus(); return false; } return true; };
  api.validateMfa = function () { var code = document.getElementById("mfa_code"); if (!code || !/^\d{6}$/.test(code.value.trim())) { showMessage("Enter a valid 6-digit authentication code."); if (code) code.focus(); return false; } return true; };
  document.addEventListener("DOMContentLoaded", function () { var toggle = document.querySelector(".ap-password-toggle"), password = document.getElementById("login_password"); if (toggle && password) toggle.addEventListener("click", function () { var reveal = password.type === "password"; password.type = reveal ? "text" : "password"; toggle.querySelector("i").className = reveal ? "fas fa-eye-slash" : "fas fa-eye"; toggle.setAttribute("aria-label", reveal ? "Hide password" : "Show password"); }); });
}(window, document));
