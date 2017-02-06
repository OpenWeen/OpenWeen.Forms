using System;
using System.Linq;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace OpenWeen.Forms.Common.Helpers
{
    internal class SettingException : Exception
    {
        public SettingException()
        {
        }

        public SettingException(string message) : base(message)
        {
        }

        public SettingException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    internal enum SetListSettingOption
    {
        ReplaceExisting,
        FailIfExists,
        AddIfExists
    }
    internal static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string[] AccessTokens
        {
            get { return AppSettings.GetValueOrDefault(nameof(AccessTokens), "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries); }
            set { AppSettings.AddOrUpdateValue(nameof(AccessTokens), value); }
        }

        public static int SelectedUserIndex
        {
            get { return AppSettings.GetValueOrDefault(nameof(SelectedUserIndex), 0); }
            internal set { AppSettings.AddOrUpdateValue(nameof(SelectedUserIndex), value); }
        }

        public static int LoadCount
        {
            get { return AppSettings.GetValueOrDefault(nameof(LoadCount), 20); }
            internal set { AppSettings.AddOrUpdateValue(nameof(LoadCount), value); }
        }
    }
}