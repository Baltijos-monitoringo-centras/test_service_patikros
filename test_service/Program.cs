using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Xml;

namespace test_service
{
    class Program
    {

        /// <summary>
        /// OER SAKAS
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        { int n = 0;
            while (n == 0)
            {

                //answer();
                answer2(11,2);
                answer2(1, 11);
                answer2(10, 11);
                travelsim();
                command();
                System.Threading.Thread.Sleep(30000);
             }
            Console.WriteLine("Done. Press any key to exit...");
            Console.ReadKey();
        }
static void command()
        {
            crm crm = new crm();
            crm.testDataTable test = new crm.testDataTable();
            crmTableAdapters.testTableAdapter testtableadapter = new crmTableAdapters.testTableAdapter();
            sherlog sherlog = new sherlog();
            sherlog.sherlogDataTable shreqn = new sherlog.sherlogDataTable();
            sherlogTableAdapters.sherlogTableAdapter shreqntableadapter = new sherlogTableAdapters.sherlogTableAdapter();
            sherlog.resultDataTable result = new sherlog.resultDataTable();
            sherlogTableAdapters.resultTableAdapter resulttableadapter = new sherlogTableAdapters.resultTableAdapter();
            sherlogTableAdapters.hideTableAdapter hideTableAdapter = new sherlogTableAdapters.hideTableAdapter();


            testtableadapter.FillByNew(test, 1);
            Console.WriteLine("start");

            foreach (DataRow nextRow in test)
            {
                try
                {

                    Console.WriteLine(nextRow["deviceid"].ToString());

                    if (Convert.ToInt16(nextRow["devicetype"]) == 1)
                    {
                        string deviceid = nextRow["deviceid"].ToString();
                        try
                        {
                            hideTableAdapter.DeleteQuery(deviceid);
                        }
                        catch { }
                        DataRow request = shreqn.NewRow();
                        request["reply"] = deviceid;
                        request["request"] = 6;
                        request["stationid"] = Convert.ToInt16(nextRow["tower"]);
                        request["user_id"] = "test";
                        shreqn.Rows.Add(request);
                        shreqntableadapter.Update(request);
                        /* DataRow newTest = test.NewRow();
                         newTest["status"] = 1;
                         newTest["result"] = "";
                         newTest["deviceid"] = deviceid;
                         testtableadapter.Update(newTest);*/
                        nextRow["status"] = 10;
                        nextRow["result"] = "";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);

                    }
                    else if (Convert.ToInt16(nextRow["devicetype"]) == 4)
                    {
                        string deviceid = nextRow["deviceid"].ToString().Substring(1);
                        string user = nextRow["userid"].ToString();
                        DateTime dt = DateTime.Now;
                        SxObject sxObject = new SxObject(deviceid, user, dt);
                        Sx sx = new Sx();
                        sx.sendSxTest(sxObject);
                        nextRow["status"] = 10;
                        nextRow["result"] = "";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);
                    }
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            testtableadapter.FillByNew(test,0);

            int n = 0;
            foreach (DataRow nextRow in test)
            {
                try
                {
                    Console.WriteLine(n);
                    n++;
                    Console.WriteLine(nextRow["deviceid"].ToString());
                    int devtype = Convert.ToInt16(nextRow["devicetype"]);
                    if (devtype == 1)
                    {   string deviceid = nextRow["deviceid"].ToString();
                        try
                        {
                            hideTableAdapter.DeleteQuery(deviceid);
                        }
                        catch { }
                        DataRow request = shreqn.NewRow();
                        request["reply"] = deviceid;
                        request["request"] = 6;
                        request["stationid"] = Convert.ToInt16(nextRow["tower"]);
                        request["user_id"] = "test";
                        shreqn.Rows.Add(request);
                        shreqntableadapter.Update(request);

                        nextRow["status"] = 1;
                        nextRow["result"] = "";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);

                    }
                    else if (Convert.ToInt16(nextRow["devicetype"]) == 4)
                    {
                        string deviceid = nextRow["deviceid"].ToString().Substring(1);
                        string user = nextRow["userid"].ToString();
                        DateTime dt = DateTime.Now;
                        SxObject sxObject = new SxObject(deviceid, user, dt);
                        Sx sx = new Sx();
                        sx.sendSxTest(sxObject);
                        nextRow["status"] = 1;
                        nextRow["result"] = "";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);
                    }

                    if (devtype == 2 || devtype == 5 || devtype == 6 || devtype == 7 || devtype == 8 || devtype == 9)
                    {
                        nextRow["status"] = 1;
                        nextRow["result"] = "";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);
                    }
                }
                catch(Exception e) { Console.WriteLine(e); }
            }
        }    
  

        static void answer2( int pre, int post)
        {
            crm crm = new crm();
            crm.testDataTable test = new crm.testDataTable();
            crmTableAdapters.testTableAdapter testtableadapter = new crmTableAdapters.testTableAdapter();
            crm.NormsDataTable norms = new crm.NormsDataTable();
            crmTableAdapters.NormsTableAdapter normstableadapter = new crmTableAdapters.NormsTableAdapter();
            crm.TowerLevelDataTable towerlevel = new crm.TowerLevelDataTable();
            crmTableAdapters.TowerLevelTableAdapter towerleveltableadapter = new crmTableAdapters.TowerLevelTableAdapter();
            crm.SignalsDataTable signals = new crm.SignalsDataTable();
            crmTableAdapters.SignalsTableAdapter signalsTableAdapter = new crmTableAdapters.SignalsTableAdapter();
            sherlog sherlog = new sherlog();
            sherlog.sherlogDataTable shreqn = new sherlog.sherlogDataTable();
            sherlogTableAdapters.sherlogTableAdapter shreqntableadapter = new sherlogTableAdapters.sherlogTableAdapter();
            sherlog.resultDataTable result = new sherlog.resultDataTable();
            sherlogTableAdapters.resultTableAdapter resulttableadapter = new sherlogTableAdapters.resultTableAdapter();

            testtableadapter.FillByNew(test, pre);
            foreach (DataRow nextRow in test)
            {
                try
                {

                    
                    Console.WriteLine(nextRow["deviceid"].ToString());
                    DateTime signalTime = Convert.ToDateTime(nextRow["date"].ToString());

                    Console.WriteLine("test time: "+signalTime);
                    Console.WriteLine("test time dif: " + DateTime.Now.Subtract(signalTime).TotalMinutes);
                    if (DateTime.Now.Subtract(signalTime).TotalMinutes > 6 && Convert.ToInt16(nextRow["devicetype"]) == 1)
                    {
                        nextRow["status"] = 2;
                        nextRow["result"] = "";
                        nextRow["conclusion"] = 0;
                        nextRow["issues"] = "No answer!\n";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);
                    } else
                    if (DateTime.Now.Subtract(signalTime).TotalMinutes > 3 && Convert.ToInt16(nextRow["devicetype"]) == 4)
                    {
                        nextRow["status"] = 2;
                        nextRow["result"] = "";
                        nextRow["conclusion"] = 0;
                        nextRow["issues"] = "No answer!\n";
                        nextRow.AcceptChanges();
                        nextRow.SetModified();
                        testtableadapter.Update(test);
                    }
                    
                    else  
                    if (Convert.ToInt16(nextRow["devicetype"]) == 1)
                    {
                        int reply = Convert.ToInt32(nextRow["devicereply"]);
                        string deviceid = nextRow["deviceid"].ToString();
                        string user = nextRow["userid"].ToString();
                        resulttableadapter.Fill(result, Convert.ToDateTime(nextRow["date"]), deviceid);
                        string res = "";
                        res = res + "<device id=\"" + deviceid + "\"><type>1</type>";
                        Console.WriteLine(reply.ToString());
                        normstableadapter.FillBySherlog(norms, 1, reply, reply);
                        int normu1 = Convert.ToInt16(norms[0]["p1"]);
                        int normu2 = Convert.ToInt16(norms[0]["p2"]);
                        string notes = norms[0]["notes"].ToString();
                        signalsTableAdapter.Fill(signals, deviceid, DateTime.Now.AddDays(-30));
                        string signalsCount = signals[0]["cnt"].ToString();
                        string issues = "";

                        foreach (DataRow row in result)
                        {
                            int level = Convert.ToInt16(row["level"]);
                            // int direction = Convert.ToInt16(row["direction"]);
                            int stationid = Convert.ToInt16(row["stationid"]);
                            int why = Convert.ToInt16(row["why"]);
                            int inf1 = Convert.ToInt16(row["Inf1"]);
                            int u1 = Convert.ToInt16(row["U1"]);
                            int u2 = Convert.ToInt16(row["U2"]);
                            towerleveltableadapter.Fill(towerlevel, nextRow["userid"].ToString(), stationid.ToString());
                            int normlevel = Convert.ToInt16(towerlevel[0]["level"]);
                            //int normdirection = Convert.ToInt16(towerlevel[0]["direction"]);


                            res = res + "<tower towernr=\" " + stationid + "\">";


                            res = res + "<SignalLevel>";
                            if (level > (normlevel - 2))
                                res = res + level + " OK";
                            else
                            {
                                res = res + level + " too low";
                                issues = issues + "too low signal: " + stationid + " - " + level + "\n";
                            }

                            res = res + "</SignalLevel>";

                            res = res + "<U1>";
                            if (u1 > 0)
                            {
                                if (u1 > normu1 - 10)
                                    res = res + "OK: " + u1 * 0.17;
                                else
                                {
                                    res = res + u1 * 0.17 + " too low";
                                    issues = issues + "too low main voltage: " + u1 * 0.17 + "\n";

                                }
                            }
                            else res = res + "N/A";
                            res = res + "</U1>";


                            res = res + "<U2>";
                            if (u2 > 0)
                            {
                                if (u2 > normu2)
                                    res = res + "OK: " + u2 * 0.09;
                                else
                                {
                                    res = res + u2 * 0.09 + " too low";
                                    issues = issues + "too low backup voltage: " + u2 * 0.09 + "\n";
                                }
                            }
                            else res = res + "N/A";
                            res = res + "</U2>";

                            res = res + "</tower>";
                        }

                        res = res + "<notes>" + notes + "</notes>";
                        res = res + "<signals>" + signalsCount + "</signals>";
                        res = res + "</device>";
                        try
                        {
                            XDocument doc = XDocument.Parse(res);
                            res = doc.ToString();
                        }
                        catch (Exception)
                        {
                            // Handle and throw if fatal exception here; don't just ignore them

                        }

                        Console.WriteLine(res);
                        Console.WriteLine("issues: "+issues+" count: "+ issues.Length);
                        int conclusion = 0;
                        if (issues.Equals("")||issues.Length<2)
                        conclusion = 1;

                        if (!res.Equals("") && res.Contains("tower"))
                        {

                            nextRow["status"] = 2;
                            nextRow["result"] = res;
                            nextRow["conclusion"] = conclusion;
                            nextRow["issues"] = issues;
                            nextRow.AcceptChanges();
                            nextRow.SetModified();
                            testtableadapter.Update(test);
                        }
                    }
                    else if (Convert.ToInt16(nextRow["devicetype"]) == 4)
                    {
                        string deviceid = nextRow["deviceid"].ToString().Substring(1);
                        string user = nextRow["userid"].ToString();
                        DateTime dt = Convert.ToDateTime(nextRow["date"]);
                        SxObject sxObject = new SxObject(deviceid, user, dt);
                        Sx sx = new Sx();
                        string  res= sx.getSxResult(sxObject).Item1;
                        string issues = sx.getSxResult(sxObject).Item2;
                        int conclusion = 0;
                        if (issues.Equals("") || issues.Length < 2)
                            conclusion = 1;
                        if (!res.Equals("") && res.Contains("tower"))
                        {
                            nextRow["status"] = post;
                            nextRow["result"] = res;
                            nextRow["conclusion"] = conclusion;
                            nextRow["issues"] = issues;
                            nextRow.AcceptChanges();
                            nextRow.SetModified();
                            testtableadapter.Update(test);
                        }
                    }

                    } catch(Exception e) { Console.WriteLine(e); }
            }
        }
            static void travelsim()
        {
            crm crm = new crm();
            crm.testDataTable test = new crm.testDataTable();
            crmTableAdapters.testTableAdapter testtableadapter = new crmTableAdapters.testTableAdapter();
            crm.NormsDataTable norms = new crm.NormsDataTable();
            crmTableAdapters.NormsTableAdapter normstableadapter = new crmTableAdapters.NormsTableAdapter();
            crm.TowerLevelDataTable towerlevel = new crm.TowerLevelDataTable();
            crmTableAdapters.TowerLevelTableAdapter towerleveltableadapter = new crmTableAdapters.TowerLevelTableAdapter();
            sherlog sherlog = new sherlog();
            sms sms = new sms();
            sms.GmSmsDataTable gmSms = new sms.GmSmsDataTable();
            smsTableAdapters.GmSmsTableAdapter gmSmsTableAdapter = new smsTableAdapters.GmSmsTableAdapter();
            string smsCount = "";
            string res = "";
            testtableadapter.FillByNew(test, 1);
            foreach (DataRow nextRow in test)
            {
                
                Console.WriteLine(nextRow["date"].ToString());
                int devtype = Convert.ToInt16(nextRow["devicetype"]);
                List<int> types = new List<int> { 2,5,6,7,8,9,14,15,16,17,18,19,20 };
                List<int> itc_t = new List<int> { 5, 6, 14, 15, 16};
                if (types.Contains(devtype))
                {
                    string issues = "";
                    string deviceid = nextRow["deviceid"].ToString();
                    string phone = nextRow["devicereply"].ToString();
                    Console.WriteLine(phone);

                    if (phone.StartsWith("372"))
                    {

                        res = "Old SIM card, please contact Operating Center";
                        issues = issues + "Old SIM card, please contact Operating Center\n";

                    
                    
                    smsCount = "<sms>";
                    
                    try
                    {
                        smsCount = smsCount+"\n\t<date>"+ DateTime.Now.AddDays(-30)+":</date>\n";
                        gmSmsTableAdapter.Fill(gmSms, DateTime.Now.AddDays(-30), deviceid);
                        if (gmSms.Rows.Count < 1)
                            smsCount = smsCount + "\t\t<alarm>No SMS Messages</alarm>\n";
                        else
                        {
                            foreach (DataRow row in gmSms)
                            {
                                smsCount = smsCount + "\t\t<alarm>" + row["alarm"] + ": " + row["cnt"] + " </alarm>\n";
                            }
                        }
                    }
                    catch { }
                    smsCount = smsCount + "</sms>";
                    }
                    else if (phone.StartsWith("280") || phone.StartsWith("228"))
                    {
                        ItcCommands itcCommands = new ItcCommands();
                        if (itc_t.Contains(devtype))
                        {
                            crm.ItcDataTable itc = new crm.ItcDataTable();
                            crmTableAdapters.ItcTableAdapter itcTableAdapter = new crmTableAdapters.ItcTableAdapter();
                            crm.MontageDataTable montage = new crm.MontageDataTable();
                            crmTableAdapters.MontageTableAdapter montageTableAdapter = new crmTableAdapters.MontageTableAdapter();
                           
                                    try
                            {
                                montageTableAdapter.Fill(montage, phone);
                                itcTableAdapter.Fill(itc,montage[0]["DisplayCode"].ToString());
                                DateTime dateTime = DateTime.Parse(itc[0]["date"].ToString());
                                int minutesDifference = (int)(DateTime.Now - dateTime).TotalMinutes;
                                if (minutesDifference >= 0)
                                {
                                    res = minutesDifference.ToString();
                                }
                                else
                                {
                                    try { res = itcCommands.itcStatus(phone); }
                                    catch (Exception ex) { Console.WriteLine(ex); }
                                }
                            }
                            catch (Exception ex) { Console.WriteLine(ex);
                                try { res = itcCommands.itcStatus(phone); }
                                catch (Exception e) { Console.WriteLine(e); }
                            }
                        }
                        else
                        {
                            try { res = itcCommands.itcStatus(phone); }
                            catch (Exception ex) { Console.WriteLine(ex); }
                        }
                        smsCount = "<sms>\t\t<alarm>No SMS Messages</alarm>\n</sms>";
                    }
                    int n;
                    bool isNumeric = int.TryParse(res, out n);
                    if (!isNumeric)
                        issues = issues + res;
                    if (!res.Equals(""))
                {
                    res = "<device id=\"" + deviceid + "\"><type>2</type><age>" + res + "</age>"+ smsCount + "</device>";
                        try
                        {
                            XDocument doc = XDocument.Parse(res);
                            res = doc.ToString();
                        }
                        catch (Exception)
                        {
                            // Handle and throw if fatal exception here; don't just ignore them

                        }
                        Console.WriteLine("issues: " + issues + " count: " + issues.Length);
                        int conclusion = 0;
                        if (issues.Equals("") || issues.Length < 2)
                            conclusion = 1;

                    nextRow["status"] = 2;
                    nextRow["result"] = res;
                    nextRow["conclusion"] = conclusion;
                    nextRow["issues"] = issues;
                    nextRow.AcceptChanges();
                    nextRow.SetModified();
                    testtableadapter.Update(test);
                }

                }
                
            }    
        }

       static string Get(string uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                //return reader.ReadToEnd();
                return reader.ReadToEnd();
            }
        }

    }
    public class SxObject
        {
            public string deviceid { get; set; }
            public string user { get; set; }
        public DateTime testTime { get; set; }
        public SxObject(string devid,string usr, DateTime dt)
        {
            deviceid = devid;
            user = usr;
            testTime = dt;

        }
        }

}
