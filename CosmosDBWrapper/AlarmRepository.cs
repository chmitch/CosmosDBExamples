using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using System;

namespace CosmosDBWrapper
{    
    public class AlarmRepository : DocumentDb
    {
        //each repo can specify it's own database and document collection
        public AlarmRepository() : base("oncalldemodb2", "Alarms") { }

        //Get a full list of alarms : don't ever do this. 
        public Task<List<Alarm>> GetAlarmsAsync()
        {
            return Task<List<Alarm>>.Run(() =>
                Client.CreateDocumentQuery<Alarm>(Collection.DocumentsLink)
                .ToList());
        }

        //Get a specific alarm by id.
        public async Task<Alarm> GetAlarmAsync(string id)
        {

            var doc = await Task<Alarm>.Run(() =>
                Client.CreateDocumentQuery<Alarm>(Collection.DocumentsLink)
                .Where(r => r.Id == id)
                .AsEnumerable()
                .FirstOrDefault());

            return (Alarm)(dynamic)doc;

        }

        public async Task<List<Alarm>> GetAlarmsByTennantAsync(int tennantId)
        {

            return await Task<List<Alarm>>.Run(() =>
                Client.CreateDocumentQuery<Alarm>(Collection.DocumentsLink)
                .Where(r => r.TenantId == tennantId)
            .ToList());
        }

        //Create / update an alarm.
        public async Task<ResourceResponse<Document>> SaveAlarm(Alarm alarm)
        {
            alarm.Id = alarm.Id ?? System.Guid.NewGuid().ToString();

            var ac = new AccessCondition { Condition = alarm.ETag, Type = AccessConditionType.IfMatch };

            return await Client.CreateDocumentAsync(Collection.DocumentsLink, alarm);
            //save the item to docuemntdb.
            //return await Client.UpsertDocumentAsync(Collection.DocumentsLink, alarm, new RequestOptions { AccessCondition = ac });
        }
    }
}