﻿using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using WiredBrainCoffee.Models;

namespace WiredBrainCoffee.UI.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient http;

        public ContactService(HttpClient http)
        {
            this.http = http;
        }

        public async Task PostContact(Contact contact)
        {
            await http.PostAsJsonAsync("contact", contact);
        }

        public async Task PostContact(Contact contact,
            IReadOnlyList<IBrowserFile> attachedFiles)
        {
            foreach (var file in attachedFiles)
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);

                contact.AttachedFiles.Add(new UploadedFile()
                {
                    FileName = file.Name,
                    FileContent = buffer,
                    ContentType = file.ContentType
                });
            }

            await http.PostAsJsonAsync("contact", contact);
        }
    }
}