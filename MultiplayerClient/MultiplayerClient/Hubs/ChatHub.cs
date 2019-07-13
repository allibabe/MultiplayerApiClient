using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiplayerClient.Hubs
{  
    public class ChatHub : Hub
    {


      
        private const string codePrefix = "?code=";
        private const string apiKey = "3zAAVfyq47XbeMEvUmn855SXjTMZf/Z7nt0Cj5UrQ3w/UCCuHheGjQ==";
        static HttpClient client = new HttpClient();



        public async Task GetSignalRConnectionInfo()
        {
            string baseUrl = "http://localhost:7071/api/v1/negotiate";
            var result = await client.PostAsync($"{baseUrl}", null);

            var res = JsonConvert.DeserializeObject<SignalRConnectionInfo>(await result.Content.ReadAsStringAsync());

            await Clients.Caller.SendAsync("ReceiveMessage2", res);

       
        }

        public async Task addtogroup(string id, Group item)
        {
            string baseUrl = "http://localhost:7071/api/v1/addgroup/";

            var result = await client.PostAsJsonAsync($"{baseUrl}{id}", item);

            await Clients.Caller.SendAsync("ReceiveFromAdd", result);
            var res = await result.Content.ReadAsStringAsync();

            await Clients.Caller.SendAsync("ReceiveMessage3", res);

            //await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            //await Clients.Group(groupName).SendAsync("ReceiveMessage3", $"{Context.ConnectionId} has joined the group {groupName}.");

        }

        //int count = 0;
        //public async Task AddToGroup(string groupName)
        //{
        //    count++;
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has {count}joined the group {groupName}.");
        //}

        //public async Task RemoveFromGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        //}


        //public async Task messageSend(string groupName, string message)
        //{
        //   // string name = Context.ConnectionId;

        //    await Clients.Group(groupName).SendAsync("Send", message);

        //}





        public async Task sendThis(string message, Group item)
        {
            string baseUrl = " http://localhost:7071/api/v1/SendGroupMessage/";

            var result = await client.PostAsJsonAsync($"{baseUrl}{message}",item);

            var res = await result.Content.ReadAsStringAsync();
            await Clients.Caller.SendAsync("newMessageagain", res);


            //     var res = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());

            //   await Clients.Caller.SendAsync("ReceiveMessage3", res);

            //await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            //await Clients.Group(groupName).SendAsync("ReceiveMessage3", $"{Context.ConnectionId} has joined the group {groupName}.");

        }




        //public async Task MultiplayerSetup2(GamePlayer player)
        //{
        //    string newUrl = "";
        //    var result = await client.PostAsync($"{newUrl}/multiplayersetup{codePrefix}{apiKey}", new StringContent(JsonConvert.SerializeObject(player)));
        //    var res = JsonConvert.DeserializeObject<SignalRConnectionInfo>(await result.Content.ReadAsStringAsync());

        //    await Clients.Caller.SendAsync("ReceiveMessage3", res);


        //}






        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
    }
}


public class Group
{
    [JsonProperty("group_name")]
    public string Group_name { get; set; }

}

public class SignalRConnectionInfo
{
    [JsonProperty("url")]
    public string Url { get; set; }
    [JsonProperty("accessToken")]
    public string AccessToken { get; set; }
}


public class GamePlayer
{
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    [JsonProperty("user_name")]
    public string UserName { get; set; }

    [JsonProperty("user_photo")]
    public string UserPhoto { get; set; }

    [JsonProperty("player_number", NullValueHandling = NullValueHandling.Ignore)]
    public int? PlayerNumber { get; set; } = null;
}
public class GameInfo
{
    [JsonProperty("id")]
    public string Id { get; set; } = "gameinfo-" + Guid.NewGuid().ToString();

    [JsonProperty("players")]
    public List<GamePlayer> Players { get; set; } = new List<GamePlayer>();

    [JsonProperty("room_id")]
    public string RoomId { get; set; }

    [JsonProperty("toks")]
    public List<int> Toks { get; set; }

    [JsonProperty("content")]
    public dynamic Content { get; set; }
}


public class TokBlitzRace : GameInfo
{
    [JsonProperty("player1_points")]
    public int Player1Points { get; set; }

    [JsonProperty("player2_points")]
    public int Player2Points { get; set; }

    [JsonProperty("player1_points_per_round")]
    public List<int> Player1PointsPerRound { get; set; } //Example for rounds (1-5): [100,50,75,25,100]. Do not count Finished first points, that is added in the api

    [JsonProperty("player2_points_per_round")]
    public List<int> Player2PointsPerRound { get; set; }

    [JsonProperty("player1_guess")]
    public int Player1Guess { get; set; }

    [JsonProperty("player2_guess")]
    public int Player2Guess { get; set; }

    [JsonProperty("player1_round")]
    public int Player1Round { get; set; }

    [JsonProperty("player2_round")]
    public int Player2Round { get; set; }

    [JsonProperty("seconds_elapsed")]
    public int SecondsElasped { get; set; }

    [JsonProperty("finished_first")]
    public GamePlayer FinishedFirst { get; set; }

    [JsonProperty("winner")]
    public GamePlayer Winner { get; set; }
}

