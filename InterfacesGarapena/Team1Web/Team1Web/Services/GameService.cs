﻿using System.Net.Http.Headers;
using Team1Web.Models;
using Newtonsoft.Json;
using System.Text;
namespace Team1Web.Services
{
    public class GameService : IGameService
    {
        string baseurl = "http://192.168.65.111:8080/";

        public async Task<List<GameData>> GetGameData()
        {
            List<GameData> gameInfo = new List<GameData>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("team1/allgamesdata");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    gameInfo = JsonConvert.DeserializeObject<List<GameData>>(empResponse);
                    gameInfo = gameInfo.OrderByDescending(g => g.puntuazioa).ToList();
                }

            }
            return gameInfo;
        }

    }
}
