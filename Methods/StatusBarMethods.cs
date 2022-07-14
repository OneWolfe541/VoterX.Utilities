using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Utilities.BasePageDefinitions;

namespace VoterX.Utilities.Methods
{
    public static class StatusBarExtensions
    {
        //private static MasterBasePage MAINWINDOW = ((App)Application.Current).mainpage;

        public static void SetPageHeader(this MasterBasePage MAINWINDOW, string value)
        {
            if (value != null)
            {
                MAINWINDOW.PageHeaderName = value.ToUpper();
            }
        }

        public static string GetPageHeader(this MasterBasePage MAINWINDOW)
        {
            return MAINWINDOW.PageHeaderName;
        }

        // CONVERTED TO STATUS BAR USER CONTROL VIA StatusBarControl.xaml.cs
        // ~\VoterX.Utilities\UserControls\StatusBarControl.xaml
        //// Display a text message on the status bar left
        //public static void SetStatusLeft(this MasterBasePage MAINWINDOW, string value)
        //{
        //    MAINWINDOW.ApplicationStatusLeft = value;
        //}

        //public static string GetStatusLeft(this MasterBasePage MAINWINDOW)
        //{
        //    return MAINWINDOW.ApplicationStatusLeft;
        //}

        //// Display a text message on the status bar right
        //public static void SetStatusRight(this MasterBasePage MAINWINDOW, string value)
        //{
        //    MAINWINDOW.ApplicationStatusRight = value;
        //}

        //public static string GetStatusRight(this MasterBasePage MAINWINDOW)
        //{
        //    return MAINWINDOW.ApplicationStatusRight;
        //}

        //// Display a text message on the status bar center
        //public static void SetStatusCenter(this MasterBasePage MAINWINDOW, string value)
        //{
        //    MAINWINDOW.ApplicationStatusCenter = value;
        //}

        //public static string GetStatusCenter(this MasterBasePage MAINWINDOW)
        //{
        //    return MAINWINDOW.ApplicationStatusCenter;
        //}

        //public static void HidePrinterStatus(this MasterBasePage MAINWINDOW)
        //{
        //    MAINWINDOW.ApplicationPrinterStatusOK = System.Windows.Visibility.Collapsed;
        //    MAINWINDOW.ApplicationPrinterStatusBad = System.Windows.Visibility.Collapsed;
        //}

        //public static void SetPrinterStatus(this MasterBasePage MAINWINDOW, bool status)
        //{
        //    if (status == true)
        //    {
        //        MAINWINDOW.ApplicationPrinterStatusOK = System.Windows.Visibility.Visible;
        //        MAINWINDOW.ApplicationPrinterStatusBad = System.Windows.Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        MAINWINDOW.ApplicationPrinterStatusOK = System.Windows.Visibility.Collapsed;
        //        MAINWINDOW.ApplicationPrinterStatusBad = System.Windows.Visibility.Visible;
        //    }
        //}

        //public static bool GetPrinterStatus(this MasterBasePage MAINWINDOW)
        //{
        //    if(MAINWINDOW.ApplicationPrinterStatusOK == System.Windows.Visibility.Visible)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static void HideSignaturePadStatus(this MasterBasePage MAINWINDOW)
        //{
        //    MAINWINDOW.ApplicationSignatureStatusOK = System.Windows.Visibility.Collapsed;
        //    MAINWINDOW.ApplicationSignatureStatusBad = System.Windows.Visibility.Collapsed;
        //}

        //public static void SetSignaturePadStatus(this MasterBasePage MAINWINDOW, bool status)
        //{
        //    if (status == true)
        //    {
        //        MAINWINDOW.ApplicationSignatureStatusOK = System.Windows.Visibility.Visible;
        //        MAINWINDOW.ApplicationSignatureStatusBad = System.Windows.Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        MAINWINDOW.ApplicationSignatureStatusOK = System.Windows.Visibility.Collapsed;
        //        MAINWINDOW.ApplicationSignatureStatusBad = System.Windows.Visibility.Visible;
        //    }
        //}

        //public static bool GetSignaturePadStatus(this MasterBasePage MAINWINDOW)
        //{
        //    if (MAINWINDOW.ApplicationSignatureStatusOK == System.Windows.Visibility.Visible)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
