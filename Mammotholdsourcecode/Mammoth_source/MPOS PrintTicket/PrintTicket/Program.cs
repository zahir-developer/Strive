using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using System.Drawing.Printing;


namespace PrintTicket
{
    class Program
    {
        static SqlDataReader dr;
        static void Main(string[] args)
        {


            string strConnString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string strLocationID = ConfigurationManager.AppSettings["LocationID"];
            string strPrinterName = ConfigurationManager.AppSettings["PrinterName"];
            //Console.WriteLine(value: "strLocationID: " + strLocationID);
            string strPrintID = "0";
            string strRecID = "0";
            string strReport = "Ticket";
 

          for (int n = 0; n < 43200; n++)
            {

                String nextN = Convert.ToString(n);
                using (SqlConnection con = new SqlConnection(strConnString))
                using (SqlCommand cmd = new SqlCommand())



                    try
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT top(1) PrintID, RecID, Report  FROM dbo.PrintTicket where (LocationID = @LocationID) Order by PrintID";
                        cmd.Parameters.AddWithValue(parameterName: "@LocationID", value: strLocationID);
                        con.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            strPrintID = dr[0].ToString().Trim();
                            strRecID = dr[1].ToString().Trim();
                            strReport = dr[2].ToString().Trim();
                            //     Console.WriteLine(value: "Found RecID:" + strRecID);

                            // Console.WriteLine(value: "Print RecID:" + strRecID);
                            System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
                            var printerSettings = new PrinterSettings();
                            if (strReport == "VehicalCopy")
                            {
                                printerSettings.PrinterName = strPrinterName;
                                string reportName = typeof(MPOSReportLibrary.VehicalCopy).AssemblyQualifiedName;
                                var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                                var typeReportSource = new Telerik.Reporting.TypeReportSource();
                                typeReportSource.TypeName = reportName;
                                typeReportSource.Parameters.Add(new Telerik.Reporting.Parameter(name: "LocationID", value: Convert.ToInt32(strLocationID)));
                                typeReportSource.Parameters.Add(new Telerik.Reporting.Parameter(name: "RecID", value: Convert.ToInt32(strRecID)));
                                reportProcessor.PrintReport(typeReportSource, printerSettings);
                            }
                            else
                            {
                                printerSettings.PrinterName = strPrinterName;
                                string reportName = typeof(MPOSReportLibrary.Ticket).AssemblyQualifiedName;
                                var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                                var typeReportSource = new Telerik.Reporting.TypeReportSource();
                                typeReportSource.TypeName = reportName;
                                typeReportSource.Parameters.Add(new Telerik.Reporting.Parameter(name: "LocationID", value: Convert.ToInt32(strLocationID)));
                                typeReportSource.Parameters.Add(new Telerik.Reporting.Parameter(name: "RecID", value: Convert.ToInt32(strRecID)));
                                reportProcessor.PrintReport(typeReportSource, printerSettings);
                            }


                            using (SqlConnection con2 = new SqlConnection(strConnString))
                            using (SqlCommand cmd2 = new SqlCommand())
                            {
                                cmd2.Connection = con2;
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = "Delete FROM dbo.PrintTicket where (LocationID = @LocationID) and PrintID=@PrintID";
                                cmd2.Parameters.AddWithValue(parameterName: "@LocationID", value: strLocationID);
                                cmd2.Parameters.AddWithValue(parameterName: "@PrintID", value: strPrintID);
                                con2.Open();
                                cmd2.ExecuteNonQuery();
                                //Console.WriteLine(value: "Delete Completed ");
                                con2.Close();
                            }
                        }


                        con.Close();

                    }
                    catch (SqlException er)
                    {
                        Console.WriteLine("There was an error reported by SQL Server, " + er.Message);
                    }
                //Console.WriteLine(value: "Next:" + nextN);
              Thread.Sleep(millisecondsTimeout: 2000);

            }
            // Keep the console window open in debug mode.
            //Console.WriteLine(value: "Press any key to exit.");
            //Console.ReadKey();
        }
    }
}
