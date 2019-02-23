﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NakamaNetwork.Entities.EnumTypes;
using NakamaNetwork.Entities.Models;
using TreasureGuide.Web.Helpers;

namespace TreasureGuide.Web.Services
{
    public interface IPreferenceService
    {
        Task SetPreference(string userId, UserPreferenceType type, string value);
        Task ClearPreference(string userId, UserPreferenceType type);
    }

    public class PreferenceService : IPreferenceService
    {
        private readonly NakamaNetworkContext _entities;

        public PreferenceService(NakamaNetworkContext entities)
        {
            _entities = entities;
        }

        public async Task SetPreference(string userId, UserPreferenceType type, string value)
        {
            var existing = await _entities.UserPreferences.SingleOrDefaultAsync(x => x.UserId == userId && x.Key == type);
            var existed = existing != null;
            if (String.IsNullOrEmpty(value))
            {
                if (existed)
                {
                    _entities.UserPreferences.Remove(existing);
                    await _entities.SaveChangesSafe();
                }
            }
            else
            {
                existing = existing ?? new UserPreference { UserId = userId, Key = type };
                existing.Value = value;
                if (!existed)
                {
                    _entities.UserPreferences.Add(existing);
                }
                await _entities.SaveChangesSafe();
            }
        }

        public async Task ClearPreference(string userId, UserPreferenceType type)
        {
            await SetPreference(userId, type, null);
        }
    }
}
