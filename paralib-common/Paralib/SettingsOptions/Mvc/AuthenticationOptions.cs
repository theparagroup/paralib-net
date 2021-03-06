﻿using System;

namespace com.paralib.SettingsOptions.Mvc
{
    public class AuthenticationOptions
    {
        public bool Enabled { get; set; }
        public string LoginUrl { get; set; }
        public string UnauthorizedUrl { get; set; }
        public string DefaultUrl { get; set; }
        public bool Global { get; set; }
        public int Timeout { get; set; }
        public bool Persist { get; set; }
    }
}
