using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace MyClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 客户端模式
            //var client = new HttpClient();
            //var disco = client.GetDiscoveryDocumentAsync("https://localhost:5001/").Result;
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    return;
            //}
            //var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "ClientPattern",
            //    ClientSecret = "ClientPatternSecret",
            //    Scope = "ClientScope"
            //}).Result;

            //var token = tokenResponse.AccessToken;

            //Console.Write(token);


            //var apiClient = new HttpClient();
            ////apiClient.SetBearerToken(token);
            //var response = apiClient.GetAsync("https://localhost:7001/identity/GetInit").Result;
            //var content = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(content);


            //apiClient.SetBearerToken(token);
            //response = apiClient.GetAsync("https://localhost:7001/identity/GetUser").Result;
            //content = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(content);
            #endregion


            #region
            //var client = new HttpClient();
            //var disco = client.GetDiscoveryDocumentAsync("https://localhost:5001/").Result;
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    return;
            //}

            //var tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "PassPattern",
            //    ClientSecret = "PassPatternSecret",
            //    Scope = "ClientScope openid profile",
            //    UserName = "name",
            //    Password = "password",
            //}).Result;

            //var token = tokenResponse.AccessToken;
            //Console.Write(token);


            //HttpClient apiClient = new HttpClient();
            //var userResponse = apiClient.GetUserInfoAsync(new UserInfoRequest()
            //{
            //    Token = token,
            //    Address = disco.UserInfoEndpoint,
            //}).Result;

            //HttpClient apiClient1 = new HttpClient();
            //apiClient1.SetBearerToken(token);
            //var response = apiClient1.GetAsync("https://localhost:7001/identity/GetResource").Result;

            //var content = response.Content.ReadAsStringAsync().Result;

            #endregion



            Console.ReadKey();
        }
    }
}
