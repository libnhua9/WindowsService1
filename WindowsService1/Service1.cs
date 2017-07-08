using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Timers;
namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
         Timer timer;
        public Service1()
        {
            InitializeComponent();
        }

        public void onLoad()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {

            timer = new Timer( );
            timer.Interval = 1000 * 6;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = true;
            try
            {
                timer.Start();
            }
            catch (ArgumentOutOfRangeException Ex)
            {
                throw Ex;
            }
        }
        private void timer_Elapsed(object obj, EventArgs args)
        {
            //using (FileStream fs = new FileStream(@"E:\test.txt", FileMode.Append, FileAccess.Write))
            //{
                //using (StreamWriter sw = new StreamWriter(fs))
                //{
                    var resultContent = "";
                    //https://way.jd.com/jisuapi/weather?city=深圳&cityid=111&citycode=101260301&appkey=101260301&appkey=ccba4c1751078e7be9caa133a3c2439c
                    using (DB_TestEntities db = new DB_TestEntities())
                    {
                        var data = db.Tb_WeatherForecast.ToList();
                        foreach (var item in data)
                        {
                            using (HttpClient client = new HttpClient())
                            {
                                var url = "https://way.jd.com/jisuapi/weather?";
                                var postData = $"city={item.City}&cityid=111&citycode=101260301&appkey=ccba4c1751078e7be9caa133a3c2439c";
                                var result = client.PostAsync(url + postData, null).Result;
                                resultContent = result.Content.ReadAsStringAsync().Result;
                                //Console.WriteLine(resultContent);
                            }
                            if (item != null)
                            {
                                item.Config = resultContent;
                                item.Verion += 1;
                                item.UpdateDate = DateTime.Now;
                                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                        db.SaveChanges();
                    }
                //    sw.WriteLine(DateTime.Now.ToString());
                //}
            //}

        }
        protected override void OnStop()
        {
        }

    }
}
