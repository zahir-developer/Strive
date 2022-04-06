using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace Greeter.Services.Printer
{
    public class ConnectionManager : DiscoveryHandler
    {
        public Connection Connection;
        public DiscoveredPrinter Printer;
        public static string IpAddress { get; set; }
        //public string ipAddress = "10.254.2.157";
        public int port = 6101;
        IBaseView baseView; 

        public ConnectionManager(IBaseView baseView)
        {
            this.baseView = baseView;
        }

        //public IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public void CreateConnection()
        {
            try
            {
                Connection = new TcpConnection(IpAddress, port);
                Connection.Open();
            }
            catch (ConnectionException e)
            {
                //_userDialog.Alert(e.Message);
                baseView.ShowAlertMsg(e.Message);
                Debug.WriteLine("exception in connection " + e.Message);
            }
        }

        public void printImage(string imageData)
        {
            try
            {
                if (PostPrintCheckPrinterStatus(Connection))
                {
                    //string zplData = "^XA^FO20,20^A0N,25,25^FDThis is a ZPL test.^FS^XZ";
                    Connection.Write(Encoding.ASCII.GetBytes(imageData));

                    //ZebraPrinter printer = ZebraPrinterFactory.GetInstance(Connection);
                    //printer.PrintImage(imageLocation, 0, 0);
                }
            }
            catch (ConnectionException e)
            {
                //_userDialog.Alert(e.Message);
                baseView.ShowAlertMsg(e.Message);
                Debug.WriteLine("exception in printImage" + e.Message);
            }
        }

        //private void StartBluetoothDiscovery()
        //{
           
        //    //handler.DiscoveryError += DiscoveryHandler_OnDiscoveryError;
        //    //handler.OnDiscoveryFinished += DiscoveryHandler_OnDiscoveryFinished;
        //    //handler.OnFoundPrinter += DiscoveryHandler_OnFoundPrinter;

        //    //For iOS
        //    BluetoothDiscoverer.FindPrinters(DiscoveryHandler handler);
        //}

       
        public  bool PostPrintCheckPrinterStatus(Connection connection)
        {
            ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, connection);
            try
            {
                PrinterStatus printerStatus = printer.GetCurrentStatus();

                // loop while printing until print is complete or there is an error
                while ((printerStatus.numberOfFormatsInReceiveBuffer > 0) && (printerStatus.isReadyToPrint))
                {
                    Thread.Sleep(500);
                    printerStatus = printer.GetCurrentStatus();
                    
                }
                if (printerStatus.isReadyToPrint)
                {
                    Debug.WriteLine("Ready To Print");
                    return true;
                }
                else if (printerStatus.isPaused)
                {
                    Debug.WriteLine("Cannot Print because the printer is paused.");
                }
                else if (printerStatus.isHeadOpen)
                {
                    Debug.WriteLine("Cannot Print because the printer head is open.");
                }
                else if (printerStatus.isPaperOut)
                {
                    Debug.WriteLine("Cannot Print because the paper is out.");
                }
                else
                {
                    Debug.WriteLine("Cannot Print.");
                }
            }
            catch (ConnectionException e)
            {
                //Console.WriteLine($"Error getting status from printer: {e.Message}");
                baseView.ShowAlertMsg($"Error getting status from printer: {e.Message}");
            }


            
            return false;
        }

        public void FoundPrinter(DiscoveredPrinter printer)
        {
            Debug.WriteLine("Printer found " + printer.Address);

            Printer = printer;
        }

        public void DiscoveryFinished()
        {
            Debug.WriteLine("DiscoveryFinished");
        }

        public void DiscoveryError(string message)
        {
            Debug.WriteLine("DiscoveryError");
        }
    }
}
