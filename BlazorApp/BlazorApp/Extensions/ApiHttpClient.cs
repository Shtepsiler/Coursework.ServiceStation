using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BlazorApp.Extensions
{
    public class ApiHttpClient
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        private void AddRefererHeader()
        {
            var referer = navigationManager.Uri;
            httpClient.DefaultRequestHeaders.Referrer = new Uri(referer);
            httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");
            // httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            var response = await httpClient.GetAsync(requestUri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
            return responseBody.Deserialize<T>();
        }
        /*
                public async Task<(T, PaginationHeaderViewModel)> GetWithPaginationHeaderAsync<T>(
                    string requestUri)
                {
                    httpClient.DefaultRequestHeaders.Authorization = await GenerateAuthorizationHeaderAsync();
                    var response = await httpClient.GetAsync(requestUri);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var pagination = response.Headers.GetValues("X-Pagination")
                                                     .FirstOrDefault()
                                                     .Deserialize<PaginationHeaderViewModel>();

                    StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
                    return (responseBody.Deserialize<T>(), pagination);
                }*/

        public async Task PostAsync<T>(string requestUri, T viewModel)
        {
            var response = await httpClient.PostAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        }

        public async Task PostFormDataAsync(string requestUri, MultipartFormDataContent content)
        {
            var response = await httpClient.PostAsync(requestUri, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        }

        public async Task<TOut> PostAsync<T, TOut>(string requestUri, T viewModel)
        {
            var response = await httpClient.PostAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
            return responseBody.Deserialize<TOut>();
        }

        public async Task<TOut> PostWithoutAuthorizationAsync<T, TOut>(string requestUri, T viewModel)
        {
            var response = await httpClient.PostAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
            return responseBody.Deserialize<TOut>();
        }
        /*        public async Task<JwtViewModel> PostWithoutAuthorizationTokenAsync(string requestUri)
                {
                    httpClient.DefaultRequestHeaders.Add("RefreshToken", await stateProvider.GetRefreshTokenAsync());
                    httpClient.BaseAddress = new Uri("https://localhost:7190/api/Client/");
                    var response = await httpClient.PostAsync(requestUri,null);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
                    return responseBody.Deserialize<JwtViewModel>();
                }*/
        public async Task PostAsync(string requestUri)
        {
            var response = await httpClient.PostAsync(requestUri, null);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        }
        public async Task PutAsync<T>(string requestUri, T viewModel)
        {
            var response = await httpClient.PutAsJsonAsync(requestUri, viewModel, options);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        }

        public async Task DeleteAsync(string requestUri)
        {
            var response = await httpClient.DeleteAsync(requestUri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
        }



        public ApiHttpClient(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;

        }


        public ApiHttpClient SetHttpclient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            AddRefererHeader();
            return this;

        }
    }
}
