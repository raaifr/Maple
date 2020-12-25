Imports System.Web.Optimization
Imports System.Diagnostics

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        'Dim objErr As Exception = Server.GetLastError().GetBaseException()
        'Dim err As String = "Error Caught in Application_Error event" &
        '                        System.Environment.NewLine &
        '                        "Error in: " & Request.Url.ToString() &
        '                        System.Environment.NewLine &
        '                        "Error Message: " & objErr.Message.ToString() &
        '                        System.Environment.NewLine &
        '                        "Stack Trace:" & objErr.StackTrace.ToString()
        'EventLog.WriteEntry("Maple", err, EventLogEntryType.Error)
        'Server.ClearError() 'use this line if using Application_Error event
    End Sub
End Class
