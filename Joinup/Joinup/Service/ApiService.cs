﻿using Joinup.Common.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Service
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if ( !CrossConnectivity.Current.IsConnected )
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Revise su conexión a internet e intentelo de nuevo",
                };
            }

            var isReacheable = await CrossConnectivity.Current.IsRemoteReachable( "google.es" );

            if ( !isReacheable )
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No tiene conexión a internet",
                };
            }

            return new Response
            {
                IsSuccess = true,
            };

        }
        public async Task<Response> GetList<T>(string urlBase,string prefix,string controller)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";

                var response = await client.GetAsync(url);

                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var list=JsonConvert.DeserializeObject<List<T>>(answer);

                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }




    }
}
