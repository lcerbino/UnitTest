using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XUnitTest
{
    public class BaseSpecification
    {
        private readonly HttpClient client;
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly TestServer testServer;
        private bool disposedValue = false; // To detect redundant calls


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecification"/> class.
        /// </summary>
        public BaseSpecification()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            string url = @"https://localhost:44355/";
          
            if (string.IsNullOrWhiteSpace(url))
            {
                var webHostBuilder = new WebHostBuilder();
                this.testServer = new TestServer(webHostBuilder);
                this.client = this.testServer.CreateClient();

                // TODO: Remove this in the future and use environment variable to figure out what user information to expect.
                this.TestCreateByAndModifiedBy = true;
            }
            else
            {
                this.TestCreateByAndModifiedBy = false;
                Console.WriteLine($"Testing :{url}");
                this.client = new HttpClient
                {
                    BaseAddress = new Uri(url),
                };
            }
        }

        /// <summary>
        /// Gets Http Response Message.
        /// </summary>
        public HttpResponseMessage Result { get; private set; }

        /// <summary>
        /// Gets a value indicating whether created by and modifed test assertions should be checked.
        /// </summary>
        public bool TestCreateByAndModifiedBy { get; private set; }


        /// <summary>
        /// Call delete action at uri.
        /// </summary>
        /// <param name="requestUri">resource to delete.</param>
        /// <returns>a task.</returns>
        public async Task DeleteAsync(Uri requestUri)
        {
            this.Result = await this.client.DeleteAsync(requestUri).ConfigureAwait(false);
        }

        /// <summary>
        /// Deserialize content to a concrete type.
        /// </summary>
        /// <typeparam name="T">Concrete type to deserialize to.</typeparam>
        /// <returns>The concrete type.</returns>
        public async Task<T> DeserializeResultAsync<T>()
        {
            return JsonConvert.DeserializeObject<T>(await this.Result.Content.ReadAsStringAsync().ConfigureAwait(false), this.jsonSerializerSettings);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Makes a get request to the uri.
        /// </summary>
        /// <param name="requestUri">Uri to make request to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task GetAsync(Uri requestUri)
        {
            this.Result = await this.client.GetAsync(requestUri).ConfigureAwait(false);
        }

        /// <summary>
        /// Return body a string.
        /// </summary>
        /// <returns>body string.</returns>
        public async Task<string> GetBodyAsStringAsync()
        {
            return await this.Result.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Post content to uri.
        /// </summary>
        /// <param name="requestUri">Uri to post to.</param>
        /// <param name="content">Data to be serialized.</param>
        /// <returns>a task.</returns>
        public async Task PostAsync(Uri requestUri, object content)
        {
            using (StringContent jsonContent = this.CreateJsonContent(content))
            {
                this.Result = await this.client.PostAsync(requestUri, jsonContent).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Put content to uri.
        /// </summary>
        /// <param name="requestUri">Uri to put to.</param>
        /// <param name="content">Data to be serialized.</param>
        /// <returns>A task.</returns>
        public async Task PutAsync(Uri requestUri, object content)
        {
            using (StringContent jsonContent = this.CreateJsonContent(content))
            {
                this.Result = await this.client.PutAsync(requestUri, jsonContent).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">Is Dispose.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.client?.Dispose();
                    this.testServer?.Dispose();
                }

                this.disposedValue = true;
            }

        }

        private StringContent CreateJsonContent(object obj)
        {
            var data = JsonConvert.SerializeObject(obj, this.jsonSerializerSettings);
            return new StringContent(data, Encoding.UTF8, "application/json");
        }
    }
}

