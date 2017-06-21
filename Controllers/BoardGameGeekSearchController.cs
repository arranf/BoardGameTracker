using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BoardGameTracker.Data;
using BoardGameTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace BoardGameTracker.Controllers
{
    [Route("api/BoardGameGeek/search")]
    public class BoardGameGeekSearch : Controller
    {
        const string API_URL = "https://www.boardgamegeek.com";
        const string SEARCH_RESOURCE = "/xmlapi/search";
        const string SEARCH_PARAM = "search";

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            RestClient client = new RestClient(API_URL);
            RestRequest request = new RestRequest(SEARCH_RESOURCE, Method.GET);
            request.AddQueryParameter(SEARCH_PARAM, query);

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync(
            request, r => taskCompletion.SetResult(r));

            RestResponse response = (RestResponse)(await taskCompletion.Task);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response.Content);
            return SearchXmlToJson(xml);
        }

        private IActionResult SearchXmlToJson(XmlDocument xml) 
        {
            JObject jobject = new JObject();
            JArray games = new JArray();

            var gameElements = xml.GetElementsByTagName("boardgame").Cast<XmlElement>();

            foreach (var game in gameElements) 
            {
                JObject obj = new JObject();
                obj["Id"] = game.GetAttribute("objectid");
                
                var nameNodes = game.GetElementsByTagName("name");
                if (nameNodes.Count > 0) {
                    obj["Name"] = nameNodes.Item(0).InnerText;
                }

                var yearpublishedNodes = game.GetElementsByTagName("yearpublished");
                if (yearpublishedNodes.Count > 0) {
                    obj["Year"] = yearpublishedNodes.Item(0).InnerText;
                }
                games.Add(obj);
            }
            jobject["Games"] = games;
            if (games.LongCount() > 0) {
                return new ObjectResult(jobject.ToString());
            }
            return NotFound();
        }
    }
}
