﻿using CoffeePi.Shared.Converters;
using Newtonsoft.Json;
using System.Text;

namespace CoffeePi.Shared.Utils;

public static class HttpExtensions
{
    private static readonly JsonSerializerSettings settings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        Converters = { new TimeOnlyJsonConverter() }
    };

    public static async Task<T> GetBasicAsync<T>(this HttpClient client, string url)
    {
        HttpResponseMessage response = await client.GetAsync(url);

        return await response.DeserializeResponseAsync<T>();
    }

    public static async Task<HttpResponseMessage> PostBasicAsync<T>(this HttpClient client, string url, T value)
    {
        string json = JsonConvert.SerializeObject(value, settings);

        StringContent content = GetContent(json);

        HttpResponseMessage result = await client.PostAsync(url, content);

        return result;
    }

    public static async Task<HttpResponseMessage> PutBasicAsync<T>(this HttpClient client, string url, T value)
    {
        string json = JsonConvert.SerializeObject(value, settings);

        StringContent content = GetContent(json);

        HttpResponseMessage result = await client.PutAsync(url, content);

        return result;
    }

    public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage response)
    {
        string json = await response.Content.ReadAsStringAsync();

        T value = JsonConvert.DeserializeObject<T>(json, settings);

        return value;
    }

    private static StringContent GetContent(string json) => new(json, Encoding.UTF8, "application/json");
}
