<html><body> <center>
<%
Set WSHShell = CreateObject("Wscript.Shell")
Set WSHProcess = WSHShell.Environment("Process")
ServerName = WSHProcess("COMPUTERNAME")

  Response.Write "<br><br><br> <font size=12><bold> Servidor: " & ServerName & "<br><br>"
  Response.Write "Data: " & Now() 

%>
</bold> </font></center></body></html>