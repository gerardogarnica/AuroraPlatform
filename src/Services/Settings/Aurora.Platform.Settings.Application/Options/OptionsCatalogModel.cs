﻿namespace Aurora.Platform.Settings.Application.Options
{
    public class OptionsCatalogModel
    {
        public int OptionsId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public List<OptionsCatalogItem> Items { get; set; }
    }

    public class OptionsCatalogItem
    {
        public int ItemId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
    }
}