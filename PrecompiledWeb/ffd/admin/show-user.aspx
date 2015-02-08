<%@ Page Language="C#" %>
<%@ Import namespace="System.Security.Principal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html  >
<head id="Head1" runat="server">
    <title>ASP.NET Example</title>
<script runat="server">

public bool WillFlowAcrossNetwork(WindowsIdentity w)
{
  foreach (SecurityIdentifier s in w.Groups)
  {
    if (s.IsWellKnown(WellKnownSidType.InteractiveSid)) { return true; }
    if (s.IsWellKnown(WellKnownSidType.BatchSid))       { return true; }
    if (s.IsWellKnown(WellKnownSidType.ServiceSid))     { return true; }
  }

  return false;
}

</script>
</head>
<body>
<%
  WindowsIdentity current = WindowsIdentity.GetCurrent();
  Response.Write("User : <b>" + current.Name + "</b><br />" + "Flowable: <b>" + WillFlowAcrossNetwork(current) + "</b><br />");
%>
</body>
</html>