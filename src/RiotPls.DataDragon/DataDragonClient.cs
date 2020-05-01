﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RiotPls.DataDragon.Converters;
using RiotPls.DataDragon.Entities;
using RiotPls.DataDragon.Helpers;

namespace RiotPls.DataDragon
{
    public class DataDragonClient : IDisposable
    {
        public const string Host = "https://ddragon.leagueoflegends.com";
        public const string Api = "/api";
        public const string Cdn = "/cdn";
        public const string DefaultLanguage = "en_US";

        private readonly DataDragonClientOptions _options;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DataDragonClient() : this(null)
        {
        }
        
        public DataDragonClient(DataDragonClientOptions? options)
        {
            _options = options ?? new DataDragonClientOptions();
            _client = new HttpClient 
            {
                BaseAddress = new Uri(Host)
            };
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(new GameVersionConverter());
        }

        /// <summary>
        ///     Returns a list of every available version of Data Dragon.
        /// </summary>
        public ValueTask<IReadOnlyCollection<GameVersion>?> GetVersionsAsync()
            => ValueTaskHelper.Create(
                !_options.Versions.IsExpired,
                this,
                @this => @this._options.Versions.Data, 
                async @this =>
                {
                    var request = await @this._client.GetStreamAsync($"{Api}/versions.json").ConfigureAwait(false);
                    var data = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<GameVersion>>(
                        request, @this._jsonSerializerOptions).ConfigureAwait(false);

                    @this._options.Versions.Data = data;
                    return @this._options.Versions.Data;
                });

        /// <summary>
        ///     Returns a list of every available language for the latest version
        ///     of Data Dragon, expressed as UTF-8 culture codes. (i.e. en_US)
        /// </summary>
        public ValueTask<IReadOnlyCollection<string>?> GetLanguagesAsync()
            => ValueTaskHelper.Create(
                !_options.Languages.IsExpired,
                this,
                @this => @this._options.Languages.Data,
                async @this =>
                {
                    var request = await @this._client.GetStreamAsync($"{Cdn}/languages.json").ConfigureAwait(false);
                    var data = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<string>>(
                        request, @this._jsonSerializerOptions).ConfigureAwait(false);

                    @this._options.Languages.Data = data;
                    return @this._options.Languages.Data;
                });

        /// <summary>
        ///     Returns a <see cref="ChampionData"/> containing base information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        public ValueTask<ChampionData?> GetChampionsAsync(GameVersion version)
            => GetChampionsAsync(version, DefaultLanguage);

        /// <summary>
        ///     Returns a <see cref="ChampionData"/> containing base information
        ///     about every champion on the game.
        /// </summary>
        /// <param name="version">
        ///     The version of Data Dragon to use.
        /// </param>
        /// <param name="language">
        ///     The language in which the data must be returned. Defaults to English (United States).
        /// </param>
        public ValueTask<ChampionData?> GetChampionsAsync(GameVersion version, string language)
            => ValueTaskHelper.Create(
                !_options.Champions.IsExpired,
                (Client: this, Version: version, Language: language),
                state => state.Client._options.Champions.Data,
                async state =>
                {
                    var request = await state.Client._client.GetStreamAsync(
                        $"{Cdn}/{state.Version}/data/{state.Language}/champion.json").ConfigureAwait(false);
                    var dto = await JsonSerializer.DeserializeAsync<ChampionDataDto>(
                        request, state.Client._jsonSerializerOptions).ConfigureAwait(false);
                    var data = new ChampionData(dto);

                    state.Client._options.Champions.Data = data;
                    return data;
                });

        public void Dispose()
        {
            _client.CancelPendingRequests();
            _client.Dispose();
        }
    }
}