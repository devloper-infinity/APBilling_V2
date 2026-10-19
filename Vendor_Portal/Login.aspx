<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vendor_Portal.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Infinity IPS | AP Billing</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="dist/css/adminlte.min.css" />
    <link rel="stylesheet" href="dist/css/ap-billing-theme.css" />
</head>
<body class="ap-login-page">
    <main class="ap-login-shell">
        <section class="ap-login-brand" aria-label="AP Billing overview">
            <div class="ap-brand-mark"><i class="fas fa-file-invoice-dollar"></i></div>
            <p class="ap-eyebrow">Infinity IPS</p>
            <h1>Accounts payable,<br />made effortless.</h1>
            <p class="ap-brand-copy">A secure workspace for vendor invoices, reconciliation, approvals, and reporting.</p>
            <div class="ap-feature-grid">
                <div><i class="fas fa-check-circle"></i><span><strong>Streamlined billing</strong><small>Manage invoices in one place</small></span></div>
                <div><i class="fas fa-chart-line"></i><span><strong>Clear reporting</strong><small>See the details that matter</small></span></div>
                <div><i class="fas fa-shield-alt"></i><span><strong>Secure access</strong><small>Protected with MFA in production</small></span></div>
            </div>
        </section>
        <section class="ap-login-panel">
            <div class="ap-login-card">
                <img class="ap-login-logo" src="images/logo.png" alt="Infinity IPS" />
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true" />
                    <div id="dvError" runat="server" role="alert"></div>
                    <asp:Panel ID="pnlCredentials" runat="server">
                        <div class="ap-login-heading"><h2>Welcome back</h2><p>Sign in to continue to AP Billing.</p></div>
                        <div class="form-group"><label for="login_username">Username</label><div class="input-group ap-input-group"><div class="input-group-prepend"><span class="input-group-text"><i class="fas fa-user"></i></span></div><input id="login_username" name="login_username" class="form-control" placeholder="Enter username" autocomplete="username" style="text-transform:uppercase;" /></div></div>
                        <div class="form-group"><label for="login_password">Password</label><div class="input-group ap-input-group"><div class="input-group-prepend"><span class="input-group-text"><i class="fas fa-lock"></i></span></div><input id="login_password" name="login_password" type="password" class="form-control" placeholder="Enter password" autocomplete="current-password" /><div class="input-group-append"><button class="input-group-text ap-password-toggle" type="button" aria-label="Show password"><i class="fas fa-eye"></i></button></div></div></div>
                        <div class="d-flex justify-content-between align-items-center ap-login-options"><label class="ap-check"><input id="chkRemember" runat="server" type="checkbox" /> <span>Remember me</span></label></div>
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block ap-login-button" Text="Sign in" OnClick="btnLogin_Click" OnClientClick="return APBilling.validateLogin();" />
                    </asp:Panel>
                    <asp:Panel ID="pnlMfa" runat="server" Visible="false" DefaultButton="btnVerifyMfa">
                        <div class="ap-mfa-icon"><i class="fas fa-mobile-alt"></i></div>
                        <div class="ap-login-heading text-center"><h2>Verification required</h2><p>Enter the 6-digit code from your authenticator app.</p></div>
                        <div class="form-group"><label for="mfa_code">Authentication code</label><input id="mfa_code" name="mfa_code" class="form-control ap-otp-input" inputmode="numeric" autocomplete="one-time-code" maxlength="6" placeholder="000000" /></div>
                        <asp:Button ID="btnVerifyMfa" runat="server" CssClass="btn btn-primary btn-block ap-login-button" Text="Verify and continue" OnClick="btnVerifyMfa_Click" OnClientClick="return APBilling.validateMfa();" />
                        <asp:Button ID="btnCancelMfa" runat="server" CssClass="btn btn-link btn-block" Text="Use another account" OnClick="btnCancelMfa_Click" CausesValidation="false" />
                    </asp:Panel>
                    <p class="ap-login-footer">&copy; 2026 Infinity IPS. Secure AP Billing Portal.</p>
                </form>
            </div>
        </section>
    </main>
    <script src="Scripts/ap-billing-login.js"></script>
</body>
</html>
