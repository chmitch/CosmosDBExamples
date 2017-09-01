using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosDBWrapper;
using Microsoft.Azure.Documents.Client;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Alarm a = new Alarm();
            a.Id = Guid.NewGuid().ToString();
            a.TenantId = 1;
            a.SomeData = "this is some data";
            
            AlarmRepository repository = new AlarmRepository();
            repository.Initilization.Wait();

            //create a new Alarm
            repository.SaveAlarm(a).Wait();

            //Fetch an alarm by its ID
            Alarm aResult = repository.GetAlarmAsync(a.Id).Result;

            //Get a list of alarms by tenantId
            List<Alarm> alarms = repository.GetAlarmsByTennantAsync(1).Result;
        }
    }
}
