using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace test_service
{
    public class Sx
    {
        DataSetSx dataSetSx = new DataSetSx();
        public void sendSxTest(SxObject sxObject)
        {
            DataSetSx.OutgoingDataTable outgoings = new DataSetSx.OutgoingDataTable();
            DataSetSxTableAdapters.OutgoingTableAdapter outgoingTableAdapter = new DataSetSxTableAdapters.OutgoingTableAdapter();
            DataSetSx.TowerSignalDataTable towers = new DataSetSx.TowerSignalDataTable();
            DataSetSxTableAdapters.TowerSignalTableAdapter towerSignalTableAdapter = new DataSetSxTableAdapters.TowerSignalTableAdapter();

            string user = sxObject.user;
            string deviceid = sxObject.deviceid;

            towerSignalTableAdapter.FillByUser(towers, user);
            foreach(DataRow row in towers.Rows)
            {
                int tower = Convert.ToInt32(row["tower"].ToString());
                outgoingTableAdapter.InsertQuery(deviceid, tower, user);
            }

        }


        public Tuple<string,string> getSxResult(SxObject sxObject)
        {
            string alarm = "";
            string res = "";
            DataSetSx.IncomingDataTable incomings = new DataSetSx.IncomingDataTable();
            DataSetSxTableAdapters.IncomingTableAdapter incomingTableAdapter = new DataSetSxTableAdapters.IncomingTableAdapter();
            incomingTableAdapter.Fill(incomings, sxObject.deviceid, sxObject.testTime);
            Console.WriteLine(sxObject.deviceid + " " + sxObject.testTime);
            crm.SignalsDataTable signals = new crm.SignalsDataTable();
            crmTableAdapters.SignalsTableAdapter signalsTableAdapter = new crmTableAdapters.SignalsTableAdapter();
            DataSetSx.TowerSignalDataTable towers = new DataSetSx.TowerSignalDataTable();
            DataSetSxTableAdapters.TowerSignalTableAdapter towerSignalTableAdapter = new DataSetSxTableAdapters.TowerSignalTableAdapter();

            
            signalsTableAdapter.Fill(signals, sxObject.deviceid, DateTime.Now.AddDays(-30));
            string signalsCount = signals[0]["cnt"].ToString();
            if (incomings.Count > 0)
            {
              res = res + "<device id=\"" + sxObject.deviceid + "\"><type>4</type>";

                foreach (DataRow row in incomings.Rows)
                {
                    try
                    {
                        string tower = row["stationid"].ToString();
                        towerSignalTableAdapter.FillByUserTower(towers, sxObject.user, tower);

                        if (tower.Count() > 0)

                        {

                            int normLevel = Convert.ToInt32(towers[0]["level"].ToString());
                            int level = Convert.ToInt32(row["level"].ToString());
                            string lon = row["longitude"].ToString();
                            string lat = row["latitude"].ToString();
                                                        if (level < normLevel - 10)
                                alarm =alarm+ "too low signal: " + tower + " - " + level + "\n";
                            if (lon.Equals("0") && lat.Equals("0"))
                                alarm = alarm + "No GPS data!\n";
                            res = res + "<tower towernr=\" " + tower + "\">";
                            res = res + "<Level>" + level + "</Level>";
                            res = res + "<Longitude>" + lon + "</Longitude>";
                            res = res + "<Latitude>" + lat + "</Latitude>";
                            res = res + "<Alarm>"+"</Alarm>";
                            res = res + "</tower>";
                            res = res + "<signals>" + signalsCount + "</signals>";

                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                           
                } res = res + "</device>";
            }

            return Tuple.Create(res,alarm);
        }
                public Tuple<string, string> getSxResultBT(SxObject sxObject)
        {
            string alarm = "";
            string res = "";
            DataSetSx.IncomingDataTable incomings = new DataSetSx.IncomingDataTable();
            DataSetSxTableAdapters.IncomingTableAdapter incomingTableAdapter = new DataSetSxTableAdapters.IncomingTableAdapter();
            incomingTableAdapter.Fill(incomings, sxObject.deviceid, sxObject.testTime);
            Console.WriteLine(sxObject.deviceid + " " + sxObject.testTime);
            crm.SignalsDataTable signals = new crm.SignalsDataTable();
            crmTableAdapters.SignalsTableAdapter signalsTableAdapter = new crmTableAdapters.SignalsTableAdapter();
            DataSetSx.TowerSignalDataTable towers = new DataSetSx.TowerSignalDataTable();
            DataSetSxTableAdapters.TowerSignalTableAdapter towerSignalTableAdapter = new DataSetSxTableAdapters.TowerSignalTableAdapter();


            signalsTableAdapter.Fill(signals, sxObject.deviceid, DateTime.Now.AddDays(-30));
            string signalsCount = signals[0]["cnt"].ToString();
            if (incomings.Count > 0)
            {
                res = res + "<device id=\"" + sxObject.deviceid + "\"><type>21</type>";

                foreach (DataRow row in incomings.Rows)
                {
                    try
                    {
                        string tower = row["stationid"].ToString();
                        towerSignalTableAdapter.FillByUserTower(towers, sxObject.user, tower);

                        if (tower.Count() > 0)

                        {

                            int normLevel = Convert.ToInt32(towers[0]["level"].ToString());
                            int level = Convert.ToInt32(row["level"].ToString());
                            string lon = row["longitude"].ToString();
                            string lat = row["latitude"].ToString();
                            if (level < normLevel - 10)
                                alarm = alarm + "too low signal: " + tower + " - " + level + "\n";
                            if (lon.Equals("0") && lat.Equals("0"))
                                alarm = alarm + "No GPS data!\n";
                            res = res + "<tower towernr=\" " + tower + "\">";
                            res = res + "<Level>" + level + "</Level>";
                            res = res + "<Longitude>" + lon + "</Longitude>";
                            res = res + "<Latitude>" + lat + "</Latitude>";
                            res = res + "<Alarm>" + "</Alarm>";
                            res = res + "</tower>";
                            res = res + "<signals>" + signalsCount + "</signals>";

                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }

                }
                res = res + "</device>";
            }

            return Tuple.Create(res, alarm);
        }
    }
}
