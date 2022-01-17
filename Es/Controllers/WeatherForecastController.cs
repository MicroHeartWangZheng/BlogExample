using Elasticsearch.Net;
using Es.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Es.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public void Get()
        {
            var indexName = "user"; //索引名称只能为小写
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://127.0.0.1:9200/"));
            var connectionSettings = new ConnectionSettings(connectionPool);
            var client = new ElasticClient(connectionSettings);


            var resp = client.DeleteByQuery<User>(x => x.Index(indexName).Query(q => q.Range(r => r.Field(f => f.Age).LessThan(18))));


            var resp3 = client.GetMany<User>(new List<long>() { 19L, 20L, 21L }, indexName);

            var resp2 = client.Search<User>(x => x.Index(indexName)
                                                 .From(0)
                                                 .Size(20)
                                                 .Query(q => q.Range(r => r.Field(f => f.Age).GreaterThan(18)) &&
                                                             q.Range(r => r.Field(f => f.Age).LessThan(30)) &&
                                                             q.Term(t => t.Gender, true)));

            var user = new User()
            {
                Age = 18,
                Gender = false,
                Name = "test"
            };
            var resp4 = client.Update<User>(20, u => u.Index(indexName).Doc(user));

            var resp5 = client.UpdateByQuery<User>(u => u.Index(indexName)
                                              .Query(q => q.Range(r => r.Field(f => f.Age).GreaterThan(18)) &&
                                                          q.Range(r => r.Field(f => f.Age).LessThan(30))).Script("age=18"));

            client.Indices.UpdateSettings
        }
    }
}
