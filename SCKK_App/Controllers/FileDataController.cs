using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;

namespace SCKK_App.Controllers
{
    public class FileDataController
    {
        public List<Models.ResultModel> results = new List<Models.ResultModel>();
        private List<Models.CallModel> calls = new List<Models.CallModel>();

        public void FileRead()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            if ((bool)fileDialog.ShowDialog())
            {
                foreach (String file in fileDialog.FileNames)
                {
                    try
                    {
                        var fs = new FileStream(file, FileMode.Open);
                        var sr = new StreamReader(fs);

                        string _input;

                        while ((_input = sr.ReadLine()) != null)
                        {
                            string[] row = _input.Split(' ');

                            if (row.Length > 10 && row[6] == "Taxi]:")
                            {
                                //   Bejövő hívás [call]   \\
                                if (row[10] == "/accepttaxi")
                                {
                                    Models.CallModel output = new Models.CallModel();

                                    output.time = Time(row[0], row[1]);
                                    output.identifier = ushort.Parse(row[11]);

                                    CallDistrict(output);
                                    output = null;
                                }

                                //   Elfogadott hívás [result]   \\
                                else if (row[row.Length - 5] == "elfogadta")
                                {
                                    Models.ResultModel output = new Models.ResultModel();

                                    output.time = Time(row[0], row[1]);
                                    output.identifier = ushort.Parse(row[row.Length - 3].Remove(row[row.Length - 3].Length - 1));

                                    if (row.Length == 13) //1 neves
                                        output.name = row[7];
                                    else if (row.Length == 14) //2 neves
                                        output.name = row[7] + " " + row[8];
                                    else if (row.Length == 15) //3 neves
                                        output.name = row[7] + " " + row[8] + " " + row[9];
                                    else if (row.Length == 15) //4 neves
                                        output.name = row[7] + " " + row[8] + " " + row[9] + " " + row[10];

                                    output.name = output.name;


                                    ResultDistrict(output);
                                    output = null;
                                }

                                //   Lemondott hívás [result]   \\
                                else if (row[7].StartsWith("Lemondt")) //Ékezet!!!!!
                                {
                                    Models.ResultModel output = new Models.ResultModel();

                                    output.time = Time(row[0], row[1]);
                                    output.identifier = ushort.Parse(row[9].Remove(row[9].Length - 1));
                                    output.name = "Lemondott";


                                    ResultDistrict(output);
                                    output = null;
                                }

                                //   15perces hívás [result]   \\
                                else if (row[7] == "Mivel")
                                {
                                    Models.ResultModel output = new Models.ResultModel();

                                    output.time = Time(row[0], row[1]);
                                    output.identifier = ushort.Parse(row[9].Remove(row[9].Length - 1));
                                    output.name = "Lemondott";


                                    ResultDistrict(output);
                                    output = null;
                                }
                            }
                        }

                        sr.Close();
                        fs.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, file);
                        continue;
                    }
                }
            }
            else MessageBox.Show("Hiba");
        }

        private DateTime Time(string a, string b) => DateTime.ParseExact(a.TrimStart('[') + b.Remove(b.Length - 1), "yyyy-MM-ddHH:mm:ss", CultureInfo.InvariantCulture);

        private void CallDistrict(Models.CallModel call)  //   Ismétlődés kiszűrése [call]   \\
        {
            bool notContain = true;
            for (int i = 0; i < calls.Count; i++)
                if (calls[i].identifier == call.identifier && calls[i].time.DayOfYear == call.time.DayOfYear)
                    notContain = false;
            if (notContain)
                calls.Add(call);
        }

        private void ResultDistrict(Models.ResultModel result)  //   Ismétlődés kiszűrése [result]   \\
        {
            bool notContain = true;
            for (int i = 0; i < results.Count; i++)
                if (results[i].identifier == result.identifier && results[i].time.DayOfYear == result.time.DayOfYear)
                    notContain = false;
            if (notContain)
                results.Add(result);
        }

        public void FileCompletion()
        {

            //   Bejövő hívás eredményhez kötése   \\
            foreach (var result in results)
            {
                for (int i = 0; i < calls.Count; i++)
                {
                    if (calls[i].identifier == result.identifier && calls[i].time.AddMinutes(20) > result.time)
                    {
                        result.callId = calls[i];
                    }
                }
            }
            calls = null;

            //   Elfogadott Lemondott hívások kiszűrése   \\
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].name == "Lemondott")
                {
                    for (int j = 0; j < results.Count; j++)
                    {
                        if (results[i].identifier == results[j].identifier && results[j].name != "Lemondott" && results[i].time.AddHours(-1) < results[j].time)
                        {
                            results[i].name = null;
                        }
                    }
                }
            }
            results.RemoveAll(result => result.name is null);

            //   1 perces hívások   \\
            foreach (var result in results)
                if (result.name == "Lemondott" && result.callId != null) //1 perces
                    if (result.time < result.callId.time.AddMinutes(1))
                        result.name = "1-perces";

            //   Hiányzó hívások   \\
            if (results.Count > 5)
            {
                //Adat rendezés\\
                results = results.OrderBy(x => x.time).ToList();
                //-------------\\
                //Hiányzók feltöltése\\
                int start = results.Min(x => x.time).DayOfYear;
                for (int i = 0; i <= (results.Max(x => x.time).DayOfYear - start); i++) //Ahány nap - annyi kör //
                {
                    //Aznapi és következő napi hívások kiválogatása
                    List<Models.ResultModel> result_tmp = results.Where(x => x.time.DayOfYear <= start + i + 1 && x.time.DayOfYear >= start + i).ToList();
                    List<int> missing = new List<int>();
                    try
                    {
                        //Hiányzó hívások kiszámolása/megkeresése
                        missing = Enumerable.Range(result_tmp.Min(x => x.identifier), result_tmp.Max(x => x.identifier) - result_tmp.Min(x => x.identifier) - 2).Except(result_tmp.Select(x => (int)x.identifier)).ToList();
                        if (missing.Count > 60) continue;

                    }
                    catch (Exception) { continue; }
                    //Hiányzó hívások behelyezése időpont alapján a Hívások közé
                    for (int j = 0; j < missing.Count; j++)
                    {
                        int y = results.IndexOf(results.Where(x => x.time.DayOfYear >= start + i).Select(x => x).First());
                        while (missing[j] > results[y].identifier) { y++; };
                        Models.ResultModel rm_tmp = new Models.ResultModel();
                        rm_tmp.time = results[y].time;
                        rm_tmp.name = "Hiányzó";
                        rm_tmp.identifier = (ushort)missing[j];
                        results.Insert(y, rm_tmp);
                    }
                }
            }
        }
    }
}
