// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using NUnit.Framework;

namespace WebApi.Samples
{
    public class Samples_WebApiClient
    {
        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_GetNote()
        {
            var client = new WebApiClient();

            Response response = client.GetNote("<version>", new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result[0].ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_GetNote_AllParameters()
        {
            var client = new WebApiClient();

            Response response = client.GetNote("<version>", new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result[0].GetProperty("id").ToString());
            Console.WriteLine(result[0].GetProperty("title").ToString());
            Console.WriteLine(result[0].GetProperty("content").ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_GetNote_Async()
        {
            var client = new WebApiClient();

            Response response = await client.GetNoteAsync("<version>", new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result[0].ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_GetNote_AllParameters_Async()
        {
            var client = new WebApiClient();

            Response response = await client.GetNoteAsync("<version>", new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result[0].GetProperty("id").ToString());
            Console.WriteLine(result[0].GetProperty("title").ToString());
            Console.WriteLine(result[0].GetProperty("content").ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_CreateNote()
        {
            var client = new WebApiClient();

            var data = new { };

            Response response = client.CreateNote("<version>", RequestContent.Create(data), ContentType.ApplicationOctetStream);

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_CreateNote_AllParameters()
        {
            var client = new WebApiClient();

            var data = new
            {
                id = 1234,
                title = "<title>",
                content = "<content>",
            };

            Response response = client.CreateNote("<version>", RequestContent.Create(data), ContentType.ApplicationOctetStream);

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.GetProperty("id").ToString());
            Console.WriteLine(result.GetProperty("title").ToString());
            Console.WriteLine(result.GetProperty("content").ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_CreateNote_Async()
        {
            var client = new WebApiClient();

            var data = new { };

            Response response = await client.CreateNoteAsync("<version>", RequestContent.Create(data), ContentType.ApplicationOctetStream);

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_CreateNote_AllParameters_Async()
        {
            var client = new WebApiClient();

            var data = new
            {
                id = 1234,
                title = "<title>",
                content = "<content>",
            };

            Response response = await client.CreateNoteAsync("<version>", RequestContent.Create(data), ContentType.ApplicationOctetStream);

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.GetProperty("id").ToString());
            Console.WriteLine(result.GetProperty("title").ToString());
            Console.WriteLine(result.GetProperty("content").ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_GetNoteById()
        {
            var client = new WebApiClient();

            Response response = client.GetNoteById("<version>", 1234, new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_GetNoteById_AllParameters()
        {
            var client = new WebApiClient();

            Response response = client.GetNoteById("<version>", 1234, new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.GetProperty("id").ToString());
            Console.WriteLine(result.GetProperty("title").ToString());
            Console.WriteLine(result.GetProperty("content").ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_GetNoteById_Async()
        {
            var client = new WebApiClient();

            Response response = await client.GetNoteByIdAsync("<version>", 1234, new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_GetNoteById_AllParameters_Async()
        {
            var client = new WebApiClient();

            Response response = await client.GetNoteByIdAsync("<version>", 1234, new RequestContext());

            JsonElement result = JsonDocument.Parse(response.ContentStream).RootElement;
            Console.WriteLine(result.GetProperty("id").ToString());
            Console.WriteLine(result.GetProperty("title").ToString());
            Console.WriteLine(result.GetProperty("content").ToString());
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_UpdateNoteById()
        {
            var client = new WebApiClient();

            var data = new { };

            Response response = client.UpdateNoteById("<version>", 1234, RequestContent.Create(data), ContentType.ApplicationOctetStream);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_UpdateNoteById_AllParameters()
        {
            var client = new WebApiClient();

            var data = new
            {
                id = 1234,
                title = "<title>",
                content = "<content>",
            };

            Response response = client.UpdateNoteById("<version>", 1234, RequestContent.Create(data), ContentType.ApplicationOctetStream);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_UpdateNoteById_Async()
        {
            var client = new WebApiClient();

            var data = new { };

            Response response = await client.UpdateNoteByIdAsync("<version>", 1234, RequestContent.Create(data), ContentType.ApplicationOctetStream);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_UpdateNoteById_AllParameters_Async()
        {
            var client = new WebApiClient();

            var data = new
            {
                id = 1234,
                title = "<title>",
                content = "<content>",
            };

            Response response = await client.UpdateNoteByIdAsync("<version>", 1234, RequestContent.Create(data), ContentType.ApplicationOctetStream);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_DeleteNoteById()
        {
            var client = new WebApiClient();

            Response response = client.DeleteNoteById("<version>", 1234);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public void Example_DeleteNoteById_AllParameters()
        {
            var client = new WebApiClient();

            Response response = client.DeleteNoteById("<version>", 1234);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_DeleteNoteById_Async()
        {
            var client = new WebApiClient();

            Response response = await client.DeleteNoteByIdAsync("<version>", 1234);
            Console.WriteLine(response.Status);
        }

        [Test]
        [Ignore("Only validating compilation of examples")]
        public async Task Example_DeleteNoteById_AllParameters_Async()
        {
            var client = new WebApiClient();

            Response response = await client.DeleteNoteByIdAsync("<version>", 1234);
            Console.WriteLine(response.Status);
        }
    }
}
