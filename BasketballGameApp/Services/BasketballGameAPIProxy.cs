using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BasketballGameApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using BasketballGameApp.DTO;

namespace BasketballGameApp.Services
{
    class BasketballGameAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:46288/BasketballGameApi"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:46288/BasketballGameApi"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "https://localhost:44369/BasketballGameApi"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:46288/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:46288/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "https://localhost:44369/Images/"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static BasketballGameAPIProxy proxy = null;

        #region CreateProxy
        public static BasketballGameAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
            }

            if (proxy == null)
                proxy = new BasketballGameAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }
        #endregion

        #region Constructor
        private BasketballGameAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }
        #endregion

        #region GetHello
        public async Task<string> GetHello()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Test");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    return "errrrror";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "exceptio happened";

            }
        }
        #endregion

        #region GetBasePhotoUri
        public string GetBasePhotoUri() { return this.basePhotosUri; }
        #endregion

        #region LoginAsync
        public async Task<User> LoginAsync(UserDTO userDTO)
        {
            try
            {
                string json = JsonSerializer.Serialize(userDTO);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<User>(res, options);
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region PlayerSignUpAsync
        public async Task<Player> PlayerSignUpAsync(Player player)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Player>(player, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/PlayerSignUp", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Player a = JsonSerializer.Deserialize<Player>(jsonObject, options);
                    return a;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region CoachSignUpAsync
        public async Task<Coach> CoachSignUpAsync(Coach coach)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Coach>(coach, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/CoachSignUp", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Coach a = JsonSerializer.Deserialize<Coach>(jsonObject, options);
                    return a;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region UserExistByEmailAsync
        public async Task<bool> UserExistByEmailAsync(string email)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/UserExistByEmail?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region UserExistByPasswordAsync
        public async Task<bool> UserExistByPasswordAsync(string password)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/UserExistByPassword?password={password}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region UploadImage
        //Upload file to server (only images!)
        public async Task<bool> UploadImage(Models.FileInfo fileInfo, string targetFileName)
        {
            try
            {
                var multipartFormDataContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(fileInfo.Name));
                multipartFormDataContent.Add(fileContent, "file", targetFileName);
                HttpResponseMessage response = await client.PostAsync($"{this.baseUri}/UploadImage", multipartFormDataContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetGamesAsync
        public async Task<List<Game>> GetGamesAsync(Team team)
        {
            try
            {
                int teamId = -1;
                if (team != null)
                    teamId = team.Id;
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetGames?teamId={teamId}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Game>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetPlayerOnTeamForSeasonAsync

        public async Task<PlayerOnTeamForSeason> GetPlayerOnTeamForSeasonAsync(int userId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetPlayerOnTeamForSeason?userId={userId}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PlayerOnTeamForSeason>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region AddTeamAsync
        public async Task<Team> AddTeamAsync(Team team)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Team>(team, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddTeam", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Team t = JsonSerializer.Deserialize<Team>(jsonObject, options);
                    return t;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetLeaguesAsync
        //public async Task<List<League>> GetLeaguesAsync()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetLeagues");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            JsonSerializerOptions options = new JsonSerializerOptions
        //            {
        //                ReferenceHandler = ReferenceHandler.Preserve,
        //                PropertyNameCaseInsensitive = true
        //            };

        //            string res = await response.Content.ReadAsStringAsync();
        //            return JsonSerializer.Deserialize<List<League>>(res, options);
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }
        //}
        #endregion

        #region GetOpenTeamsAsync
        public async Task<List<Team>> GetOpenTeamsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetOpenTeams");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Team>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region AddRequestToJoinTeamAsync
        public async Task<bool> AddRequestToJoinTeamAsync(RequestToJoinTeam request)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<RequestToJoinTeam>(request, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddRequestToJoinTeam", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    bool r = JsonSerializer.Deserialize<bool>(jsonObject, options);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetRequestsToJoinTeamAsync
        public async Task<List<RequestToJoinTeam>> GetRequestsToJoinTeamAsync(Coach coach)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetRequestsToJoinTeam?coachId={coach.Id}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<RequestToJoinTeam>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region UpdatePlayerAsync
        public async Task<bool> UpdatePlayerAsync(Player player)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize(player, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdatePlayer", content);
                if (response.IsSuccessStatusCode)
                {

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(res, options);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region ApproveRequestToJoinTeamAsync
        public async Task<bool> ApproveRequestToJoinTeamAsync(Player player)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(player, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/ApproveRequestToJoinTeam", content);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(res, options);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region DeleteRequestToJoinTeamAsync
        public async Task<bool> DeleteRequestToJoinTeamAsync(Player player)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(player, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/DeleteRequestToJoinTeam", content);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(res, options);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetTeamsAsync
        public async Task<List<Team>> GetTeamsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetTeams");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Team>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region HasGameAsync
        public async Task<bool> HasGameAsync(int teamId1, int teamId2, DateTime date)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/HasGame?teamId1={teamId1}&&teamId2={teamId2}&& date={date}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region AddRequestToGameAsync
        public async Task<bool> AddRequestToGameAsync(RequestGame request)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<RequestGame>(request, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddRequestToGame", content);

                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    bool r = JsonSerializer.Deserialize<bool>(jsonObject, options);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetRequestsGameAsync
        public async Task<List<RequestGame>> GetRequestsGameAsync(Team team)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetRequestsGame?teamId={team.Id}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<RequestGame>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region ApproveRequestToGameAsync
        public async Task<bool> ApproveRequestToGameAsync(RequestGame request)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(request, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/ApproveRequestToGame", content);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(res, options);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region DeleteRequestToGameAsync
        public async Task<bool> DeleteRequestToGameAsync(RequestGame request)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(request, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/DeleteRequestToGame", content);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(res, options);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetMyRequestToJoinTeamAsync
        public async Task<RequestToJoinTeam> GetMyRequestToJoinTeamAsync(Player player)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetMyRequestToJoinTeam?playerId={player.Id}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<RequestToJoinTeam>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetPlayersAsync

        public async Task<List<Player>> GetPlayersAsync(User user)
        {
            try
            {
                int userId = -1;
                if (user != null)
                    userId = user.Id;
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetPlayers?userId={userId}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Player>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetGameStatsAsync
        public async Task<List<GameStat>> GetGameStatsAsync(Game game, Team team)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetGameStats?gameId={game.Id}&&teamId={team.Id}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<GameStat>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region SaveGameStatsAsync
        public async Task<bool> SaveGameStatsAsync(List<GameStat> listGameStats)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<List<GameStat>>(listGameStats, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/SaveGameStats", content);

                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    bool r = JsonSerializer.Deserialize<bool>(jsonObject, options);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetPlayersRankingAsync
        public async Task<IDictionary<Player, double>> GetPlayersRankingAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetPlayersRanking");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IDictionary<Player, double>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetTeamsRankingAsync
        public async Task<List<Team>> GetTeamsRankingAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/GetTeamsRanking");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };

                    string res = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Team>>(res, options);
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion
    }
}

