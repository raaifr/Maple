Imports System.Web.Mvc

Namespace Controllers
    Public Class KioskController
        Inherits Controller

        ' GET: Kiosk
        Function Index() As ActionResult
            'TODO this is kiosk login page
            Return RedirectToAction("koisk")

        End Function

        Function authorize() As ActionResult

            Return View()
        End Function

        Function koisk() As ActionResult
            'TODO check creds
            Return View()
        End Function

    End Class
End Namespace